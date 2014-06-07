/*
 * Сделано в SharpDevelop.
 * Пользователь: Андрей Михайлович
 * Дата: 07.06.2014
 * Время: 18:00
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenBlocker
{
	/// <summary>
	/// Description of TryItOut.
	/// </summary>
	public class CheckItOut
	{
		public CheckItOut(String login, String password)
		{
			 try
		      {
		      	if (WorkWithDb.Instance.UserExist(login,password) == true)
		      	{
		      		if (WorkWithDb.Instance.IsBaned(login) == false)
		      		{
		      			if (WorkWithDb.Instance.LastLoginNotToday(login) == false)
		      			{
		      				WorkWithDb.Instance.UpdateUserBalance(login);
		      			}
		      			Program.MainForm.tmpBool = true;
		      		}
		      		else
		      		{
		      			MessageBox.Show("Ваш аккаунт заблокирован");
		      		}
		      	}
		      	else
		      	{
		      		MessageBox.Show("Данные введены неверно. Попробуйте снова.");
		      	}
		      }
		      catch
		      {
		      	MessageBox.Show("Ошибка подключения");
		      }
		      
		}
	}
}
