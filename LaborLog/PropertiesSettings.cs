using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace LaborLog
{
    public partial class PropertiesSettings : Form
    {
        private List<UsersClass> Users = null;
        private List<CategoryClass> Categories = null;

        public string logPath { get; private set; }
        public LabLog labLog { get; private set; }
        public eMail.MailProperties mailProperties { get; private set; }

        public bool PropertiesChanged { get; private set; }
        private bool suppress = false;

        public PropertiesSettings(string logPath, LabLog lablog, eMail.MailProperties mailProperties, bool activeEntry)
        {
            this.logPath = logPath;
            this.labLog = lablog;
            this.mailProperties = mailProperties;
            PropertiesChanged = false;

            if (this.labLog == null)
                this.labLog = new LabLog();

            if (this.labLog.metaInformation != null && this.labLog.metaInformation.AllUsers != null)
                Users = new List<UsersClass>(this.labLog.metaInformation.AllUsers);
            else
                Users = new List<UsersClass>();

            if (this.labLog.metaInformation != null && this.labLog.metaInformation.Categories != null)
                Categories = new List<CategoryClass>(this.labLog.metaInformation.Categories);
            else
                Categories = new List<CategoryClass>(CategoryClass.setStandard());

            if (checkedListCategories == null)
                checkedListCategories = new CheckedListBox();

            InitializeComponent();

            this.listBoxUsers.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxUsers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);

            suppress = true;

            fillUsers();

            fillCategories();

            textBoxUserPassword.PasswordChar = '*';
            setPassWordProperties();

            setMailproperties();

            textBoxPath.Text = Path.GetFileName(logPath);

            if (activeEntry)
            {
                buttonChangePath.Enabled = false;
                labelError.Text = "The Log File can be changed only for no active Session!";
            }

            suppress = false;
        }

        //Close
        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (logPath == null)
            {
                MessageBox.Show("Select a Log File!", "LaborLog", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (checkBoxUsePassword.Checked && textBoxUserPassword.Text == "")
            {
                MessageBox.Show("Enter a password!", "LaborLog", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (labLog.metaInformation.Password != eMail.MailProperties.SimpleEncode(textBoxUserPassword.Text))
                PropertiesChanged = true;

            labLog.metaInformation.Password = eMail.MailProperties.SimpleEncode(textBoxUserPassword.Text);

            labLog.metaInformation.AllUsers = Users.ToArray();
            labLog.metaInformation.Categories = Categories.ToArray();

            if (mailProperties != userControlMailProperties1.MailProperties)
                PropertiesChanged = true;

            mailProperties = userControlMailProperties1.MailProperties;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void setPassWordProperties()
        {
            checkBoxShowPassword.Checked = false;
            checkBoxUsePassword.Checked = this.labLog.metaInformation.UsePassword;
            if (this.labLog.metaInformation.Password != null)
                textBoxUserPassword.Text = eMail.MailProperties.SimpleDecode(this.labLog.metaInformation.Password);
        }
        private void setMailproperties()
        {
            if (mailProperties != null)
            {
                userControlMailProperties1.MailProperties = mailProperties;
                checkBoxNotification.Checked = mailProperties.Used;
                userControlMailProperties1.Enabled = mailProperties.Used;
            }
            else
            {
                checkBoxNotification.Checked = false;
                userControlMailProperties1.Enabled = false;
            }
        }

        #region Users
        private void fillUsers()
        {
            listBoxUsers.Items.Clear();

            if (Users.Count == 0)
                Users.Add(new UsersClass("<User>", true));

            if (Users.Count == 1 && Users[0].Name == "<User>")
            {
                listBoxUsers.ForeColor = Color.Red;

                for (int i = 0; i < Users.Count; i++)
                    listBoxUsers.Items.Add(Users[i].Name);
            }
            else
            {
                listBoxUsers.ForeColor = SystemColors.WindowText;

                for (int i = 0; i < Users.Count; i++)
                {
                    if (Users[i].Active)
                        listBoxUsers.Items.Add(
                            new MyListBoxItem(Color.Green, Users[i].Name));
                    else
                        listBoxUsers.Items.Add(
                            new MyListBoxItem(Color.DarkGray, Users[i].Name));
                }
            }
            buttonUsersUp.Enabled = false;
            buttonUsersDown.Enabled = false;
        }

        private void buttonUsersAdd_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            List<string> users = new List<string>();
            if (Users != null)
                foreach (LaborLog.UsersClass cc in Users)
                    if (cc.Active)
                        users.Add(cc.Name);

            PropertiesAdd uAdd = new PropertiesAdd("User", users.ToArray(), 15);
            if (uAdd.ShowDialog() == DialogResult.OK)
            {
                if (Users.Count == 1 && Users[0].Name == "<User>")
                {
                    Users.RemoveAt(0);
                    listBoxUsers.ForeColor = System.Drawing.SystemColors.ControlText;
                }


                bool existent = false;
                for (int i = 0; i < Users.Count; i++)
                {
                    if (Users[i].Name == uAdd.result)
                    {
                        Users[i].Active = true;
                        existent = true;
                    }
                }
                if (!existent)
                {
                    if (listBoxUsers.SelectedIndex != -1)
                        Users.Insert(listBoxUsers.SelectedIndex, new UsersClass(uAdd.result, true));
                    else
                        Users.Add(new UsersClass(uAdd.result, true));
                }
                fillUsers();
            }
        }

        private void buttonUsersDelete_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            string tuser = Users[listBoxUsers.SelectedIndex].Name;

            bool existent = false;

            for (int i = 0; i < labLog.InfoEntryItems.Length; i++)
            {
                for (int j = 0; j < labLog.InfoEntryItems[i].Users.Length; j++)
                {
                    for (int k = 0; k < Users.Count; k++)
                    {
                        if (tuser == labLog.InfoEntryItems[i].Users[j])
                        {
                            existent = true;
                            break;
                        }
                    }
                    if (existent)
                        break;
                }
                if (existent)
                    break;
            }

            if (existent)
                Users[listBoxUsers.SelectedIndex].Active = false;
            else
                Users.RemoveAt(listBoxUsers.SelectedIndex);

            buttonUsersDelete.Enabled = false;
            buttonUsersUp.Enabled = false;
            buttonUsersDown.Enabled = false;

            fillUsers();
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex != -1)
            {
                buttonUsersDelete.Enabled = true;

                if (listBoxUsers.SelectedIndex > 0 && Users.Count > 1)
                    buttonUsersUp.Enabled = true;
                else
                    buttonUsersUp.Enabled = false;

                if (listBoxUsers.SelectedIndex < (Users.Count - 1) && Users.Count > 1)
                    buttonUsersDown.Enabled = true;
                else
                    buttonUsersDown.Enabled = false;
            }
        }

        private void buttonUsersUp_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            int i_old = listBoxUsers.SelectedIndex;
            int i_new = listBoxUsers.SelectedIndex - 1;

            List<LaborLog.UsersClass> user = new List<LaborLog.UsersClass>(Users);
            LaborLog.UsersClass tmp = user[i_old];

            user.RemoveAt(i_old);
            user.Insert(i_new, tmp);

            Users = new List<UsersClass>(user);

            fillUsers();

            listBoxUsers.SelectedIndex = i_new;
        }
        private void buttonUsersDown_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            int i_old = listBoxUsers.SelectedIndex;
            int i_new = listBoxUsers.SelectedIndex + 1;

            List<LaborLog.UsersClass> user = new List<LaborLog.UsersClass>(Users);
            LaborLog.UsersClass tmp = user[i_old];

            user.RemoveAt(i_old);
            user.Insert(i_new, tmp);

            Users = new List<UsersClass>(user);

            fillUsers();

            listBoxUsers.SelectedIndex = i_new;
        }
        #endregion

        #region Catergories
        private void fillCategories()
        {
            checkedListCategories.Items.Clear();

            for (int i = 0; i < Categories.Count; i++)
                checkedListCategories.Items.Add(((CategoryClass)Categories[i]).Name, ((CategoryClass)Categories[i]).UsedForNotfication);

            buttonCategoriesUp.Enabled = false;
            buttonCategoriesDown.Enabled = false;
        }

        private void buttonCategoriesAdd_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            List<string> c = new List<string>();
            foreach (CategoryClass cc in Categories)
                c.Add(cc.Name);
            PropertiesAdd uAdd = new PropertiesAdd("Categories", (string[])c.ToArray(), 15);
            if (uAdd.ShowDialog() == DialogResult.OK)
            {
                if (checkedListCategories.SelectedIndex != -1)
                    Categories.Insert(checkedListCategories.SelectedIndex, new CategoryClass(uAdd.result, false, true));
                else
                    Categories.Add(new CategoryClass(uAdd.result, false, true));

                fillCategories();
            }
        }
        private void buttonCatergoriesDelete_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            string tcategorie = Categories[checkedListCategories.SelectedIndex].Name;

            List<LaborLog.CategoryClass> catergories = new List<LaborLog.CategoryClass>(Categories);

            bool existent = false;

            for (int i = 0; i < labLog.InfoEntryItems.Length; i++)
            {
                for (int k = 0; k < catergories.Count; k++)
                {
                    if (tcategorie == labLog.InfoEntryItems[i].Category.Name)
                    {
                        existent = true;
                        break;
                    }
                }
                if (existent)
                    break;
            }

            if (existent)
                catergories[checkedListCategories.SelectedIndex].Active = false;
            else
                catergories.RemoveAt(checkedListCategories.SelectedIndex);

            Categories = new List<CategoryClass>(catergories);

            buttonCatergoriesDelete.Enabled = false;
            buttonCategoriesUp.Enabled = false;
            buttonCategoriesDown.Enabled = false;

            fillCategories();
        }

        private void checkedListCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListCategories.SelectedIndex != -1)
            {
                buttonCatergoriesDelete.Enabled = true;

                if (checkedListCategories.SelectedIndex > 0 && Categories.Count > 1)
                    buttonCategoriesUp.Enabled = true;
                else
                    buttonCategoriesUp.Enabled = false;

                if (checkedListCategories.SelectedIndex < (Categories.Count - 1) && Categories.Count > 1)
                    buttonCategoriesDown.Enabled = true;
                else
                    buttonCategoriesDown.Enabled = false;
            }
        }
        private void checkedListCategories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (suppress)
                return;

            PropertiesChanged = true;

            Categories[e.Index].UsedForNotfication = Convert.ToBoolean(e.NewValue);
        }

        private void buttonCategoriesUp_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            int i_old = checkedListCategories.SelectedIndex;
            int i_new = checkedListCategories.SelectedIndex - 1;

            CategoryClass tmp = Categories[i_old];

            Categories.RemoveAt(i_old);
            Categories.Insert(i_new, tmp);

            fillCategories();

            checkedListCategories.SelectedIndex = i_new;
        }
        private void buttonCategoriesDown_Click(object sender, EventArgs e)
        {
            PropertiesChanged = true;

            int i_old = checkedListCategories.SelectedIndex;
            int i_new = checkedListCategories.SelectedIndex + 1;

            CategoryClass tmp = Categories[i_old];

            Categories.RemoveAt(i_old);
            Categories.Insert(i_new, tmp);

            fillCategories();

            checkedListCategories.SelectedIndex = i_new;
        }
        #endregion

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            SelectionMode selectionMode = listBox.SelectionMode;

            MyListBoxItem item = listBox.Items[e.Index] as MyListBoxItem; // Get the current item and cast it to MyListBoxItem
            if (item != null)
            {
                e.DrawBackground();

                if (selectionMode == SelectionMode.One)
                {
                    for (int i = 0; i < listBox.Items.Count; i++)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), listBox.GetItemRectangle(i));
                        MyListBoxItem titem = listBox.Items[i] as MyListBoxItem;
                        e.Graphics.DrawString( // Draw the appropriate text in the ListBox
                            titem.Message, // The message linked to the item
                            listBox.Font, // Take the font from the listbox
                            new SolidBrush(titem.ItemColor), // Set the color 
                            0, // X pixel coordinate
                            i * listBox.ItemHeight // Y pixel coordinate.  Multiply the index by the ItemHeight defined in the listbox.
                            );
                    }
                }

                if (selectionMode != SelectionMode.None)
                {
                    if (listBox.GetSelected(e.Index))
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), e.Bounds);
                    else
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                }
                e.Graphics.DrawString( // Draw the appropriate text in the ListBox
                    item.Message, // The message linked to the item
                    listBox.Font, // Take the font from the listbox
                    new SolidBrush(item.ItemColor), // Set the color 
                    0, // X pixel coordinate
                    e.Index * listBox.ItemHeight // Y pixel coordinate.  Multiply the index by the ItemHeight defined in the listbox.
                );
            }
            else
            {
                // The item isn't a MyListBoxItem, do something about it
            }
        }

        public class MyListBoxItem
        {
            public MyListBoxItem(Color c, string m)
            {
                ItemColor = c;
                Message = m;
            }
            public Color ItemColor { get; set; }
            public string Message { get; set; }
        }

        #region Log File Path
        private void buttonChangePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Log File| *.xml";
            sfd.InitialDirectory = Path.GetDirectoryName(logPath);
            sfd.FileName = Path.GetFileNameWithoutExtension(logPath);
            sfd.OverwritePrompt = false;
            // initial directory 

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                PropertiesChanged = true;

                logPath = sfd.FileName;
                textBoxPath.Text = Path.GetFileName(logPath);

                if (File.Exists(logPath))
                {
                    try
                    {
                        labLog = LabLog.deserialisieren(logPath);

                        if (labLog.metaInformation.AllUsers != null)
                            Users = new List<UsersClass>(labLog.metaInformation.AllUsers);

                        if (labLog.metaInformation.Categories != null)
                            Categories = new List<CategoryClass>(labLog.metaInformation.Categories);

                        setPassWordProperties();

                        string mailPropertiesPath = Path.GetDirectoryName(logPath) + Path.DirectorySeparatorChar +
                           Path.GetFileNameWithoutExtension(logPath) + "-Notification" + Path.GetExtension(logPath);
                        if (File.Exists(mailPropertiesPath))
                            mailProperties = eMail.MailProperties.deserialisieren(mailPropertiesPath);
                        setMailproperties();
                    }
                    catch
                    {
                        labLog = new LabLog();
                        Users = new List<UsersClass>();
                        Categories = new List<CategoryClass>(labLog.metaInformation.Categories);
                    }
                }

                fillUsers();
                fillCategories();
            }
        }
        #endregion

        private void checkBoxNotification_CheckedChanged(object sender, EventArgs e)
        {
            if (suppress)
                return;

            PropertiesChanged = true;

            if (((CheckBox)sender).Checked)
            {
                userControlMailProperties1.Enabled = true;
                if (mailProperties == null)
                    mailProperties = new eMail.MailProperties();
                mailProperties.Used = true;
            }
            else
            {
                userControlMailProperties1.Enabled = false;
                mailProperties.Used = false;
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxShowPassword.Checked)
                textBoxUserPassword.PasswordChar = '*';
            else
                textBoxUserPassword.PasswordChar = '\0';
            textBoxUserPassword.Focus();
        }

        private void checkBoxUsePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (suppress)
                return;

            PropertiesChanged = true;

            if (labLog != null)
                labLog.metaInformation.UsePassword = checkBoxUsePassword.Checked;
            if (((CheckBox)sender).Checked)
            {
                labelPassword.Enabled = true;
                textBoxUserPassword.Enabled = true;
                checkBoxShowPassword.Enabled = true;
            }
            else
            {
                labelPassword.Enabled = false;
                textBoxUserPassword.Enabled = false;
                checkBoxShowPassword.Enabled = false;
            }
        }
    }
}
