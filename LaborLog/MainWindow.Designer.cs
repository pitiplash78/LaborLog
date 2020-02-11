namespace LaborLog
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panelUsers = new System.Windows.Forms.Panel();
            this.panelEntries = new System.Windows.Forms.Panel();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.panelTime = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelmJD = new System.Windows.Forms.Label();
            this.labelWoY = new System.Windows.Forms.Label();
            this.labelDoY = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonAddSession = new System.Windows.Forms.Button();
            this.labelActiveSessions = new System.Windows.Forms.Label();
            this.buttonProperties = new System.Windows.Forms.Button();
            this.buttonSendMail = new System.Windows.Forms.Button();
            this.labelPendingMails = new System.Windows.Forms.Label();
            this.buttonShowAllEntries = new System.Windows.Forms.Button();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelUsers
            // 
            this.panelUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelUsers.AutoScroll = true;
            this.panelUsers.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelUsers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelUsers.Location = new System.Drawing.Point(12, 71);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(139, 411);
            this.panelUsers.TabIndex = 7;
            // 
            // panelEntries
            // 
            this.panelEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEntries.AutoScroll = true;
            this.panelEntries.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelEntries.Location = new System.Drawing.Point(156, 71);
            this.panelEntries.Name = "panelEntries";
            this.panelEntries.Size = new System.Drawing.Size(751, 470);
            this.panelEntries.TabIndex = 16;
            // 
            // buttonFinish
            // 
            this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFinish.Enabled = false;
            this.buttonFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFinish.ForeColor = System.Drawing.Color.Green;
            this.buttonFinish.Location = new System.Drawing.Point(12, 544);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(138, 30);
            this.buttonFinish.TabIndex = 26;
            this.buttonFinish.Text = "Finish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // panelTime
            // 
            this.panelTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTime.Controls.Add(this.label9);
            this.panelTime.Controls.Add(this.label8);
            this.panelTime.Controls.Add(this.label6);
            this.panelTime.Controls.Add(this.label5);
            this.panelTime.Controls.Add(this.labelmJD);
            this.panelTime.Controls.Add(this.labelWoY);
            this.panelTime.Controls.Add(this.labelDoY);
            this.panelTime.Controls.Add(this.labelDate);
            this.panelTime.Controls.Add(this.labelTime);
            this.panelTime.Controls.Add(this.label7);
            this.panelTime.Location = new System.Drawing.Point(156, 9);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(676, 56);
            this.panelTime.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(414, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "mJD";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(360, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "WoY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(309, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "DoY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Date";
            // 
            // labelmJD
            // 
            this.labelmJD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelmJD.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelmJD.ForeColor = System.Drawing.Color.Red;
            this.labelmJD.Location = new System.Drawing.Point(414, 21);
            this.labelmJD.Name = "labelmJD";
            this.labelmJD.Size = new System.Drawing.Size(195, 22);
            this.labelmJD.TabIndex = 28;
            this.labelmJD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWoY
            // 
            this.labelWoY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelWoY.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWoY.ForeColor = System.Drawing.Color.Red;
            this.labelWoY.Location = new System.Drawing.Point(363, 21);
            this.labelWoY.Name = "labelWoY";
            this.labelWoY.Size = new System.Drawing.Size(45, 22);
            this.labelWoY.TabIndex = 27;
            this.labelWoY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDoY
            // 
            this.labelDoY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDoY.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoY.ForeColor = System.Drawing.Color.Red;
            this.labelDoY.Location = new System.Drawing.Point(312, 21);
            this.labelDoY.Name = "labelDoY";
            this.labelDoY.Size = new System.Drawing.Size(45, 22);
            this.labelDoY.TabIndex = 26;
            this.labelDoY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDate
            // 
            this.labelDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDate.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.ForeColor = System.Drawing.Color.Red;
            this.labelDate.Location = new System.Drawing.Point(116, 21);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(190, 22);
            this.labelDate.TabIndex = 25;
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTime
            // 
            this.labelTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTime.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Red;
            this.labelTime.Location = new System.Drawing.Point(10, 21);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(100, 22);
            this.labelTime.TabIndex = 24;
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Time";
            // 
            // buttonAddSession
            // 
            this.buttonAddSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddSession.Location = new System.Drawing.Point(12, 488);
            this.buttonAddSession.Name = "buttonAddSession";
            this.buttonAddSession.Size = new System.Drawing.Size(138, 30);
            this.buttonAddSession.TabIndex = 29;
            this.buttonAddSession.Text = "Add Session";
            this.buttonAddSession.UseVisualStyleBackColor = true;
            this.buttonAddSession.Visible = false;
            this.buttonAddSession.Click += new System.EventHandler(this.buttonAddSession_Click);
            // 
            // labelActiveSessions
            // 
            this.labelActiveSessions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelActiveSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelActiveSessions.ForeColor = System.Drawing.Color.Red;
            this.labelActiveSessions.Location = new System.Drawing.Point(13, 521);
            this.labelActiveSessions.Name = "labelActiveSessions";
            this.labelActiveSessions.Size = new System.Drawing.Size(138, 20);
            this.labelActiveSessions.TabIndex = 31;
            this.labelActiveSessions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonProperties
            // 
            this.buttonProperties.Location = new System.Drawing.Point(12, 10);
            this.buttonProperties.Name = "buttonProperties";
            this.buttonProperties.Size = new System.Drawing.Size(138, 23);
            this.buttonProperties.TabIndex = 32;
            this.buttonProperties.Text = "Properties  ...";
            this.buttonProperties.UseVisualStyleBackColor = true;
            this.buttonProperties.Click += new System.EventHandler(this.buttonProperties_Click);
            // 
            // buttonSendMail
            // 
            this.buttonSendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSendMail.Location = new System.Drawing.Point(156, 551);
            this.buttonSendMail.Name = "buttonSendMail";
            this.buttonSendMail.Size = new System.Drawing.Size(138, 23);
            this.buttonSendMail.TabIndex = 34;
            this.buttonSendMail.Text = "Send Mail  ...";
            this.buttonSendMail.UseVisualStyleBackColor = true;
            this.buttonSendMail.Click += new System.EventHandler(this.buttonSendMail_Click);
            // 
            // labelPendingMails
            // 
            this.labelPendingMails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPendingMails.ForeColor = System.Drawing.Color.Red;
            this.labelPendingMails.Location = new System.Drawing.Point(309, 550);
            this.labelPendingMails.Name = "labelPendingMails";
            this.labelPendingMails.Size = new System.Drawing.Size(396, 24);
            this.labelPendingMails.TabIndex = 35;
            // 
            // buttonShowAllEntries
            // 
            this.buttonShowAllEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowAllEntries.Location = new System.Drawing.Point(711, 551);
            this.buttonShowAllEntries.Name = "buttonShowAllEntries";
            this.buttonShowAllEntries.Size = new System.Drawing.Size(196, 23);
            this.buttonShowAllEntries.TabIndex = 36;
            this.buttonShowAllEntries.Text = "Show all entries / Modify - add entry";
            this.buttonShowAllEntries.UseVisualStyleBackColor = true;
            this.buttonShowAllEntries.Click += new System.EventHandler(this.buttonShowAllEntries_Click);
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Location = new System.Drawing.Point(12, 39);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(138, 23);
            this.buttonAddUser.TabIndex = 37;
            this.buttonAddUser.Text = "Add User  ...";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(838, 9);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(69, 56);
            this.buttonClose.TabIndex = 38;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 581);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonAddUser);
            this.Controls.Add(this.buttonAddSession);
            this.Controls.Add(this.labelActiveSessions);
            this.Controls.Add(this.buttonShowAllEntries);
            this.Controls.Add(this.labelPendingMails);
            this.Controls.Add(this.buttonSendMail);
            this.Controls.Add(this.buttonProperties);
            this.Controls.Add(this.panelTime);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.panelEntries);
            this.Controls.Add(this.panelUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(665, 550);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LabLog";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.ResizeBegin += new System.EventHandler(this.MainWindow_ResizeBegin);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.panelTime.ResumeLayout(false);
            this.panelTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.Panel panelEntries;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Panel panelTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelmJD;
        private System.Windows.Forms.Label labelWoY;
        private System.Windows.Forms.Label labelDoY;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonAddSession;
        private System.Windows.Forms.Label labelActiveSessions;
        private System.Windows.Forms.Button buttonProperties;
        private System.Windows.Forms.Button buttonSendMail;
        private System.Windows.Forms.Label labelPendingMails;
        private System.Windows.Forms.Button buttonShowAllEntries;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.Button buttonClose;

    }
}

