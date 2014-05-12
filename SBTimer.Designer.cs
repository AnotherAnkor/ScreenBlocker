/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 12.11.2013
 * Time: 16:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ScreenBlocker
{
	partial class SBTimer
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.stopWork = new System.Windows.Forms.Button();
			this.userName = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timeShow = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// stopWork
			// 
			this.stopWork.Location = new System.Drawing.Point(12, 79);
			this.stopWork.Name = "stopWork";
			this.stopWork.Size = new System.Drawing.Size(106, 23);
			this.stopWork.TabIndex = 1;
			this.stopWork.Text = "Закончить работу";
			this.stopWork.UseVisualStyleBackColor = true;
			this.stopWork.Click += new System.EventHandler(this.StopWorkClick);
			// 
			// userName
			// 
			this.userName.Location = new System.Drawing.Point(12, 9);
			this.userName.Name = "userName";
			this.userName.Size = new System.Drawing.Size(104, 51);
			this.userName.TabIndex = 2;
			this.userName.Text = "Остаток времени";
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// timeShow
			// 
			this.timeShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.timeShow.Location = new System.Drawing.Point(16, 37);
			this.timeShow.Name = "timeShow";
			this.timeShow.Size = new System.Drawing.Size(100, 23);
			this.timeShow.TabIndex = 3;
			// 
			// SBTimer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(124, 111);
			this.ControlBox = false;
			this.Controls.Add(this.timeShow);
			this.Controls.Add(this.userName);
			this.Controls.Add(this.stopWork);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SBTimer";
			this.Opacity = 0.8D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Ваше время";
			this.TopMost = true;
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label timeShow;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label userName;
		private System.Windows.Forms.Button stopWork;
	}
}
