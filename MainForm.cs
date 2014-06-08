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
using MRG.Controls.UI;

namespace ScreenBlocker
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form//AsyncBaseDialog
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
		
		bool tmpBool = false;
		
		public bool TmpBool
		{
			set{tmpBool = value;}
		}
	
		void Button1Click(object sender, EventArgs e)
		{
			MyBridge mb = new MyBridge();
			mb.UserLogin = login.Text.ToString();
			mb.UserPassword = password.Text.ToString();
			mb.RunThat();
			if (tmpBool)
			{
				this.Hide();
				SBTimer sbt = new SBTimer(login.Text.ToString());
		      	sbt.Show();
			}					
		}
		
		public void ClearForm()
		{
			login.Text = "";
			password.Text = "";
			tmpBool = false;
		}
	}
}
