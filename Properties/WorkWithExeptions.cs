/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 06.11.2013
 * Time: 14:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenBlocker.Properties
{
	/// <summary>
	/// Description of WorkWithExeptions.
	/// </summary>
	public class WorkWithExeptions:Exception
	{
		public WorkWithExeptions()
		{
			MessageBox.Show("Что-то пошло не так");
		}
	}
}
