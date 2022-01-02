using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sky_UI;
using System.IO;

namespace Sky_multi
{
    internal sealed class DVDDialog : SkyForms
    {
        private ComboBox comboBox1;
        private Label label1;
        private Sky_UI.Button button2;
        private Sky_UI.Button button1;
        private readonly Language language;

        internal DVDDialog(Language language) : base()
        {
            this.language = language;
            InitializeComponent();

            this.Location = new Point(Screen.FromControl(this).WorkingArea.Width / 2 - this.Width / 2, Screen.FromControl(this).WorkingArea.Height / 2 - this.Height / 2);
            DriveInfo[] driveInfo = DriveInfo.GetDrives();

            for (int index = 0; index < driveInfo.Length; index++)
            {
                if (driveInfo[index].DriveType == DriveType.CDRom && driveInfo[index].IsReady == true)
                {
                    comboBox1.Items.Add(driveInfo[index].RootDirectory.FullName);
                }
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DVDDialog));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new Sky_UI.Button();
            this.button2 = new Sky_UI.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = FlatStyle.Popup;
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.Location = new System.Drawing.Point(12, 87);
            this.comboBox1.Name = "textBox1";
            this.comboBox1.Size = new System.Drawing.Size(303, 23);
            this.comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 25);
            this.label1.TabIndex = 4;
            if (language == Language.French)
            {
                this.label1.Text = "Choisir le DVD :";
            }
            else
            {
                this.label1.Text = "Choose DVD :";
            }
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Border = false;
            this.button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.borderRadius = 5;
            this.button1.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ID = 0;
            this.button1.Image = null;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Location = new System.Drawing.Point(280, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            if (language == Language.French)
            {
                this.button1.Text = "Ouvrir";
            }
            else
            {
                this.button1.Text = "Open";
            }
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Border = false;
            this.button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.borderRadius = 5;
            this.button2.BorderSize = 0;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ID = 0;
            this.button2.Image = null;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.Location = new System.Drawing.Point(199, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            if (language == Language.French)
            {
                this.button2.Text = "Annuler";
            }
            else
            {
                this.button2.Text = "Cancel";
            }
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WebFeedDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(367, 206);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebFeedDialog";
            this.Redimensionnable = false;
            this.Text = "Sky multi - DVD";
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                if (language == Language.French)
                {
                    MessageBox.Show("Veuillez selectionner un DVD si aucun lecteur n'est disponible veuillez cliquer sur annuler.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select DVD if no drive is available please click cancel.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string DVDPath
        {
            get
            {
                return comboBox1.Text;
            }
        }
    }
}
