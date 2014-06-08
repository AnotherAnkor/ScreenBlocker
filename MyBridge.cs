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
			 		if (WorkWithDb.Instance.IsBaned(userLogin) == false)
		      		{
			 			if (WorkWithDb.Instance.LastLoginNotToday(userLogin) == false)
		      			{
			 				WorkWithDb.Instance.UpdateUserBalance(userLogin);
		      			}
		      			Program.MainForm.TmpBool = true;
		      		}
		      		else
		      		{
		      			MessageBox.Show("Ваш аккаунт заблокирован");
		      		}
		      	}
			 }
			  catch
		      {
		      	MessageBox.Show("Ошибка подключения");
		      }
		}
		
		public void RunThat()
		{
			BackgroundLoading BL = new BackgroundLoading(CheckItOut);
            BL.Start();
		}
	}
}
