using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sky_multi
{
    internal partial class PanelInfoCodecVideo : Control
    {
        internal PanelInfoCodecVideo(Language lang)
        {
            InitializeComponent();

            if (lang != Language.French)
            {
                label12.Text = "Height : ";
                label13.Text = "Width : ";
                label9.Text = "Video Information :";
            }
        }

        internal string CodecVideo
        {
            set
            {
                label11.Text += value;
            }
        }

        internal string Largeur
        {
            set
            {
                label12.Text += value;
            }
        }

        internal string Hauteur
        {
            set
            {
                label13.Text += value;
            }
        }

        internal string Orientation
        {
            set
            {
                label18.Text += value;
            }
        }

        internal int nbFlux
        {
            set
            {
                label1.Text = "Flux " + value;
            }
        }

        internal string TrackName
        {
            set
            {
                label1.Text += "   (" + value + ")";
            }
        }
    }
}
