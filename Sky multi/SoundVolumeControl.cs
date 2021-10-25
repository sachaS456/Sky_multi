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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sky_framework;

namespace Sky_multi
{
    internal delegate void EventSoundSetHandler(int Volume);
    internal delegate void EventMutedHandler(bool IsMute);

    internal sealed class SoundVolumeControl : Control
    {
        private Label Label = new Label();
        private Label LabelVolume = new Label();
        private Sky_framework.ProgressBar VolumeBar = new Sky_framework.ProgressBar();
        private Sky_framework.Button ButtonMute = new Sky_framework.Button();
        private bool BarMouseDown = false;
        private int Volume = 100;
        private bool Mute = false;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

        internal EventSoundSetHandler EventSoundSet = null;
        internal EventMutedHandler EventMute = null;

        internal SoundVolumeControl(int Volume, bool Mute, Language Lang)
        {
            this.Size = new Size(200, 50);
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Resize += new EventHandler(This_Resize);
            this.Volume = Volume;
            this.Mute = Mute;

            Label.AutoSize = true;
            Label.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            if (Lang == Language.French)
            {
                Label.Text = "Volume du son :";
            }
            else
            {
                Label.Text = "Sound volume :";
            }           
            Label.Location = new Point(this.Width / 2 - Label.Width / 2, 2);
            Label.Anchor = AnchorStyles.Top;
            Label.ForeColor = Color.FromArgb(224, 224, 224);
            this.Controls.Add(Label);

            LabelVolume.AutoSize = true;
            LabelVolume.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LabelVolume.Text = Volume + "%";
            LabelVolume.Location = new Point(160, this.Height / 2);
            LabelVolume.Anchor = AnchorStyles.Right;
            LabelVolume.ForeColor = Color.FromArgb(224, 224, 224);
            this.Controls.Add(LabelVolume);

            VolumeBar.Location = new Point(25, this.Height / 2 + 4);
            VolumeBar.Size = new Size(130, 10);
            VolumeBar.ValuePourcentages = Volume;
            VolumeBar.Anchor = AnchorStyles.Right | AnchorStyles.Left;
            VolumeBar.Color = Color.Orange;
            VolumeBar.BackColor = Color.FromArgb(40, 40, 40);
            VolumeBar.MouseUp += new MouseEventHandler(VolumeBar_MouseUp);
            VolumeBar.MouseMove += new MouseEventHandler(VolumeBar_MouseMove);
            VolumeBar.MouseDown += new MouseEventHandler(VolumeBar_MouseDown);
            this.Controls.Add(VolumeBar);

            ButtonMute.borderRadius = 5;
            ButtonMute.Location = new Point(5, this.Height / 2 + 1);
            ButtonMute.Size = new Size(15, 15);
            ButtonMute.Anchor = AnchorStyles.Right | AnchorStyles.Left;
            ButtonMute.ForeColor = Color.FromArgb(224, 224, 224);
            this.ButtonMute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            ButtonMute.Click += new EventHandler(ButtonMute_Click);
            this.Controls.Add(ButtonMute);

            if (Mute == true)
            {
                this.ButtonMute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonMute")));
            }
            else
            {
                this.ButtonMute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonUnmute")));
            }
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

        private void ButtonMute_Click(object sender, EventArgs e)
        {
            Mute = !Mute;

            if (Mute == true)
            {
                this.ButtonMute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonMute")));
            }
            else
            {
                this.ButtonMute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonUnmute")));
            }

            if (EventMute != null)
            {
                EventMute(Mute);
            }
        }

        private void VolumeBar_MouseUp(object sender, MouseEventArgs e)
        {
            BarMouseDown = false;
        }

        private void VolumeBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BarMouseDown = true;
            }
        }

        private void VolumeBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (BarMouseDown == true)
            {
                if (e.X > VolumeBar.Width)
                {
                    VolumeBar.ValuePourcentages = 100;
                }
                else if (e.X < 0)
                {
                    VolumeBar.ValuePourcentages = 0;
                }
                else
                {
                    VolumeBar.ValuePourcentages = (int)((double)e.X / VolumeBar.Width * 100);
                }

                Volume = VolumeBar.ValuePourcentages;
                LabelVolume.Text = Volume + "%";

                if (EventSoundSet != null)
                {
                    EventSoundSet(Volume);
                }
            }
        }

        new internal async void Show()
        {
            this.Height = 0;
            this.Visible = true;

            while (this.Height < 50)
            {
                this.Height += 10;
                this.Location = new Point(this.Location.X, this.Location.Y - 10);
                this.BringToFront();
                await Task.Delay(10);
            }

            this.Height = 50;
        }

        new internal async void Hide()
        {
            this.Height = 50;
            
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
            this.Height = 50;

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
    }
}
