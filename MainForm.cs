﻿/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 16.10.2013
 * Time: 11:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows;
using Microsoft.Xna.Framework;

namespace ScreenBlocker
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		AddHooks ad; 
//		[DllImport("User32.dll")]
//   		private static extern short GetAsyncKeyState( System.Windows.Forms.Keys vKey);
		
		bool tmpFlag = false;
		
		[DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = false)]
        private static extern bool SystemParametersInfo(uint action, uint param,
            ref SKEY vparam, uint init);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = false)]
        private static extern bool SystemParametersInfo(uint action, uint param,
            ref FILTERKEY vparam, uint init);

        private const uint SPI_GETFILTERKEYS = 0x0032;
        private const uint SPI_SETFILTERKEYS = 0x0033;
        private const uint SPI_GETTOGGLEKEYS = 0x0034;
        private const uint SPI_SETTOGGLEKEYS = 0x0035;
        private const uint SPI_GETSTICKYKEYS = 0x003A;
        private const uint SPI_SETSTICKYKEYS = 0x003B;

        private static bool StartupAccessibilitySet = false;
        private static SKEY StartupStickyKeys;
        private static SKEY StartupToggleKeys;
        private static FILTERKEY StartupFilterKeys;

        private const uint SKF_STICKYKEYSON = 0x00000001;
        private const uint TKF_TOGGLEKEYSON = 0x00000001;
        private const uint SKF_CONFIRMHOTKEY = 0x00000008;
        private const uint SKF_HOTKEYACTIVE = 0x00000004;
        private const uint TKF_CONFIRMHOTKEY = 0x00000008;
        private const uint TKF_HOTKEYACTIVE = 0x00000004;
        private const uint FKF_CONFIRMHOTKEY = 0x00000008;
        private const uint FKF_HOTKEYACTIVE = 0x00000004;
        private const uint FKF_FILTERKEYSON = 0x00000001;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SKEY
        {
            public uint cbSize;
            public uint dwFlags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct FILTERKEY
        {
            public uint cbSize;
            public uint dwFlags;
            public uint iWaitMSec;
            public uint iDelayMSec;
            public uint iRepeatMSec;
            public uint iBounceMSec;
        }
 
        private static uint SKEYSize = sizeof(uint) * 2;
        private static uint FKEYSize = sizeof(uint) * 6;
        /// <summary>
        /// False to stop the sticky keys popup.
        /// True to return to whatever the system has been set to.
        /// </summary>
        public static void AllowAccessibilityShortcutKeys(bool bAllowKeys)
        {
            if (!StartupAccessibilitySet)
            {
                StartupStickyKeys.cbSize = SKEYSize;
                StartupToggleKeys.cbSize = SKEYSize;
                StartupFilterKeys.cbSize = FKEYSize;
                SystemParametersInfo(SPI_GETSTICKYKEYS, SKEYSize, ref StartupStickyKeys, 0);
                SystemParametersInfo(SPI_GETTOGGLEKEYS, SKEYSize, ref StartupToggleKeys, 0);
                SystemParametersInfo(SPI_GETFILTERKEYS, FKEYSize, ref StartupFilterKeys, 0);
                StartupAccessibilitySet = true;
            }

            if (bAllowKeys)
            {
                // Restore StickyKeys/etc to original state and enable Windows key 
                SystemParametersInfo(SPI_SETSTICKYKEYS, SKEYSize, ref StartupStickyKeys, 0);
                SystemParametersInfo(SPI_SETTOGGLEKEYS, SKEYSize, ref StartupToggleKeys, 0);
                SystemParametersInfo(SPI_SETFILTERKEYS, FKEYSize, ref StartupFilterKeys, 0);
            }
            else
            {
                // Disable StickyKeys/etc shortcuts but if the accessibility feature is on,  
                // then leave the settings alone as its probably being usefully used 
                SKEY skOff = StartupStickyKeys;
                if ( ( skOff.dwFlags & SKF_STICKYKEYSON ) == 0 ) 
                {
                    // Disable the hotkey and the confirmation 
                    skOff.dwFlags &= ~SKF_HOTKEYACTIVE;
                    skOff.dwFlags &= ~SKF_CONFIRMHOTKEY;
                    SystemParametersInfo(SPI_SETSTICKYKEYS, SKEYSize, ref skOff, 0);
                }
                SKEY tkOff = StartupToggleKeys;
                if ( ( tkOff.dwFlags & TKF_TOGGLEKEYSON ) == 0 ) 
                {
                    // Disable the hotkey and the confirmation 
                    tkOff.dwFlags &= ~TKF_HOTKEYACTIVE;
                    tkOff.dwFlags &= ~TKF_CONFIRMHOTKEY;
                    SystemParametersInfo(SPI_SETTOGGLEKEYS, SKEYSize, ref tkOff, 0);
                }

                FILTERKEY fkOff = StartupFilterKeys;
                if ( ( fkOff.dwFlags & FKF_FILTERKEYSON ) == 0 ) 
                {
                    // Disable the hotkey and the confirmation 
                    fkOff.dwFlags &= ~FKF_HOTKEYACTIVE;
                    fkOff.dwFlags &= ~FKF_CONFIRMHOTKEY;
                    SystemParametersInfo(SPI_SETFILTERKEYS, FKEYSize, ref fkOff, 0);
                }
            }
        } 
		
		public MainForm()
		{ 
			InitializeComponent();
			HooksOn();
			ad.KillCtrlAltDelete();
			AllowAccessibilityShortcutKeys(false);
		}
		
		public bool TmpFlag
		{
			set {tmpFlag = value;}
		}
		
		public void SetLabelText(string tmp)
		{
			label1.Text = tmp;
		}
		
		private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		
		protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        try {  throw new Exception("Resetting FPU control register, please ignore"); }
        catch { }
    }
		
		public void HooksOn()
		{
			ad.SomeMethod();
			ad.KillStartMenu();
		}
		
		private void Form1_Load(object sender, EventArgs e)
        {
//	        base.OnLoad(e);
//	        try {  throw new Exception("Resetting FPU control register, please ignore"); }
//	        catch { }
			
        }
		
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
		  switch (e.CloseReason)
		  {
		    case CloseReason.UserClosing:
		      e.Cancel = true;
		      break;
		  }
		
		  base.OnFormClosing(e);
		}
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            ad.EnableCTRLALTDEL();
            AllowAccessibilityShortcutKeys(true);
        }
		
		bool tmpBool = false;
		
		public bool TmpBool
		{
			set{tmpBool = value;}
		}
	
		void Button1Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			LoadingForm lf = new LoadingForm();
			lf.Show();
			MyBridge mb = new MyBridge();
			mb.UserLogin = login.Text.ToString();
			mb.UserPassword = password.Text.ToString();
			mb.RunThat();
			if (tmpBool == true)
			{
				this.Hide();
				SBTimer sbt = new SBTimer(login.Text.ToString());
		      	sbt.Show();
		      	ad.ShowStartMenu();
			}
			else
			{
				ClearForm();
			}
			lf.Close();
			lf.Dispose();
			Cursor.Current = Cursors.Default;			
		}
		
		public void ClearForm()
		{
			login.Text = "Логин";
			password.Text = "Пароль";
			tmpBool = false;
		}
				
		void MainForm_KeyPress(object sender, KeyPressEventArgs e)
		{
		}
	}
}
