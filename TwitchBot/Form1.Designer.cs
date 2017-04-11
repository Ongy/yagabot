namespace Database {
    partial class Form1 {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.chatInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.checkBoxMeEmote = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.buttonOpenQuotes = new System.Windows.Forms.Button();
            this.buttonOpenRabite = new System.Windows.Forms.Button();
            this.buttonOpenFoodbox = new System.Windows.Forms.Button();
            this.buttonOpenVariables = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxCooldownFoodbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonCooldownHromp = new System.Windows.Forms.Button();
            this.textBoxCooldownHromp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelAnnouncerEnabled = new System.Windows.Forms.Label();
            this.buttonAnnouncerEnable = new System.Windows.Forms.Button();
            this.checkBoxAutoannouncer = new System.Windows.Forms.CheckBox();
            this.textBoxAnnouncerTimer = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxAnnouncer2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonAnnouncerDataSave = new System.Windows.Forms.Button();
            this.textBoxAnnouncer1 = new System.Windows.Forms.TextBox();
            this.textBoxDiscord = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonConnectionDataSave = new System.Windows.Forms.Button();
            this.labelConnectionInfo = new System.Windows.Forms.Label();
            this.textBoxConnectionInfoOAuth = new System.Windows.Forms.TextBox();
            this.labelConnectionInfoOAuth = new System.Windows.Forms.Label();
            this.labelConnectionInfoUser = new System.Windows.Forms.Label();
            this.labelConnectionInfoChannel = new System.Windows.Forms.Label();
            this.textBoxConnectionInfoChannel = new System.Windows.Forms.TextBox();
            this.textBoxConnectionInfoUser = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBoxAutotimer = new System.Windows.Forms.CheckBox();
            this.labelTimerEnabled = new System.Windows.Forms.Label();
            this.buttonTimerFoodboxEnable = new System.Windows.Forms.Button();
            this.buttonTimerFoodbox = new System.Windows.Forms.Button();
            this.textBoxTimerFoodboxEmpty = new System.Windows.Forms.TextBox();
            this.textBoxTimerFoodboxFilled = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxAutoconnect = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelConnection = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chatBoxRaw = new System.Windows.Forms.RichTextBox();
            this.timerAnnouncer = new System.Windows.Forms.Timer(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatBox
            // 
            resources.ApplyResources(this.chatBox, "chatBox");
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            // 
            // chatInput
            // 
            resources.ApplyResources(this.chatInput, "chatInput");
            this.chatInput.Name = "chatInput";
            this.chatInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatInput_KeyPress);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chatBox);
            this.tabPage1.Controls.Add(this.chatInput);
            this.tabPage1.Controls.Add(this.button1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.panel7);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.checkBoxMeEmote);
            this.panel7.Controls.Add(this.label16);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // checkBoxMeEmote
            // 
            resources.ApplyResources(this.checkBoxMeEmote, "checkBoxMeEmote");
            this.checkBoxMeEmote.Name = "checkBoxMeEmote";
            this.checkBoxMeEmote.UseVisualStyleBackColor = true;
            this.checkBoxMeEmote.CheckedChanged += new System.EventHandler(this.checkBoxMeEmote_CheckedChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.button7);
            this.panel6.Controls.Add(this.buttonOpenQuotes);
            this.panel6.Controls.Add(this.buttonOpenRabite);
            this.panel6.Controls.Add(this.buttonOpenFoodbox);
            this.panel6.Controls.Add(this.buttonOpenVariables);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.button2);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // buttonOpenQuotes
            // 
            resources.ApplyResources(this.buttonOpenQuotes, "buttonOpenQuotes");
            this.buttonOpenQuotes.Name = "buttonOpenQuotes";
            this.buttonOpenQuotes.UseVisualStyleBackColor = true;
            this.buttonOpenQuotes.Click += new System.EventHandler(this.buttonOpenQuotes_Click);
            // 
            // buttonOpenRabite
            // 
            resources.ApplyResources(this.buttonOpenRabite, "buttonOpenRabite");
            this.buttonOpenRabite.Name = "buttonOpenRabite";
            this.buttonOpenRabite.UseVisualStyleBackColor = true;
            this.buttonOpenRabite.Click += new System.EventHandler(this.buttonOpenRabite_Click);
            // 
            // buttonOpenFoodbox
            // 
            resources.ApplyResources(this.buttonOpenFoodbox, "buttonOpenFoodbox");
            this.buttonOpenFoodbox.Name = "buttonOpenFoodbox";
            this.buttonOpenFoodbox.UseVisualStyleBackColor = true;
            this.buttonOpenFoodbox.Click += new System.EventHandler(this.buttonOpenFoodbox_Click);
            // 
            // buttonOpenVariables
            // 
            resources.ApplyResources(this.buttonOpenVariables, "buttonOpenVariables");
            this.buttonOpenVariables.Name = "buttonOpenVariables";
            this.buttonOpenVariables.UseVisualStyleBackColor = true;
            this.buttonOpenVariables.Click += new System.EventHandler(this.buttonOpenVariables_Click);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.textBoxCooldownFoodbox);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.buttonCooldownHromp);
            this.panel5.Controls.Add(this.textBoxCooldownHromp);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label11);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // textBoxCooldownFoodbox
            // 
            resources.ApplyResources(this.textBoxCooldownFoodbox, "textBoxCooldownFoodbox");
            this.textBoxCooldownFoodbox.Name = "textBoxCooldownFoodbox";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // buttonCooldownHromp
            // 
            resources.ApplyResources(this.buttonCooldownHromp, "buttonCooldownHromp");
            this.buttonCooldownHromp.Name = "buttonCooldownHromp";
            this.buttonCooldownHromp.UseVisualStyleBackColor = true;
            this.buttonCooldownHromp.Click += new System.EventHandler(this.buttonCooldownHromp_Click);
            // 
            // textBoxCooldownHromp
            // 
            resources.ApplyResources(this.textBoxCooldownHromp, "textBoxCooldownHromp");
            this.textBoxCooldownHromp.Name = "textBoxCooldownHromp";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.labelAnnouncerEnabled);
            this.panel4.Controls.Add(this.buttonAnnouncerEnable);
            this.panel4.Controls.Add(this.checkBoxAutoannouncer);
            this.panel4.Controls.Add(this.textBoxAnnouncerTimer);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.textBoxAnnouncer2);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.buttonAnnouncerDataSave);
            this.panel4.Controls.Add(this.textBoxAnnouncer1);
            this.panel4.Controls.Add(this.textBoxDiscord);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // labelAnnouncerEnabled
            // 
            resources.ApplyResources(this.labelAnnouncerEnabled, "labelAnnouncerEnabled");
            this.labelAnnouncerEnabled.Name = "labelAnnouncerEnabled";
            // 
            // buttonAnnouncerEnable
            // 
            resources.ApplyResources(this.buttonAnnouncerEnable, "buttonAnnouncerEnable");
            this.buttonAnnouncerEnable.Name = "buttonAnnouncerEnable";
            this.buttonAnnouncerEnable.UseVisualStyleBackColor = true;
            this.buttonAnnouncerEnable.Click += new System.EventHandler(this.buttonAnnouncerEnable_Click);
            // 
            // checkBoxAutoannouncer
            // 
            resources.ApplyResources(this.checkBoxAutoannouncer, "checkBoxAutoannouncer");
            this.checkBoxAutoannouncer.Name = "checkBoxAutoannouncer";
            this.checkBoxAutoannouncer.UseVisualStyleBackColor = true;
            this.checkBoxAutoannouncer.CheckedChanged += new System.EventHandler(this.checkBoxAutoannouncer_CheckedChanged);
            // 
            // textBoxAnnouncerTimer
            // 
            resources.ApplyResources(this.textBoxAnnouncerTimer, "textBoxAnnouncerTimer");
            this.textBoxAnnouncerTimer.Name = "textBoxAnnouncerTimer";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // textBoxAnnouncer2
            // 
            resources.ApplyResources(this.textBoxAnnouncer2, "textBoxAnnouncer2");
            this.textBoxAnnouncer2.Name = "textBoxAnnouncer2";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // buttonAnnouncerDataSave
            // 
            resources.ApplyResources(this.buttonAnnouncerDataSave, "buttonAnnouncerDataSave");
            this.buttonAnnouncerDataSave.Name = "buttonAnnouncerDataSave";
            this.buttonAnnouncerDataSave.UseVisualStyleBackColor = true;
            this.buttonAnnouncerDataSave.Click += new System.EventHandler(this.buttonAnnouncerDataSave_Click);
            // 
            // textBoxAnnouncer1
            // 
            resources.ApplyResources(this.textBoxAnnouncer1, "textBoxAnnouncer1");
            this.textBoxAnnouncer1.Name = "textBoxAnnouncer1";
            // 
            // textBoxDiscord
            // 
            resources.ApplyResources(this.textBoxDiscord, "textBoxDiscord");
            this.textBoxDiscord.Name = "textBoxDiscord";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.buttonConnectionDataSave);
            this.panel3.Controls.Add(this.labelConnectionInfo);
            this.panel3.Controls.Add(this.textBoxConnectionInfoOAuth);
            this.panel3.Controls.Add(this.labelConnectionInfoOAuth);
            this.panel3.Controls.Add(this.labelConnectionInfoUser);
            this.panel3.Controls.Add(this.labelConnectionInfoChannel);
            this.panel3.Controls.Add(this.textBoxConnectionInfoChannel);
            this.panel3.Controls.Add(this.textBoxConnectionInfoUser);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // buttonConnectionDataSave
            // 
            resources.ApplyResources(this.buttonConnectionDataSave, "buttonConnectionDataSave");
            this.buttonConnectionDataSave.Name = "buttonConnectionDataSave";
            this.buttonConnectionDataSave.UseVisualStyleBackColor = true;
            this.buttonConnectionDataSave.Click += new System.EventHandler(this.buttonConnectionDataSave_Click);
            // 
            // labelConnectionInfo
            // 
            resources.ApplyResources(this.labelConnectionInfo, "labelConnectionInfo");
            this.labelConnectionInfo.Name = "labelConnectionInfo";
            // 
            // textBoxConnectionInfoOAuth
            // 
            resources.ApplyResources(this.textBoxConnectionInfoOAuth, "textBoxConnectionInfoOAuth");
            this.textBoxConnectionInfoOAuth.Name = "textBoxConnectionInfoOAuth";
            // 
            // labelConnectionInfoOAuth
            // 
            resources.ApplyResources(this.labelConnectionInfoOAuth, "labelConnectionInfoOAuth");
            this.labelConnectionInfoOAuth.Name = "labelConnectionInfoOAuth";
            // 
            // labelConnectionInfoUser
            // 
            resources.ApplyResources(this.labelConnectionInfoUser, "labelConnectionInfoUser");
            this.labelConnectionInfoUser.Name = "labelConnectionInfoUser";
            // 
            // labelConnectionInfoChannel
            // 
            resources.ApplyResources(this.labelConnectionInfoChannel, "labelConnectionInfoChannel");
            this.labelConnectionInfoChannel.Name = "labelConnectionInfoChannel";
            // 
            // textBoxConnectionInfoChannel
            // 
            resources.ApplyResources(this.textBoxConnectionInfoChannel, "textBoxConnectionInfoChannel");
            this.textBoxConnectionInfoChannel.Name = "textBoxConnectionInfoChannel";
            // 
            // textBoxConnectionInfoUser
            // 
            resources.ApplyResources(this.textBoxConnectionInfoUser, "textBoxConnectionInfoUser");
            this.textBoxConnectionInfoUser.Name = "textBoxConnectionInfoUser";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.checkBoxAutotimer);
            this.panel2.Controls.Add(this.labelTimerEnabled);
            this.panel2.Controls.Add(this.buttonTimerFoodboxEnable);
            this.panel2.Controls.Add(this.buttonTimerFoodbox);
            this.panel2.Controls.Add(this.textBoxTimerFoodboxEmpty);
            this.panel2.Controls.Add(this.textBoxTimerFoodboxFilled);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // checkBoxAutotimer
            // 
            resources.ApplyResources(this.checkBoxAutotimer, "checkBoxAutotimer");
            this.checkBoxAutotimer.Name = "checkBoxAutotimer";
            this.checkBoxAutotimer.UseVisualStyleBackColor = true;
            this.checkBoxAutotimer.CheckedChanged += new System.EventHandler(this.checkBoxAutotimer_CheckedChanged);
            // 
            // labelTimerEnabled
            // 
            resources.ApplyResources(this.labelTimerEnabled, "labelTimerEnabled");
            this.labelTimerEnabled.Name = "labelTimerEnabled";
            // 
            // buttonTimerFoodboxEnable
            // 
            resources.ApplyResources(this.buttonTimerFoodboxEnable, "buttonTimerFoodboxEnable");
            this.buttonTimerFoodboxEnable.Name = "buttonTimerFoodboxEnable";
            this.buttonTimerFoodboxEnable.UseVisualStyleBackColor = true;
            // 
            // buttonTimerFoodbox
            // 
            resources.ApplyResources(this.buttonTimerFoodbox, "buttonTimerFoodbox");
            this.buttonTimerFoodbox.Name = "buttonTimerFoodbox";
            this.buttonTimerFoodbox.UseVisualStyleBackColor = true;
            this.buttonTimerFoodbox.Click += new System.EventHandler(this.buttonTimerFoodbox_Click);
            // 
            // textBoxTimerFoodboxEmpty
            // 
            resources.ApplyResources(this.textBoxTimerFoodboxEmpty, "textBoxTimerFoodboxEmpty");
            this.textBoxTimerFoodboxEmpty.Name = "textBoxTimerFoodboxEmpty";
            // 
            // textBoxTimerFoodboxFilled
            // 
            resources.ApplyResources(this.textBoxTimerFoodboxFilled, "textBoxTimerFoodboxFilled");
            this.textBoxTimerFoodboxFilled.Name = "textBoxTimerFoodboxFilled";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.checkBoxAutoconnect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelConnection);
            this.panel1.Controls.Add(this.buttonConnect);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // checkBoxAutoconnect
            // 
            resources.ApplyResources(this.checkBoxAutoconnect, "checkBoxAutoconnect");
            this.checkBoxAutoconnect.Name = "checkBoxAutoconnect";
            this.checkBoxAutoconnect.UseVisualStyleBackColor = true;
            this.checkBoxAutoconnect.CheckedChanged += new System.EventHandler(this.checkBoxAutoconnect_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelConnection
            // 
            resources.ApplyResources(this.labelConnection, "labelConnection");
            this.labelConnection.Name = "labelConnection";
            // 
            // buttonConnect
            // 
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chatBoxRaw);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chatBoxRaw
            // 
            resources.ApplyResources(this.chatBoxRaw, "chatBoxRaw");
            this.chatBoxRaw.Name = "chatBoxRaw";
            // 
            // timerAnnouncer
            // 
            this.timerAnnouncer.Interval = 10000;
            this.timerAnnouncer.Tick += new System.EventHandler(this.timerAnnouncer_Tick);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.TextBox chatInput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelConnection;
        private System.Windows.Forms.TextBox textBoxTimerFoodboxFilled;
        private System.Windows.Forms.TextBox textBoxTimerFoodboxEmpty;
        private System.Windows.Forms.Button buttonTimerFoodbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonConnectionDataSave;
        private System.Windows.Forms.Label labelConnectionInfo;
        private System.Windows.Forms.TextBox textBoxConnectionInfoOAuth;
        protected internal System.Windows.Forms.Label labelConnectionInfoOAuth;
        private System.Windows.Forms.Label labelConnectionInfoUser;
        private System.Windows.Forms.Label labelConnectionInfoChannel;
        private System.Windows.Forms.TextBox textBoxConnectionInfoChannel;
        private System.Windows.Forms.TextBox textBoxConnectionInfoUser;
        private System.Windows.Forms.Label labelTimerEnabled;
        private System.Windows.Forms.Button buttonTimerFoodboxEnable;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonAnnouncerDataSave;
        private System.Windows.Forms.TextBox textBoxAnnouncer1;
        private System.Windows.Forms.TextBox textBoxDiscord;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAnnouncer2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button buttonCooldownHromp;
        private System.Windows.Forms.TextBox textBoxCooldownHromp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxCooldownFoodbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBoxAutotimer;
        private System.Windows.Forms.CheckBox checkBoxAutoconnect;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button buttonOpenQuotes;
        private System.Windows.Forms.Button buttonOpenRabite;
        private System.Windows.Forms.Button buttonOpenFoodbox;
        private System.Windows.Forms.Button buttonOpenVariables;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer timerAnnouncer;
        private System.Windows.Forms.TextBox textBoxAnnouncerTimer;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox checkBoxAutoannouncer;
        private System.Windows.Forms.Label labelAnnouncerEnabled;
        private System.Windows.Forms.Button buttonAnnouncerEnable;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox chatBoxRaw;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox checkBoxMeEmote;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}

