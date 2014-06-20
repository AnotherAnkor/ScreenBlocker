/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 16.10.2013
 * Time: 13:08
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace ScreenBlocker
{
	/// <summary>
	/// Description of WorkWithDb.
	/// </summary>
	public sealed class WorkWithDb
	{
		private int balance = 0;
		private string userName = "";
		string connStr;
		string pass;
		private static volatile WorkWithDb instance;
   		private static object syncRoot = new Object();

		private WorkWithDb()
		{
			ReadConnStr();
			ReadPassStr();
		}
		
		public static WorkWithDb Instance
		{
      		get 
      			{
			         if (instance == null) 
			         {
			            lock (syncRoot) 
			            {
			               if (instance == null)
			               {
			               		instance = new WorkWithDb();
			               }
			            }
			         }
			         return instance;
			   }
	   }
		
		private void ReadConnStr()
		{
			try
			{
				string file = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\connect.txt");
				connStr = File.ReadAllText(file);
			}
			catch
			{
				MessageBox.Show("Файл строки подключения не найден");
			}
		}
		
		
		private void ReadPassStr()
		{
			try
			{
				string file = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\pass.txt");
				pass = File.ReadAllText(file);
			}
			catch
			{
				MessageBox.Show("Пароль не найден");
			}
		}
		
		string GetMD5Hash(string word)
		{
			// создаем объект этого класса. Отмечу, что он создается не через new, а вызовом метода Create
			MD5 md5Hasher = MD5.Create();
						 
			// Преобразуем входную строку в массив байт и вычисляем хэш
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(word));
			                                    //Unicode.GetBytes(word));
						 
			// Создаем новый Stringbuilder (Изменяемую строку) для набора байт
			StringBuilder sBuilder = new StringBuilder();
			
			// Преобразуем каждый байт хэша в шестнадцатеричную строку
			for (int i = 0; i < data.Length; i++)
			{
			    //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
			    sBuilder.Append(data[i].ToString("x2")); 
			}
			return sBuilder.ToString();
		}
		
		/// <summary>
		/// UserExist - проверяет наличие записи о пользователе в БД
		/// </summary>
		/// <param name="login">Номер читательского билета</param>
		/// <param name="password">Личный пароль пользователя</param>
		/// <returns>Возвращает булев результат</returns>
		public bool UserExist (string login, string password)
		{
			bool bit = false;
			if ((login == "Админ") && (password == pass))
			{
				AddHooks ad;
				ad.EnableCTRLALTDEL();
				ad.ShowStartMenu();
				Program.MainForm.Close();
				Application.Exit();
			}

			else if ((login != "") && (password != ""))
			{
		        	MySqlConnection con = new MySqlConnection(connStr);
		        	try 
		        	{
		        		con.Open();
		        		string sql = (@"select case when exists (select id, pass from users where (id = @id) and pass like @password) then 1 else 0 end;");
		        		MySqlCommand cmd = new MySqlCommand(sql, con);
		        		cmd.Parameters.AddWithValue("@id",login);
		        		cmd.Parameters.AddWithValue("@password",GetMD5Hash(GetMD5Hash(password)));
		        		string strbit = cmd.ExecuteScalar().ToString();
		        		if (strbit == "1")
		        			bit = true;
		        		if (strbit == "0")
		        			bit = false;
		        	}
		        	catch (MySqlException){
		        		bit = false;
		        	}

		        	finally {
		        		con.Close();
		        		con.Dispose();
		        	}

			}
			return (bool)bit;
		}

		/// <summary>
		/// Проверяет остаток времени у пользователя на данный момент
		/// </summary>
		/// <param name="login">Номер читательского</param>
		/// <returns>Возвращает int количество минут остатка времени</returns>
		public int TimeBalance(string login)
		{
		    MySqlConnection con = new MySqlConnection(connStr);
		    try {
		        
		        string sql = (@"select time_balance from time_used where user_id = (@id);");
		        MySqlCommand cmd = new MySqlCommand(sql);
		        cmd.Parameters.AddWithValue("@id",login);
		        cmd.Connection = con;
		        con.Open();
		        string tmp;
		        tmp = cmd.ExecuteScalar().ToString();
		        balance = int.Parse(tmp);
		    }
		    catch (MySqlException){
		        
		    }
		    finally {
		        con.Close();
		        con.Dispose();
		    }
		    
		    return balance;
		}
		
		public bool IsBurthday(string login)
		{
			bool birthdayBool = false;
		    MySqlConnection con = new MySqlConnection(connStr);
		        	try {
		        		con.Open();
		        		string sql = (@"select (select date_format((current_date),'%m'))-(select date_format((select birthday from users where id = @id),'%m'))+(select date_format((current_date),'%d'))-(select date_format((select birthday from users where id = @id),'%d'));");
		        		MySqlCommand cmd = new MySqlCommand(sql, con);
		        		cmd.Parameters.AddWithValue("@id",login);
		        		string tmp = cmd.ExecuteScalar().ToString();
		        		int birthday = int.Parse(tmp);
		        		if (birthday == 0)
		        			birthdayBool = true;
		        	}
		        	catch (MySqlException){
		        	}
		        	finally {
		        		con.Close();
		        		con.Dispose();
		        	}
			return birthdayBool;
		}
		
		public bool IsBaned(string login)
		{
			bool ban = false;
		    MySqlConnection con = new MySqlConnection(connStr);
		        	try {
		        		con.Open();
		        		string sql = (@"select ban from users where id = @id;");
						//string sql = (@"select case when exists (select id, pass from users where id = 2 and pass like pass) then 1 else 0 end;");
		        		MySqlCommand cmd = new MySqlCommand(sql, con);
		        		cmd.Parameters.AddWithValue("@id",login);
		        		string strbit = cmd.ExecuteScalar().ToString();
		        		if (strbit == "True")
		        			ban = true;
		        		if (strbit == "False")
		        			ban = false;
		        	}
		        	catch (MySqlException){
				    	Program.MainForm.Show();
		        	}

		        	finally {
		        		con.Close();
		        		con.Dispose();
		        	}			
			return ban;
		}
		
		public bool LastLoginNotToday(string login1)
		{
			bool today = false;
		    MySqlConnection con = new MySqlConnection(connStr);
		        	try {
		        		con.Open();
						string sql = (@"select (select date_format((current_date),'%m'))-(select date_format((select time_start from time_used where user_id = @id),'%m'))+(select date_format((current_date),'%d'))-(select date_format((select time_start from time_used where user_id = @id),'%d'));");
		        		MySqlCommand cmd = new MySqlCommand(sql, con);
		        		cmd.Parameters.AddWithValue("@id",login1);
		        		int strbit = int.Parse(cmd.ExecuteScalar().ToString());
		        		if (strbit == 0)
		        			today = true;
		        	}
		        	catch (MySqlException){
		        		throw new MySqlException();
		        	}

		        	finally {
		        		con.Close();
		        		con.Dispose();
		        	}			
			return today;
		}
	
		/// <summary>Делает запись в БД о том, что работа была завершена в такой-то момент</summary>
		public void StopWork(string login)
		{
			MySqlConnection con = new MySqlConnection(connStr);
			try {
		        con.Open();
		        using (MySqlCommand cmd = new MySqlCommand("UPDATE time_used SET time_balance = @balance WHERE user_id = @id;", con))
		        {
					cmd.Parameters.AddWithValue("@id",login);
					cmd.Parameters.AddWithValue("@balance",balance);
		        }
		    }
		    catch (MySqlException) {
		        throw new MySqlException();
		    }
			finally {
		        con.Close();
		        con.Dispose();
		    }
		}
		
		public void CheckRecordInTimeUsed(string login)
		{
			MySqlConnection con = new MySqlConnection(connStr);
			try {
		        con.Open();
		        using (MySqlCommand cmd = new MySqlCommand(@"INSERT INTO time_used (user_id,time_balance)
					 SELECT * FROM 
					(SELECT @id,
					(SELECT norm FROM restrictions WHERE id = 
					(select year(curdate())-(select birthday from users where id = @id) as DiffDate)
														)
					)
					 AS tmp 
					WHERE NOT EXISTS 
					(SELECT user_id FROM time_used WHERE user_id = @id) LIMIT 1", con))
		        {
					cmd.Parameters.AddWithValue("@id",login);
		        }
		    }
		    catch (MySqlException) {
		        throw new MySqlException();
		    }
			finally {
		        con.Close();
		        con.Dispose();
		    }			
		}
		
		/// <summary>
		/// Метод вызывается, чтобы сминусовать минуту и внести запись в БД. Так сделано потому, что сеть может исчезнуть,
		/// но при этом записи об изменениях в остатке времени не будет. И чтобы после восстановления сети были получены точные результаты,
		/// ежеминутно должен вызываться этот метод.
		/// </summary>
		public void BalanceMinus(string login, int newBalance)
		{
			MySqlConnection con = new MySqlConnection(connStr);
			try
			{
				string myQuery = (@"UPDATE time_used SET time_balance = @balance WHERE user_id = @id;");
		        MySqlCommand cmd = new MySqlCommand(myQuery);
		        cmd.Parameters.AddWithValue("@balance",newBalance.ToString());
		   		cmd.Parameters.AddWithValue("@id",login);
		   		
				cmd.Connection = con;
				con.Open();
				cmd.ExecuteNonQuery();
				cmd.Connection.Close();
		    }
		    catch (MySqlException) {
				MessageBox.Show("Не удалось связаться с сервером БД");
				MainForm.ActiveForm.Activate();
		    }
			finally {
				con.Close();
		     	con.Dispose();
		    }
		}
		
		/// <summary>
		///Вызывается для того, чтобы выставить новую норму времени из базы
		/// </summary>
		public void UpdateUserBalance(string login)
		{
			MySqlConnection con = new MySqlConnection(connStr);
			try
			{
				string myQuery = (@"UPDATE time_used SET time_balance = 
 									(SELECT norm FROM restrictions WHERE id = 
										(select year(curdate())-(select birthday from users where id = @id) as DiffDate)
									)
									WHERE user_id = @id;");
		        MySqlCommand cmd = new MySqlCommand(myQuery);
		   		cmd.Parameters.AddWithValue("@id",login);
		   		
				cmd.Connection = con;
				con.Open();
				cmd.ExecuteNonQuery();
				cmd.Connection.Close();
		    }
		    catch (MySqlException) {
				MessageBox.Show("Не удалось связаться с сервером БД");
				MainForm.ActiveForm.Activate();
		    }
			finally {
				con.Close();
		     	con.Dispose();
		    }
		}

		public string Name(string login)
		{
			MySqlConnection con = new MySqlConnection(connStr);
		    try {
		        string sql = (@"select name from users where id = (@id);");
				MySqlCommand cmd = new MySqlCommand(sql);
		        cmd.Parameters.AddWithValue("@id",login);
		        cmd.Connection = con;
		        con.Open();
		        userName = cmd.ExecuteScalar().ToString();
		    }
		    catch (MySqlException){
		    }
		    finally {
		        con.Close();
		        con.Dispose();
		    }					
		    return userName;
		}

		public sealed class MySqlException : SystemException
		{
			public MySqlException()
			{
				MessageBox.Show("Сбой подключения к БД");
			}
		}
	
		public class NonConvertable : ArgumentException
		{
			public NonConvertable()
			{
				MessageBox.Show("Введены некорректные данные. Попробуйте снова.");
			}
		}     
	}
}

