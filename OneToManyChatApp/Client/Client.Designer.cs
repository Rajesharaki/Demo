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
            this.textMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.Textstatus = new System.Windows.Forms.ListBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.textName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblClientmsg = new System.Windows.Forms.Label();
            this.frdCheckList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // textMsg
            // 
            this.textMsg.Location = new System.Drawing.Point(35, 397);
            this.textMsg.Name = "textMsg";
            this.textMsg.Size = new System.Drawing.Size(572, 22);
            this.textMsg.TabIndex = 13;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(651, 371);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(121, 49);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click_1);
            // 
            // Textstatus
            // 
            this.Textstatus.FormattingEnabled = true;
            this.Textstatus.ItemHeight = 16;
            this.Textstatus.Location = new System.Drawing.Point(35, 110);
            this.Textstatus.Name = "Textstatus";
            this.Textstatus.Size = new System.Drawing.Size(363, 228);
            this.Textstatus.TabIndex = 11;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(476, 24);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(188, 56);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click_1);
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(96, 41);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(329, 22);
            this.textName.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            // 
            // lblClientmsg
            // 
            this.lblClientmsg.AutoSize = true;
            this.lblClientmsg.Location = new System.Drawing.Point(-2, 430);
            this.lblClientmsg.Name = "lblClientmsg";
            this.lblClientmsg.Size = new System.Drawing.Size(0, 17);
            this.lblClientmsg.TabIndex = 14;
            // 
            // frdCheckList
            // 
            this.frdCheckList.FormattingEnabled = true;
            this.frdCheckList.Location = new System.Drawing.Point(442, 110);
            this.frdCheckList.Name = "frdCheckList";
            this.frdCheckList.Size = new System.Drawing.Size(311, 225);
            this.frdCheckList.TabIndex = 15;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.frdCheckList);
            this.Controls.Add(this.lblClientmsg);
            this.Controls.Add(this.textMsg);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.Textstatus);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.label1);
            this.Name = "Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox Textstatus;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClientmsg;
        private System.Windows.Forms.CheckedListBox frdCheckList;
    }
}

