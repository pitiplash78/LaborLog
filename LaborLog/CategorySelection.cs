using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaborLog
{
    public partial class CategorySelection : Form
    {
        public string Category { get; private set; }
        public int CategoryIndex { get; private set; }

        internal Button[] bCategories = null;
        internal CategoryClass[] categories = null;

        public CategorySelection(int entrynumber, CategoryClass[] categories)
        {
            this.categories = categories;
            InitializeComponent();

            textBox1.Text = entrynumber.ToString();

            bCategories = new Button[categories.Length];

            this.Height = 66 * categories.Length + 36 +24;
            Panel panel1 = new Panel();
            panel1.BackColor = System.Drawing.SystemColors.Info;
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            panel1.Location = new System.Drawing.Point(12, 36);
            panel1.Size = new System.Drawing.Size(this.Width-24, this.Height- 24 - 24);
            this.Controls.Add(panel1);
            
            for (int i = 0; i < categories.Length; i++)
            {
                Color backGoundColor = Color.Green;

                if (categories[i].UsedForNotfication)
                    backGoundColor = Color.Red;

                bCategories[i] = new Button()
                {
                    Name = i.ToString(),
                    Size = new Size(panel1.Width - 12, 60),
                    Location = new Point(6, 6 + i * 66),
                    Text = categories[i].Name,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    ForeColor = backGoundColor,
                    BackColor = SystemColors.Control,
                    UseVisualStyleBackColor = false,
                };
                bCategories[i].Click += new EventHandler(UserControlCategorySelection_Click);
            }
            panel1.Controls.AddRange(bCategories);
        }

        private void UserControlCategorySelection_Click(object sender, EventArgs e)
        {
            CategoryIndex = int.Parse(((Button)sender).Name);
            Category = categories[CategoryIndex].Name;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
