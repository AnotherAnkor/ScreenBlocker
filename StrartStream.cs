/*
 * Created by SharpDevelop.
 * User: andrei
 * Date: 16.12.2013
 * Time: 16:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Threading;

namespace ScreenBlocker
{
	/// <summary>
	/// Description of StrartStream.
	/// </summary>
	public class StrartStream
	{
		public StrartStream()
		{
		}
		public void StreamStart()
		{
			String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			Process.Start(appStartPath + @"\ScreenStream.exe");
		}
		public void StopStream()
		{
			Process[] proc = Process.GetProcessesByName("ScreenStream");
            foreach (Process prs in proc)
            {
                prs.Kill();
            }
		}
	}
}
