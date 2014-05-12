/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 12.11.2013
 * Time: 16:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

			//
			// TODO: Сделать обработчик для обрыва сети.
			//
namespace ScreenBlocker
{
	/// <summary>
	/// Description of SBTimer.
	/// </summary>
	public partial class SBTimer : Form
	{
		public string Login;
		int myTime = 1;
		StrartStream ss;
		
		public SBTimer(string login)
		{
			Login = login;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			myTime = WorkWithDb.Instance.TimeBalance(Login);
			TimeToLabel();
			timer1.Interval = 60000;
			timer1.Enabled = true;
			timer1.Start();
			AddHooks.UnhookWindowsHookEx(AddHooks.intLLKey);
			ss = new StrartStream();
			ss.StreamStart();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		protected override void OnLoad(EventArgs e) {
	        base.OnLoad(e);
	        try {  throw new Exception("Resetting FPU control register, please ignore"); }
	        catch { }
    	}
				
		void NameToLabel(string str)
		{
			string tmp = WorkWithDb.Instance.Name(str);
			userName.Text = tmp;
		}
		
		void TimeToLabel()
		{
			timeShow.Text = myTime.ToString();
		}
		
		AddHooks ad;
		void StopWorkClick(object sender, EventArgs e)
		{
			Program.MainForm.Show();
			ad.SomeMethod();
			//Program.MainForm.HooksOn();
			ss.StopStream();
			this.Close();
		}
			
		void Timer1Tick(object sender, EventArgs e)
		{
			if (WorkWithDb.Instance.IsBaned(Login) == true)
				myTime = 0;
			if (myTime != 0)
			{
				myTime = WorkWithDb.Instance.TimeBalance(Login)-1;
				TimeToLabel();
				WorkWithDb.Instance.BalanceMinus(Login, myTime);
			}	
			else
			{
				Program.MainForm.Show();
				timer1.Stop();
				this.Close();
				ad.SomeMethod();
				ss.StopStream();
			}
		}	
	}
}
