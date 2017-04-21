namespace TwitchBot {
    partial class BotGUI {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BotGUI));
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.chatInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.AddAnnounce = new System.Windows.Forms.Button();
            this.AnnouncerList = new System.Windows.Forms.FlowLayoutPanel();
            this.AnnouncersSet = new System.Windows.Forms.Button();
            this.Secretbar = new System.Windows.Forms.TrackBar();
            this.panel7 = new System.Windows.Forms.Panel();
            this.Kraken = new System.Windows.Forms.CheckBox();
            this.Hromp = new System.Windows.Forms.CheckBox();
            this.Secretbox = new System.Windows.Forms.CheckBox();
            this.FoodboxBox = new System.Windows.Forms.CheckBox();
            this.AnnounceBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
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
            this.label10 = new System.Windows.Forms.Label();
            this.AnnounceTimerText = new System.Windows.Forms.TextBox();
            this.CommandTimeoutText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.FoodboxTimeoutText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FoodboxTimerText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HrompTimeoutText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxAutoconnect = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelConnection = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chatBoxRaw = new System.Windows.Forms.RichTextBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Secretbar)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.VersionLabel);
            this.tabPage2.Controls.Add(this.AddAnnounce);
            this.tabPage2.Controls.Add(this.AnnouncerList);
            this.tabPage2.Controls.Add(this.AnnouncersSet);
            this.tabPage2.Controls.Add(this.Secretbar);
            this.tabPage2.Controls.Add(this.panel7);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // AddAnnounce
            // 
            resources.ApplyResources(this.AddAnnounce, "AddAnnounce");
            this.AddAnnounce.Name = "AddAnnounce";
            this.AddAnnounce.UseVisualStyleBackColor = true;
            this.AddAnnounce.Click += new System.EventHandler(this.AddAnnounce_Click);
            // 
            // AnnouncerList
            // 
            resources.ApplyResources(this.AnnouncerList, "AnnouncerList");
            this.AnnouncerList.Name = "AnnouncerList";
            // 
            // AnnouncersSet
            // 
            resources.ApplyResources(this.AnnouncersSet, "AnnouncersSet");
            this.AnnouncersSet.Name = "AnnouncersSet";
            this.AnnouncersSet.UseVisualStyleBackColor = true;
            this.AnnouncersSet.Click += new System.EventHandler(this.AnnouncersSet_Click);
            // 
            // Secretbar
            // 
            this.Secretbar.LargeChange = 10;
            resources.ApplyResources(this.Secretbar, "Secretbar");
            this.Secretbar.Maximum = 100;
            this.Secretbar.Name = "Secretbar";
            this.Secretbar.Value = 54;
            this.Secretbar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Secretbar_Mouse_Up);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.Kraken);
            this.panel7.Controls.Add(this.Hromp);
            this.panel7.Controls.Add(this.Secretbox);
            this.panel7.Controls.Add(this.FoodboxBox);
            this.panel7.Controls.Add(this.AnnounceBox);
            this.panel7.Controls.Add(this.label16);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // Kraken
            // 
            resources.ApplyResources(this.Kraken, "Kraken");
            this.Kraken.Name = "Kraken";
            this.Kraken.UseVisualStyleBackColor = true;
            this.Kraken.CheckedChanged += new System.EventHandler(this.Kraken_CheckedChanged);
            // 
            // Hromp
            // 
            resources.ApplyResources(this.Hromp, "Hromp");
            this.Hromp.Name = "Hromp";
            this.Hromp.UseVisualStyleBackColor = true;
            this.Hromp.CheckedChanged += new System.EventHandler(this.Hromp_CheckedChanged);
            // 
            // Secretbox
            // 
            resources.ApplyResources(this.Secretbox, "Secretbox");
            this.Secretbox.Name = "Secretbox";
            this.Secretbox.UseVisualStyleBackColor = true;
            this.Secretbox.CheckedChanged += new System.EventHandler(this.Secretbox_CheckedChanged);
            // 
            // FoodboxBox
            // 
            resources.ApplyResources(this.FoodboxBox, "FoodboxBox");
            this.FoodboxBox.Name = "FoodboxBox";
            this.FoodboxBox.UseVisualStyleBackColor = true;
            this.FoodboxBox.CheckedChanged += new System.EventHandler(this.FoodboxBox_CheckedChanged);
            // 
            // AnnounceBox
            // 
            resources.ApplyResources(this.AnnounceBox, "AnnounceBox");
            this.AnnounceBox.Name = "AnnounceBox";
            this.AnnounceBox.UseVisualStyleBackColor = true;
            this.AnnounceBox.CheckedChanged += new System.EventHandler(this.AnnounceBox_CheckedChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
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
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.AnnounceTimerText);
            this.panel2.Controls.Add(this.CommandTimeoutText);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.FoodboxTimeoutText);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.FoodboxTimerText);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.HrompTimeoutText);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label11);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // AnnounceTimerText
            // 
            resources.ApplyResources(this.AnnounceTimerText, "AnnounceTimerText");
            this.AnnounceTimerText.Name = "AnnounceTimerText";
            // 
            // CommandTimeoutText
            // 
            resources.ApplyResources(this.CommandTimeoutText, "CommandTimeoutText");
            this.CommandTimeoutText.Name = "CommandTimeoutText";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.timerButtonClick);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // FoodboxTimeoutText
            // 
            resources.ApplyResources(this.FoodboxTimeoutText, "FoodboxTimeoutText");
            this.FoodboxTimeoutText.Name = "FoodboxTimeoutText";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // FoodboxTimerText
            // 
            resources.ApplyResources(this.FoodboxTimerText, "FoodboxTimerText");
            this.FoodboxTimerText.Name = "FoodboxTimerText";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // HrompTimeoutText
            // 
            resources.ApplyResources(this.HrompTimeoutText, "HrompTimeoutText");
            this.HrompTimeoutText.Name = "HrompTimeoutText";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chatBox);
            this.tabPage1.Controls.Add(this.chatInput);
            this.tabPage1.Controls.Add(this.button1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // VersionLabel
            // 
            resources.ApplyResources(this.VersionLabel, "VersionLabel");
            this.VersionLabel.Name = "VersionLabel";
            // 
            // BotGUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "BotGUI";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Secretbar)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.TextBox FoodboxTimerText;
        private System.Windows.Forms.Panel panel2;
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
        private System.Windows.Forms.TextBox HrompTimeoutText;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox FoodboxTimeoutText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBoxAutoconnect;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox chatBoxRaw;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TrackBar Secretbar;
        private System.Windows.Forms.CheckBox Secretbox;
        private System.Windows.Forms.CheckBox FoodboxBox;
        private System.Windows.Forms.CheckBox AnnounceBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox AnnounceTimerText;
        private System.Windows.Forms.TextBox CommandTimeoutText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button AnnouncersSet;
        private System.Windows.Forms.FlowLayoutPanel AnnouncerList;
        private System.Windows.Forms.Button AddAnnounce;
        private System.Windows.Forms.CheckBox Kraken;
        private System.Windows.Forms.CheckBox Hromp;
        private System.Windows.Forms.Label VersionLabel;
    }
}

