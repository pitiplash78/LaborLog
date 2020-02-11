namespace LaborLog
{
    partial class EditAddEntry
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.panelUsers = new System.Windows.Forms.Panel();
            this.comboBoxCategories = new System.Windows.Forms.ComboBox();
            this.labelDuration = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dTP1 = new System.Windows.Forms.DateTimePicker();
            this.dTP2 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(516, 205);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonContinue
            // 
            this.buttonContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonContinue.Enabled = false;
            this.buttonContinue.Location = new System.Drawing.Point(597, 205);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 1;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // panelUsers
            // 
            this.panelUsers.AutoScroll = true;
            this.panelUsers.Location = new System.Drawing.Point(12, 12);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(168, 150);
            this.panelUsers.TabIndex = 8;
            // 
            // comboBoxCategories
            // 
            this.comboBoxCategories.FormattingEnabled = true;
            this.comboBoxCategories.Location = new System.Drawing.Point(12, 169);
            this.comboBoxCategories.Name = "comboBoxCategories";
            this.comboBoxCategories.Size = new System.Drawing.Size(168, 21);
            this.comboBoxCategories.TabIndex = 9;
            this.comboBoxCategories.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxCategories_KeyPress);
            // 
            // labelDuration
            // 
            this.labelDuration.Location = new System.Drawing.Point(472, 64);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(57, 19);
            this.labelDuration.TabIndex = 14;
            this.labelDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(196, 92);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(476, 98);
            this.textBoxInfo.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Begin:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "End:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Duration:";
            // 
            // dTP1
            // 
            this.dTP1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP1.Location = new System.Drawing.Point(275, 12);
            this.dTP1.Name = "dTP1";
            this.dTP1.Size = new System.Drawing.Size(159, 20);
            this.dTP1.TabIndex = 19;
            this.dTP1.ValueChanged += new System.EventHandler(this.dTP1_ValueChanged);
            // 
            // dTP2
            // 
            this.dTP2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP2.Location = new System.Drawing.Point(275, 38);
            this.dTP2.Name = "dTP2";
            this.dTP2.Size = new System.Drawing.Size(159, 20);
            this.dTP2.TabIndex = 20;
            this.dTP2.ValueChanged += new System.EventHandler(this.dTP2_ValueChanged);
            // 
            // EditAddEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 240);
            this.Controls.Add(this.dTP2);
            this.Controls.Add(this.dTP1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.labelDuration);
            this.Controls.Add(this.comboBoxCategories);
            this.Controls.Add(this.panelUsers);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditAddEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LabLog - ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.ComboBox comboBoxCategories;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dTP1;
        private System.Windows.Forms.DateTimePicker dTP2;
    }
}