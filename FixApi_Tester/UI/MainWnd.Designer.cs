namespace FixApi_Tester
{
    partial class MainWnd
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtPrice_TargetCompID = new System.Windows.Forms.TextBox();
            this.m_txtPrice_SenderCompID = new System.Windows.Forms.TextBox();
            this.m_txtPrice_Port = new System.Windows.Forms.TextBox();
            this.m_txtPrice_Host = new System.Windows.Forms.TextBox();
            this.m_txtPrice_Password = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_txtTrade_TargetCompID = new System.Windows.Forms.TextBox();
            this.m_txtTrade_SenderCompID = new System.Windows.Forms.TextBox();
            this.m_txtTrade_Port = new System.Windows.Forms.TextBox();
            this.m_txtTrade_Host = new System.Windows.Forms.TextBox();
            this.m_txtTrade_Password = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_btnConnect = new System.Windows.Forms.Button();
            this.m_txtLogs = new System.Windows.Forms.RichTextBox();
            this.m_btnSendMarketOrder = new System.Windows.Forms.Button();
            this.m_btnSendLimitOrder = new System.Windows.Forms.Button();
            this.m_btnSendPrevQuotedOrder = new System.Windows.Forms.Button();
            this.m_btnPositionForRequest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_txtPrice_TargetCompID);
            this.groupBox1.Controls.Add(this.m_txtPrice_SenderCompID);
            this.groupBox1.Controls.Add(this.m_txtPrice_Port);
            this.groupBox1.Controls.Add(this.m_txtPrice_Host);
            this.groupBox1.Controls.Add(this.m_txtPrice_Password);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OneZero Price Connection";
            // 
            // m_txtPrice_TargetCompID
            // 
            this.m_txtPrice_TargetCompID.Location = new System.Drawing.Point(100, 128);
            this.m_txtPrice_TargetCompID.Name = "m_txtPrice_TargetCompID";
            this.m_txtPrice_TargetCompID.Size = new System.Drawing.Size(187, 20);
            this.m_txtPrice_TargetCompID.TabIndex = 1;
            // 
            // m_txtPrice_SenderCompID
            // 
            this.m_txtPrice_SenderCompID.Location = new System.Drawing.Point(100, 103);
            this.m_txtPrice_SenderCompID.Name = "m_txtPrice_SenderCompID";
            this.m_txtPrice_SenderCompID.Size = new System.Drawing.Size(187, 20);
            this.m_txtPrice_SenderCompID.TabIndex = 1;
            // 
            // m_txtPrice_Port
            // 
            this.m_txtPrice_Port.Location = new System.Drawing.Point(100, 78);
            this.m_txtPrice_Port.Name = "m_txtPrice_Port";
            this.m_txtPrice_Port.Size = new System.Drawing.Size(187, 20);
            this.m_txtPrice_Port.TabIndex = 1;
            // 
            // m_txtPrice_Host
            // 
            this.m_txtPrice_Host.Location = new System.Drawing.Point(100, 53);
            this.m_txtPrice_Host.Name = "m_txtPrice_Host";
            this.m_txtPrice_Host.Size = new System.Drawing.Size(187, 20);
            this.m_txtPrice_Host.TabIndex = 1;
            // 
            // m_txtPrice_Password
            // 
            this.m_txtPrice_Password.Location = new System.Drawing.Point(100, 28);
            this.m_txtPrice_Password.Name = "m_txtPrice_Password";
            this.m_txtPrice_Password.Size = new System.Drawing.Size(187, 20);
            this.m_txtPrice_Password.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "TargetCompID : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "SenderCompID : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Port : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Host : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Password : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_txtTrade_TargetCompID);
            this.groupBox2.Controls.Add(this.m_txtTrade_SenderCompID);
            this.groupBox2.Controls.Add(this.m_txtTrade_Port);
            this.groupBox2.Controls.Add(this.m_txtTrade_Host);
            this.groupBox2.Controls.Add(this.m_txtTrade_Password);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(12, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 167);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OneZero Trade Connection";
            // 
            // m_txtTrade_TargetCompID
            // 
            this.m_txtTrade_TargetCompID.Location = new System.Drawing.Point(100, 128);
            this.m_txtTrade_TargetCompID.Name = "m_txtTrade_TargetCompID";
            this.m_txtTrade_TargetCompID.Size = new System.Drawing.Size(187, 20);
            this.m_txtTrade_TargetCompID.TabIndex = 1;
            // 
            // m_txtTrade_SenderCompID
            // 
            this.m_txtTrade_SenderCompID.Location = new System.Drawing.Point(100, 103);
            this.m_txtTrade_SenderCompID.Name = "m_txtTrade_SenderCompID";
            this.m_txtTrade_SenderCompID.Size = new System.Drawing.Size(187, 20);
            this.m_txtTrade_SenderCompID.TabIndex = 1;
            // 
            // m_txtTrade_Port
            // 
            this.m_txtTrade_Port.Location = new System.Drawing.Point(100, 78);
            this.m_txtTrade_Port.Name = "m_txtTrade_Port";
            this.m_txtTrade_Port.Size = new System.Drawing.Size(187, 20);
            this.m_txtTrade_Port.TabIndex = 1;
            // 
            // m_txtTrade_Host
            // 
            this.m_txtTrade_Host.Location = new System.Drawing.Point(100, 53);
            this.m_txtTrade_Host.Name = "m_txtTrade_Host";
            this.m_txtTrade_Host.Size = new System.Drawing.Size(187, 20);
            this.m_txtTrade_Host.TabIndex = 1;
            // 
            // m_txtTrade_Password
            // 
            this.m_txtTrade_Password.Location = new System.Drawing.Point(100, 28);
            this.m_txtTrade_Password.Name = "m_txtTrade_Password";
            this.m_txtTrade_Password.Size = new System.Drawing.Size(187, 20);
            this.m_txtTrade_Password.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "TargetCompID : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "SenderCompID : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Port : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(56, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Host : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Password : ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_btnPositionForRequest);
            this.groupBox3.Controls.Add(this.m_btnSendPrevQuotedOrder);
            this.groupBox3.Controls.Add(this.m_btnSendLimitOrder);
            this.groupBox3.Controls.Add(this.m_btnSendMarketOrder);
            this.groupBox3.Controls.Add(this.m_btnConnect);
            this.groupBox3.Location = new System.Drawing.Point(326, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(655, 48);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // m_btnConnect
            // 
            this.m_btnConnect.Location = new System.Drawing.Point(13, 19);
            this.m_btnConnect.Name = "m_btnConnect";
            this.m_btnConnect.Size = new System.Drawing.Size(107, 23);
            this.m_btnConnect.TabIndex = 0;
            this.m_btnConnect.Text = "Connect";
            this.m_btnConnect.UseVisualStyleBackColor = true;
            this.m_btnConnect.Click += new System.EventHandler(this.m_btnConnect_Click);
            // 
            // m_txtLogs
            // 
            this.m_txtLogs.BackColor = System.Drawing.Color.Black;
            this.m_txtLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtLogs.ForeColor = System.Drawing.Color.Yellow;
            this.m_txtLogs.Location = new System.Drawing.Point(326, 67);
            this.m_txtLogs.Margin = new System.Windows.Forms.Padding(4);
            this.m_txtLogs.Name = "m_txtLogs";
            this.m_txtLogs.ReadOnly = true;
            this.m_txtLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.m_txtLogs.Size = new System.Drawing.Size(655, 285);
            this.m_txtLogs.TabIndex = 21;
            this.m_txtLogs.Text = "";
            // 
            // m_btnSendMarketOrder
            // 
            this.m_btnSendMarketOrder.Location = new System.Drawing.Point(126, 19);
            this.m_btnSendMarketOrder.Name = "m_btnSendMarketOrder";
            this.m_btnSendMarketOrder.Size = new System.Drawing.Size(107, 23);
            this.m_btnSendMarketOrder.TabIndex = 0;
            this.m_btnSendMarketOrder.Text = "SendMarketOrder";
            this.m_btnSendMarketOrder.UseVisualStyleBackColor = true;
            this.m_btnSendMarketOrder.Click += new System.EventHandler(this.m_btnSendMarketOrder_Click);
            // 
            // m_btnSendLimitOrder
            // 
            this.m_btnSendLimitOrder.Location = new System.Drawing.Point(239, 19);
            this.m_btnSendLimitOrder.Name = "m_btnSendLimitOrder";
            this.m_btnSendLimitOrder.Size = new System.Drawing.Size(107, 23);
            this.m_btnSendLimitOrder.TabIndex = 0;
            this.m_btnSendLimitOrder.Text = "SendLimitOrder";
            this.m_btnSendLimitOrder.UseVisualStyleBackColor = true;
            this.m_btnSendLimitOrder.Click += new System.EventHandler(this.m_btnSendLimitOrder_Click);
            // 
            // m_btnSendPrevQuotedOrder
            // 
            this.m_btnSendPrevQuotedOrder.Location = new System.Drawing.Point(352, 19);
            this.m_btnSendPrevQuotedOrder.Name = "m_btnSendPrevQuotedOrder";
            this.m_btnSendPrevQuotedOrder.Size = new System.Drawing.Size(135, 23);
            this.m_btnSendPrevQuotedOrder.TabIndex = 0;
            this.m_btnSendPrevQuotedOrder.Text = "SendPrevQuotedOrder";
            this.m_btnSendPrevQuotedOrder.UseVisualStyleBackColor = true;
            this.m_btnSendPrevQuotedOrder.Click += new System.EventHandler(this.m_btnSendPrevQuotedOrder_Click);
            // 
            // m_btnPositionForRequest
            // 
            this.m_btnPositionForRequest.Location = new System.Drawing.Point(493, 19);
            this.m_btnPositionForRequest.Name = "m_btnPositionForRequest";
            this.m_btnPositionForRequest.Size = new System.Drawing.Size(135, 23);
            this.m_btnPositionForRequest.TabIndex = 0;
            this.m_btnPositionForRequest.Text = "PositionForRequest";
            this.m_btnPositionForRequest.UseVisualStyleBackColor = true;
            this.m_btnPositionForRequest.Click += new System.EventHandler(this.m_btnPositionForRequest_Click);
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 368);
            this.Controls.Add(this.m_txtLogs);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainWnd";
            this.Text = "OneZero-FixApi-Tester";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox m_txtPrice_TargetCompID;
        private System.Windows.Forms.TextBox m_txtPrice_SenderCompID;
        private System.Windows.Forms.TextBox m_txtPrice_Port;
        private System.Windows.Forms.TextBox m_txtPrice_Host;
        private System.Windows.Forms.TextBox m_txtPrice_Password;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox m_txtTrade_TargetCompID;
        private System.Windows.Forms.TextBox m_txtTrade_SenderCompID;
        private System.Windows.Forms.TextBox m_txtTrade_Port;
        private System.Windows.Forms.TextBox m_txtTrade_Host;
        private System.Windows.Forms.TextBox m_txtTrade_Password;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.RichTextBox m_txtLogs;
        private System.Windows.Forms.Button m_btnConnect;
        private System.Windows.Forms.Button m_btnPositionForRequest;
        private System.Windows.Forms.Button m_btnSendPrevQuotedOrder;
        private System.Windows.Forms.Button m_btnSendLimitOrder;
        private System.Windows.Forms.Button m_btnSendMarketOrder;
    }
}

