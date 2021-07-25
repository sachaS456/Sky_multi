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
    internal partial class PanelInfoCodecText : Control
    {
        internal PanelInfoCodecText(Language lang)
        {
            InitializeComponent();

            if (lang != Language.French)
            {
                label19.Text = "Subtitle information :";
            }
        }

        internal string CodecText
        {
            set
            {
                label20.Text += value;
            }
        }

        internal string Encoding
        {
            set
            {
                label21.Text += value;
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
