using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Sky_framework;

namespace Sky_multi
{
    internal class ImageModifierDialog : SkyForms
    {
        internal ImageModifierDialog()
        {

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageModifierDialog));
            this.SuspendLayout();
            // 
            // ImageModifierDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ButtonMaximizedVisible = true;
            this.ClientSize = new System.Drawing.Size(587, 496);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageModifierDialog";
            this.Text = "Sky multi - screenshot";
            this.ResumeLayout(false);

        }
    }
}
