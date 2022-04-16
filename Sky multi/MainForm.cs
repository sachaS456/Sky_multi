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
using Sky_UI;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using Sky_Updater;
using Sky_multi_Core.VlcWrapper;
using Sky_multi_Core.ImageReader;

namespace Sky_multi
{
    internal sealed class MainForm : SkyForms
    {
        private Sky_UI.ProgressBar progressBar;
        private Sky_UI.Button buttonPlay;
        private Sky_UI.Button buttonMoreMinute;
        private Sky_UI.Button buttonLessMinute;
        private Sky_UI.Button buttonMore;
        private Sky_UI.Button buttonInfo;
        private Sky_UI.Button buttonFullScreen;
        private Sky_UI.Button buttonSettings;
        private Sky_UI.Button ButtonMediaRight;
        private Sky_UI.Button ButtonMediaLeft;
        private MenuDeroulant MenuDeroulantMore;
        private MenuDeroulant MenuDeroulantExt = new MenuDeroulant();
        private MenuDeroulant menuDeroulantLink;
        private Sky_multi_Viewer.MultiMediaViewer multiMediaViewer;
        private Label label1;
        private Label label2;
        private string MediaLoaded = string.Empty;
        private Panel panel1;
        private bool MousePressedLeft = false;
        private Region Panel1Region = null;
        private Control c = new Control();
        private ImageConvertDialogControl ImageConvertDialogControl = null;
        private SetTimeDialog SetTimeDialog = null;
        private Sky_multi_Viewer.VideoPreview VideoPreview = new Sky_multi_Viewer.VideoPreview();
        private Sky_UI.Button buttonSound = new Sky_UI.Button();
        private Sky_UI.Button buttonReadingSpeed = new Sky_UI.Button();
        private SoundVolumeControl SoundVolumeControl = null;
        private ChoiceSpeed ChoiceSpeed = null;
        private DataSettings DataSettings;
        private bool MediaEnd = false;
        private string subTitlePath = null;
        private bool ButtonMediaMouseEnter = false;

        internal MainForm() : base()
        {
            DataSettings = ReadSettings();
            MenuDeroulantMore = new MenuDeroulant((byte)DataSettings.Language);
            menuDeroulantLink = new MenuDeroulant((byte)DataSettings.Language);

            InitializeComponent();

            this.Location = new Point(Screen.FromControl(this).WorkingArea.Width / 2 - this.Width / 2, Screen.FromControl(this).WorkingArea.Height / 2 - this.Height / 2);
            Panel1Region = panel1.Region;

            multiMediaViewer.HardwareAcceleration = DataSettings.HardwareAcceleration;
            VideoPreview.HardwareAcceleration = DataSettings.HardwareAcceleration;

            string[] Buttons;

            if (DataSettings.Language == Language.French)
            {
                Buttons = new string[10]
                {
                   "Ouvrir un fichier", "Ouvrir un DVD", "Ouvrir un flux web", "Renommer ce fichier", "Supprimer ce fichier", "Copier dans le presse papier", "Capture d'écran", "Lecture Audio et Vidéo",
                   "Lecture Image", "Quitter"
                };
            }
            else
            {
                Buttons = new string[10]
                {
                   "Open file", "Open DVD", "Open a web flux", "Rename this file", "Delete this file", "Copy to clipboard ", "Screenshot", "Audio and Video Playback ",
                   "Reading Image", "Leave"
                };
            }

            MenuDeroulantMore.BackColor = Color.FromArgb(64, 64, 64);
            MenuDeroulantMore.Border = 0;
            MenuDeroulantMore.BorderColor = Color.FromArgb(224, 224, 224);
            MenuDeroulantMore.BorderRadius = 15;
            MenuDeroulantMore.Location = new Point(350, 60);
            MenuDeroulantMore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MenuDeroulantMore.Width = 300;
            //MenuDeroulantMore.SizeHeight = 20;
            //MenuDeroulantMore.SizeWidth = 150;
            MenuDeroulantMore.TabIndex = 13;
            MenuDeroulantMore.Visible = false;
            MenuDeroulantMore.SetButton(Buttons);
            MenuDeroulantMore.AddBar(2);
            MenuDeroulantMore.AddBar(6);
            MenuDeroulantMore.AddBar(8);
            Controls.Add(MenuDeroulantMore);
            this.Controls.SetChildIndex(this.MenuDeroulantMore, 0);

            MenuDeroulantMore.SetButtonClique(0, new MouseEventHandler(ButtonOpenFile_Click));
            MenuDeroulantMore.SetButtonClique(1, new MouseEventHandler(ButtonOpenDVD_Click));
            MenuDeroulantMore.SetButtonClique(2, new MouseEventHandler(ButtonOpenWebFlux_Click));
            MenuDeroulantMore.SetButtonClique(3, new MouseEventHandler(RenameFile_Click));
            MenuDeroulantMore.SetButtonClique(4, new MouseEventHandler(DeleteFile_Click));
            MenuDeroulantMore.SetButtonClique(5, new MouseEventHandler(CopyClipboard));
            MenuDeroulantMore.SetButtonClique(6, new MouseEventHandler(ButtonScreenShot_Click));
            MenuDeroulantMore.SetButtonClique(7, new MouseEventHandler(VideoAudioReader_Click));
            MenuDeroulantMore.SetButtonClique(8, new MouseEventHandler(ImageReader_Click));
            MenuDeroulantMore.SetButtonClique(9, new MouseEventHandler(Quitter_Click));

            menuDeroulantLink.BackColor = Color.FromArgb(64, 64, 64);
            menuDeroulantLink.Border = 0;
            menuDeroulantLink.BorderColor = Color.FromArgb(224, 224, 224);
            menuDeroulantLink.BorderRadius = 15;
            menuDeroulantLink.Anchor = AnchorStyles.None;
            menuDeroulantLink.ShowSide = Side.Top;
            menuDeroulantLink.Width = 300;
            menuDeroulantLink.Visible = false;
            Controls.Add(menuDeroulantLink);
            this.Controls.SetChildIndex(this.menuDeroulantLink, 0);

            MenuDeroulantExt.BackColor = Color.FromArgb(64, 64, 64);
            MenuDeroulantExt.Border = 0;
            MenuDeroulantExt.BorderColor = Color.FromArgb(224, 224, 224);
            MenuDeroulantExt.BorderRadius = 15;
            MenuDeroulantExt.Location = new Point(45, 60);
            MenuDeroulantExt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MenuDeroulantExt.ShowSide = Side.Top;
            MenuDeroulantExt.Width = 300;
            MenuDeroulantExt.TabIndex = 13;
            MenuDeroulantExt.Visible = false;
            Controls.Add(MenuDeroulantExt);
            this.Controls.SetChildIndex(this.MenuDeroulantExt, 0);

            for (int index = 0; index < this.Controls.Count; index++)
            {
                if (this.Controls[index] != MenuDeroulantMore)
                {
                    if (this.Controls[index] != MenuDeroulantExt)
                    {
                        this.Controls[index].MouseDown += new MouseEventHandler(CloseMenuderoulant);
                    }
                }
                else
                {
                    this.Controls[index].MouseDown += new MouseEventHandler(CloseMenuderoulantExt);
                }

                if (this.Controls[index] != menuDeroulantLink)
                {
                    this.Controls[index].MouseDown += new MouseEventHandler(CloseMenuderoulantLink);
                }
            }

            panel1.BringToFront();
            VideoPreview.BringToFront();
            CheckUpdate();
            this.Show();
            vlcControl1_TimeChanged();
            MouseAndButtonShowAndHide();
            WindowState_Changed();
            EndMediaStartAction();

            OpenMediaOnStarting(); // dernière méthode a éxécuter!
            /*System.Threading.Thread threadUpdate = new System.Threading.Thread(CheckUpdate);
            threadUpdate.Priority = System.Threading.ThreadPriority.Highest;
            threadUpdate.Start();*/
        }

        private async void CheckUpdate()
        {
            string CurrentVersion;

            if (Environment.Is64BitProcess)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi setup x64.exe"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi setup x64.exe");
                }

                CurrentVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false).GetValue("DisplayVersion").ToString();
                if (await Sky_Updater.Update.CheckUpdateAsync("Sky multi", CurrentVersion))
                {
                    this.KeyPreview = false;

                    do
                    {
                        await Task.Delay(10);
                    }
                    while (this.Opacity < 1);

                    BlurMainForm();

                    UpdateDetectDialogControl update;
                    if (DataSettings.Language == Language.French)
                    {
                                            update = new UpdateDetectDialogControl((sbyte)DataSettings.Language, CurrentVersion, await Sky_Updater.Update.DownloadStringAsync(
                        "https://serie-sky.netlify.app/Download/Sky multi/Version.txt"), await Sky_Updater.Update.DownloadStringAsync(
                            "https://serie-sky.netlify.app/Download/Sky multi/ReleaseNoteFR.txt"));
                    }
                    else
                    {
                                            update = new UpdateDetectDialogControl((sbyte)DataSettings.Language, CurrentVersion, await Sky_Updater.Update.DownloadStringAsync(
                        "https://serie-sky.netlify.app/Download/Sky multi/Version.txt"), await Sky_Updater.Update.DownloadStringAsync(
                            "https://serie-sky.netlify.app/Download/Sky multi/ReleaseNoteEN.txt"));
                    }

                    update.BringToFront();
                    update.Location = new Point(this.Width / 2 - update.Width / 2, this.Height / 2 - update.Height / 2);
                    update.Anchor = AnchorStyles.None;
                    update.ButtonUpdate += new EventBoolHandler(ActionButtonUpdate);
                    this.Controls.Add(update);
                    update.BringToFront();
                }
            }
            else
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi setup x86.exe"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi setup x86.exe");
                }

                CurrentVersion = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false).GetValue("DisplayVersion").ToString();
                if (await Sky_Updater.Update.CheckUpdateAsync("Sky multi", CurrentVersion))
                {
                    this.KeyPreview = false;

                    do
                    {
                        await Task.Delay(10);
                    }
                    while (this.Opacity < 1);

                    BlurMainForm();

                    UpdateDetectDialogControl update;
                    if (DataSettings.Language == Language.French)
                    {
                                            update = new UpdateDetectDialogControl((sbyte)DataSettings.Language, CurrentVersion, await Sky_Updater.Update.DownloadStringAsync(
                        "https://serie-sky.netlify.app/Download/Sky multi/Version.txt"), await Sky_Updater.Update.DownloadStringAsync(
                            "https://serie-sky.netlify.app/Download/Sky multi/ReleaseNoteFR.txt"));
                    }
                    else
                    {
                                            update = new UpdateDetectDialogControl((sbyte)DataSettings.Language, CurrentVersion, await Sky_Updater.Update.DownloadStringAsync(
                        "https://serie-sky.netlify.app/Download/Sky multi/Version.txt"), await Sky_Updater.Update.DownloadStringAsync(
                            "https://serie-sky.netlify.app/Download/Sky multi/ReleaseNoteEN.txt"));
                    }

                    update.BringToFront();
                    update.Location = new Point(this.Width / 2 - update.Width / 2, this.Height / 2 - update.Height / 2);
                    update.Anchor = AnchorStyles.None;
                    update.ButtonUpdate += new EventBoolHandler(ActionButtonUpdate);
                    this.Controls.Add(update);
                    update.BringToFront();
                }
            }
        }

        private void ActionButtonUpdate(bool Download)
        {
            foreach (UpdateDetectDialogControl i in this.Controls.OfType<UpdateDetectDialogControl>())
            {
                i.Dispose();
                this.Controls.Remove(i);
            }

            if (Download == true)
            {
                DownloadUpdaterDialog update = new DownloadUpdaterDialog((sbyte)DataSettings.Language, "Sky multi");
                update.BringToFront();
                update.Location = new Point(this.Width / 2 - update.Width / 2, this.Height / 2 - update.Height / 2);
                update.Anchor = AnchorStyles.None;
                this.Controls.Add(update);
                update.BringToFront();
            }
            else
            {
                this.KeyPreview = true;
                UnBlurMainForm();
            }
        }

        private async void OpenMediaOnStarting()
        {
            if (Environment.GetCommandLineArgs().Last() != Application.ExecutablePath && Environment.GetCommandLineArgs().Last() != Application.StartupPath + @"Sky multi.dll")
            {
                while (multiMediaViewer.ControlLoaded == false || this.Opacity < 1)
                {
                    await Task.Delay(1);

                    if (multiMediaViewer.ControlLoaded == true && this.Opacity >= 1)
                    {
                        break;
                    }
                }

                if (File.Exists(Environment.GetCommandLineArgs().Last()))
                {
                    multiMediaViewer.OpenFile(Environment.GetCommandLineArgs().Last());
                    MediaLoaded = Environment.GetCommandLineArgs().Last();
                    MediaEnd = false;
                    this.Text = "Sky multi - " + MediaLoaded;
                }
                else
                {
                    if (DataSettings.Language == Language.French)
                    {
                        MessageBox.Show("Une erreur est survenue, le fichier spécifié n'existe pas!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred, the specified file does not exist!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CloseMenuderoulant(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MenuDeroulantMore.Hide();
                MenuDeroulantExt.Hide();

                if (SoundVolumeControl != null)
                {
                    SoundVolumeControl.HideDispose();
                    SoundVolumeControl = null;
                }

                if (ChoiceSpeed != null)
                {
                    ChoiceSpeed.HideDispose();
                    ChoiceSpeed = null;
                }
            }
        }

        private void CloseMenuderoulantExt(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MenuDeroulantExt.Hide();
            }
        }

        private void CloseMenuderoulantLink(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                menuDeroulantLink.Hide();
            }
        }

        private async void MouseAndButtonShowAndHide()
        {
            int X;
            int Y;
            bool MouseShow = true;

            while (multiMediaViewer != null && this != null)
            {
                X = Cursor.Position.X;
                Y = Cursor.Position.Y;

                await Task.Delay(100);

                if (this.WindowState == FormWindowState.Minimized)
                {
                    continue;
                }

                if (Cursor.Position.X != X || Cursor.Position.Y != Y || ButtonMediaMouseEnter)
                {
                    if (MouseShow == false)
                    {
                        Cursor.Show();
                    }

                    MouseShow = true;

                    buttonFullScreen.Visible = true;
                    buttonInfo.Visible = true;
                    buttonMore.Visible = true;
                    buttonSettings.Visible = true;

                    while (buttonFullScreen.Height < 30)
                    {
                        buttonFullScreen.Height += 5;
                        buttonInfo.Height += 5;
                        buttonMore.Height += 5;
                        buttonSettings.Height += 5;

                        ButtonMediaRight.Location = new Point(ButtonMediaRight.Location.X - 6, ButtonMediaRight.Location.Y);
                        ButtonMediaRight.Width += 5;
                        ButtonMediaLeft.Width += 5;

                        await Task.Delay(10);
                    }

                    buttonFullScreen.Height = 30;
                    buttonInfo.Height = 30;
                    buttonMore.Height = 30;
                    buttonSettings.Height = 30;
                    ButtonMediaRight.Width = 40;
                    ButtonMediaRight.Location = new Point(this.Width - ButtonMediaRight.Width - Border, ButtonMediaRight.Location.Y);
                    ButtonMediaLeft.Width = 40;

                    while (Cursor.Position.X != X || Cursor.Position.Y != Y || ButtonMediaMouseEnter)
                    {
                        X = Cursor.Position.X;
                        Y = Cursor.Position.Y;

                        await Task.Delay(400);

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y || ButtonMediaMouseEnter)
                        {
                            continue;
                        }

                        await Task.Delay(400);

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y || ButtonMediaMouseEnter)
                        {
                            continue;
                        }

                        await Task.Delay(400);

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y || ButtonMediaMouseEnter)
                        {
                            continue;
                        }

                        await Task.Delay(400);

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y || ButtonMediaMouseEnter)
                        {
                            continue;
                        }

                        await Task.Delay(400);

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y || ButtonMediaMouseEnter)
                        {
                            continue;
                        }

                        // 2s
                    }
                }
                else
                {
                    if (MouseShow == true)
                    {
                        Cursor.Hide();
                        MouseShow = false;

                        while (ButtonMediaRight.Width > 0)
                        {
                            if (buttonFullScreen.Height > 0)
                            {
                                buttonFullScreen.Height -= 5;
                                buttonInfo.Height -= 5;
                                buttonMore.Height -= 5;
                                buttonSettings.Height -= 5;
                            }

                            ButtonMediaRight.Width -= 5;
                            ButtonMediaRight.Location = new Point(ButtonMediaRight.Location.X + 5, ButtonMediaRight.Location.Y);
                            ButtonMediaLeft.Width -= 5;

                            await Task.Delay(10);
                        }

                        buttonFullScreen.Height = 0;
                        buttonInfo.Height = 0;
                        buttonMore.Height = 0;
                        buttonSettings.Height = 0;
                        ButtonMediaRight.Width = 0;
                        ButtonMediaRight.Location = new Point(this.Width - Border, ButtonMediaRight.Location.Y);
                        ButtonMediaLeft.Width = 0;

                        buttonFullScreen.Visible = false;
                        buttonInfo.Visible = false;
                        buttonMore.Visible = false;
                        buttonSettings.Visible = false;
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.multiMediaViewer = new Sky_multi_Viewer.MultiMediaViewer();
            this.progressBar = new Sky_UI.ProgressBar();
            this.buttonPlay = new Sky_UI.Button();
            this.buttonMoreMinute = new Sky_UI.Button();
            this.buttonLessMinute = new Sky_UI.Button();
            this.buttonMore = new Sky_UI.Button();
            this.buttonInfo = new Sky_UI.Button();
            this.buttonFullScreen = new Sky_UI.Button();
            this.buttonSettings = new Sky_UI.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonMediaRight = new Sky_UI.Button();
            this.ButtonMediaLeft = new Sky_UI.Button();
            ((System.ComponentModel.ISupportInitialize)(this.multiMediaViewer)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vlcControl1
            // 
            this.multiMediaViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiMediaViewer.BackColor = System.Drawing.Color.Black;
            this.multiMediaViewer.Location = new System.Drawing.Point(3, 20);
            this.multiMediaViewer.Name = "vlcControl1";
            this.multiMediaViewer.Size = new System.Drawing.Size(652, 382);
            this.multiMediaViewer.Spu = -1;
            this.multiMediaViewer.TabIndex = 2;
            this.multiMediaViewer.Text = "vlcControl1";
            this.multiMediaViewer.VlcLibDirectory = null;
            this.multiMediaViewer.VlcMediaplayerOptions = null;
            this.multiMediaViewer.VlcLibDirectoryNeeded += new System.EventHandler<Sky_multi_Viewer.VlcLibDirectoryNeededEventArgs>(this.vlcControl1_VlcLibDirectoryNeeded);
            this.multiMediaViewer.Paused += new System.EventHandler<VlcMediaPlayerPausedEventArgs>(this.vlcControl1_Paused);
            this.multiMediaViewer.Playing += new System.EventHandler<VlcMediaPlayerPlayingEventArgs>(this.vlcControl1_Playing);
            this.multiMediaViewer.EndReached += new EventHandler<VlcMediaPlayerEndReachedEventArgs>(this.vlcControl1_EndReached);
            this.multiMediaViewer.ItIsPicture += new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsPicture);
            this.multiMediaViewer.ItIsAudioOrVideo += new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsAudioOrVideo);
            this.multiMediaViewer.MouseClick += new MouseEventHandler(multiMediaViewer_MouseClick);
            this.multiMediaViewer.MouseDoubleClick += new MouseEventHandler(multiMediaViewer_MouseDoubleClick);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.progressBar.Color = System.Drawing.Color.Orange;
            this.progressBar.Location = new System.Drawing.Point(15, 5);
            this.progressBar.MouseClick = null;
            this.progressBar.MouseDown = null;
            this.progressBar.MouseMove = null;
            this.progressBar.MouseUp = null;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(621, 7);
            this.progressBar.TabIndex = 5;
            this.progressBar.Text = "progressBar1";
            this.progressBar.ValuePourcentages = 100;
            this.progressBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBar_MouseDown);
            this.progressBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.progressBar_MouseMove);
            this.progressBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.progressBar_MouseUp);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonPlay")));
            this.buttonPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonPlay.Border = false;
            this.buttonPlay.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonPlay.Location = new System.Drawing.Point(302, 16);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(50, 50);
            this.buttonPlay.TabIndex = 6;
            this.buttonPlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonMoreMinute
            // 
            this.buttonMoreMinute.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonMoreMinute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonMoreMinute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonMoreMinute")));
            this.buttonMoreMinute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoreMinute.Border = false;
            this.buttonMoreMinute.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonMoreMinute.Location = new System.Drawing.Point(386, 29);
            this.buttonMoreMinute.Name = "buttonMoreMinute";
            this.buttonMoreMinute.Size = new System.Drawing.Size(25, 25);
            this.buttonMoreMinute.TabIndex = 7;
            this.buttonMoreMinute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonMoreMinute.Click += new System.EventHandler(this.buttonMoreMinute_Click);
            // 
            // buttonLessMinute
            // 
            this.buttonLessMinute.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonLessMinute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonLessMinute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonLessMinute")));
            this.buttonLessMinute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonLessMinute.Border = false;
            this.buttonLessMinute.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonLessMinute.Location = new System.Drawing.Point(251, 29);
            this.buttonLessMinute.Name = "buttonLessMinute";
            this.buttonLessMinute.Size = new System.Drawing.Size(25, 25);
            this.buttonLessMinute.TabIndex = 8;
            this.buttonLessMinute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonLessMinute.Click += new System.EventHandler(this.buttonLessMinute_Click);
            // 
            // buttonMore
            // 
            this.buttonMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonMore.Border = true;
            this.buttonMore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonMore.Location = new System.Drawing.Point(615, 26);
            this.buttonMore.Name = "buttonMore";
            this.buttonMore.Size = new System.Drawing.Size(32, 32);
            this.buttonMore.borderRadius = 15;
            this.buttonMore.TabIndex = 9;
            this.buttonMore.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonMore")));
            this.buttonMore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonMore.BorderColor = Color.Black;
            this.buttonMore.BorderSize = 2;
            this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonInfo.Border = true;
            this.buttonInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonInfo.Location = new System.Drawing.Point(579, 26);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(32, 32);
            this.buttonInfo.borderRadius = 15;
            this.buttonInfo.TabIndex = 10;
            this.buttonInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonInfo")));
            this.buttonInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonInfo.BorderColor = Color.Black;
            this.buttonInfo.BorderSize = 2;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonFullScreen
            // 
            this.buttonFullScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonFullScreen.Border = true;
            this.buttonFullScreen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonFullScreen.Location = new System.Drawing.Point(13, 26);
            this.buttonFullScreen.Name = "buttonFullScreen";
            this.buttonFullScreen.Size = new System.Drawing.Size(32, 32);
            this.buttonFullScreen.borderRadius = 15;
            this.buttonFullScreen.TabIndex = 12;
            this.buttonFullScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonFullScreen")));
            this.buttonFullScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonFullScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonFullScreen.BorderColor = Color.Black;
            this.buttonFullScreen.BorderSize = 2;
            this.buttonFullScreen.Click += new System.EventHandler(this.buttonFullScreen_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSettings.Border = true;
            this.buttonSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonSettings.Location = new System.Drawing.Point(49, 26);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(32, 32);
            this.buttonSettings.borderRadius = 15;
            this.buttonSettings.TabIndex = 11;
            this.buttonSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonSettings")));
            this.buttonSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonSettings.BorderColor = Color.Black;
            this.buttonSettings.BorderSize = 2;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(568, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "0h0min0s";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(5, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 30);
            this.label2.TabIndex = 14;
            if (DataSettings.Language == Language.French)
            {
                this.label2.Text = "Durée restante :\r\n0h0min0s";
            }
            else
            {
                this.label2.Text = "Remaining time :\r\n0h0min0s";
            }
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.buttonPlay);
            this.panel1.Controls.Add(this.buttonMoreMinute);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.buttonLessMinute);
            this.panel1.Controls.Add(this.buttonSound);
            this.panel1.Controls.Add(this.buttonReadingSpeed);
            this.panel1.Location = new System.Drawing.Point(3, 405);
            this.panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 71);
            this.panel1.TabIndex = 15;
            // 
            // ButtonMediaRight
            // 
            this.ButtonMediaRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ButtonMediaRight.Border = true;
            this.ButtonMediaRight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ButtonMediaRight.Location = new System.Drawing.Point(615, 150);
            this.ButtonMediaRight.Name = "buttonSettings";
            this.ButtonMediaRight.Size = new System.Drawing.Size(42, 102);
            this.ButtonMediaRight.borderRadius = 15;
            this.ButtonMediaRight.TabIndex = 11;
            this.ButtonMediaRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ButtonMediaRight.Anchor = AnchorStyles.Right;
            this.ButtonMediaRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NextMedia")));
            this.ButtonMediaRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonMediaRight.BorderColor = Color.Black;
            this.ButtonMediaRight.BorderSize = 2;
            this.ButtonMediaRight.Click += new System.EventHandler(this.ButtonMediaRight_Click);
            this.ButtonMediaRight.MouseEnter += new EventHandler(this.ButtonMediaRight_MouseEnter);
            this.ButtonMediaRight.MouseLeave += new EventHandler(this.ButtonMediaRight_MouseLeave);
            // 
            // ButtonMediaLeft
            // 
            this.ButtonMediaLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ButtonMediaLeft.Border = true;
            this.ButtonMediaLeft.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ButtonMediaLeft.Location = new System.Drawing.Point(3, 150);
            this.ButtonMediaLeft.Name = "buttonSettings";
            this.ButtonMediaLeft.Size = new System.Drawing.Size(42, 102);
            this.ButtonMediaLeft.borderRadius = 15;
            this.ButtonMediaLeft.TabIndex = 11;
            this.ButtonMediaLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ButtonMediaLeft.Anchor = AnchorStyles.Left;
            this.ButtonMediaLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackMedia")));
            this.ButtonMediaLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonMediaLeft.BorderColor = Color.Black;
            this.ButtonMediaLeft.BorderSize = 2;
            this.ButtonMediaLeft.Click += new System.EventHandler(this.ButtonMediaLeft_Click);
            this.ButtonMediaLeft.MouseEnter += new EventHandler(this.ButtonMediaLeft_MouseEnter);
            this.ButtonMediaLeft.MouseLeave += new EventHandler(this.ButtonMediaLeft_MouseLeave);
            // 
            // VideoPreview
            // 
            this.VideoPreview.Resize += new EventHandler(VideoPreview_Resize);
            this.VideoPreview.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.VideoPreview.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.VideoPreview.Location = new System.Drawing.Point(15, 325);
            this.VideoPreview.Name = "VideoPreview";
            this.VideoPreview.Size = new System.Drawing.Size(128, 86);
            this.VideoPreview.TabIndex = 2;
            this.VideoPreview.Visible = false;
            // 
            // buttonSound
            // 
            this.buttonSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonSound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSound.Border = false;
            this.buttonSound.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonSound.Location = new System.Drawing.Point(445, 29);
            this.buttonSound.Name = "buttonInfo";
            this.buttonSound.Size = new System.Drawing.Size(25, 25);
            this.buttonSound.TabIndex = 10;
            this.buttonSound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonSound.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonSound")));
            this.buttonSound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSound.Click += new System.EventHandler(this.buttonSound_Click);
            // 
            // buttonReadingSpeed
            // 
            this.buttonReadingSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonReadingSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonReadingSpeed.Border = false;
            this.buttonReadingSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonReadingSpeed.Location = new System.Drawing.Point(201, 29);
            this.buttonReadingSpeed.Name = "buttonInfo";
            this.buttonReadingSpeed.Size = new System.Drawing.Size(25, 25);
            this.buttonReadingSpeed.TabIndex = 10;
            this.buttonReadingSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonReadingSpeed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonReadingSpeed")));
            this.buttonReadingSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonReadingSpeed.Click += new System.EventHandler(this.buttonReadingSpeed_Click);
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.buttonFullScreen);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.buttonMore);
            this.Controls.Add(this.multiMediaViewer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ButtonMediaRight);
            this.Controls.Add(this.ButtonMediaLeft);
            this.Controls.Add(this.VideoPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Sky multi";
            this.MinimumSize = new Size(550, 400);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(This_KeyDown);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.multiMediaViewer, 0);
            this.Controls.SetChildIndex(this.buttonMore, 0);
            this.Controls.SetChildIndex(this.buttonInfo, 0);
            this.Controls.SetChildIndex(this.buttonSettings, 0);
            this.Controls.SetChildIndex(this.buttonFullScreen, 0);
            this.Controls.SetChildIndex(this.ButtonMediaRight, 0);
            this.Controls.SetChildIndex(this.ButtonMediaLeft, 0);
            this.Controls.SetChildIndex(this.VideoPreview, 0);
            this.ClientSize = new System.Drawing.Size(658, 480);
            ((System.ComponentModel.ISupportInitialize)(this.multiMediaViewer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void This_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (e.Control && e.KeyCode == Keys.O)
            {
                OpenFile();
                return;
            }

            if (e.Control && e.KeyCode == Keys.Q)
            {
                this.Close();
                return;
            }

            if (e.Control && e.KeyCode == Keys.R)
            {
                Settings();
                return;
            }

            if (e.Control && e.KeyCode == Keys.I)
            {
                ShowInformationDialog();
                return;
            }

            if (e.Control && e.KeyCode == Keys.T && multiMediaViewer.ItIsAImage == false && MediaLoaded != string.Empty)
            {
                SetTimeDialog = new SetTimeDialog(multiMediaViewer.Time, ref MediaLoaded, GetVideo() && DataSettings.VideoPreviewOnSetTime);
                SetTimeDialog.TimeDefined = new EventSetTimeHandler(TimeSet);
                SetTimeDialog.Show();
                return;
            }

            if (e.Control && e.KeyCode == Keys.Left)
            {
                MultimediaLeft();
                return;
            }

            if (e.Control && e.KeyCode == Keys.Right)
            {
                MultimediaRight();
                return;
            }

            if (e.KeyCode == Keys.F11)
            {
                FullScreen_M();
                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (multiMediaViewer.Location == new Point(0, 0))
                {
                    FullScreen_M();
                }
                return;
            }

            if (e.Alt && e.KeyCode == Keys.Left && multiMediaViewer.ItIsAImage == false && MediaLoaded != string.Empty)
            {
                multiMediaViewer.Time -= 60000;
                return;
            }

            if (e.Alt && e.KeyCode == Keys.Right && multiMediaViewer.ItIsAImage == false && MediaLoaded != string.Empty)
            {
                multiMediaViewer.Time += 60000;
                return;
            }

            if (e.KeyCode == Keys.Space)
            {
                if (MediaLoaded != string.Empty && multiMediaViewer.ItIsAImage == false)
                {
                    multiMediaViewer.Pause();
                }

                return;
            }

            if (e.KeyCode == Keys.MediaPlayPause)
            {
                if (MediaLoaded != string.Empty && multiMediaViewer.ItIsAImage == false)
                {
                    multiMediaViewer.Pause();
                }

                return;
            }

            if (e.KeyCode == Keys.MediaStop && multiMediaViewer.ItIsAImage == false)
            {
                multiMediaViewer.Stop();
                MediaLoaded = string.Empty;
                return;
            }

            if (e.KeyCode == Keys.MediaPreviousTrack)
            {
                MultimediaLeft();
                return;
            }

            if (e.KeyCode == Keys.MediaNextTrack)
            {
                MultimediaRight();
                return;
            }
        }

        private async void WindowState_Changed()
        {
            while (this != null)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    if (DataSettings.MinimizeWindowsMediaPause == true)
                    {
                        multiMediaViewer.SetPause(true);
                    }
                }

                await Task.Delay(10);
            }
        }

        private void multiMediaViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataSettings.CliqueMadiaPause == true && e.Button == MouseButtons.Left)
            {
                multiMediaViewer.Pause();
            }

            if (e.Button == MouseButtons.Right && string.IsNullOrEmpty(MediaLoaded) == false)
            {
                string[] Buttons;

                if (multiMediaViewer.ItIsAImage == false)
                {
                    if (DataSettings.Language == Language.French)
                    {
                        Buttons = new string[5]
                        {
                   "Aller à un temps spécifié", "Rapport largeur/hauteur de la vidéo", "Piste(s) vidéo", "Piste(s) audio", "Piste(s) de sous titres"
                        };
                    }
                    else
                    {
                        Buttons = new string[5]
                        {
                   "Go to a specified time", "Video aspect ratio", "Video track(s)", "Audio track(s)", "Sub titles track(s)"
                        };
                    }
                }
                else
                {
                    if (DataSettings.Language == Language.French)
                    {
                        Buttons = new string[5]
                        {
                   "Imprimer", "Convertir image", "Définir en tant qu'arrière plan", "Faire pivoter l'image", "Modifier l'image"
                        };
                    }
                    else
                    {
                        Buttons = new string[5]
                        {
                   "Print", "Convert image", "Set as background", "Rotate image", "Edit image"
                        };
                    }
                }

                menuDeroulantLink.Location = e.Location;
                menuDeroulantLink.SetButton(Buttons);
                if (e.Y + 20 * menuDeroulantLink.NbButton > multiMediaViewer.Height)
                {
                    menuDeroulantLink.ShowSide = Side.Bottom;
                }
                else
                {
                    menuDeroulantLink.ShowSide = Side.Top;
                }

                if (e.X + menuDeroulantLink.Width >= multiMediaViewer.Width - 1)
                {
                    menuDeroulantLink.Location = new Point(multiMediaViewer.Width - 301, menuDeroulantLink.Location.Y);
                }

                if (multiMediaViewer.ItIsAImage == false)
                {
                    menuDeroulantLink.SetButtonClique(0, new MouseEventHandler(SpecifiqueTime));
                    menuDeroulantLink.SetButtonClique(1, new MouseEventHandler(RatioMDLink));
                    menuDeroulantLink.SetButtonClique(2, new MouseEventHandler(VideoTracksMDLink));
                    menuDeroulantLink.SetButtonClique(3, new MouseEventHandler(AudioTracksMDLink));
                    menuDeroulantLink.SetButtonClique(4, new MouseEventHandler(SubtitlesTracksMDLink));
                }
                else
                {
                    menuDeroulantLink.SetButtonClique(0, new MouseEventHandler(Print));
                    menuDeroulantLink.SetButtonClique(1, new MouseEventHandler(ConvertImage));
                    menuDeroulantLink.SetButtonClique(2, new MouseEventHandler(DefineBackground));
                    menuDeroulantLink.SetButtonClique(3, new MouseEventHandler(RotateImage));
                    menuDeroulantLink.SetButtonClique(4, new MouseEventHandler(EditImage));
                }

                menuDeroulantLink.Show();
                menuDeroulantLink.BringToFront();
            }
        }

        private void buttonSound_Click(object sender, EventArgs e)
        {
            if (SoundVolumeControl == null)
            {
                SoundVolumeControl = new SoundVolumeControl(multiMediaViewer.Audio.Volume, multiMediaViewer.Audio.IsMute, DataSettings.Language);
                SoundVolumeControl.EventSoundSet += new EventSoundSetHandler(SoundVolumeControl_EventSoundSet);
                SoundVolumeControl.EventMute += new EventMutedHandler(SoundVolumeControl_EventMute);
                SoundVolumeControl.Location = new Point(panel1.Location.X + buttonSound.Location.X - SoundVolumeControl.Width / 2, panel1.Location.Y - 5);
                SoundVolumeControl.Anchor = AnchorStyles.Bottom;
                SoundVolumeControl.BringToFront();
                this.Controls.Add(SoundVolumeControl);
                SoundVolumeControl.Show();
            }
            else
            {
                SoundVolumeControl.HideDispose();
                SoundVolumeControl = null;
            }
        }

        private void SoundVolumeControl_EventSoundSet(int Volume)
        {
            multiMediaViewer.Audio.Volume = Volume;
        }

        private void SoundVolumeControl_EventMute(bool IsMute)
        {
            multiMediaViewer.Audio.IsMute = IsMute;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            if (IsMute == true)
            {
                this.buttonSound.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonSoundMute")));
            }
            else
            {
                this.buttonSound.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonSound")));
            }

            resources = null;
        }

        private void buttonReadingSpeed_Click(object sender, EventArgs e)
        {
            if (ChoiceSpeed == null)
            {
                ChoiceSpeed = new ChoiceSpeed(multiMediaViewer.Rate);
                ChoiceSpeed.EventSpeedChanged += new EventSpeedHandler(ChoiceSpeed_EventSpeedChanged);
                ChoiceSpeed.Location = new Point(panel1.Location.X + buttonReadingSpeed.Location.X - ChoiceSpeed.Width / 2, panel1.Location.Y - 5);
                ChoiceSpeed.Anchor = AnchorStyles.Bottom;
                ChoiceSpeed.BringToFront();
                this.Controls.Add(ChoiceSpeed);
                ChoiceSpeed.Show();
            }
            else
            {
                ChoiceSpeed.HideDispose();
                ChoiceSpeed = null;
            }
        }

        private void ChoiceSpeed_EventSpeedChanged(ref float Rate, ref bool SpeedMore)
        {
            if (SpeedMore == true)
            {
                multiMediaViewer.Rate = 1 * Rate; // multiplie
            }
            else
            {
                multiMediaViewer.Rate = 1 / Rate; // divise
            }
        }

        private void VideoPreview_Resize(object sender, EventArgs e)
        {
            IntPtr handle = Win32.CreateRoundRectRgn(0, 0, VideoPreview.Width, VideoPreview.Height, 15, 15);

            if (handle != IntPtr.Zero)
            {
                VideoPreview.Region = Region.FromHrgn(handle);
                Win32.DeleteObject(handle);
            }
        }

        private async void multiMediaViewer_ItIsPicture(bool DifferentBackMedia)
        {
            if (DifferentBackMedia == true)
            {
                MenuDeroulantMore.MainPage();

                if (this.FullScreen == true)
                {
                    while (panel1.Height > 0)
                    {
                        panel1.Height -= 10;
                        panel1.Location = new Point(panel1.Location.X, panel1.Location.Y + 10);
                        await Task.Delay(10);
                    }

                    panel1.Height = 0;
                    panel1.Location = new Point(panel1.Location.X, this.Height - this.Border);
                    panel1.Visible = false;
                }
                else
                {
                    while (panel1.Height > 0)
                    {
                        panel1.Height -= 10;
                        panel1.Location = new Point(panel1.Location.X, panel1.Location.Y + 10);
                        multiMediaViewer.Height += 10;
                        await Task.Delay(10);
                    }

                    panel1.Height = 0;
                    panel1.Location = new Point(panel1.Location.X, this.Height - this.Border);
                    panel1.Visible = false;
                    multiMediaViewer.Height = this.Height - this.Border * 2 - 20;
                }

                multiMediaViewer.BackColor = Color.FromArgb(30, 30, 30);
            }
        }

        private async void multiMediaViewer_ItIsAudioOrVideo(bool DifferentBackMedia)
        {
            if (DifferentBackMedia == true)
            {
                MenuDeroulantMore.MainPage();
                panel1.Visible = true;

                if (this.FullScreen == true)
                {
                    while (multiMediaViewer.ItIsAImage == true)
                    {
                        await Task.Delay(1);
                    }

                    panel1.Location = new Point(this.Width / 2 - panel1.Width / 2, this.Height);
                    panel1.Height = 71;

                    IntPtr handle = Sky_UI.Win32.CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 20, 20);

                    if (handle != IntPtr.Zero)
                    {
                        panel1.Region = Region.FromHrgn(handle);
                        Sky_UI.Win32.DeleteObject(handle);
                    }

                    ShowAndHideProgressBar();
                }
                else
                {
                    int multiMediaViewerHeight = multiMediaViewer.Height;

                    while (panel1.Height < 70)
                    {
                        panel1.Height += 10;
                        panel1.Location = new Point(panel1.Location.X, panel1.Location.Y - 10);
                        multiMediaViewer.Height -= 10;
                        await Task.Delay(10);
                    }

                    panel1.Height = 71;
                    multiMediaViewer.Height = multiMediaViewerHeight - 71;
                    panel1.Location = new Point(panel1.Location.X, multiMediaViewer.Height + multiMediaViewer.Location.Y + 3);
                }

                multiMediaViewer.BackColor = Color.Black;
            }

            if (DataSettings.WhenVideoSetFullScreen == true && GetVideo() == true && FullScreen == false)
            {
                await Task.Delay(300);
                FullScreen_M();
            }
        }

        private void ButtonMediaRight_Click(object sender, EventArgs e)
        {
            if (MediaLoaded == string.Empty)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Veuillez charger un Média!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please load a Media!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            MultimediaRight();
        }

        private void ButtonMediaLeft_Click(object sender, EventArgs e)
        {
            if (MediaLoaded == string.Empty)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Veuillez charger un Média!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please load a Media!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            MultimediaLeft();
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Sky_multi_Viewer.VlcLibDirectoryNeededEventArgs e)
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

        private void FullScreen_M()
        {
            this.FullScreen = !this.FullScreen;

            if (this.FullScreen == true) // mettre en plein écran
            {
                multiMediaViewer.Location = new Point(0, 0);
                multiMediaViewer.Size = this.Size;

                if (DataSettings.UsingDefinitionMax == true && Sky_multi_Core.ResolutionMonitor.SetResolutionMax() == true)
                {
                    this.FullScreen = true;
                    if (DataSettings.Language == Language.French)
                    {
                        MessageBox.Show("Sky multi a changé de définition pour une meilleur qualité d'image.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sky multi has changed the definition for better picture quality.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (panel1.Width >= 1024 - this.Border / 2)
                {
                    panel1.Width = panel1.Width / 2;
                }

                panel1.Location = new Point(this.Width / 2 - panel1.Width / 2, panel1.Location.Y + panel1.Height + this.Border + 1);

                IntPtr handle = Sky_UI.Win32.CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 20, 20);

                if (handle != IntPtr.Zero)
                {
                    panel1.Region = Region.FromHrgn(handle);
                    Sky_UI.Win32.DeleteObject(handle);
                }

                if (multiMediaViewer.ItIsAImage == false)
                {
                    ShowAndHideProgressBar();
                }
            }
            else // enlever le plein écran
            {
                this.ClientSize = new Size(658, 480);
                multiMediaViewer.Location = new Point(3, 20);

                if (multiMediaViewer.ItIsAImage == false)
                {
                    multiMediaViewer.Size = new Size(652, 382);
                    panel1.Location = new Point(3, 405);
                    panel1.Size = new Size(651, 71);
                }
                else
                {
                    multiMediaViewer.Size = new Size(652, 453);
                    panel1.Location = new Point(3, 476);
                    panel1.Size = new Size(651, 0);
                }

                panel1.Region = Panel1Region;
            }
        }

        private void buttonFullScreen_Click(object sender, EventArgs e)
        {
            FullScreen_M();
        }

        private async void ShowAndHideProgressBar()
        {
            int X;
            int Y;
            int PanelY = panel1.Location.Y;

            while (this.FullScreen == true && multiMediaViewer.ItIsAImage == false)
            {
                X = Cursor.Position.X;
                Y = Cursor.Position.Y;

                await Task.Delay(100);

                if (Cursor.Position.X != X || Cursor.Position.Y != Y)
                {

                    while (panel1.Location.Y > PanelY - panel1.Height)
                    {
                        if (this.FullScreen == false)
                        {
                            return;
                        }

                        if (multiMediaViewer.ItIsAImage == true)
                        {
                            return;
                        }

                        panel1.Location = new Point(panel1.Location.X, panel1.Location.Y - 5);
                        await Task.Delay(10);
                    }

                    if (this.FullScreen == false)
                    {
                        return;
                    }

                    if (multiMediaViewer.ItIsAImage == true)
                    {
                        return;
                    }

                    panel1.Location = new Point(panel1.Location.X, PanelY - panel1.Height);

                    while (Cursor.Position.X != X || Cursor.Position.Y != Y)
                    {
                        X = Cursor.Position.X;
                        Y = Cursor.Position.Y;

                        await Task.Delay(400);
                        if (this.FullScreen == false)
                        {
                            return;
                        }

                        if (multiMediaViewer.ItIsAImage == true)
                        {
                            return;
                        }

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y)
                        {
                            continue;
                        }

                        await Task.Delay(400);
                        if (this.FullScreen == false)
                        {
                            return;
                        }

                        if (multiMediaViewer.ItIsAImage == true)
                        {
                            return;
                        }

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y)
                        {
                            continue;
                        }

                        await Task.Delay(400);
                        if (this.FullScreen == false)
                        {
                            return;
                        }

                        if (multiMediaViewer.ItIsAImage == true)
                        {
                            return;
                        }

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y)
                        {
                            continue;
                        }

                        await Task.Delay(400);
                        if (this.FullScreen == false)
                        {
                            return;
                        }

                        if (multiMediaViewer.ItIsAImage == true)
                        {
                            return;
                        }

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y)
                        {
                            continue;
                        }

                        await Task.Delay(400);
                        if (this.FullScreen == false)
                        {
                            return;
                        }

                        if (multiMediaViewer.ItIsAImage == true)
                        {
                            return;
                        }

                        if (Cursor.Position.X != X || Cursor.Position.Y != Y)
                        {
                            continue;
                        }

                        // 2s
                    }
                }
                else
                {
                    while (panel1.Location.Y < PanelY)
                    {
                        if (this.FullScreen == false)
                        {
                            return;
                        }

                        if (multiMediaViewer.ItIsAImage == true)
                        {
                            return;
                        }

                        panel1.Location = new Point(panel1.Location.X, panel1.Location.Y + 5);
                        await Task.Delay(10);
                    }

                    if (this.FullScreen == false)
                    {
                        return;
                    }

                    if (multiMediaViewer.ItIsAImage == true)
                    {
                        return;
                    }

                    panel1.Location = new Point(panel1.Location.X, PanelY);
                }
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Settings();
        }

        private void Settings()
        {
            SettingsDialog settingsDialog = new SettingsDialog(this.DataSettings);

            if (settingsDialog.ShowDialog() == DialogResult.OK)
            {
                this.DataSettings = settingsDialog.DataSettings;
                SaveSettings(this.DataSettings);

                multiMediaViewer.HardwareAcceleration = DataSettings.HardwareAcceleration;
                VideoPreview.HardwareAcceleration = DataSettings.HardwareAcceleration;

                MenuDeroulantMore.Dispose();
                MenuDeroulantMore = new MenuDeroulant((byte)DataSettings.Language);
                string[] Buttons;

                if (DataSettings.Language == Language.French)
                {
                    Buttons = new string[10]
                    {
                       "Ouvrir un fichier", "Ouvrir un DVD", "Ouvrir un flux web", "Renommer ce fichier", "Supprimer ce fichier", "Copier dans le presse papier", "Capture d'écran", "Lecture Audio et Vidéo",
                       "Lecture Image", "Quitter"
                    };
                }
                else
                {
                    Buttons = new string[10]
                    {
                       "Open file", "Open DVD", "Open a web flux", "Rename this file", "Delete this file", "Copy to clipboard ", "Screenshot", "Audio and Video Playback ",
                       "Reading Image", "Leave"
                    };
                }

                MenuDeroulantMore.BackColor = Color.FromArgb(64, 64, 64);
                MenuDeroulantMore.Border = 0;
                MenuDeroulantMore.BorderColor = Color.FromArgb(224, 224, 224);
                MenuDeroulantMore.BorderRadius = 15;
                MenuDeroulantMore.Location = new Point(350, 60);
                MenuDeroulantMore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                MenuDeroulantMore.ShowSide = Side.Top;
                MenuDeroulantMore.Width = 300;
                //MenuDeroulantMore.SizeHeight = 20;
                //MenuDeroulantMore.SizeWidth = 150;
                MenuDeroulantMore.TabIndex = 13;
                MenuDeroulantMore.Visible = false;
                MenuDeroulantMore.SetButton(Buttons);
                MenuDeroulantMore.AddBar(2);
                MenuDeroulantMore.AddBar(6);
                MenuDeroulantMore.AddBar(8);
                Controls.Add(MenuDeroulantMore);
                this.Controls.SetChildIndex(this.MenuDeroulantMore, 0);

                MenuDeroulantMore.SetButtonClique(0, new MouseEventHandler(ButtonOpenFile_Click));
                MenuDeroulantMore.SetButtonClique(1, new MouseEventHandler(ButtonOpenDVD_Click));
                MenuDeroulantMore.SetButtonClique(2, new MouseEventHandler(ButtonOpenWebFlux_Click));
                MenuDeroulantMore.SetButtonClique(3, new MouseEventHandler(RenameFile_Click));
                MenuDeroulantMore.SetButtonClique(4, new MouseEventHandler(DeleteFile_Click));
                MenuDeroulantMore.SetButtonClique(5, new MouseEventHandler(CopyClipboard));
                MenuDeroulantMore.SetButtonClique(6, new MouseEventHandler(ButtonScreenShot_Click));
                MenuDeroulantMore.SetButtonClique(7, new MouseEventHandler(VideoAudioReader_Click));
                MenuDeroulantMore.SetButtonClique(8, new MouseEventHandler(ImageReader_Click));
                MenuDeroulantMore.SetButtonClique(9, new MouseEventHandler(Quitter_Click));

            }
        }

        private void SaveSettings(DataSettings dataSettings)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi") == false)
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\");
            }

            XmlSerializer xs = new XmlSerializer(typeof(DataSettings));

            using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi Settings.xml"))
            {
                xs.Serialize(wr, dataSettings);
                wr.Close();
            }
        }

        private DataSettings ReadSettings()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi Settings.xml") == false)
            {
                SaveSettings(new DataSettings());
                return new DataSettings();
            }

            XmlSerializer xs = new XmlSerializer(typeof(DataSettings));
            DataSettings ds;

            using (StreamReader rd = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi Settings.xml"))
            {
                try
                {
                    ds = xs.Deserialize(rd) as DataSettings;
                    rd.Close();
                }
                catch
                {
                    rd.Close();
                    ds = null;
                    SaveSettings(new DataSettings());
                    return new DataSettings();
                }
            }

            return ds;
        }

        private void ShowInformationDialog()
        {
            string[] Version = new string[7]
            {
                Environment.Version.ToString(),
                progressBar.ProductVersion,
                VlcMediaPlayer.VlcVersionNumber.ToString(),
                RawDecoder.LibRawVersionNumber.ToString(),
                WebPDecoder.GetDecoderVersion(),
                this.ProductVersion,
                Sky_multi_Core.ImageReader.Heif.LibHeifInfo.Version.ToString()
            };

            if (MediaLoaded != string.Empty)
            {
                if (multiMediaViewer.ItIsAImage == false)
                {
                    new InformationDialog(in MediaLoaded, multiMediaViewer.GetCurrentMedia().Tracks, multiMediaViewer.Video.Tracks.All.ToList(), multiMediaViewer.Audio.Tracks.All.ToList(),
                      multiMediaViewer.SubTitles.All.ToList(), DataSettings.Language, in Version).Show();
                }
                else
                {
                    new InformationDialog(in MediaLoaded, multiMediaViewer.Image.Width + "x" + multiMediaViewer.Image.Height,
                        multiMediaViewer.Image.PixelFormat, DataSettings.Language, in Version).Show();
                }
            }
            else
            {
                new InformationDialog(in MediaLoaded, DataSettings.Language, in Version).Show();
            }
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            ShowInformationDialog();
        }

        private void buttonMore_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            MenuDeroulantMore.Visible = true;

            if (MenuDeroulantMore.View == false)
            {
                MenuDeroulantMore.Show();
            }
            /*else
            {
                MenuDeroulantMore.Hide();
            }*/
        }

        private void ButtonOpenDVD_Click(object sender, EventArgs e)
        {
            using (DVDDialog dialog = new DVDDialog(DataSettings.Language))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    multiMediaViewer.OpenDVD(dialog.DVDPath);
                }
            }
        }

        private void ButtonOpenWebFlux_Click(object sender, EventArgs e)
        {
            /*using (WebFluxDialog dialog = new WebFluxDialog(DataSettings.Language))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }*/

            multiMediaViewer.VlcMediaPlayer.PlayStreaming("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }

        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            if (multiMediaViewer.ControlLoaded == false)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Sky multi Viewer est en cours de chargement veuillez patienter.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sky multi Viewer is loading please wait.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(dialog.FileName) == false)
                    {
                        if (DataSettings.Language == Language.French)
                        {
                            MessageBox.Show("Une erreur est survenue, le fichier spécifié n'existe pas!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("An error occurred, the specified file does not exist!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return;
                    }

                    multiMediaViewer.OpenFile(dialog.FileName);
                    MediaLoaded = dialog.FileName;
                    subTitlePath = null;
                    this.Text = "Sky multi - " + dialog.FileName;
                }
            }
        }

        private void ButtonScreenShot_Click(object sender, EventArgs e)
        {
            if (DataSettings.Language == Language.French)
            {
                MenuDeroulantMore.NewPage(new string[3] { "Capturer tout l'écran", "Capture découpé", "Capture de la vidéo" },
                   new MouseEventHandler[3] { new MouseEventHandler(SnapshotAllToScreen), new MouseEventHandler(SnapshotPerso), new MouseEventHandler(SnapshotVideo) });
            }
            else
            {
                MenuDeroulantMore.NewPage(new string[3] { "Capture the whole screen", "Cutted Out capture", "Video capture" },
                   new MouseEventHandler[3] { new MouseEventHandler(SnapshotAllToScreen), new MouseEventHandler(SnapshotPerso), new MouseEventHandler(SnapshotVideo) });
            }
        }

        private void SnapshotAllToScreen(object sender, MouseEventArgs e)
        {
            //this.Visible = false;
            Bitmap bitmap = new Bitmap(Screen.FromControl(this).Bounds.Width, Screen.FromControl(this).Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(Screen.FromControl(this).Bounds.Left, Screen.FromControl(this).Bounds.Top, 0, 0, Screen.FromControl(this).Bounds.Size, CopyPixelOperation.SourceCopy);
            }

            //this.Visible = true;

            ImageModifierDialog dialog = new ImageModifierDialog(bitmap);
            dialog.Show();
        }

        private void SnapshotPerso(object sender, MouseEventArgs e)
        {
            this.Visible = false;

            using (ZoneCapture zoneCapture = new ZoneCapture())
            {
                zoneCapture.ShowDialog();
                this.Visible = true;
                ImageModifierDialog dialog = new ImageModifierDialog(zoneCapture.BitmapCapture);
                zoneCapture.Dispose();
                dialog.Show();            
            }

            this.Visible = true;
        }

        private void SnapshotVideo(object sender, MouseEventArgs e)
        {
            multiMediaViewer.SetPause(true);

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Images png| *.png |Images jpeg| *.jpeg; *.jpg |Images bmp| *.bmp |Images ico| *.ico |Images gif| *.gif |Images tiff| *.tiff; *.tif |Images heic| *.heic";
                dialog.DefaultExt = ".png";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    multiMediaViewer.TakeSnapshot(dialog.FileName);
                }

                dialog.Dispose();
            }
        }

        private void BlurMainForm()
        {
            this.Redimensionnable = false;
            this.ButtonMaximizedVisible = false;

            if (MediaLoaded != string.Empty && multiMediaViewer.ItIsAImage == false)
            {
                multiMediaViewer.SetPause(true);
            }

            Bitmap b = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            Graphics.FromImage(b).CopyFromScreen(this.Location.X + Border, this.Location.Y + 20, 0, 0, new Size(this.ClientRectangle.Size.Width - Border, this.ClientRectangle.Size.Height - Border),
                CopyPixelOperation.SourceCopy);
            c.BackgroundImage = b;
            c.Size = new Size(this.Width - Border * 2, this.Height - Border - 20);
            c.Location = new Point(Border, 20);
            c.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            c.BringToFront();
            c.Visible = true;
            this.Controls.Add(c);

            for (int index = 0; index < 20; index++)
            {
                c.BringToFront();
                Effect.GaussianBlur(ref b);
                c.BackgroundImage = b;
            }
        }

        private void UnBlurMainForm()
        {
            /*Bitmap b = (Bitmap)c.BackgroundImage;

            for (int index = 0; index < 20; index++)
            {
                c.BringToFront();
                Effect.GaussianBlurRemove(ref b);
                c.BackgroundImage = b;
            }*/

            this.Redimensionnable = true;
            this.ButtonMaximizedVisible = true;

            this.Controls.Remove(c);
            c.BackgroundImage = null;
        }

        private void RenameFile_Click(object sender, EventArgs e)
        {
            if (MediaLoaded == string.Empty)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Veuillez charger un Média!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please load a Media!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            RenameFileDialog renameFileDialog = new RenameFileDialog(DataSettings.Language);

            if (this.FullScreen == false)
            {
                BlurMainForm();
            }

            if (renameFileDialog.ShowDialog(MediaLoaded) == DialogResult.OK)
            {               
                try
                {
                    string NewName = Path.GetDirectoryName(MediaLoaded) + @"\" + renameFileDialog.NewName + Path.GetExtension(MediaLoaded);

                    if (File.Exists(NewName))
                    {
                        if (DataSettings.Language == Language.French)
                        {
                            if (MessageBox.Show("Un fichier est déjà existant voulez vous le remplacer?", "Sky multi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                try
                                {
                                    Sky_multi_Core.RecycleBin.MoveFileToRB(NewName);
                                }
                                catch
                                {
                                    MessageBox.Show("Une erreur est survenue! Impossible de remplacer le fichier.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("A file already exists do you want to replace it?", "Sky multi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                try
                                {
                                    Sky_multi_Core.RecycleBin.MoveFileToRB(NewName);
                                }
                                catch
                                {
                                    MessageBox.Show("An error has occurred! Unable to replace file.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }

                    if (multiMediaViewer.ItIsAImage == true)
                    {
                        multiMediaViewer.DisposeImage();
                    }
                    else
                    {
                        multiMediaViewer.Stop();
                    }

                    File.Move(MediaLoaded, NewName);
                    multiMediaViewer.OpenFile(NewName);
                    MediaLoaded = NewName;
                    this.Text = "Sky multi - " + NewName;
                    NewName = null;
                }
                catch
                {
                    multiMediaViewer.OpenFile(MediaLoaded);
                    if (DataSettings.Language == Language.French)
                    {
                        MessageBox.Show("Une erreur est survenue! Impossible de renommer l'image.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("An error has occurred! Unable to rename the image.", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            renameFileDialog.Dispose();
            renameFileDialog = null;

            if (this.FullScreen == false)
            {
                UnBlurMainForm();
            }
        }

        private void DeleteFile_Click(object sender, EventArgs e)
        {
            if (MediaLoaded == string.Empty)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Veuillez charger un Média!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please load a Media!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            if (DataSettings.Language == Language.French)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce fichier? Il sera déplacé dans la corbeille.", "Sky multi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (multiMediaViewer.ItIsAImage == true)
                    {
                        multiMediaViewer.DisposeImage();
                    }
                    else
                    {
                        multiMediaViewer.Stop();
                    }

                    try
                    {
                        //File.Delete(MediaLoaded);
                        Sky_multi_Core.RecycleBin.MoveFileToRB(MediaLoaded);
                    }
                    catch
                    {
                        MessageBox.Show("Une erreur est survenue, impossible de supprimer ce fichier!", "Sky mutli", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        multiMediaViewer.OpenFile(MediaLoaded);
                        return;
                    }
                    MediaLoaded = string.Empty;
                    this.Text = "Sky multi";
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this file? It will be moved to the recycle bin.", "Sky multi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (multiMediaViewer.ItIsAImage == true)
                    {
                        multiMediaViewer.DisposeImage();
                    }
                    else
                    {
                        multiMediaViewer.Stop();
                    }

                    try
                    {
                        //File.Delete(MediaLoaded);
                        Sky_multi_Core.RecycleBin.MoveFileToRB(MediaLoaded);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred, cannot delete this file!", "Sky mutli", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        multiMediaViewer.OpenFile(MediaLoaded);
                        return;
                    }
                    MediaLoaded = string.Empty;
                    this.Text = "Sky multi";
                }
            }
        }

        private void VideoAudioReader_Click(object sender, EventArgs e)
        {
            if (multiMediaViewer.ItIsAImage == true || MediaLoaded == string.Empty)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Veuillez charger un Audio ou une vidéo pour acceder à ces fonctionnalités!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please load Audio or Video to access these features!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            if (DataSettings.Language == Language.French)
            {
                MenuDeroulantMore.NewPage(new string[5] { "Aller à un temps spécifié", "Rapport largeur/hauteur de la vidéo", "Piste(s) vidéo", "Piste(s) audio", "Piste(s) de sous titres" },
                new MouseEventHandler[5] { new MouseEventHandler(SpecifiqueTime), new MouseEventHandler(Ratio), new MouseEventHandler(VideoTracks),
                new MouseEventHandler(AudioTracks), new MouseEventHandler(SubtitlesTracks)});
            }
            else
            {
                MenuDeroulantMore.NewPage(new string[5] { "Go to a specified time", "Video aspect ratio", "Video track(s)", "Audio track(s)", "Sub titles track(s)" },
                new MouseEventHandler[5] { new MouseEventHandler(SpecifiqueTime), new MouseEventHandler(Ratio), new MouseEventHandler(VideoTracks),
                new MouseEventHandler(AudioTracks), new MouseEventHandler(SubtitlesTracks)});
            }
        }

        private void SpecifiqueTime(object sender, MouseEventArgs e)
        {
            SetTimeDialog = new SetTimeDialog(multiMediaViewer.Time , ref MediaLoaded, GetVideo() && DataSettings.VideoPreviewOnSetTime);
            SetTimeDialog.TimeDefined = new EventSetTimeHandler(TimeSet);
            SetTimeDialog.Show();
        }

        private void TimeSet(long VlcTime)
        {
            multiMediaViewer.Time = VlcTime;
            SetTimeDialog = null;
        }

        private void RatioMDLink(object sender, MouseEventArgs e)
        {
            string[] RatioList;

            if (DataSettings.Language == Language.French)
            {
                RatioList = new string[17] { "Automatique", "16:9", "16:10", "3:2", "4:3", "1:1", "5:4", "5:3", "21:9", "31:9", "7:5", "2:1", "48:9", "9:5", "8:5", "9:10", "16:5" };
            }
            else
            {
                RatioList = new string[17] { "Automatic", "16:9", "16:10", "3:2", "4:3", "1:1", "5:4", "5:3", "21:9", "31:9", "7:5", "2:1", "48:9", "9:5", "8:5", "9:10", "16:5" };
            }

            menuDeroulantLink.NewPage(RatioList, new MouseEventNameHandler(SetRatio), true);
        }

        private void Ratio(object sender, MouseEventArgs e)
        {
            string[] RatioList;

            if (DataSettings.Language == Language.French)
            {
                RatioList = new string[17] { "Automatique", "16:9", "16:10", "3:2", "4:3", "1:1", "5:4", "5:3", "21:9", "31:9", "7:5", "2:1", "48:9", "9:5", "8:5", "9:10", "16:5" };
            }
            else
            {
                RatioList = new string[17] { "Automatic", "16:9", "16:10", "3:2", "4:3", "1:1", "5:4", "5:3", "21:9", "31:9", "7:5", "2:1", "48:9", "9:5", "8:5", "9:10", "16:5" };
            }

            MenuDeroulantExt.SetButton(ref RatioList, new MouseEventNameHandler(SetRatio), true);
            MenuDeroulantExt.Show();
        }

        private void SetRatio(string Text, int ID, MouseEventArgs e)
        {
            if (Text == "Automatique" || Text == "Automatic")
            {
                Text = string.Empty;
            }

            multiMediaViewer.Video.AspectRatio = Text;
        }

        private void VideoTracksMDLink(object sender, MouseEventArgs e)
        {
            List<string> VideoTrackList = new List<string>();

            foreach (TrackDescription i in multiMediaViewer.Video.Tracks.All)
            {
                VideoTrackList.Add(i.Name);
            }

            menuDeroulantLink.NewPage(VideoTrackList.ToArray(), new MouseEventNameHandler(SetVideoTracks), false);
        }

        private void VideoTracks(object sender, MouseEventArgs e)
        {
            List<string> VideoTrackList = new List<string>();

            foreach (TrackDescription i in multiMediaViewer.Video.Tracks.All)
            {
                VideoTrackList.Add(i.Name);
            }

            MenuDeroulantExt.SetButton(VideoTrackList.ToArray(), new MouseEventNameHandler(SetVideoTracks));
            MenuDeroulantExt.Show();
        }

        private void SetVideoTracks(string Text, int ID, MouseEventArgs e)
        {
            foreach (TrackDescription i in multiMediaViewer.Video.Tracks.All)
            {
                if (i.Name == Text)
                {
                    multiMediaViewer.Video.Tracks.Current = i;
                    return;
                }
            }
        }

        private void AudioTracksMDLink(object sender, MouseEventArgs e)
        {
            List<string> AudioTrackList = new List<string>();

            foreach (TrackDescription i in multiMediaViewer.Audio.Tracks.All)
            {
                AudioTrackList.Add(i.Name);
            }

            menuDeroulantLink.NewPage(AudioTrackList.ToArray(), new MouseEventNameHandler(SetAudioTracks), false);
        }

        private void AudioTracks(object sender, MouseEventArgs e)
        {
            List<string> AudioTrackList = new List<string>();

            foreach (TrackDescription i in multiMediaViewer.Audio.Tracks.All)
            {
                AudioTrackList.Add(i.Name);
            }

            MenuDeroulantExt.SetButton(AudioTrackList.ToArray(), new MouseEventNameHandler(SetAudioTracks));
            MenuDeroulantExt.Show();
        }

        private void SetAudioTracks(string Text, int ID, MouseEventArgs e)
        {
            foreach (TrackDescription i in multiMediaViewer.Audio.Tracks.All)
            {
                if (i.Name == Text)
                {
                    multiMediaViewer.Audio.Tracks.Current = i;
                    return;
                }
            }
        }

        private void SubtitlesTracksMDLink(object sender, MouseEventArgs e)
        {
            List<string> SubtitlesTrackList = new List<string>();

            if (DataSettings.Language == Language.French)
            {
                SubtitlesTrackList.Add("Selectionner un fichier");
            }
            else
            {
                SubtitlesTrackList.Add("Select a file");
            }

            foreach (TrackDescription i in multiMediaViewer.SubTitles.All)
            {
                SubtitlesTrackList.Add(i.Name);
            }

            menuDeroulantLink.NewPage(SubtitlesTrackList.ToArray(), new MouseEventNameHandler(SetSubtitlesTracks), false);
        }

        private void SubtitlesTracks(object sender, MouseEventArgs e)
        {
            List<string> SubtitlesTrackList = new List<string>();

            if (DataSettings.Language == Language.French)
            {
                SubtitlesTrackList.Add("Selectionner un fichier");
            }
            else
            {
                SubtitlesTrackList.Add("Select a file");
            }

            foreach (TrackDescription i in multiMediaViewer.SubTitles.All)
            {
                SubtitlesTrackList.Add(i.Name);
            }

            MenuDeroulantExt.SetButton(SubtitlesTrackList.ToArray(), new MouseEventNameHandler(SetSubtitlesTracks));
            MenuDeroulantExt.Show();
        }

        private void SetSubtitlesTracks(string Text, int ID, MouseEventArgs e)
        {
            if (ID == 0)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();

                if (DataSettings.Language == Language.French)
                {
                    fileDialog.Filter = "Fichiers sous-titres| *.cdg; *.idx; *.srt; *.sub; *.utf; *.ass; *.ssa; *.aqt; *.jss; *.psb; *.rt; *.sami; *.smi; " +
                        "*.txt; *.smil; *.stl; *.usf; *.dks; *.pjs; *.mpl2; *.mks; *.vtt; *.tt; *.ttml; *.dfxp; *.scc |Tous les fichiers| *.*";
                }
                else
                {
                    fileDialog.Filter = "Subtitles file|*.cdg; *.idx; *.srt; *.sub; *.utf; *.ass; *.ssa; *.aqt; *.jss; *.psb; *.rt; *.sami; *.smi; " +
                        "*.txt; *.smil; *.stl; *.usf; *.dks; *.pjs; *.mpl2; *.mks; *.vtt; *.tt; *.ttml; *.dfxp; *.scc |All the files| *.*";
                }

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    long TimeCode = multiMediaViewer.Time;
                    subTitlePath = @"sub-file=" + fileDialog.FileName;
                    multiMediaViewer.SetMedia(@"file:///" + MediaLoaded, @"sub-file=" + fileDialog.FileName);
                    multiMediaViewer.Play();
                    multiMediaViewer.Time = TimeCode;
                }

                fileDialog.Dispose();
                fileDialog = null;
                return;
            }

            foreach (TrackDescription i in multiMediaViewer.SubTitles.All)
            {
                if (i.Name == Text)
                {
                    multiMediaViewer.SubTitles.Current = i;
                    return;
                }
            }
        }

        private void ImageReader_Click(object sender, EventArgs e)
        {
            if (multiMediaViewer.ItIsAImage == false || MediaLoaded == string.Empty)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Veuillez charger une image pour acceder à ces fonctionnalités!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please load an image to access these features!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            if (DataSettings.Language == Language.French)
            {
                MenuDeroulantMore.NewPage(new string[5] { "Imprimer", "Convertir image", "Définir en tant qu'arrière plan", "Faire pivoter l'image", "Modifier l'image" }, 
                    new MouseEventHandler[5] { new MouseEventHandler(Print), new MouseEventHandler(ConvertImage), new MouseEventHandler(DefineBackground),
                    new MouseEventHandler(RotateImage), new MouseEventHandler(EditImage)});
            }
            else
            {
                MenuDeroulantMore.NewPage(new string[5] { "Print", "Convert image", "Set as background", "Rotate image", "Edit image" },
                    new MouseEventHandler[5] { new MouseEventHandler(Print), new MouseEventHandler(ConvertImage), new MouseEventHandler(DefineBackground),
                    new MouseEventHandler(RotateImage), new MouseEventHandler(EditImage)});
            }
        }

        private void EditImage(object sender, MouseEventArgs e)
        {
            //ImageModifierDialog imageModifier = new ImageModifierDialog(multiMediaViewer.Image);
            //imageModifier.Show();
        }

        private void RotateImage(object sender, MouseEventArgs e)
        {
            multiMediaViewer.RotateImage();
        }

        private void Print(object sender, MouseEventArgs e)
        {
            PrintDialog DialogPrint = new PrintDialog();
            System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();

            doc.PrintPage += docPage;
            DialogPrint.Document = doc;

            if (DialogPrint.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void docPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(multiMediaViewer.BackgroundImage, multiMediaViewer.Bounds);
        }

        private void ConvertImage(object sender, MouseEventArgs e)
        {
            if (ImageConvertDialogControl == null)
            {
                ImageConvertDialogControl = null;
                ImageConvertDialogControl = new ImageConvertDialogControl(Path.GetExtension(MediaLoaded), new MouseEventHandler(ImageConvertDialogControl_Convert),
                    new MouseEventHandler(ImageConvertDialogControl_Cancel), DataSettings.Language);
                if (this.FullScreen == false)
                {
                    BlurMainForm();
                }
                this.Controls.Add(ImageConvertDialogControl);
                ImageConvertDialogControl.BringToFront();
                ImageConvertDialogControl.Location = new Point(this.Width / 2 - ImageConvertDialogControl.Width / 2, this.Height / 2 - 50);
                ImageConvertDialogControl.Anchor = AnchorStyles.None;
                ImageConvertDialogControl.Show();
            }
        }

        private void ImageConvertDialogControl_Convert(object sender, MouseEventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = ImageConvertDialogControl.GetNewFormat + " | *" + ImageConvertDialogControl.GetNewFormat;
                dialog.InitialDirectory = Path.GetDirectoryName(MediaLoaded);
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    /*switch (ImageConvertDialogControl.GetNewFormat)
                    {
                        case ".png":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToPng(dialog.FileName);
                            }
                            else
                            {
                                
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            /*break;

                        case ".jpg":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToJpeg(dialog.FileName, ".jpg");
                            }
                            else
                            {
                                
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            /*break;

                        case ".jpeg":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToJpeg(dialog.FileName);
                            }
                            else
                            {
                               
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            /*break;

                        case ".ico":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToIco(dialog.FileName);
                            }
                            else
                            {
                                
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Icon);
                            /*break;

                        case ".gif":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToGif(dialog.FileName);
                            }
                            else
                            {
                                
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                            /*break;

                        case ".tiff":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToTiff(dialog.FileName);
                            }
                            else
                            {
                                
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
                            /*break;

                        case ".tif":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToTiff(dialog.FileName, ".tif");
                            }
                            else
                            {
                                
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
                            /*break;

                        case ".bmp":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToBmp(dialog.FileName);
                            }
                            else
                            {
                                
                            }*/
                            //multiMediaViewer.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            /*break;

                        case ".webp":
                            /*if (multiMediaViewer.RawImage == true)
                            {
                                Sky_multi_Core.RawDecoder.ConvertRawToWebp(dialog.FileName);
                            }
                            else
                            {
                                
                            }*/
                            //WebPEncoder.EncodeWebp((Bitmap)multiMediaViewer.Image, dialog.FileName);
                            /*break;

                        case ".heif":
                            //BitmapHeifCoverter.EncodeHeif((Bitmap)multiMediaViewer.Image, dialog.FileName);
                            break;

                        case ".heic":
                            //BitmapHeifCoverter.EncodeHeif((Bitmap)multiMediaViewer.Image, dialog.FileName);
                            break;

                        case ".avif":
                            //BitmapHeifCoverter.EncodeAvif((Bitmap)multiMediaViewer.Image, dialog.FileName);
                            break;

                        default:
                            if (DataSettings.Language == Language.French)
                            {
                                MessageBox.Show("Ce format pour la conversion n'est pas pris en charge!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("This format for the conversion is not supported!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return;
                    }*/

                    multiMediaViewer.EncodeImageLoaded(dialog.FileName, ImageConvertDialogControl.GetNewFormat);

                    ImageConvertDialogControl.Close();
                    ImageConvertDialogControl = null;
                    if (this.FullScreen == false)
                    {
                        UnBlurMainForm();
                    }
                }
            }
        }

        private void ImageConvertDialogControl_Cancel(object sender, MouseEventArgs e)
        {
            ImageConvertDialogControl.Close();
            ImageConvertDialogControl = null;
            if (this.FullScreen == false)
            {
                UnBlurMainForm();
            }
        }

        private void CopyClipboard(object sender, MouseEventArgs e)
        {
            if (MediaLoaded == string.Empty)
            {
                if (DataSettings.Language == Language.French)
                {
                    MessageBox.Show("Veuillez charger un Média!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please load a Media!", "Sky multi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            System.Collections.Specialized.StringCollection PathFile = new System.Collections.Specialized.StringCollection();
            PathFile.Add(MediaLoaded);
            Clipboard.SetFileDropList(PathFile);
        }

        private void DefineBackground(object sender, MouseEventArgs e)
        {
            Sky_UI.Win32.SystemParametersInfo(20, 0, MediaLoaded, 0x1 | 0x2);
        }

        private void Quitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            multiMediaViewer.Pause();
        }

        private void vlcControl1_Paused(object sender, VlcMediaPlayerPausedEventArgs e)
        {
            //buttonPlay.BackgroundImage.Dispose();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonPlay")));
            resources = null;
        }

        private void vlcControl1_Playing(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            //buttonPlay.BackgroundImage.Dispose();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonPause")));
            resources = null;
        }

        private async void vlcControl1_TimeChanged()
        {
            while (multiMediaViewer != null && label1 != null)
            {
                if (MousePressedLeft == false)
                {
                    if (multiMediaViewer.Length != 0)
                    {
                        progressBar.SetValuePixels((int)(multiMediaViewer.Time * progressBar.Width / multiMediaViewer.Length));
                    }

                    label1.Text = ConvertVlcTimeToString(multiMediaViewer.Time);

                    if (DataSettings.Language == Language.French)
                    {
                        label2.Text = "Durée restante :\n" + ConvertVlcTimeToString(multiMediaViewer.Length - multiMediaViewer.Time);
                    }
                    else
                    {
                        label2.Text = "Remaining time :\n" + ConvertVlcTimeToString(multiMediaViewer.Length - multiMediaViewer.Time);
                    }
                }
                await Task.Delay(1);
            }
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

        private void progressBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MousePressedLeft = true;

                IntPtr handle = Win32.CreateRoundRectRgn(0, 0, VideoPreview.Width, VideoPreview.Height, 15, 15);

                if (handle != IntPtr.Zero)
                {
                    VideoPreview.Region = Region.FromHrgn(handle);
                    Win32.DeleteObject(handle);
                }

                if (DataSettings.VideoPreviewOnProgressBar == true && GetVideo() == true)
                {
                    VideoPreview.Visible = true;
                    VideoPreview.LoadMedia(MediaLoaded, multiMediaViewer.Time);
                }
            }
        }

        private void progressBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (MediaEnd == true)
            {
                multiMediaViewer.OpenFile(MediaLoaded);
                MediaEnd = false;
            }

            if (e.X <= 0)
            {
                progressBar.SetValuePixels(0);
            }
            else
            {
                progressBar.SetValuePixels(e.X);
            }

            multiMediaViewer.Time = (long)((double)progressBar.GetValuePixels() / progressBar.Width * multiMediaViewer.Length);
            VideoPreview.Visible = false;
            VideoPreview.Stop();
            MousePressedLeft = false;
        }

        private void progressBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousePressedLeft == true)
            {
                if (e.X <= 0)
                {
                    progressBar.SetValuePixels(0);
                }
                else
                {
                    progressBar.SetValuePixels(e.X);
                }

                if (DataSettings.VideoPreviewOnProgressBar == true && GetVideo() == true && 
                    VideoPreview.GetTime() != (long)((double)progressBar.GetValuePixels() / progressBar.Width * multiMediaViewer.Length))
                {
                    if (e.X >= progressBar.Width)
                    {
                        VideoPreview.Location = new Point(progressBar.Width + progressBar.Location.X + panel1.Location.X, VideoPreview.Location.Y);
                        return;
                    }

                    if (this.FullScreen == false)
                    {
                        VideoPreview.Location = new Point(e.X - VideoPreview.Width / 2, VideoPreview.Location.Y);
                    }
                    else
                    {
                        VideoPreview.Location = new Point(e.X + (panel1.Location.X + progressBar.Location.X) - VideoPreview.Width / 2, VideoPreview.Location.Y);
                    }

                    VideoPreview.SetTime((long)((double)progressBar.GetValuePixels() / progressBar.Width * multiMediaViewer.Length));
                }
            }
        }

        private void buttonMoreMinute_Click(object sender, EventArgs e)
        {
            multiMediaViewer.Time += 60000;
        }

        private void buttonLessMinute_Click(object sender, EventArgs e)
        {
            multiMediaViewer.Time -= 60000;
        }

        private void vlcControl1_EndReached(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            MediaEnd = true;
        }

        private async void EndMediaStartAction()
        {
            while (multiMediaViewer != null && this != null)
            {
                if (MediaEnd == true)
                {
                    switch (DataSettings.EndMedia)
                    {
                        case EndMediaAction.ReadMultimediaNext:
                            MediaEnd = false;
                            MultimediaRight();
                            break;

                        case EndMediaAction.RestartMultimedia:
                            MediaEnd = false;
                            multiMediaViewer.Stop();
                            multiMediaViewer.OpenFile(MediaLoaded, subTitlePath);
                            break;

                        case EndMediaAction.DoNothing:
                            break;
                    }
                }

                await Task.Delay(1);
            }
        }

        private void MultimediaRight()
        {
            if (MediaLoaded == string.Empty)
            {
                return;
            }

            List<string> FileList = new List<string>(Directory.EnumerateFiles(Path.GetDirectoryName(MediaLoaded)));

            this.multiMediaViewer.ItIsPicture -= new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsPicture);
            this.multiMediaViewer.ItIsAudioOrVideo -= new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsAudioOrVideo);
            bool Image = multiMediaViewer.ItIsAImage;

            do
            {
                int index = 0;
                foreach (string i in FileList)
                {
                    if (i == MediaLoaded)
                    {
                        break;
                    }
                    index++;
                }

                if (index < FileList.Count() - 1)
                {
                    MediaLoaded = FileList[index + 1];
                }
                else
                {
                    MediaLoaded = FileList[0];
                }

                if (FileExtentionIncorect(MediaLoaded))
                {
                    continue;
                }

                if (multiMediaViewer.ItIsAImage == true)
                {
                    multiMediaViewer.DisposeImage();
                }
                else
                {
                    multiMediaViewer.Stop();
                }
                multiMediaViewer.OpenFile(MediaLoaded);
                System.Threading.Thread.Sleep(100);
            }
            while (GetVideo() == false && GetAudio() == false && multiMediaViewer.ItIsAImage == false || FileExtentionIncorect(MediaLoaded));

            this.multiMediaViewer.ItIsPicture += new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsPicture);
            this.multiMediaViewer.ItIsAudioOrVideo += new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsAudioOrVideo);

            if (multiMediaViewer.ItIsAImage == true)
            {
                multiMediaViewer_ItIsPicture(!Image);
            }
            else
            {
                multiMediaViewer_ItIsAudioOrVideo(Image);
            }

            MediaEnd = false;
            this.Text = "Sky multi - " + MediaLoaded;
            subTitlePath = null;

            FileList.Clear();
            FileList = null;
        }

        private void MultimediaLeft()
        {
            if (MediaLoaded == string.Empty)
            {
                return;
            }

            List<string> FileList = new List<string>(Directory.EnumerateFiles(Path.GetDirectoryName(MediaLoaded)));

            this.multiMediaViewer.ItIsPicture -= new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsPicture);
            this.multiMediaViewer.ItIsAudioOrVideo -= new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsAudioOrVideo);
            bool Image = multiMediaViewer.ItIsAImage;

            do
            {
                int index = 0;
                foreach (string i in FileList)
                {
                    if (i == MediaLoaded)
                    {
                        break;
                    }
                    index++;
                }

                if (index > 0)
                {
                    MediaLoaded = FileList[index - 1];
                }
                else
                {
                    MediaLoaded = FileList[FileList.Count() - 1];
                }

                if (FileExtentionIncorect(MediaLoaded))
                {
                    continue;
                }

                if (multiMediaViewer.ItIsAImage == true)
                {
                    multiMediaViewer.DisposeImage();
                }
                else
                {
                    multiMediaViewer.Stop();
                }
                multiMediaViewer.OpenFile(MediaLoaded);
                System.Threading.Thread.Sleep(100);
            }
            while (GetVideo() == false && GetAudio() == false && multiMediaViewer.ItIsAImage == false || FileExtentionIncorect(MediaLoaded));

            this.multiMediaViewer.ItIsPicture += new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsPicture);
            this.multiMediaViewer.ItIsAudioOrVideo += new Sky_multi_Viewer.EventMediaTypedHandler(multiMediaViewer_ItIsAudioOrVideo);

            if (multiMediaViewer.ItIsAImage == true)
            {
                multiMediaViewer_ItIsPicture(!Image);
            }
            else
            {
                multiMediaViewer_ItIsAudioOrVideo(Image);
            }

            MediaEnd = false;
            this.Text = "Sky multi - " + MediaLoaded;
            subTitlePath = null;

            FileList.Clear();
            FileList = null;
        }

        private bool FileExtentionIncorect(string File)
        {
            switch (Path.GetExtension(File))
            {
                case ".nfo":

                    return true;

                case ".exe":

                    return true;

                case ".dll":

                    return true;

                case ".txt":

                    return true;

                default:

                    return false;
            }
        }

        private bool GetAudio()
        {
            if (multiMediaViewer.GetCurrentMedia() != null)
            {
                string CodecAudio = string.Empty;

                foreach (MediaTrack i in multiMediaViewer.GetCurrentMedia().Tracks)
                {
                    if (i.Type == Sky_multi_Core.VlcWrapper.Core.MediaTrackTypes.Audio)
                    {
                        CodecAudio = FourCCConverter.FromFourCC(i.CodecFourcc);
                    }
                }

                if (CodecAudio == string.Empty)
                {
                    CodecAudio = null;
                    return false;
                }
                else
                {
                    CodecAudio = null;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private bool GetVideo()
        {
            if (multiMediaViewer.GetCurrentMedia() != null)
            {
                string CodecVideo = string.Empty;

                foreach (MediaTrack i in multiMediaViewer.GetCurrentMedia().Tracks)
                {
                    if (i.Type == Sky_multi_Core.VlcWrapper.Core.MediaTrackTypes.Video)
                    {
                        CodecVideo = FourCCConverter.FromFourCC(i.CodecFourcc);
                    }
                }

                if (CodecVideo == string.Empty)
                {
                    CodecVideo = null;
                    return false;
                }
                else
                {
                    CodecVideo = null;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private void multiMediaViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FullScreen_M();
            }
        }

        private void ButtonMediaRight_MouseEnter(object sender, EventArgs e)
        {
            ButtonMediaMouseEnter = true;
        }

        private void ButtonMediaRight_MouseLeave(object sender, EventArgs e)
        {
            ButtonMediaMouseEnter = false;
        }

        private void ButtonMediaLeft_MouseEnter(object sender, EventArgs e)
        {
            ButtonMediaMouseEnter = true;
        }

        private void ButtonMediaLeft_MouseLeave(object sender, EventArgs e)
        {
            ButtonMediaMouseEnter = false;
        }
    }
}
