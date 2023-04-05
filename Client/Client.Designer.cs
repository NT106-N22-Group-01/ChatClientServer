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
            this.label1 = new System.Windows.Forms.Label();
            this.serverIPTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serverPortTxt = new System.Windows.Forms.TextBox();
            this.clientConnectButton = new System.Windows.Forms.Button();
            this.clientDisconnectButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.clientChatView = new System.Windows.Forms.RichTextBox();
            this.msgTxt = new System.Windows.Forms.TextBox();
            this.clientSendButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // serverIPTxt
            // 
            this.serverIPTxt.Location = new System.Drawing.Point(50, 80);
            this.serverIPTxt.Name = "serverIPTxt";
            this.serverIPTxt.Size = new System.Drawing.Size(100, 20);
            this.serverIPTxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // serverPortTxt
            // 
            this.serverPortTxt.Location = new System.Drawing.Point(190, 80);
            this.serverPortTxt.Name = "serverPortTxt";
            this.serverPortTxt.Size = new System.Drawing.Size(100, 20);
            this.serverPortTxt.TabIndex = 3;
            // 
            // clientConnectButton
            // 
            this.clientConnectButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientConnectButton.Location = new System.Drawing.Point(450, 50);
            this.clientConnectButton.Name = "clientConnectButton";
            this.clientConnectButton.Size = new System.Drawing.Size(90, 50);
            this.clientConnectButton.TabIndex = 4;
            this.clientConnectButton.Text = "Connect";
            this.clientConnectButton.UseVisualStyleBackColor = true;
            // 
            // clientDisconnectButton
            // 
            this.clientDisconnectButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientDisconnectButton.Location = new System.Drawing.Point(570, 50);
            this.clientDisconnectButton.Name = "clientDisconnectButton";
            this.clientDisconnectButton.Size = new System.Drawing.Size(90, 50);
            this.clientDisconnectButton.TabIndex = 5;
            this.clientDisconnectButton.Text = "Disconnect";
            this.clientDisconnectButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(30, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 80);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect to Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(320, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Name";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(320, 80);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(100, 20);
            this.nameTxt.TabIndex = 8;
            // 
            // clientChatView
            // 
            this.clientChatView.Location = new System.Drawing.Point(30, 120);
            this.clientChatView.Name = "clientChatView";
            this.clientChatView.Size = new System.Drawing.Size(650, 300);
            this.clientChatView.TabIndex = 9;
            this.clientChatView.Text = "";
            // 
            // msgTxt
            // 
            this.msgTxt.Location = new System.Drawing.Point(50, 470);
            this.msgTxt.Multiline = true;
            this.msgTxt.Name = "msgTxt";
            this.msgTxt.Size = new System.Drawing.Size(500, 50);
            this.msgTxt.TabIndex = 10;
            // 
            // clientSendButton
            // 
            this.clientSendButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientSendButton.Location = new System.Drawing.Point(570, 470);
            this.clientSendButton.Name = "clientSendButton";
            this.clientSendButton.Size = new System.Drawing.Size(90, 50);
            this.clientSendButton.TabIndex = 11;
            this.clientSendButton.Text = "Send";
            this.clientSendButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(30, 450);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(650, 90);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 561);
            this.Controls.Add(this.clientSendButton);
            this.Controls.Add(this.msgTxt);
            this.Controls.Add(this.clientChatView);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clientDisconnectButton);
            this.Controls.Add(this.clientConnectButton);
            this.Controls.Add(this.serverPortTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverIPTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

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

