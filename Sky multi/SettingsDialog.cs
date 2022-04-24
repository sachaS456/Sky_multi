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
using System.Drawing;
using System.Windows.Forms;
using Sky_UI;

namespace Sky_multi
{
    internal sealed class SettingsDialog : SkyForms
    {
        private Label label1;
        private CheckBox checkBox1;
        private Label label2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private CheckBox checkBox2;
        private Sky_UI.Rectangle rectangle2;
        private Label label3;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private CheckBox checkBox6;
        private ComboBox comboBox1;
        private Label label4;
        private Sky_UI.Button button1;
        private Sky_UI.Button button2;
        private Sky_UI.Rectangle rectangle1;
        private Sky_UI.Rectangle rectangle3;
        private Label label5;
        private Label label6;
        private Label label7;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private CheckBox checkBox7;

        internal DataSettings DataSettings { get; private set; }

        internal SettingsDialog(DataSettings DataSettings)
        {
            this.DataSettings = DataSettings;

            InitializeComponent();

            checkBox1.Checked = DataSettings.CliqueMadiaPause;
            checkBox2.Checked = DataSettings.MinimizeWindowsMediaPause;
            checkBox3.Checked = DataSettings.VideoPreviewOnProgressBar;
            checkBox4.Checked = DataSettings.VideoPreviewOnSetTime;
            checkBox5.Checked = DataSettings.UsingDefinitionMax;
            checkBox6.Checked = DataSettings.WhenVideoSetFullScreen;
            checkBox7.Checked = DataSettings.DisplayImagesWithDirec2D;

            switch (DataSettings.EndMedia)
            {
                case EndMediaAction.ReadMultimediaNext:
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    break;

                case EndMediaAction.RestartMultimedia:
                    radioButton2.Checked = true;
                    radioButton1.Checked = false;
                    radioButton3.Checked = false;
                    break;

                case EndMediaAction.DoNothing:
                    radioButton3.Checked = true;
                    radioButton2.Checked = false;
                    radioButton1.Checked = false;
                    break;
            }

            switch (DataSettings.Language)
            {
                case Language.French:
                    comboBox1.SelectedIndex = 0;
                    break;

                case Language.English:
                    comboBox1.SelectedIndex = 1;
                    break;
            }

            switch (DataSettings.HardwareAcceleration)
            {
                case Sky_multi_Core.VlcWrapper.HardwareAccelerationType.d3d11:
                    radioButton4.Checked = true;
                    radioButton5.Checked = false;
                    radioButton6.Checked = false;
                    break;

                case Sky_multi_Core.VlcWrapper.HardwareAccelerationType.dxva2:
                    radioButton4.Checked = false;
                    radioButton5.Checked = true;
                    radioButton6.Checked = false;
                    break;

                case Sky_multi_Core.VlcWrapper.HardwareAccelerationType.none:
                    radioButton4.Checked = false;
                    radioButton5.Checked = false;
                    radioButton6.Checked = true;
                    break;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.rectangle1 = new Sky_UI.Rectangle();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.rectangle2 = new Sky_UI.Rectangle();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new Sky_UI.Button();
            this.button2 = new Sky_UI.Button();
            this.rectangle3 = new Sky_UI.Rectangle();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rectangle1
            // 
            this.rectangle1.Border = true;
            this.rectangle1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.rectangle1.BorderRadius = 10;
            this.rectangle1.BorderWidth = 1;
            this.rectangle1.Location = new System.Drawing.Point(12, 30);
            this.rectangle1.Name = "rectangle1";
            this.rectangle1.Size = new System.Drawing.Size(272, 347);
            this.rectangle1.TabIndex = 3;
            this.rectangle1.Text = "rectangle1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(95, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 30);
            this.label1.TabIndex = 4;
            if (DataSettings.Language == Language.French)
            {
                this.label1.Text = "Utilitaires :";
            }
            else
            {
                this.label1.Text = "Utilities :";
            }
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox1.Location = new System.Drawing.Point(22, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(250, 38);
            this.checkBox1.TabIndex = 5;
            if (DataSettings.Language == Language.French)
            {
                this.checkBox1.Text = "Faire ou enlever une pause lorsque le \r\nmultimédia est cliqué";
            }
            else
            {
                this.checkBox1.Text = "Pause or remove a pause when\r\nmultimedia is clicked";
            }
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(14, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 20);
            this.label2.TabIndex = 6;
            if (DataSettings.Language == Language.French)
            {
                this.label2.Text = "Lors de la fin d'un multimédia :";
            }
            else
            {
                this.label2.Text = "At the end of a multimedia :";
            }
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radioButton1.Location = new System.Drawing.Point(23, 212);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(161, 19);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            if (DataSettings.Language == Language.French)
            {
                this.radioButton1.Text = "Lire le multimédia suivant";
            }
            else
            {
                this.radioButton1.Text = "Play the following multimedia";
            }
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radioButton2.Location = new System.Drawing.Point(23, 237);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(130, 19);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            if (DataSettings.Language == Language.French)
            {
                this.radioButton2.Text = "Relire le multimédia";
            }
            else
            {
                this.radioButton2.Text = "Replay multimedia";
            }
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radioButton3.Location = new System.Drawing.Point(23, 262);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(89, 19);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.TabStop = true;
            if (DataSettings.Language == Language.French)
            {
                this.radioButton3.Text = "Ne rien faire";
            }
            else
            {
                this.radioButton3.Text = "Do nothing";
            }
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox2.Location = new System.Drawing.Point(23, 124);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(247, 38);
            this.checkBox2.TabIndex = 10;
            if (DataSettings.Language == Language.French)
            {
                this.checkBox2.Text = "Faire une pause lorsque la fenêtre est\r\nréduite";
            }
            else
            {
                this.checkBox2.Text = "Set pause when the window is\r\nminimized";
            }
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // rectangle2
            // 
            this.rectangle2.Border = true;
            this.rectangle2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.rectangle2.BorderRadius = 10;
            this.rectangle2.BorderWidth = 1;
            this.rectangle2.Location = new System.Drawing.Point(291, 30);
            this.rectangle2.Name = "rectangle2";
            this.rectangle2.Size = new System.Drawing.Size(272, 251);
            this.rectangle2.TabIndex = 11;
            this.rectangle2.Text = "rectangle2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(386, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 30);
            this.label3.TabIndex = 12;
            if (DataSettings.Language == Language.French)
            {
                this.label3.Text = "Vidéo :";
            }
            else
            {
                this.label3.Text = "Video :";
            }
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox3.Location = new System.Drawing.Point(301, 80);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(249, 38);
            this.checkBox3.TabIndex = 13;
            if (DataSettings.Language == Language.French)
            {
                this.checkBox3.Text = "Activer l'aperçu vidéo sur la barre de \r\nprogression";
            }
            else
            {
                this.checkBox3.Text = "Enable video preview on the progress\r\nbar";
            }
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox4.Location = new System.Drawing.Point(301, 124);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(245, 38);
            this.checkBox4.TabIndex = 14;
            if (DataSettings.Language == Language.French)
            {
                this.checkBox4.Text = "Activer l'aperçu vidéo dans la fenêtre\r\ndéfinir le temps du multimédia";
            }
            else
            {
                this.checkBox4.Text = "Enable video preview in the window set\r\n multimedia time ";
            }
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox5.Location = new System.Drawing.Point(301, 168);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(254, 55);
            this.checkBox5.TabIndex = 15;
            if (DataSettings.Language == Language.French)
            {
                this.checkBox5.Text = "Utilisation de la définition maximale de\r\nvotre écran lors de l'activation du plein\r\nécran";
            }
            else
            {
                this.checkBox5.Text = "Using your maximum screen resolution\r\nwhen enabling full screen";
            }
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox6.Location = new System.Drawing.Point(301, 229);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(246, 38);
            this.checkBox6.TabIndex = 16;
            if (DataSettings.Language == Language.French)
            {
                this.checkBox6.Text = "Activer le plein écran lorsque c'est une\r\nvidéo en cours de lecture";
            }
            else
            {
                this.checkBox6.Text = "Enable full screen when it's a video\r\nplaying";
            }
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "France",
            "English"});
            this.comboBox1.Location = new System.Drawing.Point(23, 333);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(130, 25);
            this.comboBox1.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(14, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 18;
            if (DataSettings.Language == Language.French)
            {
                this.label4.Text = "Langage :";
            }
            else
            {
                this.label4.Text = "Language :";
            }
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Border = false;
            this.button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.borderRadius = 5;
            this.button1.Location = new System.Drawing.Point(737, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 36);
            this.button1.TabIndex = 19;
            if (DataSettings.Language == Language.French)
            {
                this.button1.Text = "Appliquer";
            }
            else
            {
                this.button1.Text = "Apply";
            }
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Border = false;
            this.button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.borderRadius = 5;
            this.button2.Location = new System.Drawing.Point(634, 337);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 36);
            this.button2.TabIndex = 20;
            if (DataSettings.Language == Language.French)
            {
                this.button2.Text = "Annuler";
            }
            else
            {
                this.button2.Text = "Cancel";
            }
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //
            // rectangle3
            //
            this.rectangle3.Border = true;
            this.rectangle3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.rectangle3.BorderRadius = 10;
            this.rectangle3.BorderWidth = 1;
            this.rectangle3.Location = new System.Drawing.Point(570, 30);
            this.rectangle3.Name = "rectangle3";
            this.rectangle3.Size = new System.Drawing.Size(272, 200);
            this.rectangle3.TabIndex = 11;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(589, 35);
            this.label5.Name = "label5";
            this.label5.TabIndex = 4;
            if (DataSettings.Language == Language.French)
            {
                this.label5.Text = "Accélération matérielle :";
            }
            else
            {
                this.label5.Text = "Hardware acceleration :";
            }
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radioButton4.Location = new System.Drawing.Point(10, 78);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(89, 19);
            this.radioButton4.TabIndex = 9;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Direct3D 11";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radioButton5.Location = new System.Drawing.Point(110, 78);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(89, 19);
            this.radioButton5.TabIndex = 9;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Directx (DXVA) 2.0";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.radioButton6.Location = new System.Drawing.Point(10, 98);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(89, 19);
            this.radioButton6.TabIndex = 9;
            this.radioButton6.TabStop = true;
            if (DataSettings.Language == Language.French)
            {
                this.radioButton6.Text = "Désactiver";
            }
            else
            {
                this.radioButton6.Text = "Disable";
            }
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(575, 75);
            this.label6.Name = "label6";
            this.label6.TabIndex = 6;
            if (DataSettings.Language == Language.French)
            {
                this.label6.Text = "Pour la lecture audio et vidéo :";
            }
            else
            {
                this.label6.Text = "For audio and video play :";
            }
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.Location = new System.Drawing.Point(575, 160);
            this.label7.Name = "label7";
            this.label7.TabIndex = 6;
            if (DataSettings.Language == Language.French)
            {
                this.label7.Text = "Pour l'affichage des images :";
            }
            else
            {
                this.label7.Text = "For images display :";
            }
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox7.Location = new System.Drawing.Point(581, 190);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(246, 38);
            this.checkBox7.TabIndex = 16;
            if (DataSettings.Language == Language.French)
            {
                this.checkBox7.Text = "Afficher les images avec Direct2D";
            }
            else
            {
                this.checkBox7.Text = "Display images with Direc2D";
            }
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ButtonMaximizedVisible = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(840, 346);
            this.Location = new Point(Screen.FromControl(this).WorkingArea.Width / 2 - this.Width / 2, Screen.FromControl(this).WorkingArea.Height / 2 - this.Height / 2);
            this.Controls.Add(this.rectangle3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rectangle2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.checkBox7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rectangle1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.rectangle3.Controls.Add(this.radioButton4);
            this.rectangle3.Controls.Add(this.radioButton5);
            this.rectangle3.Controls.Add(this.radioButton6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsDialog";
            this.Redimensionnable = false;
            this.ButtonMaximizedVisible = false;
            if (DataSettings.Language == Language.French)
            {
                this.Text = "Sky multi - Réglages";
            }
            else
            {
                this.Text = "Sky multi - Settings";
            }
            this.Controls.SetChildIndex(this.rectangle1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.radioButton1, 0);
            this.Controls.SetChildIndex(this.radioButton2, 0);
            this.Controls.SetChildIndex(this.radioButton3, 0);
            this.Controls.SetChildIndex(this.checkBox2, 0);
            this.Controls.SetChildIndex(this.rectangle2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.checkBox3, 0);
            this.Controls.SetChildIndex(this.checkBox4, 0);
            this.Controls.SetChildIndex(this.checkBox5, 0);
            this.Controls.SetChildIndex(this.checkBox6, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.rectangle3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.checkBox7, 0);
            this.rectangle3.Controls.SetChildIndex(this.radioButton4, 0);
            this.rectangle3.Controls.SetChildIndex(this.radioButton5, 0);
            this.rectangle3.Controls.SetChildIndex(this.radioButton6, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSettings.CliqueMadiaPause = checkBox1.Checked;
            DataSettings.MinimizeWindowsMediaPause = checkBox2.Checked;
            DataSettings.VideoPreviewOnProgressBar = checkBox3.Checked;
            DataSettings.VideoPreviewOnSetTime = checkBox4.Checked;
            DataSettings.UsingDefinitionMax = checkBox5.Checked;
            DataSettings.WhenVideoSetFullScreen = checkBox6.Checked;
            DataSettings.DisplayImagesWithDirec2D = checkBox7.Checked;

            if (radioButton1.Checked == true)
            {
                DataSettings.EndMedia = EndMediaAction.ReadMultimediaNext;
            }

            if (radioButton2.Checked == true)
            {
                DataSettings.EndMedia = EndMediaAction.RestartMultimedia;
            }

            if (radioButton3.Checked == true)
            {
                DataSettings.EndMedia = EndMediaAction.DoNothing;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                DataSettings.Language = Language.French;
            }
            else
            {
                DataSettings.Language = Language.English;
            }

            if (radioButton4.Checked == true)
            {
                DataSettings.HardwareAcceleration = Sky_multi_Core.VlcWrapper.HardwareAccelerationType.d3d11;
            }

            if (radioButton5.Checked == true)
            {
                DataSettings.HardwareAcceleration = Sky_multi_Core.VlcWrapper.HardwareAccelerationType.dxva2;
            }

            if (radioButton6.Checked == true)
            {
                DataSettings.HardwareAcceleration = Sky_multi_Core.VlcWrapper.HardwareAccelerationType.none;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
