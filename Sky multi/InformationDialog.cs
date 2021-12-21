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
using System.IO;
using Sky_multi_Core.VlcWrapper;

namespace Sky_multi
{
    internal sealed class InformationDialog : SkyForms
    {
        private Sky_framework.Rectangle rectangle1;
        private Sky_framework.Rectangle rectangle2;
        private Label label2;
        private Sky_framework.Rectangle rectangle3;
        private Label FileInfo;
        private Label label1;
        private Sky_framework.Rectangle rectangle4;
        private Panel panel1;
        private Sky_framework.Button buttonOK;

        internal InformationDialog(in string FileName, Language language, in string[] Version)
        {
            InitializeComponent(in language, in Version);
            this.Location = new Point(Screen.FromControl(this).WorkingArea.Width / 2 - this.Width / 2, Screen.FromControl(this).WorkingArea.Height / 2 - this.Height / 2);

            if (FileName != string.Empty)
            {
                if (language == Language.French)
                {
                   FileInfo.Text = "Nom du fichier : " + Path.GetFileNameWithoutExtension(FileName) +
                     "\n\nExtention du fichier : " + Path.GetExtension(FileName) +
                     "\n\nChemin du fichier : " + FileName +
                     "\n\nTaille du fichier : " + EspaceNombre(new FileInfo(FileName).Length / 1000) + " ko" +
                     "\n\nDate de creation : " + File.GetCreationTime(FileName);
                }
                else
                {
                    FileInfo.Text = "File name : " + Path.GetFileNameWithoutExtension(FileName) +
                      "\n\nFile extension : " + Path.GetExtension(FileName) +
                      "\n\nFile path : " + FileName +
                      "\n\nFile size : " + EspaceNombre(new FileInfo(FileName).Length / 1000) + " ko" +
                      "\n\nCreation date : " + File.GetCreationTime(FileName);
                }
                this.FileInfo.Size = new System.Drawing.Size(290, 135);
            }
        }

        internal InformationDialog(in string FileName, string Resolution, string PixelFormat, Language language, in string[] Version)
        {
            InitializeComponent(in language, in Version);
            this.Location = new Point(Screen.FromControl(this).WorkingArea.Width / 2 - this.Width / 2, Screen.FromControl(this).WorkingArea.Height / 2 - this.Height / 2);

            if (FileName != string.Empty)
            {
                if (language == Language.French)
                {
                    FileInfo.Text = "Nom du fichier : " + Path.GetFileNameWithoutExtension(FileName) +
                      "\n\nExtention du fichier : " + Path.GetExtension(FileName) +
                      "\n\nChemin du fichier : " + FileName +
                      "\n\nTaille du fichier : " + EspaceNombre(new FileInfo(FileName).Length / 1000) + " ko" +
                      "\n\nDate de creation : " + File.GetCreationTime(FileName);
                }
                else
                {
                    FileInfo.Text = "File name : " + Path.GetFileNameWithoutExtension(FileName) +
                      "\n\nFile extension : " + Path.GetExtension(FileName) +
                      "\n\nFile path : " + FileName +
                      "\n\nFile size : " + EspaceNombre(new FileInfo(FileName).Length / 1000) + " ko" +
                      "\n\nCreation date : " + File.GetCreationTime(FileName);
                }
                this.FileInfo.Size = new System.Drawing.Size(290, 135);
            }

            Label InfoImage = new Label();
            if (language == Language.French)
            {
                InfoImage.Text = "Resolution de l'image : " + Resolution + "\n\nFormat de pixel utilisé : " + PixelFormat;
            }
            else
            {
                InfoImage.Text = "Image resolution : " + Resolution + "\n\nPixel format used : " + PixelFormat;
            }
            InfoImage.AutoSize = true;
            InfoImage.Location = new Point(3, 3);
            InfoImage.ForeColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(InfoImage);
        }

        internal InformationDialog(in string FileName, MediaTrack[] ListTracks, List<TrackDescription> trackDescriptionsV, List<TrackDescription> trackDescriptionsA,
            List<TrackDescription> trackDescriptionsT, Language language, in string[] Version)
        {
            InitializeComponent(in language, in Version);
            this.Location = new Point(Screen.FromControl(this).WorkingArea.Width / 2 - this.Width / 2, Screen.FromControl(this).WorkingArea.Height / 2 - this.Height / 2);

            if (FileName != string.Empty)
            {
                if (language == Language.French)
                {
                    FileInfo.Text = "Nom du fichier : " + Path.GetFileNameWithoutExtension(FileName) +
                      "\n\nExtention du fichier : " + Path.GetExtension(FileName) +
                      "\n\nChemin du fichier : " + FileName +
                      "\n\nTaille du fichier : " + EspaceNombre(new FileInfo(FileName).Length / 1000) + " ko" +
                      "\n\nDate de creation : " + File.GetCreationTime(FileName);
                }
                else
                {
                    FileInfo.Text = "File name : " + Path.GetFileNameWithoutExtension(FileName) +
                      "\n\nFile extension : " + Path.GetExtension(FileName) +
                      "\n\nFile path : " + FileName +
                      "\n\nFile size : " + EspaceNombre(new FileInfo(FileName).Length / 1000) + " ko" +
                      "\n\nCreation date : " + File.GetCreationTime(FileName);
                }
                this.FileInfo.Size = new System.Drawing.Size(290, 135);
            }

            int nbFlux = 0;

            int nbVideo = 0;
            int nbAudio = 0;
            int nbText = 0;

            foreach (MediaTrack i in ListTracks)
            {
                if (i.Type == Sky_multi_Core.VlcWrapper.Core.MediaTrackTypes.Video)
                {
                    nbVideo++;
                    VideoTrack videoTrack = i.TrackInfo as VideoTrack;
                    PanelInfoCodecVideo panelInfoCodecVideo = new PanelInfoCodecVideo(language);

                    panelInfoCodecVideo.CodecVideo = FourCCConverter.FromFourCC(i.CodecFourcc);
                    panelInfoCodecVideo.Hauteur = videoTrack?.Height.ToString() + " pixels";
                    panelInfoCodecVideo.Largeur = videoTrack?.Width.ToString() + " pixels";
                    panelInfoCodecVideo.Orientation = videoTrack?.Orientation.ToString();

                    panelInfoCodecVideo.Location = new Point(0, nbFlux * 196 + 10);
                    panelInfoCodecVideo.Width = 270;
                    panelInfoCodecVideo.nbFlux = nbFlux;
                    panelInfoCodecVideo.TrackName = trackDescriptionsV[nbVideo].Name;
                    panel1.Controls.Add(panelInfoCodecVideo);
                    nbFlux++;
                }
                else if (i.Type == Sky_multi_Core.VlcWrapper.Core.MediaTrackTypes.Audio)
                {
                    nbAudio++;
                    AudioTrack audioTrack = i.TrackInfo as AudioTrack;
                    PanelInfoCodecAudio panelInfoCodecAudio = new PanelInfoCodecAudio(language);

                    panelInfoCodecAudio.CodecAudio = FourCCConverter.FromFourCC(i.CodecFourcc);
                    panelInfoCodecAudio.Cannaux = audioTrack?.Channels.ToString();
                    panelInfoCodecAudio.Rate = audioTrack?.Rate.ToString() + " Hz";

                    panelInfoCodecAudio.Location = new Point(0, nbFlux * 196 + 10);
                    panelInfoCodecAudio.Width = 270;
                    panelInfoCodecAudio.nbFlux = nbFlux;
                    panelInfoCodecAudio.TrackName = trackDescriptionsA[nbAudio].Name;
                    panel1.Controls.Add(panelInfoCodecAudio);
                    nbFlux++;
                }
                else if (i.Type == Sky_multi_Core.VlcWrapper.Core.MediaTrackTypes.Text)
                {
                    nbText++;
                    SubtitleTrack subtitleTrack = i.TrackInfo as SubtitleTrack;
                    PanelInfoCodecText panelInfoCodecText = new PanelInfoCodecText(language);

                    panelInfoCodecText.CodecText = FourCCConverter.FromFourCC(i.CodecFourcc);
                    panelInfoCodecText.Encoding = subtitleTrack?.Encoding;

                    panelInfoCodecText.Location = new Point(0, nbFlux * 196 + 10);
                    panelInfoCodecText.Width = 270;
                    panelInfoCodecText.nbFlux = nbFlux;
                    panelInfoCodecText.TrackName = trackDescriptionsT[nbText].Name;
                    panel1.Controls.Add(panelInfoCodecText);
                    nbFlux++;
                }
            }
        }

        private void InitializeComponent(in Language language, in string[] version)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationDialog));
            this.buttonOK = new Sky_framework.Button();
            this.rectangle1 = new Sky_framework.Rectangle();
            this.rectangle2 = new Sky_framework.Rectangle();
            this.label2 = new System.Windows.Forms.Label();
            this.rectangle3 = new Sky_framework.Rectangle();
            this.FileInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rectangle4 = new Sky_framework.Rectangle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rectangle3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonOK.Border = false;
            this.buttonOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonOK.borderRadius = 5;
            this.buttonOK.Location = new System.Drawing.Point(553, 327);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(93, 30);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "Ok";
            this.buttonOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // rectangle1
            // 
            this.rectangle1.Border = true;
            this.rectangle1.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.rectangle1.BorderRadius = 10;
            this.rectangle1.BorderWidth = 3;
            this.rectangle1.Location = new System.Drawing.Point(6, 29);
            this.rectangle1.Name = "rectangle1";
            this.rectangle1.Size = new System.Drawing.Size(356, 366);
            this.rectangle1.TabIndex = 4;
            this.rectangle1.Text = "rectangle1";
            // 
            // rectangle2
            // 
            this.rectangle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rectangle2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rectangle2.BackgroundImage")));
            this.rectangle2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rectangle2.Border = false;
            this.rectangle2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rectangle2.BorderRadius = 0;
            this.rectangle2.BorderWidth = 3;
            this.rectangle2.Location = new System.Drawing.Point(61, 33);
            this.rectangle2.Name = "rectangle2";
            this.rectangle2.Size = new System.Drawing.Size(240, 140);
            this.rectangle2.TabIndex = 6;
            this.rectangle2.Text = "rectangle2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(9, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 170);
            this.label2.TabIndex = 7;
            if (language == Language.French)
            {
                this.label2.Text = "Version : " + version[5] + "\nDéveloppée par Sacha Himber\n\nSky multi est une application libre qui permet de lire\n" +
                    "des vidéos, des audios et des images il peut lire un grand\nnombre de format de fichier.\n\nLibrairies utilisés : Net " + version[0] +
                    ", Sky Framework " + version[1] + ", \nLibVlc " + version[2] + ", LibRaw " + version[3] + ", LibWebp " + version[4] + ".";
            }
            else
            {
                this.label2.Text = "Version : " + version[5] + "\n\nDeveloped by Sacha Himber\n\nSky multi is a free application that allows you to read\n" +
                    "videos, audios and pictures it can play a great\nnumber of file format.\n\nLibraries used : Net " + version[0] + ", Sky Framework " + version[1] +
                    ",\nLibVlc " + version[2] + ", LibRaw " + version[3] + ", LibWebp " + version[4] + ".";
            }
            // 
            // rectangle3
            // 
            this.rectangle3.Border = true;
            this.rectangle3.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.rectangle3.BorderRadius = 10;
            this.rectangle3.BorderWidth = 3;
            this.rectangle3.Controls.Add(this.FileInfo);
            this.rectangle3.Location = new System.Drawing.Point(368, 29);
            this.rectangle3.Name = "rectangle3";
            this.rectangle3.Size = new System.Drawing.Size(298, 158);
            this.rectangle3.TabIndex = 8;
            this.rectangle3.Text = "rectangle3";
            // 
            // FileInfo
            // 
            this.FileInfo.AutoSize = true;
            this.FileInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FileInfo.Location = new System.Drawing.Point(3, 10);
            this.FileInfo.Name = "FileInfo";
            this.FileInfo.Size = new System.Drawing.Size(116, 135);
            this.FileInfo.TabIndex = 9;
            if (language == Language.French)
            {
                this.FileInfo.Text = "Nom du fichier :\r\n\r\nExtention du fichier :\r\n\r\nChemin du fichier :\r\n\r\nTaille du fi" +
                   "chier :\r\n\r\nDate de creation :";
            }
            else
            {
                this.FileInfo.Text = "File Name :\r\n\r\nFile extension :\r\n\r\nFile path :\r\n\r\nFile size :\r\n\r\nCreation date :";
            }
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(118, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 33);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sky multi";
            // 
            // rectangle4
            // 
            this.rectangle4.Border = true;
            this.rectangle4.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.rectangle4.BorderRadius = 10;
            this.rectangle4.BorderWidth = 3;
            this.rectangle4.Location = new System.Drawing.Point(368, 193);
            this.rectangle4.Name = "rectangle4";
            this.rectangle4.Size = new System.Drawing.Size(298, 166);
            this.rectangle4.TabIndex = 10;
            this.rectangle4.Text = "rectangle4";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(370, 198);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 156);
            this.panel1.TabIndex = 11;
            // 
            // InformationDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ButtonMaximizedVisible = false;
            this.ClientSize = new System.Drawing.Size(660, 368);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rectangle4);
            this.Controls.Add(this.rectangle3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rectangle2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rectangle1);
            this.Controls.Add(this.buttonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "InformationDialog";
            this.Redimensionnable = false;
            this.Text = "Sky multi - Information";
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.rectangle1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.rectangle2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.rectangle3, 0);
            this.Controls.SetChildIndex(this.rectangle4, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.rectangle3.ResumeLayout(false);
            this.rectangle3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string EspaceNombre(double element)
        {
            string elementString = element.ToString();
            List<char> chaineString = new List<char>();

            foreach (char i in elementString)
            {
                chaineString.Add(i);
            }

            int index = 0;
            chaineString.Reverse();
            List<char> chaineModif = new List<char>();

            foreach (char i in chaineString)
            {
                if (index == 3)
                {
                    chaineModif.Add(' ');
                    index = 0;
                }
                chaineModif.Add(i);

                index++;
            }

            chaineModif.Reverse();
            string ARetourner = string.Empty;

            foreach (char i in chaineModif)
            {
                ARetourner += i;
            }

            /* Liberation de la mémoire */
            chaineString.Clear();
            chaineModif.Clear();
            elementString = string.Empty;

            /* Liberation de la mémoire */
            chaineString = null;
            elementString = null;
            chaineModif = null;

            return ARetourner;
        }
    }
}
