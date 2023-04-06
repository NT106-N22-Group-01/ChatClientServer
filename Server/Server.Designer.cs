namespace Server
{
	partial class Server
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
			label1 = new System.Windows.Forms.Label();
			serverPortTxt = new System.Windows.Forms.TextBox();
			serverStartButton = new System.Windows.Forms.Button();
			serverStopButton = new System.Windows.Forms.Button();
			groupBox1 = new System.Windows.Forms.GroupBox();
			serverChatView = new System.Windows.Forms.RichTextBox();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			label1.Location = new System.Drawing.Point(83, 96);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 27);
			label1.TabIndex = 0;
			label1.Text = "Port";
			// 
			// serverPortTxt
			// 
			serverPortTxt.Location = new System.Drawing.Point(83, 154);
			serverPortTxt.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			serverPortTxt.Name = "serverPortTxt";
			serverPortTxt.Size = new System.Drawing.Size(164, 31);
			serverPortTxt.TabIndex = 1;
			// 
			// serverStartButton
			// 
			serverStartButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			serverStartButton.Location = new System.Drawing.Point(300, 96);
			serverStartButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			serverStartButton.Name = "serverStartButton";
			serverStartButton.Size = new System.Drawing.Size(167, 96);
			serverStartButton.TabIndex = 2;
			serverStartButton.Text = "Start";
			serverStartButton.UseVisualStyleBackColor = true;
			serverStartButton.Click += serverStartButton_Click;
			// 
			// serverStopButton
			// 
			serverStopButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			serverStopButton.Location = new System.Drawing.Point(517, 96);
			serverStopButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			serverStopButton.Name = "serverStopButton";
			serverStopButton.Size = new System.Drawing.Size(167, 96);
			serverStopButton.TabIndex = 3;
			serverStopButton.Text = "Stop";
			serverStopButton.UseVisualStyleBackColor = true;
			serverStopButton.Click += serverStopButton_Click;
			// 
			// groupBox1
			// 
			groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			groupBox1.Location = new System.Drawing.Point(50, 58);
			groupBox1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
			groupBox1.Size = new System.Drawing.Size(667, 172);
			groupBox1.TabIndex = 4;
			groupBox1.TabStop = false;
			groupBox1.Text = "Start Server";
			// 
			// serverChatView
			// 
			serverChatView.Location = new System.Drawing.Point(50, 289);
			serverChatView.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			serverChatView.Name = "serverChatView";
			serverChatView.Size = new System.Drawing.Size(1081, 726);
			serverChatView.TabIndex = 5;
			serverChatView.Text = "";
			// 
			// Server
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1173, 1079);
			Controls.Add(serverChatView);
			Controls.Add(serverStopButton);
			Controls.Add(serverStartButton);
			Controls.Add(serverPortTxt);
			Controls.Add(label1);
			Controls.Add(groupBox1);
			Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			Name = "Server";
			Text = "Server";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox serverPortTxt;
		private System.Windows.Forms.Button serverStartButton;
		private System.Windows.Forms.Button serverStopButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RichTextBox serverChatView;
	}
}

