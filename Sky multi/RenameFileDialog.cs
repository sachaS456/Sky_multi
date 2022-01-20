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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using Sky_UI;

namespace Sky_multi
{
    internal sealed class RenameFileDialog : Form
    {
        private TextBox TextBox = new TextBox();
        private Label FormatString = new Label();
        private Sky_UI.Button ButtonOk = new Sky_UI.Button();
        private Sky_UI.Button ButtonCancel = new Sky_UI.Button();

        internal RenameFileDialog(Language language)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(300, 125);
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Resize += new EventHandler(This_Resize);
            this.Icon = (Icon)resources.GetObject("$this.Icon");

            TextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            TextBox.Width = this.Width / 2;
            TextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            TextBox.BorderStyle = BorderStyle.FixedSingle;
            TextBox.Location = new Point(this.Width / 2 - TextBox.Width / 2, this.Height / 2 - TextBox.Height / 2);
            TextBox.BackColor = this.BackColor;
            TextBox.ForeColor = Color.FromArgb(224, 224, 224);
            TextBox.TabIndex = 1;
            this.Controls.Add(TextBox);

            FormatString.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Top;
            FormatString.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormatString.BorderStyle = BorderStyle.FixedSingle;
            FormatString.Resize += new EventHandler(FormatString_Resize);
            FormatString.Location = new Point(TextBox.Location.X + TextBox.Width + 5, this.TextBox.Location.Y);
            FormatString.BackColor = this.BackColor;
            FormatString.ForeColor = Color.FromArgb(224, 224, 224);
            FormatString.AutoSize = true;
            this.Controls.Add(FormatString);

            ButtonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ButtonOk.Size = new Size(100, 30);
            ButtonOk.Location = new Point(this.Width - ButtonOk.Width - 5, this.Height - ButtonOk.Height - 5);
            ButtonOk.BackColor = this.BackColor;
            ButtonOk.borderRadius = 5;
            ButtonOk.TabIndex = 2;
            ButtonOk.Text = "Ok";
            ButtonOk.MouseClick += new MouseEventHandler(ButtonOk_MouseClick);
            this.Controls.Add(ButtonOk);

            ButtonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ButtonCancel.Size = new Size(100, 30);
            ButtonCancel.Location = new Point(this.Width - ButtonOk.Width - ButtonOk.Width - 5, this.Height - ButtonOk.Height - 5);
            ButtonCancel.BackColor = this.BackColor;
            ButtonCancel.borderRadius = 5;
            ButtonCancel.TabIndex = 3;
            if (language == Language.French)
            {
                ButtonCancel.Text = "Annuler";
            }
            else
            {
                ButtonCancel.Text = "Cancel";
            }
            ButtonCancel.MouseClick += new MouseEventHandler(ButtonCancel_MouseClick);
            this.Controls.Add(ButtonCancel);
        }

        public string NewName
        {
            get
            {
                return TextBox.Text;
            }
        }

        public DialogResult ShowDialog(string FilePath)
        {
            this.CenterToParent();
            FormatString.Text = System.IO.Path.GetExtension(FilePath);
            TextBox.Text = System.IO.Path.GetFileNameWithoutExtension(FilePath);

            this.Visible = true;
            this.Opacity = 0;

            while (this.Opacity <= 0.99)
            {
                this.Opacity += 0.05;
                System.Threading.Thread.Sleep(10);
            }
            this.Visible = false;
            return base.ShowDialog();
        }

        private void ButtonOk_MouseClick(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;

            while (this.Opacity > 0)
            {
                this.Opacity -= 0.05;
                System.Threading.Thread.Sleep(10);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ButtonCancel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;

            while (this.Opacity > 0)
            {
                this.Opacity -= 0.05;
                System.Threading.Thread.Sleep(10);
            }

            this.DialogResult = DialogResult.Cancel;
        }

        private void FormatString_Resize(object sender, EventArgs e)
        {
            //FormatString.Location = new Point(this.Width - (5 + FormatString.Width), this.Location.Y);
            //TextBox.Width = this.Width - (5 + FormatString.Location.X - (TextBox.Width + TextBox.Location.X));
        }

        private void This_Resize(object sender, EventArgs e)
        {
            IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5);

            if (handle != IntPtr.Zero)
            {
                Region = Region.FromHrgn(handle);
                Win32.DeleteObject(handle);
            }

            //ControlPaint.DrawBorder(this.CreateGraphics(), this.ClientRectangle, Color.FromArgb(224, 224, 224), 1, ButtonBorderStyle.Solid, Color.FromArgb(224, 224, 224), 1, 
                //ButtonBorderStyle.Solid, Color.FromArgb(224, 224, 224), 1, ButtonBorderStyle.Solid, Color.FromArgb(224, 224, 224), 1, ButtonBorderStyle.Solid);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //ControlPaint.DrawBorder(this.CreateGraphics(), this.ClientRectangle, Color.FromArgb(150, 150, 150), 1, ButtonBorderStyle.Solid, Color.FromArgb(150, 150, 150), 1,
                 //ButtonBorderStyle.Solid, Color.FromArgb(150, 150, 150), 2, ButtonBorderStyle.Solid, Color.FromArgb(150, 150, 150), 2, ButtonBorderStyle.Solid);
            Border.DrawRoundRectangle(new Pen(Color.FromArgb(150, 150, 150), 2), 0, 0, Width -1, Height -1, 5, this.CreateGraphics());
        }     
    }
}
