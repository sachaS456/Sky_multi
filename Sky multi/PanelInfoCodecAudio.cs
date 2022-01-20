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
