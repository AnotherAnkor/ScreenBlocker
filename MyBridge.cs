/*
 * Сделано в SharpDevelop.
 * Пользователь: Андрей Михайлович
 * Дата: 08.06.2014
 * Время: 21:30
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenBlocker
{
	/// <summary>
	/// Description of MyBridge.
	/// </summary>
	public class MyBridge
	{
		private string userLogin;
		private string userPassword;
		
		public MyBridge()
		{
		}
		
		public string UserLogin
		{
			set {userLogin = value;}
		}
		
		public string UserPassword
		{
			set {userPassword = value;}
		}
		
		public void CheckItOut()
		{
			 try
		      {
			 	if (WorkWithDb.Instance.UserExist(userLogin,userPassword) == true)
		      	{
			 		WorkWithDb.Instance.CheckRecordInTimeUsed(userLogin);
			 		if (WorkWithDb.Instance.IsBaned(userLogin) == false)
		      		{
			 			Program.MainForm.TmpBool = true;
			 			if (WorkWithDb.Instance.LastLoginNotToday(userLogin) == false)
		      			{
			 				WorkWithDb.Instance.UpdateUserBalance(userLogin);
		      			}
			 			if (WorkWithDb.Instance.TimeBalance(userLogin) > 0)
		      				Program.MainForm.TmpBool = true;
			 			else
			 			{
			 				Program.MainForm.SetLabelText("На сегодня Ваше время работы за компьютером закончилось. Ждём Вас завтра!");
			 				Program.MainForm.TmpBool = false;
			 				Program.MainForm.ClearForm();
			 			}
		      		}
		      		else
		      		{
		      			Program.MainForm.SetLabelText("Ваш аккаунт заблокирован");
		      		}
		      	}
			 	else
			 	{
			 		Program.MainForm.SetLabelText("Пользователь не существует");
			 	}
			 }
			  catch
		      {
			  	Program.MainForm.SetLabelText("Ошибка подключения");
		      }
			  finally
			  {
			  
			  }
		}
		
		public void RunThat()
		{
           CheckItOut();
		}
	}
}
