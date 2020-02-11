using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using System.Net.Mail;

namespace LaborLog
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            propertiesPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "properties.ini";

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("de");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            Cinfo = CultureInfo.CurrentCulture;

            InitializeComponent();

            // Clock give always actual time and date
            Clock = new Timer();
            Clock.Interval = 1000;
            Clock.Start();
            Clock.Tick += new EventHandler(Timer_Tick);

            // set needed properties
            if (File.Exists(propertiesPath))
            {
                StreamReader reader = new StreamReader(new FileStream(propertiesPath, FileMode.Open));
                logPath = reader.ReadLine();
                reader.Close();
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            if (File.Exists(logPath))
            {
                labLog = LabLog.deserialisieren(logPath);

                mailPropertiesPath = Path.GetDirectoryName(logPath) + Path.DirectorySeparatorChar +
                    Path.GetFileNameWithoutExtension(logPath) + "-Notification" + Path.GetExtension(logPath);

                if (File.Exists(mailPropertiesPath))
                    mailProperties = eMail.MailProperties.deserialisieren(mailPropertiesPath);

                this.Text = Path.GetFileNameWithoutExtension(logPath);

                setUserbuttons();
                createEntryForms();
                setSendMailButtonProperties();
            }
            else
            {
                this.Visible = true;
                this.Refresh();

                MessageBox.Show("No Log file selected or exists!",
                    "LaborLog", MessageBoxButtons.OK, MessageBoxIcon.Error);

                setProps();
            }
        }

        #region Public Variables
        private NumberFormatInfo NumberFormatEN = new CultureInfo("en-US", false).NumberFormat;

        private string propertiesPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "properties.ini";

        public eMail.MailProperties mailProperties = null;
        public string mailPropertiesPath = null;

        private LabLog labLog;
        private List<Entry> labLogNewEntries = null;

        private bool blockPendingEntries = false;
        private System.Timers.Timer timerpendingEntries = null;
        private List<PendingEntry> pendingEntries = null;
        private class PendingEntry
        {
            public int index = -1;
            public DateTime entryTime = new DateTime();

            public PendingEntry(int index, DateTime entryTime)
            {
                this.index = index;
                this.entryTime = entryTime;
            }
        }

        private System.Timers.Timer timerpendingMails = null;
        private List<PendingMails> pendingMails = null;
        private class PendingMails
        {
            public eMail.MailProperties mailProperties = null;
            public string Subject = null;
            public string Body = null;

            public PendingMails()
            {

            }

            public PendingMails(eMail.MailProperties mailProperties, string Subject, string Body)
            {
                this.mailProperties = mailProperties;
                this.Subject = Subject;
                this.Body = Body;
            }
        }

        private string logPath = null;
        private CultureInfo Cinfo;

        private Button[] UserButtons;

        private UserControl_Entry[] ucShow;
        private UserControl_Entry[] ucShowNew;

        /// <summary>
        /// Indexnumber of the active entry. 
        /// </summary>
        private int infoNewIndexActive = 0;
        #endregion

        private void MainWindow_ResizeBegin(object sender, EventArgs e)
        {
            if (labLogNewEntries != null)
                for (int i = 0; i < labLogNewEntries.Count; i++)
                    labLogNewEntries[i].InfoString = ucShowNew[i].textBoxInfo.Lines;
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            // suppress redraw if ninimized
            if (panelEntries.Height != 0 || panelEntries.Width != 0)
            {
                createEntryForms();
                setUserbuttons();
            }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (labLog != null)
            {
                if (labLog.metaInformation.UsePassword && labLog.metaInformation.Password != null)
                {
                    PasswordEntry pw = new PasswordEntry(labLog.metaInformation.Password);
                    if (pw.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        e.Cancel = true;
                }
                if (!e.Cancel)
                {
                    if (pendingEntries != null)
                        createMails();

                    if (labLogNewEntries != null)
                    {
                        if (MessageBox.Show("At least one session is open. Should be the saved the session?",
                            "Laborlog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            for (int j = 0; j < labLogNewEntries.Count; j++)
                            {
                                int selectedIndex = ucShowNew[j].comboBoxCategories.SelectedIndex;

                                CategoryClass cc = null;
                                if (ucShowNew[infoNewIndexActive].comboBoxCategories.SelectedIndex != -1)
                                {
                                    cc = labLog.metaInformation.Categories[ucShowNew[infoNewIndexActive].comboBoxCategories.SelectedIndex];
                                }
                                else
                                {
                                    CategorySelection CategorySelection = new CategorySelection(j + 1, labLog.metaInformation.Categories);
                                    this.Enabled = false;
                                    if (CategorySelection.ShowDialog() == DialogResult.OK)
                                        cc = labLog.metaInformation.Categories[CategorySelection.CategoryIndex];
                                    this.Enabled = true;
                                }

                                Entry _entry = new Entry(labLogNewEntries[j].StartTime,
                                                         (DateTime.Now - DateTime.Parse(labLogNewEntries[j].StartTime)).ToString(),
                                                         cc,
                                                         ucShowNew[j].textBoxUsers.Lines,
                                                         ucShowNew[j].textBoxInfo.Lines);

                                LabLog.insertEntry(ref labLog, ref _entry, false);

                                serialization();
                            }
                            labLogNewEntries = null;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void setUserbuttons()
        {
            List<string> activatedUsers = new List<string>();
            if (UserButtons != null)
            {
                for (int i = 0; i < UserButtons.Length; i++)
                    if (UserButtons[i].FlatStyle == FlatStyle.Flat)
                        activatedUsers.Add(UserButtons[i].Text);

                this.panelUsers.Controls.Clear();
            }
            if (labLog != null && labLog.metaInformation.AllUsers != null)
            {
                List<string> users = new List<string>();
                foreach (LaborLog.UsersClass cc in labLog.metaInformation.AllUsers)
                    if (cc.Active)
                        users.Add(cc.Name);

                UserButtons = new Button[users.Count];

                int width = panelUsers.Width;
                if (users.Count * 30 > panelUsers.Height)
                    width = panelUsers.Width - 15;


                for (int i = 0; i < users.Count; i++)
                {
                    UserButtons[i] = new Button();
                    UserButtons[i].Size = new Size(width - UserButtons[i].Margin.Horizontal, 30);
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
            if (activatedUsers.Count != 0 && UserButtons != null && UserButtons.Length > 0)
            {
                for (int i = 0; i < activatedUsers.Count; i++)
                {
                    for (int j = 0; j < UserButtons.Length; j++)
                    {
                        if (activatedUsers[i] == UserButtons[j].Text)
                        {
                            UserButtons[j].FlatStyle = FlatStyle.Flat;
                            UserButtons[j].BackColor = Color.Red;
                            UserButtons[j].ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        #region Clock given the actual time and date
        Timer Clock;
        public void Timer_Tick(object sender, EventArgs eArgs)
        {
            if (sender == Clock)
            {
                DateTime dt = DateTime.Now;
                labelTime.Text = dt.ToString("HH':'mm':'ss");
                labelDate.Text = dt.ToString("ddd', 'dd'. 'MMM' 'yyyy", NumberFormatEN);
                labelDoY.Text = dt.DayOfYear.ToString("000");
                labelWoY.Text = Cinfo.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString("00");
                labelmJD.Text = DT_to_JulDay(DateTime.Now).ToString("00000.0000000000", NumberFormatEN);

                if (labLogNewEntries != null)
                {
                    for (int i = 0; i < labLogNewEntries.Count; i++)
                    {
                        TimeSpan sp = (DateTime.Now - DateTime.Parse(labLogNewEntries[i].StartTime));
                        sp = new TimeSpan(sp.Days, sp.Hours, sp.Minutes, sp.Seconds);
                        ucShowNew[i].labelDuration.Text = sp.ToString();
                    }
                }
            }
        }

        private static double DT_to_JulDay(DateTime dt)
        {
            double Y = (double)dt.Year;
            double M = (double)dt.Month;
            double H = (dt - dt.Date).TotalDays;

            if (M <= 2)
            {
                Y -= 1;
                M += 12;
            }
            double A = Math.Truncate((double)Y / 100d);
            double B = 2 - A + Math.Truncate(A / 4d);

            double tmp = Math.Truncate(365.25f * (Y + 4716d)) + Math.Truncate(30.6001d * (M + 1)) + dt.Day + H + B - 1524.5d;
            double dJD = 2400000.5d;
            return tmp - dJD;
        }
        #endregion

        // start/modify new session
        private void UserButtons_click(object sender, EventArgs e)
        {
            Button uB = (Button)sender;

            if (labLogNewEntries == null)
            {
                labLogNewEntries = new List<Entry>();

                buttonAddSession.Visible = true;

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

                List<string> tmpUsersList = new List<string>();
                for (int i = 0; i < UserButtons.Length; i++)
                    if (UserButtons[i].BackColor == Color.Red)
                        tmpUsersList.Add(UserButtons[i].Text);

                //
                CategoryClass c = null;
                if (mailProperties != null &&
                    mailProperties.Used)
                {
                    c = null;
                }
                else
                {
                    c = labLog.metaInformation.Categories[0];
                }
                labLogNewEntries.Add(new Entry(DateTime.Now.ToString("o"),
                                               "",
                                               c,
                                               (string[])tmpUsersList.ToArray(),
                                               new string[1] { "<everthing all right>" }));
                createEntryForms();
                ucShowNew[infoNewIndexActive].textBoxInfo.Focus();
            }
            else
            {
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

                List<string> tmpUsersList = new List<string>();
                for (int i = 0; i < UserButtons.Length; i++)
                    if (UserButtons[i].BackColor == Color.Red)
                        tmpUsersList.Add(UserButtons[i].Text);

                if (tmpUsersList.Count == 0)
                {
                    ucShowNew[infoNewIndexActive].textBoxUsers.ForeColor = Color.Red;
                    ucShowNew[infoNewIndexActive].textBoxUsers.Text = "<Add User>";
                    ucShowNew[infoNewIndexActive].buttonEdit.Enabled = false;
                    buttonFinish.Enabled = false;
                }
                else
                {
                    ucShowNew[infoNewIndexActive].textBoxUsers.ForeColor = System.Drawing.SystemColors.ControlText;
                    ucShowNew[infoNewIndexActive].textBoxUsers.Lines = (string[])tmpUsersList.ToArray();
                    ucShowNew[infoNewIndexActive].buttonEdit.Enabled = true;
                    buttonFinish.Enabled = true;
                }
                ucShowNew[infoNewIndexActive].textBoxInfo.Focus();
            }
        }
        private void buttonAddSession_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < labLogNewEntries.Count; i++)
                labLogNewEntries[i].InfoString = ucShowNew[i].textBoxInfo.Lines;

            labLogNewEntries.Add(new Entry(DateTime.Now.ToString("o"),
                                                     "",
                                                     labLog.metaInformation.Categories[0],
                                                     null,
                                                     new string[1] { "<alles I.O.>" }));

            infoNewIndexActive = labLogNewEntries.Count - 1;

            for (int i = 0; i < UserButtons.Length; i++)
            {
                UserButtons[i].FlatStyle = FlatStyle.Standard;
                Color color;
                if (Convert.ToInt32(UserButtons[i].Name) % 2 == 0)
                    color = Color.GhostWhite;
                else
                    color = Color.Gainsboro;
                UserButtons[i].BackColor = color;
                UserButtons[i].ForeColor = System.Drawing.SystemColors.ControlText;
            }

            createEntryForms();
        }

        // create Entry forms
        private void createEntryForms()
        {
            if (labLog == null && labLogNewEntries == null)
                return;

            if (ucShow != null || ucShowNew != null)
                panelEntries.Controls.Clear();

            int newEntry = 0;
            if (labLogNewEntries != null)
            {
                newEntry += labLogNewEntries.Count;
                buttonProperties.Enabled = false;
            }
            else
                buttonProperties.Enabled = true;

            int begin = 0;
            int l1 = 0;
            if (labLog != null)
            {
                l1 = labLog.InfoEntryItems.Length;

                if (l1 >= 25)
                {
                    begin = l1 - 25;
                    l1 = 25;
                }
                ucShow = new UserControl_Entry[l1];
            }

            int dy = (l1 - 1) + newEntry;
            if (labLog != null)
            {
                for (int i = begin; i < l1 + begin; i++)
                    fillUcShowOld(ref ucShow[i - begin], i - begin, i, ref dy);

                this.panelEntries.Controls.AddRange(ucShow);
            }

            if (labLogNewEntries != null)
            {
                ucShowNew = new UserControl_Entry[labLogNewEntries.Count];

                for (int i = 0; i < labLogNewEntries.Count; i++)
                {
                    fillUcShowActiveSessions(ref ucShowNew[i], i, ref dy);
                    ucShowNew[i].EntryChanged += new UserControl_Entry.EntryChangedHandler(MainWindow_EntryChanged);
                }
                this.panelEntries.Controls.AddRange(ucShowNew);

                ucShowNew[infoNewIndexActive].BackColor = Color.Orange;
                ucShowNew[infoNewIndexActive].textBoxInfo.Focus();

                labelActiveSessions.Text = "Active Sessions: " + labLogNewEntries.Count;
            }
            else
                labelActiveSessions.Text = "";

        }

        private void MainWindow_EntryChanged(object sender, UserControl_Entry.EntryChangedEventArgs e)
        {
            UserControl_Entry us = (UserControl_Entry)sender;
            int index = int.Parse(us.Name);

            if (e.Status == UserControl_Entry.EntryChangedEvent.User)
            {
                labLogNewEntries[index].Users = ucShowNew[infoNewIndexActive].textBoxUsers.Lines;
            }
            else
                if (e.Status == UserControl_Entry.EntryChangedEvent.Category)
                {
                    labLogNewEntries[index].Category = labLog.metaInformation.Categories[ucShowNew[infoNewIndexActive].comboBoxCategories.SelectedIndex];
                }
                else
                    if (e.Status == UserControl_Entry.EntryChangedEvent.Information)
                    {
                        labLogNewEntries[index].InfoString = us.textBoxInfo.Lines;
                    }
        }

        private void fillUcShowOld(ref UserControl_Entry _ucShow, int index, int i, ref int dy)
        {
            // Generall parameter
            _ucShow = new UserControl_Entry(false);
            _ucShow.Name = "labLog_" + i.ToString();
            _ucShow.Size = new Size(panelEntries.Width - 30, _ucShow.Size.Height);
            _ucShow.Location = new Point(5, (5 + (dy--) * (_ucShow.Size.Height + 5)));

            // continous counter for entry
            _ucShow.textBoxCount.Text = (i + 1).ToString("000");

            // edit button handling
            _ucShow.buttonEdit.Click += buttonEdit_Click;
            _ucShow.buttonEdit.Name = i.ToString();

            // time information
            DateTime start = DateTime.Parse(labLog.InfoEntryItems[i].StartTime);
            TimeSpan duration = TimeSpan.Parse(labLog.InfoEntryItems[i].Duration);
            _ucShow.labelStart.Text = start.ToString();
            _ucShow.labelEnd.Text = start.Add(duration).ToString();
            _ucShow.labelDuration.Text = (new TimeSpan(duration.Days, duration.Hours, duration.Minutes, duration.Seconds)).ToString();

            // category information
            if (labLog.InfoEntryItems[i].Category.UsedForNotfication)
                _ucShow.textBoxCategories.BackColor = Color.Red;
            else
                _ucShow.textBoxCategories.BackColor = Color.Green;

            _ucShow.textBoxCategories.Text = labLog.InfoEntryItems[i].Category.Name;

            // user information
            if (labLog.InfoEntryItems[i].Users != null)
                _ucShow.textBoxUsers.Lines = labLog.InfoEntryItems[i].Users;

            // information about the task
            _ucShow.textBoxInfo.Name = i.ToString();
            if (labLog.InfoEntryItems[i].InfoString != null)
                _ucShow.textBoxInfo.Lines = labLog.InfoEntryItems[i].InfoString;
        }
        private void fillUcShowActiveSessions(ref UserControl_Entry ucShowNew, int i, ref int dy)
        {
            ucShowNew = new UserControl_Entry(true);
            ucShowNew.Size = new Size(panelEntries.Width - 30, ucShowNew.Size.Height);
            ucShowNew.Location = new Point(5, (5 + (dy--) * (ucShowNew.Size.Height + 5)));
            ucShowNew.Name = i.ToString();

            ucShowNew.textBoxCount.Text = (i + 1).ToString("000");
            ucShowNew.textBoxCount.Name = i.ToString();
            ucShowNew.textBoxCount.Click += ucShowNew_textBoxCount_Click;

            ucShowNew.buttonEdit.Name = i.ToString();
            ucShowNew.buttonEdit.Click += ucShowNew_buttonFinish_Click;

            ucShowNew.panelTime.Name = i.ToString();
            ucShowNew.panelTime.Click += ucShowNew_panelTime_Click;

            DateTime _start = DateTime.Parse(labLogNewEntries[i].StartTime);
            ucShowNew.labelStart.Text = _start.ToString();
            ucShowNew.labelStart.Name = i.ToString();
            ucShowNew.labelStart.Click += ucShowNew_labelStart_Click;

            ucShowNew.labelEnd.Name = i.ToString();
            ucShowNew.labelEnd.Click += ucShowNew_labelEnd_Click;

            ucShowNew.labelDuration.Name = i.ToString();
            ucShowNew.labelDuration.Click += ucShowNew_labelDuration_Click;

            ucShowNew.comboBoxCategories.Name = i.ToString();
            int selectedIndex = -1;
            for (int j = 0; j < labLog.metaInformation.Categories.Length; j++)
            {
                ucShowNew.comboBoxCategories.Items.Add(labLog.metaInformation.Categories[j].Name);
                if (labLogNewEntries[i].Category != null &&
                    labLogNewEntries[i].Category.Name == labLog.metaInformation.Categories[j].Name)
                    selectedIndex = j;
            }
            ucShowNew.comboBoxCategories.SelectedIndex = selectedIndex;

            ucShowNew.comboBoxCategories.SelectedIndexChanged += new EventHandler(comboBoxCategories_SelectedIndexChanged);
            ucShowNew.comboBoxCategories.Click += ucShowNew_comboBoxCategories_Click;
            ucShowNew.comboBoxCategories.KeyPress += ucShowNew_comboBoxCategories_KeyPress;

            ucShowNew.textBoxUsers.Name = i.ToString();
            ucShowNew.textBoxUsers.KeyPress += ucShowNew_textBoxUsers_KeyPress;
            ucShowNew.textBoxUsers.Click += ucShowNew_textBoxUsers_Click;
            if (labLogNewEntries[i].Users != null)
            {
                ucShowNew.textBoxUsers.ForeColor = System.Drawing.SystemColors.ControlText;
                ucShowNew.textBoxUsers.Lines = labLogNewEntries[i].Users;
                ucShowNew.buttonEdit.Enabled = true;
                buttonFinish.Enabled = true;
            }
            else
            {
                ucShowNew.textBoxUsers.ForeColor = Color.Red;
                ucShowNew.textBoxUsers.Text = "<Add User>";
                ucShowNew.buttonEdit.Enabled = false;
                buttonFinish.Enabled = false;
            }

            if (ucShowNew.comboBoxCategories.SelectedIndex != -1)
            {
                if (labLog.metaInformation.Categories[ucShowNew.comboBoxCategories.SelectedIndex].UsedForNotfication)
                    ucShowNew.textBoxInfo.ForeColor = Color.Red;
                else
                    ucShowNew.textBoxInfo.ForeColor = Color.Green;
            }
            if (labLogNewEntries[i].InfoString != null)
                ucShowNew.textBoxInfo.Lines = labLogNewEntries[i].InfoString;
            ucShowNew.textBoxInfo.Name = i.ToString();
            ucShowNew.textBoxInfo.Click += ucShowNew_textBoxInfo_Click;

            ucShowNew.Click += ucShowNew_Click;
        }

        // change category of active session
        void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            int iName = int.Parse(cb.Name);
            int index = cb.SelectedIndex;

            if (labLog.metaInformation.Categories[index].UsedForNotfication)
                ucShowNew[iName].textBoxInfo.ForeColor = Color.Red;
            else
                ucShowNew[iName].textBoxInfo.ForeColor = Color.Green;
        }

        // Edits an old active entry
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (timerpendingEntries != null)
                timerpendingEntries.Enabled = false;

            Button bt = (Button)sender;
            int ID = Convert.ToInt32(bt.Name);

            // Form
            if (!labLog.InfoEntryItems[ID].edit)
            {
                if (labLog.metaInformation.UsePassword)
                {
                    PasswordEntry pw = new PasswordEntry(labLog.metaInformation.Password);
                    if (pw.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;
                }
            }
            else
            {
                blockPendingEntries = true;
            }

            List<string> users = new List<string>();
            foreach (LaborLog.UsersClass cc in labLog.metaInformation.AllUsers)
                if (cc.Active)
                    users.Add(cc.Name);

            EditAddEntry edit = new EditAddEntry(labLog.InfoEntryItems[ID], users.ToArray(), labLog.metaInformation.Categories);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                Entry _entry = edit.entry;
                _entry.modified = true;

                if (labLog.InfoEntryItems.Length > 1)
                    labLog.RemoveColoumnEntryAt(ID);
                if (blockPendingEntries)
                {
                    if (pendingEntries != null)
                    {
                        for (int i = 0; i < pendingEntries.Count; i++)
                            if (ID == pendingEntries[i].index)
                            {
                                pendingEntries.RemoveAt(i);
                                break;
                            }
                    }
                }

                // TODO was ist mit anderen einträgen wenn noch welche in der liste sind. Kommt die dann nicht durcheinander
                int index = -1;
                LabLog.insertEntry(ref labLog, ref _entry, false, out index);
                if (pendingEntries == null)
                    pendingEntries = new List<PendingEntry>();

                pendingEntries.Add(new PendingEntry(index, DateTime.Now));
                starttimerpendingEntries();
                serialization();

                createEntryForms();
            }

            // TODO was ist mit REPORT wenn schon mal geschickt --> nochmal wie lösen im System
            if (timerpendingEntries != null)
                timerpendingEntries.Enabled = true;

            blockPendingEntries = false;
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            finishEntry();
        }
        private void ucShowNew_buttonFinish_Click(object sender, EventArgs e)
        {
            if (infoNewIndexActive != Convert.ToInt32(((Button)sender).Name))
            {
                changeActive(infoNewIndexActive, Convert.ToInt32(((Button)sender).Name));
                return;
            }
            finishEntry();

        }

        private void finishEntry()
        {
            if (labLogNewEntries.Count >= 1)
                for (int i = 0; i < labLogNewEntries.Count; i++)
                    labLogNewEntries[i].InfoString = ucShowNew[i].textBoxInfo.Lines;

            CategoryClass cc = null;
            if (ucShowNew[infoNewIndexActive].comboBoxCategories.SelectedIndex != -1)
            {
                cc = labLog.metaInformation.Categories[ucShowNew[infoNewIndexActive].comboBoxCategories.SelectedIndex];
            }
            else
            {
                CategorySelection CategorySelection = new CategorySelection(infoNewIndexActive + 1, labLog.metaInformation.Categories);
                this.Enabled = false;
                if (CategorySelection.ShowDialog() == DialogResult.OK)
                    cc = labLog.metaInformation.Categories[CategorySelection.CategoryIndex];
                this.Enabled = true;
            }

            Entry _entry = new Entry(labLogNewEntries[infoNewIndexActive].StartTime,
                                     (DateTime.Now - DateTime.Parse(labLogNewEntries[infoNewIndexActive].StartTime)).ToString(),
                                     cc,
                                     ucShowNew[infoNewIndexActive].textBoxUsers.Lines,
                                     ucShowNew[infoNewIndexActive].textBoxInfo.Lines);
            _entry.edit = true;

            // inser entry at right position
            int entryNumber = -1;
            LabLog.insertEntry(ref labLog, ref _entry, true, out entryNumber);

            labLogNewEntries.RemoveAt(infoNewIndexActive);
            infoNewIndexActive = 0;

            if (labLogNewEntries.Count == 0)
            {
                labLogNewEntries = null;
                buttonAddSession.Visible = false;
            }

            resetUserButtons();

            // start sending mail if needed
            if (mailProperties != null && mailProperties.Used)
            {
                if (pendingEntries == null)
                    pendingEntries = new List<PendingEntry>();

                pendingEntries.Add(new PendingEntry(entryNumber, DateTime.Now));
                starttimerpendingEntries();
            }

            serialization();

            buttonFinish.Enabled = false;

            createEntryForms();
        }
        private void resetUserButtons()
        {
            for (int i = 0; i < UserButtons.Length; i++)
            {
                UserButtons[i].FlatStyle = FlatStyle.Standard;
                Color color;
                if (Convert.ToInt32(UserButtons[i].Name) % 2 == 0)
                    color = Color.GhostWhite;
                else
                    color = Color.Gainsboro;
                UserButtons[i].BackColor = color;
                UserButtons[i].ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void starttimerpendingEntries()
        {
            timerpendingEntries = new System.Timers.Timer(5000);
            timerpendingEntries.AutoReset = true;
            timerpendingEntries.Elapsed += new System.Timers.ElapsedEventHandler(timerpendingEntries_Elapsed);
            timerpendingEntries.Start();
        }
        void timerpendingEntries_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (blockPendingEntries)
                return;

            createMails();

            if (pendingEntries.Count == 0)
            {
                pendingEntries = null;
                timerpendingEntries.Stop();
                timerpendingEntries = null;
            }
        }

        private void createMails()
        {
            for (int i = 0; i < pendingEntries.Count; i++)
            {
                if ((DateTime.Now - pendingEntries[i].entryTime) >= new TimeSpan(0, 1, 0))
                {
                    labLog.InfoEntryItems[(int)pendingEntries[i].index].edit = false;

                    if (labLog.InfoEntryItems[(int)pendingEntries[i].index].Category.UsedForNotfication)
                    {
                        labLog.InfoEntryItems[(int)pendingEntries[i].index].NotificationSendAt = DateTime.Now.ToString("o");

                        string Subject = "Notification from " + DateTime.Parse(labLog.InfoEntryItems[(int)pendingEntries[i].index].StartTime).ToString("dd.MM.yyyy");
                        if (labLog.InfoEntryItems[(int)pendingEntries[i].index].modified)
                            Subject += " - modified";

                        string Body = null;
                        Body += "Category: " + labLog.InfoEntryItems[(int)pendingEntries[i].index].Category.Name.ToString() + Environment.NewLine;
                        Body += "  Begin : " + DateTime.Parse(labLog.InfoEntryItems[(int)pendingEntries[i].index].StartTime).ToString() + Environment.NewLine;
                        Body += "  End   : " +
                                        DateTime.Parse(labLog.InfoEntryItems[(int)pendingEntries[i].index].StartTime).Add(
                                            TimeSpan.Parse(labLog.InfoEntryItems[(int)pendingEntries[i].index].Duration)).ToString() + Environment.NewLine;
                        Body += Environment.NewLine;
                        Body += "Participant: " + labLog.InfoEntryItems[(int)pendingEntries[i].index].Users[0] + Environment.NewLine;
                        for (int j = 1; j < labLog.InfoEntryItems[(int)pendingEntries[i].index].Users.Length; j++)
                            Body += "             " + labLog.InfoEntryItems[(int)pendingEntries[i].index].Users[j] + Environment.NewLine;
                        Body += Environment.NewLine;
                        Body += "Message: " + Environment.NewLine;
                        for (int j = 0; j < labLog.InfoEntryItems[(int)pendingEntries[i].index].InfoString.Length; j++)
                            Body += "   " + labLog.InfoEntryItems[(int)pendingEntries[i].index].InfoString[j] + Environment.NewLine;

                        pendingEntries.RemoveAt(i);

                        if (!eMail.eMail.Send(mailProperties, Subject, Body, false))
                        {
                            if (pendingMails == null)
                                pendingMails = new List<PendingMails>();
                            pendingMails.Add(new PendingMails(mailProperties, Subject, Body));

                            labelPendingMails.Text = "Notifications are pending!" + Environment.NewLine +
                                                     "Check connectivity and parameters!";

                            timerpendingMails = new System.Timers.Timer(3600000);
                            timerpendingMails.AutoReset = true;
                            timerpendingMails.Elapsed += new System.Timers.ElapsedEventHandler(timerpendingMails_Elapsed);
                            timerpendingMails.Start();
                        }
                    }
                    else
                        pendingEntries.RemoveAt(i);
                }
            }
        }

        void timerpendingMails_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < pendingMails.Count; i++)
            {
                if (eMail.eMail.Send(pendingMails[i].mailProperties, pendingMails[i].Subject, pendingMails[i].Body, false))
                    pendingMails.RemoveAt(i);
            }

            if (pendingMails.Count == 0)
            {
                labelPendingMails.Text = "";
                pendingMails = null;
            }
            if (pendingMails == null)
            {
                timerpendingMails.Stop();
                timerpendingMails = null;
            }
        }

        #region Properties settings

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            if (labLog != null && labLog.metaInformation != null)
            {
                List<string> users = new List<string>();
                if (labLog.metaInformation.AllUsers != null)
                    foreach (LaborLog.UsersClass cc in labLog.metaInformation.AllUsers)
                        if (cc.Active)
                            users.Add(cc.Name);

                PropertiesAdd uAdd = new PropertiesAdd("User", users.ToArray(), 15);
                if (uAdd.ShowDialog() == DialogResult.OK)
                {
                    List<LaborLog.UsersClass> u = new List<LaborLog.UsersClass>(labLog.metaInformation.AllUsers);

                    bool existent = false;
                    for (int i = 0; i < labLog.metaInformation.AllUsers.Length; i++)
                    {
                        if (labLog.metaInformation.AllUsers[i].Name == uAdd.result)
                        {
                            labLog.metaInformation.AllUsers[i].Active = true;
                            existent = true;
                        }
                    }
                    if (!existent)
                    {
                        u.Add(new UsersClass(uAdd.result, true));
                        labLog.metaInformation.AllUsers = u.ToArray();
                    }
                    setUserbuttons();

                    serialization();
                }
            }
        }

        private void buttonProperties_Click(object sender, EventArgs e)
        {
            if (labLog != null && labLog.metaInformation.UsePassword && labLog.metaInformation.Password != null)
            {
                PasswordEntry pw = new PasswordEntry(labLog.metaInformation.Password);
                if (pw.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
            }

            setProps();
        }

        private void setProps()
        {
            string logfile = logPath;

            PropertiesSettings props = new PropertiesSettings(logPath, labLog, mailProperties, labLogNewEntries != null);
            if (props.ShowDialog() == DialogResult.OK)
            {
                logPath = props.logPath;

                if (logPath != null)
                {
                    if (logfile != logPath)
                    {
                        if (File.Exists(logPath))
                            labLog = LabLog.deserialisieren(logPath);

                        // ini datei setzen wo xml zu finden ist
                        StreamWriter wr = new StreamWriter(new FileStream(propertiesPath, FileMode.Create));
                        wr.WriteLine(logPath);
                        wr.Close();
                    }
                    else
                    {
                        if (!File.Exists(logPath))
                            return;
                    }

                    labLog = props.labLog;

                    this.Text = Path.GetFileNameWithoutExtension(logPath);

                    if (props.mailProperties != null)
                    {
                        mailPropertiesPath = Path.GetDirectoryName(logPath) + Path.DirectorySeparatorChar +
                            Path.GetFileNameWithoutExtension(logPath) + "-Notification" + Path.GetExtension(logPath);

                        mailProperties = props.mailProperties;
                        eMail.MailProperties.serialisieren(mailProperties, mailPropertiesPath);
                    }
                    else
                    {
                        mailPropertiesPath = null;
                        mailProperties = null;
                    }
                    setUserbuttons();
                    createEntryForms();

                    setSendMailButtonProperties();

                    if (props.PropertiesChanged)
                        serialization();
                }
            }
        }
        #endregion

        #region Change active Entry
        private void ucShowNew_panelTime_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((Panel)sender).Name));
        }

        private void ucShowNew_labelStart_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((Label)sender).Name));
        }

        private void ucShowNew_labelEnd_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((Label)sender).Name));
        }

        private void ucShowNew_labelDuration_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((Label)sender).Name));
        }

        private void ucShowNew_comboBoxCategories_Click(object sender, EventArgs e)
        {
            if (infoNewIndexActive != Convert.ToInt32(((ComboBox)sender).Name))
            {
                changeActive(infoNewIndexActive, Convert.ToInt32(((ComboBox)sender).Name));
                return;
            }
        }

        private void ucShowNew_comboBoxCategories_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (infoNewIndexActive != Convert.ToInt32(((ComboBox)sender).Name))
            {
                changeActive(infoNewIndexActive, Convert.ToInt32(((ComboBox)sender).Name));
                return;
            }
            if (e.KeyChar != '\r')
            {
                e.Handled = true;
                ucShowNew[infoNewIndexActive].textBoxInfo.Focus();
                ucShowNew[infoNewIndexActive].textBoxInfo.Select(ucShowNew[infoNewIndexActive].textBoxInfo.Text.Length, 0);
            }
        }

        private void ucShowNew_textBoxUsers_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((TextBox)sender).Name));
        }

        private void ucShowNew_textBoxUsers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ucShowNew_textBoxInfo_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((TextBox)sender).Name));
        }

        private void ucShowNew_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((UserControl_Entry)sender).Name));
        }

        private void ucShowNew_textBoxCount_Click(object sender, EventArgs e)
        {
            changeActive(infoNewIndexActive, Convert.ToInt32(((TextBox)sender).Name));
        }

        private void changeActive(int old_id, int new_id)
        {
            ucShowNew[old_id].BackColor = Color.Gray;
            ucShowNew[new_id].BackColor = Color.Orange;
            infoNewIndexActive = new_id;

            for (int i = 0; i < UserButtons.Length; i++)
            {
                UserButtons[i].FlatStyle = FlatStyle.Standard;
                Color color;
                if (Convert.ToInt32(UserButtons[i].Name) % 2 == 0)
                    color = Color.GhostWhite;
                else
                    color = Color.Gainsboro;
                UserButtons[i].BackColor = color;
                UserButtons[i].ForeColor = System.Drawing.SystemColors.ControlText;
            }

            string[] users = ucShowNew[infoNewIndexActive].textBoxUsers.Lines;
            bool finishactive = false;
            for (int i = 0; i < users.Length; i++)
            {
                for (int j = 0; j < UserButtons.Length; j++)
                {
                    if (users[i] == UserButtons[j].Text)
                    {
                        UserButtons[j].FlatStyle = FlatStyle.Flat;
                        UserButtons[j].BackColor = Color.Red;
                        finishactive = true;
                    }
                }
            }
            if (finishactive)
            {
                ucShowNew[new_id].buttonEdit.Enabled = true;
                buttonFinish.Enabled = true;
            }
            else
            {
                ucShowNew[new_id].buttonEdit.Enabled = false;
                buttonFinish.Enabled = false;
            }

            ucShowNew[new_id].textBoxInfo.Focus();
            ucShowNew[new_id].textBoxInfo.Select(ucShowNew[new_id].textBoxInfo.Text.Length, 0);
        }
        #endregion

        #region e-Mail notification
        private void buttonSendMail_Click(object sender, EventArgs e)
        {
            eMail.SimpleMailSend sendMail = new eMail.SimpleMailSend(mailProperties, true,
                "Notification from " + DateTime.Now.ToLongDateString());
            sendMail.ShowDialog();
        }

        private void setSendMailButtonProperties()
        {
            if (labLog != null && labLog.metaInformation != null &&
               mailProperties != null && mailProperties.Used)
            {
                buttonSendMail.Enabled = true;
            }
            else
            {
                buttonSendMail.Enabled = false;
            }
        }
        #endregion

        // TODO Wenn man hier etwas geändert hat was ist mit mails senden
        private void buttonShowAllEntries_Click(object sender, EventArgs e)
        {
            blockPendingEntries = true;
            ShowAll u = new ShowAll(labLog, false, true);
            u.ShowDialog();

            if (u.ModifiedDataBase)
            {
                labLog = u.LabLog;

                serialization();

                createEntryForms();
            }
            blockPendingEntries = false;
        }

        private bool saving_ = false;
        private void serialization()
        {
            if (saving_)
                return;

            saving_ = true;
            LabLog.serialisieren(labLog, logPath);
            saving_ = false;

        }
    }
}
