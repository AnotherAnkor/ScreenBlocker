/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 16.10.2013
 * Time: 11:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace ScreenBlocker
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		public static MainForm MainForm;
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
      		Application.EnableVisualStyles();
      		Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.UnhandledException +=new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
      		Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			
			MainForm = new MainForm();
			Application.Run(MainForm);
		}	
		
		static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			SystemSounds.Beep.Play();
			AddHooks ad;
			ad.SomeMethod();
			ad.KillCtrlAltDelete();
			ad.KillStartMenu();
			MainForm.Show();
//		  	DialogResult result = DialogResult.Abort;
//			try
//			{
//			  result = MessageBox.Show("Оюшки! Пожалуйста, свяжитесь с разработчиками, сообщив им" 
//			  + " следующую информации:\n\n" + e.Exception.Message + e.Exception.StackTrace, 
//			  "Ошибка программы", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
//			}
//			finally
//			{
//			if (result == DialogResult.Abort)
//				{
//			   		Application.Exit();
//				}
//  			}
		}
		
		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception ex = (Exception)e.ExceptionObject;
				
//				MessageBox.Show("Батюшки! Пожалуйста, сообщите разработчикам об ошибке" 
//				          + " следующую информацию:\n\n" + ex.Message + ex.StackTrace, 
//				          "Фатальная ошибка программы", MessageBoxButtons.OK);
			}
			/*
			//HACK: здесь лучше использовать метод Exit() и заново запускать, банально потому, что иначе окна с ошибками
			//будут копиться и при правильном входе пользователю придётся их закрывать. А так хоть эти окна будут закрыты
			//раньше, чем удастся войти
			*/
			finally
			{
				String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
				Process.Start(appStartPath + @"\ScreenBlocker.exe");
				Application.Exit();
			}
		}
	}
}
