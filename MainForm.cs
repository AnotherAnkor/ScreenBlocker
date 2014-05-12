/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 16.10.2013
 * Time: 11:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Diagnostics;

namespace ScreenBlocker
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		AddHooks ad; 
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			HooksOn();
			ad.KillCtrlAltDelete();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        try {  throw new Exception("Resetting FPU control register, please ignore"); }
        catch { }
    }
		
		public void HooksOn()
		{
			ad.SomeMethod();
		}
		
		private void Form1_Load(object sender, EventArgs e)
        {
        base.OnLoad(e);
        try {  throw new Exception("Resetting FPU control register, please ignore"); }
        catch { }
        }
		
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            ad.EnableCTRLALTDEL();
        }
	
		void Button1Click(object sender, EventArgs e)
		{
			//wwdb = new WorkWithDb();
			//wwdb = WorkWithDb.Instance;
			try {
				if (WorkWithDb.Instance.UserExist(login.Text.ToString(),password.Text.ToString()) == true)
				{			
					if (WorkWithDb.Instance.IsBaned(login.Text.ToString()) == false)
					{
						if (WorkWithDb.Instance.LastLoginNotToday(login.Text.ToString()) == false)
						{
							WorkWithDb.Instance.UpdateUserBalance(login.Text.ToString());
							if (WorkWithDb.Instance.IsBurthday(login.Text.ToString()) == true)
								WorkWithDb.Instance.AgeUp(login.Text.ToString());
						}
						
						this.Hide();
						SBTimer sbt = new SBTimer(login.Text.ToString());
						sbt.Show();
					}
					else
						MessageBox.Show("Ваш аккаунт заблокирован");
				}
				
				else 
				{
				   	MessageBox.Show("Данные введены неверно. Попробуйте снова.");
				}
			}
			//
			// TODO: Сделать нормальную обработку ошибок.
			//
			catch
			{
				//Program.MainForm.Show();
				label1.Text = "Ошибка подключения";
			}
		}
		
		void LoginTextChanged(object sender, EventArgs e)
		{
			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
			ad.EnableCTRLALTDEL();
			ad.ShowStartMenu();
		}
	}
}
