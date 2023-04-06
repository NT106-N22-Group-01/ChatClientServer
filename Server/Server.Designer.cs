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
			this.label1 = new System.Windows.Forms.Label();
			this.serverPortTxt = new System.Windows.Forms.TextBox();
			this.serverStartButton = new System.Windows.Forms.Button();
			this.serverStopButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.serverChatView = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(75, 77);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Port";
			// 
			// serverPortTxt
			// 
			this.serverPortTxt.Location = new System.Drawing.Point(75, 123);
			this.serverPortTxt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.serverPortTxt.Name = "serverPortTxt";
			this.serverPortTxt.Size = new System.Drawing.Size(148, 26);
			this.serverPortTxt.TabIndex = 1;
			// 
			// serverStartButton
			// 
			this.serverStartButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.serverStartButton.Location = new System.Drawing.Point(270, 77);
			this.serverStartButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.serverStartButton.Name = "serverStartButton";
			this.serverStartButton.Size = new System.Drawing.Size(150, 77);
			this.serverStartButton.TabIndex = 2;
			this.serverStartButton.Text = "Start";
			this.serverStartButton.UseVisualStyleBackColor = true;
			this.serverStartButton.Click += new System.EventHandler(this.serverStartButton_Click);
			// 
			// serverStopButton
			// 
			this.serverStopButton.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.serverStopButton.Location = new System.Drawing.Point(465, 77);
			this.serverStopButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.serverStopButton.Name = "serverStopButton";
			this.serverStopButton.Size = new System.Drawing.Size(150, 77);
			this.serverStopButton.TabIndex = 3;
			this.serverStopButton.Text = "Stop";
			this.serverStopButton.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(45, 46);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox1.Size = new System.Drawing.Size(600, 138);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Start Server";
			// 
			// serverChatView
			// 
			this.serverChatView.Location = new System.Drawing.Point(45, 231);
			this.serverChatView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.serverChatView.Name = "serverChatView";
			this.serverChatView.Size = new System.Drawing.Size(973, 582);
			this.serverChatView.TabIndex = 5;
			this.serverChatView.Text = "";
			// 
			// Server
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1056, 863);
			this.Controls.Add(this.serverChatView);
			this.Controls.Add(this.serverStopButton);
			this.Controls.Add(this.serverStartButton);
			this.Controls.Add(this.serverPortTxt);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Server";
			this.Text = "Server";
			this.ResumeLayout(false);
			this.PerformLayout();

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

