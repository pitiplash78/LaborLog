using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LaborLog
{
    public partial class EditAddEntry : Form
    {
        public Entry entry;

        private Button[] UserButtons;
        private string[] users = null;
        private CategoryClass[] categories = null;

        public EditAddEntry(Entry entry, string[] users, CategoryClass[] categories)
        {
            InitializeComponent();

            dTP1.CustomFormat = "HH:mm:ss   dd.MM.yyyy";
            dTP2.CustomFormat = "HH:mm:ss   dd.MM.yyyy";

            this.entry = entry;
            this.users = users;
            this.categories = categories;

            setUserbuttons();
            setCategories();

            if (entry != null)
            {
                this.Text += "Edit Entry";

                // Buttons setzen
                for (int i = 0; i < entry.Users.Length; i++)
                    for (int j = 0; j < UserButtons.Length; j++)
                        if (entry.Users[i] == UserButtons[j].Text)
                        {
                            UserButtons[j].FlatStyle = FlatStyle.Flat;
                            UserButtons[j].BackColor = Color.Red;
                        }

                // Kategorie setzen
                for (int i = 0; i < categories.Length; i++)
                    if (categories[i].Name == entry.Category.Name)
                        comboBoxCategories.SelectedIndex = i;

                // Zeiten setzen
                DateTime dt = DateTime.Parse(entry.StartTime);
                start = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                dTP1.Value = start;
                dt = DateTime.Parse(entry.StartTime).Add(TimeSpan.Parse(entry.Duration));
                end = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                dTP2.Value = end;
                duration = end - start;
                if (duration < new TimeSpan(0))
                    labelDuration.ForeColor = Color.Red;
                else
                    labelDuration.ForeColor = System.Drawing.SystemColors.ControlText;

                labelDuration.Text = duration.ToString();
              
                // Text setzen
                textBoxInfo.Lines = entry.InfoString;

                // aktivierter User?
                bool us = false;
                for (int i = 0; i < UserButtons.Length; i++)
                    if (UserButtons[i].BackColor == Color.Red)
                        us = true;

                if (us)
                    buttonContinue.Enabled = true;
                else
                    buttonContinue.Enabled = false;
            }
            else
            {
                this.Text += "Add Additional Entry";
                entry = new Entry();

                DateTime dt = DateTime.Now;
                start = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                dTP1.Value = start;
                end = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                dTP2.Value = end;
                duration = end - start;
                if (duration < new TimeSpan(0))
                    labelDuration.ForeColor = Color.Red;
                else
                    labelDuration.ForeColor = System.Drawing.SystemColors.ControlText;

                labelDuration.Text = duration.ToString();

                textBoxInfo.Text = "<alles I.O.>";

                buttonContinue.Enabled = false;
            }
        }

        private void setUserbuttons()
        {
            if (UserButtons != null)
                this.panelUsers.Controls.Clear();

            UserButtons = new Button[users.Length];

            int width = 151;
            if (users.Length * 30 < panelUsers.Height)
                width = panelUsers.Width;

            for (int i = 0; i < users.Length; i++)
            {
                UserButtons[i] = new Button();
                UserButtons[i].Size = new Size(width, 30);
                UserButtons[i].Location = new Point(0, i * 30);
                UserButtons[i].TextAlign = ContentAlignment.MiddleRight;
                UserButtons[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                               System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                UserButtons[i].Text = users[i];
                UserButtons[i].Name = i.ToString();
                Color color;
                if (i % 2 == 0)
                    color = Color.GhostWhite;
                else
                    color = Color.Gainsboro;
                UserButtons[i].BackColor = color;
                UserButtons[i].Click += UserButtons_click;

            }
            this.panelUsers.Controls.AddRange(UserButtons);
        }
        private void UserButtons_click(object sender, EventArgs e)
        {
            Button uB = (Button)sender;

            if (uB.FlatStyle == FlatStyle.Flat)
            {
                uB.FlatStyle = FlatStyle.Standard;
                Color color;
                if (Convert.ToInt32(uB.Name) % 2 == 0)
                    color = Color.GhostWhite;
                else
                    color = Color.Gainsboro;
                uB.BackColor = color;
                uB.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            else
            {
                uB.FlatStyle = FlatStyle.Flat;
                uB.BackColor = Color.Red;
                uB.ForeColor = Color.White;
            }

            bool us = false;
            for (int i = 0; i < UserButtons.Length; i++)
                if (UserButtons[i].BackColor == Color.Red)
                    us = true;

            if (us)
                buttonContinue.Enabled = true;
            else
                buttonContinue.Enabled = false;
        }
        private void setCategories()
        {
            for (int i = 0; i < categories.Length; i++)
                this.comboBoxCategories.Items.Add(categories[i].Name);

            this.comboBoxCategories.SelectedIndex = 0;
        }
        private void comboBoxCategories_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                e.Handled = true;
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            //entry updaten
            ArrayList tmpUsersList = new ArrayList();
            for (int i = 0; i < UserButtons.Length; i++)
                if (UserButtons[i].BackColor == Color.Red)
                    tmpUsersList.Add(UserButtons[i].Text);

            string[] tmpUsers = new string[tmpUsersList.Count];
            for (int i = 0; i < tmpUsersList.Count; i++)
                tmpUsers[i] = (string)tmpUsersList[i];

            if (entry == null)
                entry = new Entry();

            entry.Users = tmpUsers;

            entry.Category = categories[comboBoxCategories.SelectedIndex];

            entry.StartTime = start.ToString("o");
            entry.Duration = duration.ToString();
            entry.InfoString = textBoxInfo.Lines;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Time Interval
        private DateTime start;
        private DateTime end;
        private TimeSpan duration;
        private bool suppress = false;
        private void dTP1_ValueChanged(object sender, EventArgs e)
        {
            if (!suppress)
            {
                suppress = true;

                start = dTP1.Value;

                duration = end - start;
                if (duration < new TimeSpan(0))
                {
                    labelDuration.ForeColor = Color.Red;
                    buttonContinue.Enabled = false;
                }
                else
                {
                    labelDuration.ForeColor = System.Drawing.SystemColors.ControlText;
                    buttonContinue.Enabled = true;
                }
                labelDuration.Text = duration.ToString();

                suppress = false;
            }
        }
        private void dTP2_ValueChanged(object sender, EventArgs e)
        {
            if (!suppress)
            {
                suppress = true;

                end = dTP2.Value;

                duration = end - start;
                if (duration < new TimeSpan(0))
                {
                    labelDuration.ForeColor = Color.Red;
                    buttonContinue.Enabled = false;
                }
                else
                {
                    labelDuration.ForeColor = System.Drawing.SystemColors.ControlText;
                    buttonContinue.Enabled = true;
                }
                labelDuration.Text = duration.ToString();

                suppress = false;
            }
        }
        #endregion
    }
}
