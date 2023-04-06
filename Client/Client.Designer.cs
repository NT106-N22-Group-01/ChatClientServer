namespace Client
{
	partial class Client
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
			serverIPTxt = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			serverPortTxt = new System.Windows.Forms.TextBox();
			clientConnectButton = new System.Windows.Forms.Button();
			clientDisconnectButton = new System.Windows.Forms.Button();
			groupBox1 = new System.Windows.Forms.GroupBox();
			label3 = new System.Windows.Forms.Label();
			nameTxt = new System.Windows.Forms.TextBox();
			clientChatView = new System.Windows.Forms.RichTextBox();
			msgTxt = new System.Windows.Forms.TextBox();
			clientSendButton = new System.Windows.Forms.Button();
			groupBox2 = new System.Windows.Forms.GroupBox();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			label1.Location = new System.Drawing.Point(83, 96);
			label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(130, 27);
			label1.TabIndex = 0;
			label1.Text = "IP Address";
			// 
			// serverIPTxt
			// 
			serverIPTxt.Location = new System.Drawing.Point(83, 154);
			serverIPTxt.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			serverIPTxt.Name = "serverIPTxt";
			serverIPTxt.Size = new System.Drawing.Size(164, 31);
			serverIPTxt.TabIndex = 1;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			label2.Location = new System.Drawing.Point(317, 96);
			label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(58, 27);
			label2.TabIndex = 2;
			label2.Text = "Port";
			// 
			// serverPortTxt
			// 
			serverPortTxt.Location = new System.Drawing.Point(317, 154);
			serverPortTxt.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			serverPortTxt.Name = "serverPortTxt";
			serverPortTxt.Size = new System.Drawing.Size(164, 31);
			serverPortTxt.TabIndex = 3;
			// 
			// clientConnectButton
			// 
			clientConnectButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			clientConnectButton.Location = new System.Drawing.Point(750, 96);
			clientConnectButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			clientConnectButton.Name = "clientConnectButton";
			clientConnectButton.Size = new System.Drawing.Size(150, 96);
			clientConnectButton.TabIndex = 4;
			clientConnectButton.Text = "Connect";
			clientConnectButton.UseVisualStyleBackColor = true;
			clientConnectButton.Click += clientConnectButton_Click;
			// 
			// clientDisconnectButton
			// 
			clientDisconnectButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			clientDisconnectButton.Location = new System.Drawing.Point(950, 96);
			clientDisconnectButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			clientDisconnectButton.Name = "clientDisconnectButton";
			clientDisconnectButton.Size = new System.Drawing.Size(150, 96);
			clientDisconnectButton.TabIndex = 5;
			clientDisconnectButton.Text = "Disconnect";
			clientDisconnectButton.UseVisualStyleBackColor = true;
			clientDisconnectButton.Click += clientDisconnectButton_Click;
			// 
			// groupBox1
			// 
			groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			groupBox1.Location = new System.Drawing.Point(50, 58);
			groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
			groupBox1.Size = new System.Drawing.Size(1083, 154);
			groupBox1.TabIndex = 6;
			groupBox1.TabStop = false;
			groupBox1.Text = "Connect to Server";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			label3.Location = new System.Drawing.Point(533, 96);
			label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(74, 27);
			label3.TabIndex = 7;
			label3.Text = "Name";
			// 
			// nameTxt
			// 
			nameTxt.Location = new System.Drawing.Point(533, 154);
			nameTxt.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			nameTxt.Name = "nameTxt";
			nameTxt.Size = new System.Drawing.Size(164, 31);
			nameTxt.TabIndex = 8;
			// 
			// clientChatView
			// 
			clientChatView.Location = new System.Drawing.Point(50, 231);
			clientChatView.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			clientChatView.Name = "clientChatView";
			clientChatView.Size = new System.Drawing.Size(1081, 573);
			clientChatView.TabIndex = 9;
			clientChatView.Text = "";
			// 
			// msgTxt
			// 
			msgTxt.Location = new System.Drawing.Point(83, 904);
			msgTxt.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			msgTxt.Multiline = true;
			msgTxt.Name = "msgTxt";
			msgTxt.Size = new System.Drawing.Size(831, 92);
			msgTxt.TabIndex = 10;
			msgTxt.TextChanged += msgTxt_TextChanged;
			// 
			// clientSendButton
			// 
			clientSendButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			clientSendButton.Location = new System.Drawing.Point(950, 904);
			clientSendButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			clientSendButton.Name = "clientSendButton";
			clientSendButton.Size = new System.Drawing.Size(150, 96);
			clientSendButton.TabIndex = 11;
			clientSendButton.Text = "Send";
			clientSendButton.UseVisualStyleBackColor = true;
			clientSendButton.Click += clientSendButton_Click;
			// 
			// groupBox2
			// 
			groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			groupBox2.Location = new System.Drawing.Point(50, 865);
			groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			groupBox2.Name = "groupBox2";
			groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
			groupBox2.Size = new System.Drawing.Size(1083, 173);
			groupBox2.TabIndex = 12;
			groupBox2.TabStop = false;
			groupBox2.Text = "Message";
			// 
			// Client
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1173, 1079);
			Controls.Add(clientSendButton);
			Controls.Add(msgTxt);
			Controls.Add(clientChatView);
			Controls.Add(nameTxt);
			Controls.Add(label3);
			Controls.Add(clientDisconnectButton);
			Controls.Add(clientConnectButton);
			Controls.Add(serverPortTxt);
			Controls.Add(label2);
			Controls.Add(serverIPTxt);
			Controls.Add(label1);
			Controls.Add(groupBox1);
			Controls.Add(groupBox2);
			Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			Name = "Client";
			Text = "Client";
			Load += Client_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox serverIPTxt;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox serverPortTxt;
		private System.Windows.Forms.Button clientConnectButton;
		private System.Windows.Forms.Button clientDisconnectButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox nameTxt;
		private System.Windows.Forms.RichTextBox clientChatView;
		private System.Windows.Forms.TextBox msgTxt;
		private System.Windows.Forms.Button clientSendButton;
		private System.Windows.Forms.GroupBox groupBox2;
	}
}

