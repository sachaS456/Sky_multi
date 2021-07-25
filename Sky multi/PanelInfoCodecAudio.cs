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
    internal partial class PanelInfoCodecAudio : Control
    {
        internal PanelInfoCodecAudio(Language lang)
        {
            InitializeComponent();

            if (lang != Language.French)
            {
                label15.Text = "canals : ";
                label14.Text = "sampling frequency : ";
                label10.Text = "Audio Information :";
            }
        }

        internal string CodecAudio
        {
            set
            {
                label16.Text += value;
            }
        }

        internal string Cannaux
        {
            set
            {
                label15.Text += value;
            }
        }

        internal string Rate
        {
            set
            {
                label14.Text += value;
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
