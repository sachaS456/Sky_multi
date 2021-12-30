/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sky_multi
{
    internal delegate void EventSetTimeHandler(long VlcTime);

    internal sealed class SetTimeDialog : Sky_UI.SkyForms
    {
        private System.Windows.Forms.Label label1;
        private Sky_UI.ButtonCircular buttonCircular1;
        private Sky_UI.ButtonCircular buttonCircular2;
        private Sky_UI.ButtonCircular buttonCircular3;
        private Sky_UI.ButtonCircular buttonCircular4;
        private System.Windows.Forms.Label label2;
        private Sky_UI.ButtonCircular buttonCircular5;
        private Sky_UI.ButtonCircular buttonCircular6;
        private System.Windows.Forms.Label label3;
        private Sky_multi_Viewer.VlcControl vlcControl1;
        private Sky_UI.Button button1;
        private Sky_UI.Button button2;
        private long hours = 0;
        private long minutes = 0;
        private long seconds = 0;

        public EventSetTimeHandler TimeDefined = null;

        internal SetTimeDialog(long VlcTime, ref string MultiMedia, bool Video)
        {
            seconds = VlcTime / 1000;
            minutes = seconds / 60;
            hours = minutes / 60;

            seconds = seconds - 60 * minutes;
            minutes = minutes - 60 * hours;
            if (seconds < 0)
            {
                minutes--;
                seconds = 60 + seconds;
            }
            if (minutes < 0)
            {
                hours--;
                minutes = 60 + minutes;
            }

            InitializeComponent();
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);

            label1.Text = hours + " heure(s)";
            label2.Text = minutes + " minute(s)";
            label3.Text = seconds + " seconde(s)";

            if (Video == false)
            {
                vlcControl1.Dispose();
                vlcControl1 = null;
            }
            else
            {
                SetVideo(MultiMedia);
            }
        }

        private async void SetVideo(string MultiMedia)
        {
            while (vlcControl1.Chargement == true)
            {
                await Task.Delay(5);
            }

            vlcControl1.Play("File:///" + MultiMedia);
            await Task.Delay(200);
            vlcControl1.Time = VlcTime();
            vlcControl1.SetPause(true);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCircular1 = new Sky_UI.ButtonCircular();
            this.buttonCircular2 = new Sky_UI.ButtonCircular();
            this.buttonCircular3 = new Sky_UI.ButtonCircular();
            this.buttonCircular4 = new Sky_UI.ButtonCircular();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCircular5 = new Sky_UI.ButtonCircular();
            this.buttonCircular6 = new Sky_UI.ButtonCircular();
            this.label3 = new System.Windows.Forms.Label();
            this.vlcControl1 = new Sky_multi_Viewer.VlcControl();
            this.button1 = new Sky_UI.Button();
            this.button2 = new Sky_UI.Button();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(43, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "0 heure(s)";
            // 
            // buttonCircular1
            // 
            this.buttonCircular1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.buttonCircular1.Border = false;
            this.buttonCircular1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCircular1.borderRadius = 0;
            this.buttonCircular1.Location = new System.Drawing.Point(119, 54);
            this.buttonCircular1.Name = "buttonCircular1";
            this.buttonCircular1.Size = 20;
            this.buttonCircular1.TabIndex = 4;
            this.buttonCircular1.Text = "+";
            this.buttonCircular1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonCircular1.Click += new System.EventHandler(this.buttonCircular1_Click);
            // 
            // buttonCircular2
            // 
            this.buttonCircular2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.buttonCircular2.Border = false;
            this.buttonCircular2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCircular2.borderRadius = 0;
            this.buttonCircular2.Location = new System.Drawing.Point(17, 54);
            this.buttonCircular2.Name = "buttonCircular2";
            this.buttonCircular2.Size = 20;
            this.buttonCircular2.TabIndex = 5;
            this.buttonCircular2.Text = "-";
            this.buttonCircular2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonCircular2.Click += new System.EventHandler(this.buttonCircular2_Click);
            // 
            // buttonCircular3
            // 
            this.buttonCircular3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.buttonCircular3.Border = false;
            this.buttonCircular3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCircular3.borderRadius = 0;
            this.buttonCircular3.Location = new System.Drawing.Point(159, 54);
            this.buttonCircular3.Name = "buttonCircular3";
            this.buttonCircular3.Size = 20;
            this.buttonCircular3.TabIndex = 8;
            this.buttonCircular3.Text = "-";
            this.buttonCircular3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonCircular3.Click += new System.EventHandler(this.buttonCircular3_Click);
            // 
            // buttonCircular4
            // 
            this.buttonCircular4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.buttonCircular4.Border = false;
            this.buttonCircular4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCircular4.borderRadius = 0;
            this.buttonCircular4.Location = new System.Drawing.Point(270, 54);
            this.buttonCircular4.Name = "buttonCircular4";
            this.buttonCircular4.Size = 20;
            this.buttonCircular4.TabIndex = 7;
            this.buttonCircular4.Text = "+";
            this.buttonCircular4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonCircular4.Click += new System.EventHandler(this.buttonCircular4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(185, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "0 minute(s)";
            // 
            // buttonCircular5
            // 
            this.buttonCircular5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.buttonCircular5.Border = false;
            this.buttonCircular5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCircular5.borderRadius = 0;
            this.buttonCircular5.Location = new System.Drawing.Point(309, 54);
            this.buttonCircular5.Name = "buttonCircular5";
            this.buttonCircular5.Size = 20;
            this.buttonCircular5.TabIndex = 11;
            this.buttonCircular5.Text = "-";
            this.buttonCircular5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonCircular5.Click += new System.EventHandler(this.buttonCircular5_Click);
            // 
            // buttonCircular6
            // 
            this.buttonCircular6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.buttonCircular6.Border = false;
            this.buttonCircular6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCircular6.borderRadius = 0;
            this.buttonCircular6.Location = new System.Drawing.Point(426, 54);
            this.buttonCircular6.Name = "buttonCircular6";
            this.buttonCircular6.Size = 20;
            this.buttonCircular6.TabIndex = 10;
            this.buttonCircular6.Text = "+";
            this.buttonCircular6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonCircular6.Click += new System.EventHandler(this.buttonCircular6_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(335, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "0 seconde(s)";
            // 
            // vlcControl1
            // 
            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Location = new System.Drawing.Point(12, 100);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(223, 125);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 12;
            this.vlcControl1.Text = "vlcControl1";
            this.vlcControl1.VlcLibDirectory = null;
            this.vlcControl1.VlcMediaplayerOptions = null;
            this.vlcControl1.VlcLibDirectoryNeeded += new EventHandler<Sky_multi_Viewer.VlcLibDirectoryNeededEventArgs>(vlcControl1_VlcLibDirectoryNeeded);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Border = false;
            this.button1.Text = "Aller";
            this.button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.borderRadius = 5;
            this.button1.Location = new System.Drawing.Point(364, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 31);
            this.button1.TabIndex = 13;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Border = false;
            this.button2.Text = "Annuler";
            this.button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.borderRadius = 5;
            this.button2.Location = new System.Drawing.Point(276, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 31);
            this.button2.TabIndex = 14;
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SetTimeDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(458, 237);
            this.Redimensionnable = false;
            this.ButtonMaximizedVisible = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.vlcControl1);
            this.Controls.Add(this.buttonCircular5);
            this.Controls.Add(this.buttonCircular6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCircular3);
            this.Controls.Add(this.buttonCircular4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCircular2);
            this.Controls.Add(this.buttonCircular1);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "SetTimeDialog";
            this.Text = "Définir un temps spécifique";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.buttonCircular1, 0);
            this.Controls.SetChildIndex(this.buttonCircular2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.buttonCircular4, 0);
            this.Controls.SetChildIndex(this.buttonCircular3, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.buttonCircular6, 0);
            this.Controls.SetChildIndex(this.buttonCircular5, 0);
            this.Controls.SetChildIndex(this.vlcControl1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private long VlcTime()
        {
            long VlcTime = minutes + 60 * hours;
            VlcTime = seconds + 60 * VlcTime;
            VlcTime = 1000 * VlcTime;
            return VlcTime;
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Sky_multi_Viewer.VlcLibDirectoryNeededEventArgs e)
        {
            if (Environment.Is64BitProcess == true)
            {
                e.VlcLibDirectory = new System.IO.DirectoryInfo(Application.StartupPath + @"libvlc\win-x64"); // 64 bits
            }
            else
            {
                e.VlcLibDirectory = new System.IO.DirectoryInfo(Application.StartupPath + @"libvlc\win-x86"); // 32 bits
            }
        }

        private void button2_Click(object sender, EventArgs e) // Cancel
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) // Go
        {
            if (TimeDefined != null)
            {
                TimeDefined(VlcTime());
            }

            this.Close();
        }

        private void buttonCircular2_Click(object sender, EventArgs e)
        {
            hours--;
            label1.Text = hours + " heure(s)";

            if (vlcControl1 != null)
            {
                vlcControl1.Time = VlcTime();
            }
        }

        private void buttonCircular1_Click(object sender, EventArgs e)
        {
            hours++;
            label1.Text = hours + " heure(s)";

            if (vlcControl1 != null)
            {
                vlcControl1.Time = VlcTime();
            }
        }

        private void buttonCircular3_Click(object sender, EventArgs e)
        {
            if (minutes <= 0)
            {
                hours--;
                minutes = 59;
                label1.Text = hours + " heure(s)";
            }
            else
            {
                minutes--;
            }

            label2.Text = minutes + " minute(s)";

            if (vlcControl1 != null)
            {
                vlcControl1.Time = VlcTime();
            }
        }

        private void buttonCircular4_Click(object sender, EventArgs e)
        {
            if (minutes >= 59)
            {
                hours++;
                minutes = 0;
                label1.Text = hours + " heure(s)";
            }
            else
            {
                minutes++;
            }

            label2.Text = minutes + " minute(s)";

            if (vlcControl1 != null)
            {
                vlcControl1.Time = VlcTime();
            }
        }

        private void buttonCircular5_Click(object sender, EventArgs e)
        {
            if (seconds <= 0)
            {
                if (minutes <= 0)
                {
                    hours--;
                    minutes = 59;
                    label1.Text = hours + " heure(s)";
                }
                else
                {
                    minutes--;
                }

                seconds = 59;
                label2.Text = minutes + " minute(s)";
            }
            else
            {
                seconds--;
            }

            label3.Text = seconds + " seconde(s)";

            if (vlcControl1 != null)
            {
                vlcControl1.Time = VlcTime();
            }
        }

        private void buttonCircular6_Click(object sender, EventArgs e)
        {
            if (seconds >= 59)
            {
                if (minutes >= 59)
                {
                    hours++;
                    minutes = 0;
                    label1.Text = hours + " heure(s)";
                }
                else
                {
                    minutes++;
                }

                seconds = 0;
                label2.Text = minutes + " minute(s)";
            }
            else
            {
                seconds++;
            }

            label3.Text = seconds + " seconde(s)";

            if (vlcControl1 != null)
            {
                vlcControl1.Time = VlcTime();
            }
        }
    }
}
