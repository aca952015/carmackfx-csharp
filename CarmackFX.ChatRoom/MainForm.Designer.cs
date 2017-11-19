namespace CarmackFX.ChatRoom
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.message = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.send = new System.Windows.Forms.Button();
			this.userList = new System.Windows.Forms.ListBox();
			this.msgBox = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.message);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(8, 470);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(840, 123);
			this.panel1.TabIndex = 1;
			// 
			// message
			// 
			this.message.Dock = System.Windows.Forms.DockStyle.Fill;
			this.message.Location = new System.Drawing.Point(0, 0);
			this.message.Multiline = true;
			this.message.Name = "message";
			this.message.Size = new System.Drawing.Size(677, 123);
			this.message.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.send);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(677, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(163, 123);
			this.panel2.TabIndex = 0;
			// 
			// send
			// 
			this.send.Dock = System.Windows.Forms.DockStyle.Fill;
			this.send.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.send.Location = new System.Drawing.Point(0, 0);
			this.send.Name = "send";
			this.send.Size = new System.Drawing.Size(163, 123);
			this.send.TabIndex = 0;
			this.send.Text = "Send";
			this.send.UseVisualStyleBackColor = true;
			this.send.Click += new System.EventHandler(this.send_Click);
			// 
			// userList
			// 
			this.userList.Dock = System.Windows.Forms.DockStyle.Right;
			this.userList.FormattingEnabled = true;
			this.userList.ItemHeight = 12;
			this.userList.Location = new System.Drawing.Point(685, 8);
			this.userList.Name = "userList";
			this.userList.Size = new System.Drawing.Size(163, 462);
			this.userList.TabIndex = 2;
			// 
			// msgBox
			// 
			this.msgBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.msgBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.msgBox.Location = new System.Drawing.Point(8, 8);
			this.msgBox.Multiline = true;
			this.msgBox.Name = "msgBox";
			this.msgBox.ReadOnly = true;
			this.msgBox.Size = new System.Drawing.Size(677, 462);
			this.msgBox.TabIndex = 3;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(856, 601);
			this.Controls.Add(this.msgBox);
			this.Controls.Add(this.userList);
			this.Controls.Add(this.panel1);
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(8);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chat Room";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox message;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button send;
		private System.Windows.Forms.ListBox userList;
		private System.Windows.Forms.TextBox msgBox;
	}
}

