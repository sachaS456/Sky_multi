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
using System.Windows.Forms;
using System.Drawing;

namespace Sky_multi_Viewer
{
    public sealed class VideoPreview : Control
    {
        private VideoView VideoView;
        private Label labeTime = new Label();
        //private byte CanSetTime = 0;

        public VideoPreview()
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Size = new Size(128, 95);

            this.VideoView = new VideoView();
            ((System.ComponentModel.ISupportInitialize)(this.VideoView)).BeginInit();

            this.VideoView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.VideoView.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.VideoView.Location = new System.Drawing.Point(0, 0);
            this.VideoView.Name = "vlcControl1";
            this.VideoView.Size = new System.Drawing.Size(128, 72);
            this.VideoView.Spu = -1;
            this.VideoView.TabIndex = 2;
            this.VideoView.Text = "vlcControl1";
            this.VideoView.VlcLibDirectory = null;
            this.VideoView.VlcMediaplayerOptions = null;
            this.VideoView.VlcLibDirectoryNeeded += new EventHandler<VlcLibDirectoryNeededEventArgs>(this.vlcControl1_VlcLibDirectoryNeeded);

            this.labeTime.AutoSize = true;
            this.labeTime.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.labeTime.ForeColor = Color.FromArgb(224, 224, 224);
            this.labeTime.Location = new Point(this.Width / 2 - labeTime.Width / 2, 65);

            this.Controls.Add(this.VideoView);
            this.Controls.Add(this.labeTime);
            this.Controls.SetChildIndex(this.VideoView, 0);
            this.Controls.SetChildIndex(this.labeTime, 0);
            ((System.ComponentModel.ISupportInitialize)(this.VideoView)).EndInit();
        }

        public void LoadMedia(string Media, long VlcTime)
        {
            VideoView.Play("File:///" + Media);
            //await Task.Delay(100);
            VideoView.Time = VlcTime;
            VideoView.SetPause(true);
            labeTime.Text = ConvertVlcTimeToString(VlcTime);
            this.labeTime.Location = new Point(this.Width / 2 - labeTime.Width / 2, 65);
        }

        private string ConvertVlcTimeToString(long VlcTime)
        {
            long second = VlcTime / 1000;
            long minute = second / 60;
            long heure = minute / 60;

            second = second - 60 * minute;
            minute = minute - 60 * heure;
            if (second < 0)
            {
                minute--;
                second = 60 + second;
            }
            if (minute < 0)
            {
                heure--;
                minute = 60 + minute;
            }

            return heure + "h" + minute + "min" + second + "s";
        }

        public async void SetTime(long VlcTime)
        {
            await Task.Delay(1);

            //if (CanSetTime == 0)
            {
                VideoView.Time = VlcTime;
                labeTime.Text = ConvertVlcTimeToString(VlcTime);
                this.labeTime.Location = new Point(this.Width / 2 - labeTime.Width / 2, 65);
                //CanSetTime++;
                //return;
            }

            /*CanSetTime++;

            if (CanSetTime > 2)
            {
                CanSetTime = 0;
            }*/
        }

        public long GetTime()
        {
            return VideoView.Time;
        }

        public void Stop()
        {
            VideoView.Stop();
            VideoView.VlcMediaPlayer.ResetMedia();
        }

        public Sky_multi_Core.VlcWrapper.HardwareAccelerationType HardwareAcceleration
        {
            get
            {
                return VideoView.HardwareAcceleration;
            }
            set
            {
                VideoView.HardwareAcceleration = value;
            }
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, VlcLibDirectoryNeededEventArgs e)
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
    }
}
