/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sky_UI;

namespace Sky_multi
{
    internal delegate void EventSpeedHandler(ref float Speed, ref bool SpeedMore);

    internal sealed class ChoiceSpeed : Control
    {
        internal EventSpeedHandler EventSpeedChanged = null;
        private float Coef = 1;
        private bool Multiplication = true;
        private Sky_UI.Button button2;
        private Sky_UI.Button button1;
        private System.Windows.Forms.Label label1;

        internal ChoiceSpeed(float Rate)
        {
            InitializeComponent();

            if (Rate < 1.0f)
            {
                Coef = 1.0f / Rate;
                Multiplication = false;
                label1.Text = "÷" + Coef;
            }
            else
            {
                Coef = Rate;
                Multiplication = true;
                label1.Text = "x" + Coef;
            }
        }

        private void InitializeComponent()
        {
            this.button2 = new Sky_UI.Button();
            this.button1 = new Sky_UI.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.Location = new System.Drawing.Point(177, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 46);
            this.button2.TabIndex = 31;
            this.button2.Text = "x";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.borderRadius = 5;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Location = new System.Drawing.Point(25, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 46);
            this.button1.TabIndex = 32;
            this.button1.Text = "÷";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.borderRadius = 5;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(114, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 21);
            this.label1.TabIndex = 33;
            this.label1.Text = "1";
            // 
            // ChoiceSpeed
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "ChoiceSpeed";
            this.Resize += new EventHandler(This_Resize);
            this.Size = new System.Drawing.Size(254, 95);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void This_Resize(object sender, EventArgs e)
        {
            IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5);

            if (handle != IntPtr.Zero)
            {
                Region = Region.FromHrgn(handle);
                Win32.DeleteObject(handle);
            }
        }

        new internal async void Show()
        {
            this.Height = 0;
            this.Visible = true;

            while (this.Height < 95)
            {
                this.Height += 10;
                this.Location = new Point(this.Location.X, this.Location.Y - 10);
                this.BringToFront();
                await Task.Delay(10);
            }

            this.Height = 95;
        }

        new internal async void Hide()
        {
            this.Height = 95;

            while (this.Height > 0)
            {
                this.Height -= 10;
                this.Location = new Point(this.Location.X, this.Location.Y + 10);
                await Task.Delay(10);
            }

            this.Height = 0;
            this.Visible = false;
        }

        internal async void HideDispose()
        {
            this.Height = 95;

            while (this.Height > 0)
            {
                this.Height -= 10;
                this.Location = new Point(this.Location.X, this.Location.Y + 10);
                await Task.Delay(10);
            }

            this.Height = 0;
            this.Visible = false;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Multiplication == true)
            {
                Coef -= 0.5f;
                label1.Text = "x" + Coef;
            }
            else
            {
                Coef += 0.5f;
                label1.Text = "÷" + Coef;
            }

            if (Coef >= 1.0f)
            {
                EventSpeedChanged(ref Coef, ref Multiplication);
            }
            else
            {
                Multiplication = false;
                Coef += 1.0f;
                label1.Text = "÷" + Coef;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Multiplication == false)
            {
                Coef -= 0.5f;

                if (Coef == 1.0f)
                {
                    label1.Text = "x" + Coef;
                }
                else
                {
                    label1.Text = "÷" + Coef;
                }
            }
            else
            {
                Coef += 0.5f;
                label1.Text = "x" + Coef;
            }

            if (Coef >= 1.0f)
            {
                EventSpeedChanged(ref Coef, ref Multiplication);
            }
            else
            {
                Multiplication = true;
                Coef += 1.0f;
                label1.Text = "x" + Coef;
            }
        }
    }
}
