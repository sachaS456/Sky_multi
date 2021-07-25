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
        private VlcControl VlcControl;
        private Label labeTime = new Label();
        //private byte CanSetTime = 0;

        public VideoPreview()
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Size = new Size(128, 86);

            this.VlcControl = new VlcControl();
            ((System.ComponentModel.ISupportInitialize)(this.VlcControl)).BeginInit();

            this.VlcControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.VlcControl.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.VlcControl.Location = new System.Drawing.Point(0, 0);
            this.VlcControl.Name = "vlcControl1";
            this.VlcControl.Size = new System.Drawing.Size(128, 72);
            this.VlcControl.Spu = -1;
            this.VlcControl.TabIndex = 2;
            this.VlcControl.Text = "vlcControl1";
            this.VlcControl.VlcLibDirectory = null;
            this.VlcControl.VlcMediaplayerOptions = null;
            this.VlcControl.VlcLibDirectoryNeeded += new EventHandler<VlcLibDirectoryNeededEventArgs>(this.vlcControl1_VlcLibDirectoryNeeded);

            this.labeTime.AutoSize = true;
            this.labeTime.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.labeTime.ForeColor = Color.FromArgb(224, 224, 224);
            this.labeTime.Location = new Point(this.Width / 2 - labeTime.Width / 2, 72);

            ((System.ComponentModel.ISupportInitialize)(this.VlcControl)).EndInit();
            this.Controls.Add(this.VlcControl);
            this.Controls.Add(this.labeTime);
            this.Controls.SetChildIndex(this.VlcControl, 0);
            this.Controls.SetChildIndex(this.labeTime, 0);
        }

        public void LoadMedia(string Media, long VlcTime)
        {
            VlcControl.Play("File:///" + Media);
            //await Task.Delay(100);
            VlcControl.Time = VlcTime;
            VlcControl.SetPause(true);
            labeTime.Text = ConvertVlcTimeToString(VlcTime);
            this.labeTime.Location = new Point(this.Width / 2 - labeTime.Width / 2, 72);
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
                VlcControl.Time = VlcTime;
                labeTime.Text = ConvertVlcTimeToString(VlcTime);
                this.labeTime.Location = new Point(this.Width / 2 - labeTime.Width / 2, 72);
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
            return VlcControl.Time;
        }

        public void Stop()
        {
            VlcControl.Stop();
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
