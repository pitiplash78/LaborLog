namespace LaborLog
{
    partial class PropertiesSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesSettings));
            this.labelUsers = new System.Windows.Forms.Label();
            this.labelCatergories = new System.Windows.Forms.Label();
            this.labelLogFile = new System.Windows.Forms.Label();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.buttonUsersAdd = new System.Windows.Forms.Button();
            this.buttonUsersDelete = new System.Windows.Forms.Button();
            this.buttonCategoriesAdd = new System.Windows.Forms.Button();
            this.buttonCatergoriesDelete = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonChangePath = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.checkBoxNotification = new System.Windows.Forms.CheckBox();
            this.checkedListCategories = new System.Windows.Forms.CheckedListBox();
            this.checkBoxShowPassword = new System.Windows.Forms.CheckBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUserPassword = new System.Windows.Forms.TextBox();
            this.checkBoxUsePassword = new System.Windows.Forms.CheckBox();
            this.buttonCategoriesDown = new System.Windows.Forms.Button();
            this.buttonCategoriesUp = new System.Windows.Forms.Button();
            this.buttonUsersDown = new System.Windows.Forms.Button();
            this.buttonUsersUp = new System.Windows.Forms.Button();
            this.userControlMailProperties1 = new eMail.UserControlMailProperties();
            this.SuspendLayout();
            // 
            // labelUsers
            // 
            this.labelUsers.AutoSize = true;
            this.labelUsers.Location = new System.Drawing.Point(12, 12);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(34, 13);
            this.labelUsers.TabIndex = 0;
            this.labelUsers.Text = "Users";
            // 
            // labelCatergories
            // 
            this.labelCatergories.AutoSize = true;
            this.labelCatergories.Location = new System.Drawing.Point(225, 12);
            this.labelCatergories.Name = "labelCatergories";
            this.labelCatergories.Size = new System.Drawing.Size(57, 13);
            this.labelCatergories.TabIndex = 1;
            this.labelCatergories.Text = "Categories";
            // 
            // labelLogFile
            // 
            this.labelLogFile.AutoSize = true;
            this.labelLogFile.Location = new System.Drawing.Point(12, 326);
            this.labelLogFile.Name = "labelLogFile";
            this.labelLogFile.Size = new System.Drawing.Size(47, 13);
            this.labelLogFile.TabIndex = 2;
            this.labelLogFile.Text = "Log File:";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.Location = new System.Drawing.Point(15, 28);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(156, 238);
            this.listBoxUsers.TabIndex = 1;
            this.listBoxUsers.SelectedIndexChanged += new System.EventHandler(this.listBoxUsers_SelectedIndexChanged);
            // 
            // buttonUsersAdd
            // 
            this.buttonUsersAdd.Location = new System.Drawing.Point(15, 287);
            this.buttonUsersAdd.Name = "buttonUsersAdd";
            this.buttonUsersAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonUsersAdd.TabIndex = 2;
            this.buttonUsersAdd.Text = "Add ...";
            this.buttonUsersAdd.UseVisualStyleBackColor = true;
            this.buttonUsersAdd.Click += new System.EventHandler(this.buttonUsersAdd_Click);
            // 
            // buttonUsersDelete
            // 
            this.buttonUsersDelete.Enabled = false;
            this.buttonUsersDelete.Location = new System.Drawing.Point(96, 287);
            this.buttonUsersDelete.Name = "buttonUsersDelete";
            this.buttonUsersDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonUsersDelete.TabIndex = 3;
            this.buttonUsersDelete.Text = "Delete";
            this.buttonUsersDelete.UseVisualStyleBackColor = true;
            this.buttonUsersDelete.Click += new System.EventHandler(this.buttonUsersDelete_Click);
            // 
            // buttonCategoriesAdd
            // 
            this.buttonCategoriesAdd.Location = new System.Drawing.Point(228, 287);
            this.buttonCategoriesAdd.Name = "buttonCategoriesAdd";
            this.buttonCategoriesAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonCategoriesAdd.TabIndex = 7;
            this.buttonCategoriesAdd.Text = "Add ...";
            this.buttonCategoriesAdd.UseVisualStyleBackColor = true;
            this.buttonCategoriesAdd.Click += new System.EventHandler(this.buttonCategoriesAdd_Click);
            // 
            // buttonCatergoriesDelete
            // 
            this.buttonCatergoriesDelete.Enabled = false;
            this.buttonCatergoriesDelete.Location = new System.Drawing.Point(309, 287);
            this.buttonCatergoriesDelete.Name = "buttonCatergoriesDelete";
            this.buttonCatergoriesDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonCatergoriesDelete.TabIndex = 8;
            this.buttonCatergoriesDelete.Text = "Delete";
            this.buttonCatergoriesDelete.UseVisualStyleBackColor = true;
            this.buttonCatergoriesDelete.Click += new System.EventHandler(this.buttonCatergoriesDelete_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(65, 323);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(296, 20);
            this.textBoxPath.TabIndex = 11;
            // 
            // buttonChangePath
            // 
            this.buttonChangePath.Location = new System.Drawing.Point(367, 321);
            this.buttonChangePath.Name = "buttonChangePath";
            this.buttonChangePath.Size = new System.Drawing.Size(52, 23);
            this.buttonChangePath.TabIndex = 11;
            this.buttonChangePath.Text = "Change";
            this.buttonChangePath.UseVisualStyleBackColor = true;
            this.buttonChangePath.Click += new System.EventHandler(this.buttonChangePath_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(719, 414);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelError
            // 
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(82, 516);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(357, 17);
            this.labelError.TabIndex = 12;
            // 
            // checkBoxNotification
            // 
            this.checkBoxNotification.AutoSize = true;
            this.checkBoxNotification.Location = new System.Drawing.Point(436, 18);
            this.checkBoxNotification.Name = "checkBoxNotification";
            this.checkBoxNotification.Size = new System.Drawing.Size(157, 17);
            this.checkBoxNotification.TabIndex = 15;
            this.checkBoxNotification.Text = "enable notifcation via e-mail";
            this.checkBoxNotification.UseVisualStyleBackColor = true;
            this.checkBoxNotification.CheckedChanged += new System.EventHandler(this.checkBoxNotification_CheckedChanged);
            // 
            // checkedListCategories
            // 
            this.checkedListCategories.FormattingEnabled = true;
            this.checkedListCategories.Location = new System.Drawing.Point(228, 28);
            this.checkedListCategories.Name = "checkedListCategories";
            this.checkedListCategories.Size = new System.Drawing.Size(156, 244);
            this.checkedListCategories.TabIndex = 16;
            this.checkedListCategories.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListCategories_ItemCheck);
            this.checkedListCategories.SelectedIndexChanged += new System.EventHandler(this.checkedListCategories_SelectedIndexChanged);
            // 
            // checkBoxShowPassword
            // 
            this.checkBoxShowPassword.AutoSize = true;
            this.checkBoxShowPassword.Location = new System.Drawing.Point(228, 411);
            this.checkBoxShowPassword.Name = "checkBoxShowPassword";
            this.checkBoxShowPassword.Size = new System.Drawing.Size(101, 17);
            this.checkBoxShowPassword.TabIndex = 30;
            this.checkBoxShowPassword.Text = "Show password";
            this.checkBoxShowPassword.UseVisualStyleBackColor = true;
            this.checkBoxShowPassword.CheckedChanged += new System.EventHandler(this.checkBoxShowPassword_CheckedChanged);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(12, 411);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 29;
            this.labelPassword.Text = "Password:";
            // 
            // textBoxUserPassword
            // 
            this.textBoxUserPassword.Location = new System.Drawing.Point(74, 408);
            this.textBoxUserPassword.Name = "textBoxUserPassword";
            this.textBoxUserPassword.Size = new System.Drawing.Size(147, 20);
            this.textBoxUserPassword.TabIndex = 28;
            // 
            // checkBoxUsePassword
            // 
            this.checkBoxUsePassword.AutoSize = true;
            this.checkBoxUsePassword.Checked = true;
            this.checkBoxUsePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUsePassword.Location = new System.Drawing.Point(15, 385);
            this.checkBoxUsePassword.Name = "checkBoxUsePassword";
            this.checkBoxUsePassword.Size = new System.Drawing.Size(286, 17);
            this.checkBoxUsePassword.TabIndex = 32;
            this.checkBoxUsePassword.Text = "Use password for properties and editing inactive entries";
            this.checkBoxUsePassword.UseVisualStyleBackColor = true;
            this.checkBoxUsePassword.CheckedChanged += new System.EventHandler(this.checkBoxUsePassword_CheckedChanged);
            // 
            // buttonCategoriesDown
            // 
            this.buttonCategoriesDown.Enabled = false;
            this.buttonCategoriesDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonCategoriesDown.Image")));
            this.buttonCategoriesDown.Location = new System.Drawing.Point(390, 57);
            this.buttonCategoriesDown.Name = "buttonCategoriesDown";
            this.buttonCategoriesDown.Size = new System.Drawing.Size(29, 23);
            this.buttonCategoriesDown.TabIndex = 10;
            this.buttonCategoriesDown.UseVisualStyleBackColor = true;
            this.buttonCategoriesDown.Click += new System.EventHandler(this.buttonCategoriesDown_Click);
            // 
            // buttonCategoriesUp
            // 
            this.buttonCategoriesUp.Enabled = false;
            this.buttonCategoriesUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonCategoriesUp.Image")));
            this.buttonCategoriesUp.Location = new System.Drawing.Point(390, 28);
            this.buttonCategoriesUp.Name = "buttonCategoriesUp";
            this.buttonCategoriesUp.Size = new System.Drawing.Size(29, 23);
            this.buttonCategoriesUp.TabIndex = 9;
            this.buttonCategoriesUp.UseVisualStyleBackColor = true;
            this.buttonCategoriesUp.Click += new System.EventHandler(this.buttonCategoriesUp_Click);
            // 
            // buttonUsersDown
            // 
            this.buttonUsersDown.Enabled = false;
            this.buttonUsersDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonUsersDown.Image")));
            this.buttonUsersDown.Location = new System.Drawing.Point(178, 57);
            this.buttonUsersDown.Name = "buttonUsersDown";
            this.buttonUsersDown.Size = new System.Drawing.Size(29, 23);
            this.buttonUsersDown.TabIndex = 5;
            this.buttonUsersDown.UseVisualStyleBackColor = true;
            this.buttonUsersDown.Click += new System.EventHandler(this.buttonUsersDown_Click);
            // 
            // buttonUsersUp
            // 
            this.buttonUsersUp.Enabled = false;
            this.buttonUsersUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonUsersUp.Image")));
            this.buttonUsersUp.Location = new System.Drawing.Point(178, 28);
            this.buttonUsersUp.Name = "buttonUsersUp";
            this.buttonUsersUp.Size = new System.Drawing.Size(29, 23);
            this.buttonUsersUp.TabIndex = 4;
            this.buttonUsersUp.UseVisualStyleBackColor = true;
            this.buttonUsersUp.Click += new System.EventHandler(this.buttonUsersUp_Click);
            // 
            // userControlMailProperties1
            // 
            this.userControlMailProperties1.Location = new System.Drawing.Point(436, 41);
            this.userControlMailProperties1.MailProperties = null;
            this.userControlMailProperties1.Name = "userControlMailProperties1";
            this.userControlMailProperties1.Size = new System.Drawing.Size(427, 383);
            this.userControlMailProperties1.TabIndex = 31;
            this.userControlMailProperties1.ValidProperites = false;
            // 
            // PropertiesSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 449);
            this.Controls.Add(this.checkBoxUsePassword);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.userControlMailProperties1);
            this.Controls.Add(this.checkBoxShowPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxUserPassword);
            this.Controls.Add(this.checkedListCategories);
            this.Controls.Add(this.checkBoxNotification);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.buttonCategoriesDown);
            this.Controls.Add(this.buttonCategoriesUp);
            this.Controls.Add(this.buttonUsersDown);
            this.Controls.Add(this.buttonUsersUp);
            this.Controls.Add(this.buttonChangePath);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.buttonCatergoriesDelete);
            this.Controls.Add(this.buttonCategoriesAdd);
            this.Controls.Add(this.buttonUsersDelete);
            this.Controls.Add(this.buttonUsersAdd);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.labelLogFile);
            this.Controls.Add(this.labelCatergories);
            this.Controls.Add(this.labelUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PropertiesSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsers;
        private System.Windows.Forms.Label labelCatergories;
        private System.Windows.Forms.Label labelLogFile;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button buttonUsersAdd;
        private System.Windows.Forms.Button buttonUsersDelete;
        private System.Windows.Forms.Button buttonCategoriesAdd;
        private System.Windows.Forms.Button buttonCatergoriesDelete;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonChangePath;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUsersUp;
        private System.Windows.Forms.Button buttonUsersDown;
        private System.Windows.Forms.Button buttonCategoriesDown;
        private System.Windows.Forms.Button buttonCategoriesUp;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.CheckBox checkBoxNotification;
        private System.Windows.Forms.CheckedListBox checkedListCategories;
        private System.Windows.Forms.CheckBox checkBoxShowPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUserPassword;
        private eMail.UserControlMailProperties userControlMailProperties1;
        private System.Windows.Forms.CheckBox checkBoxUsePassword;
    }
}