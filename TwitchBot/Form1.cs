using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchIRC;
using System.IO;
using System.Collections;
using DataBase;
using Nocksoft.IO.ConfigFiles;
using TwitchBot;

namespace Database {
    public partial class Form1 :Form {

        public Form1() {
            InitializeComponent();

            getData();
            setFromConfig();
            checkBoxes();

            this.Closing += this.closing;

            YagaBot.instance().lineReceived += this.appendRaw;
            YagaBot.instance().chatReceived += this.appendChat;
        }

        void closing(object s, CancelEventArgs e) {
            YagaBot.instance().stop();
        }

        private void appendRaw(string raw) {
            chatBoxRaw.AppendText(raw + Environment.NewLine);
        }

        private void appendChat(TwitchBot.Message msg, string message) {
            bool highlight = message.Contains("yag") || message.Contains("Yag");

            if (highlight) {
                chatBox.SelectionColor = Color.White;
                chatBox.SelectionBackColor = Color.Red;
            }

            chatBox.AppendText(DateTime.Now + " " + msg.getName() + ": " + message + Environment.NewLine);

            chatBox.SelectionStart = chatBox.Text.Length;
            chatBox.ScrollToCaret();
        }

        private void chatInput_KeyPress(object sender, KeyPressEventArgs e) {

            if (labelConnection.Text == "Connected") {

                if (e.KeyChar == (char)13) {

                    YagaBot.instance().sendMessage(chatInput.Text);

                    chatBox.AppendText(DateTime.Now + " Bot: " + chatInput.Text + Environment.NewLine);

                    chatInput.Clear();

                    e.Handled = true;
                }

            }
            else {
                System.Windows.Forms.MessageBox.Show("You have to connect before sending Messages!");
            }
        }

        private void button1_Click(object sender, EventArgs e) {

            if (labelConnection.Text == "Connected") {

                YagaBot.instance().sendMessage(chatInput.Text);

                chatBox.AppendText(DateTime.Now + " Bot: " + chatInput.Text + Environment.NewLine);

                chatInput.Clear();

            }
            else {
                System.Windows.Forms.MessageBox.Show("You have to connect before sending Messages!");
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e) {
            connectToTwitch();
        }

        private void connectToTwitch() {
            Settings s = Config.instance().settings;

            YagaBot.instance().connect(s.channel, s.username, s.oauth);
            labelConnection.Text = "Connected";
        }

        private void setFromConfig() {
            Settings s = Config.instance().settings;
            textBoxConnectionInfoUser.Text = s.username;
            textBoxConnectionInfoChannel.Text = s.channel;
            textBoxConnectionInfoOAuth.Text = s.oauth;


        }

        private void getData() {
            //connection textboxes
            textBoxConnectionInfoUser.Text = Variables.settingsGetData("general", "user");
            textBoxConnectionInfoChannel.Text = Variables.settingsGetData("general", "channel");
            textBoxConnectionInfoOAuth.Text = Variables.settingsGetData("general", "oauth");
            //foodbox textboxes
            textBoxTimerFoodboxEmpty.Text = Variables.settingsGetData("timer", "timerFoodboxEmpty");
            textBoxTimerFoodboxFilled.Text = Variables.settingsGetData("timer", "timerFoodboxFilled");
            //variable textboxes
            textBoxDiscord.Text = Variables.varRead("discord");
            textBoxAnnouncer1.Text = Variables.varRead("announcer1");
            textBoxAnnouncer2.Text = Variables.varRead("announcer2");
            textBoxAnnouncerTimer.Text = Variables.settingsGetData("timer", "timerAnnouncer");
            //Cooldown textboxes
            textBoxCooldownHromp.Text = Variables.settingsGetData("timer", "timerHrompCooldown");
            textBoxCooldownFoodbox.Text = Variables.settingsGetData("timer", "timerFoodboxCooldown");
        }

        private void buttonConnectionDataSave_Click(object sender, EventArgs e) {
            Settings s = Config.instance().settings;

            s.username = textBoxConnectionInfoUser.Text;
            s.channel = textBoxConnectionInfoChannel.Text;
            s.oauth = textBoxConnectionInfoOAuth.Text;

            Config.instance().saveSettings();
        }

        private void buttonTimerFoodbox_Click(object sender, EventArgs e) {
            string timerFoodboxEmpty = textBoxTimerFoodboxEmpty.Text;
            string timerFoodboxFilled = textBoxTimerFoodboxFilled.Text;
            Variables.settingsSaveData("timer", "timerFoodboxEmpty", timerFoodboxEmpty);
            Variables.settingsSaveData("timer", "timerFoodboxFilled", timerFoodboxFilled);
        }

        private void checkBoxes() {
            string checkboxAutoConnectState = Variables.settingsGetData("general", "autoconnect");
            string checkboxAutoTimerState = Variables.settingsGetData("general", "autotimer");
            string checkboxAutoAnnouncerState = Variables.settingsGetData("general", "autoannouncer");
            string checkboxAutoEmoteState = Variables.settingsGetData("general", "emote_me");
            //set checkboxes
            if (checkboxAutoConnectState == "true") {
                checkBoxAutoconnect.Checked = true;

                if (checkboxAutoTimerState == "true") {
                    checkBoxAutotimer.Checked = true;
                }

                if (checkboxAutoAnnouncerState == "true") {
                    checkBoxAutoannouncer.Checked = true;
                }

                if (checkboxAutoEmoteState.Equals("true")) {
                    checkBoxMeEmote.Checked = true;
                }
            }

            //causees if checkboxes are checked
            if (checkBoxAutoconnect.Checked == true) {
                connectToTwitch();
            }

            if (checkBoxAutoannouncer.Checked == true) {
                timerAnnouncer.Enabled = true;
                labelAnnouncerEnabled.Text = "Enabled";
            }

        }

        private void buttonAnnouncerDataSave_Click(object sender, EventArgs e) {
            string variableDiscord = textBoxDiscord.Text;
            string variableAnnouncer1 = textBoxAnnouncer1.Text;
            string variableAnnouncer2 = textBoxAnnouncer2.Text;
            string variableTimer = textBoxAnnouncerTimer.Text;
            Variables.varWrite("discord", variableDiscord);
            Variables.varWrite("announcer1", variableAnnouncer1);
            Variables.varWrite("announcer2", variableAnnouncer2);
            Variables.settingsSaveData("timer", "timerAnnouncer", variableTimer);
        }

        private void buttonCooldownHromp_Click(object sender, EventArgs e) {
            string timerHrompCooldown = textBoxCooldownHromp.Text;
            string timerFoodboxCooldown = textBoxCooldownFoodbox.Text;
            Variables.settingsSaveData("timer", "timerHrompCooldown", timerHrompCooldown);
            Variables.settingsSaveData("timer", "timerFoodboxCooldown", timerFoodboxCooldown);
        }

        private void button2_Click(object sender, EventArgs e) {
            string directory = Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                FileName = directory,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void checkBoxAutoconnect_CheckedChanged(object sender, EventArgs e) {
            string checkboxState;
            if (checkBoxAutoconnect.Checked == true) {
                checkboxState = "true";
            }
            else {
                checkboxState = "false";
            }
            Variables.settingsSaveData("general", "autoconnect", checkboxState);
        }

        private void checkBoxAutotimer_CheckedChanged(object sender, EventArgs e) {
            string checkboxState;
            if (checkBoxAutotimer.Checked == true) {
                checkboxState = "true";
            }
            else {
                checkboxState = "false";
            }
            Variables.settingsSaveData("general", "autotimer", checkboxState);
        }

        private void checkBoxAutoannouncer_CheckedChanged(object sender, EventArgs e) {
            string checkboxState;
            if (checkBoxAutoannouncer.Checked == true) {
                checkboxState = "true";
            }
            else {
                checkboxState = "false";
            }
            Variables.settingsSaveData("general", "autoannouncer", checkboxState);
        }

        private void checkBoxMeEmote_CheckedChanged(object sender, EventArgs e) {
            string checkboxState;
            if (checkBoxMeEmote.Checked) {
                checkboxState = "true";
            }
            else {
                checkboxState = "false";
            }

            Variables.settingsSaveData("general", "emote_me", checkboxState);
            // irc.useSlashMe = checkBoxMeEmote.Checked;
        }

        private void timerAnnouncer_Tick(object sender, EventArgs e) {

            string timerAnnouncerVar = Variables.settingsGetData("timer", "timerAnnouncer");
            int timerAnnouncerInt = Convert.ToInt32(timerAnnouncerVar);
            timerAnnouncer.Interval = (timerAnnouncerInt * 60000 + 1);

            string varDiscord = Variables.varRead("discord");
            string varAnnouncer1 = Variables.varRead("announcer1");
            string varAnnouncer2 = Variables.varRead("announcer2");
            if (varDiscord != "") {
                YagaBot.instance().sendMessage(varDiscord);
            }
            if (varAnnouncer1 != "") {
                YagaBot.instance().sendMessage(varAnnouncer1);
            }
            if (varAnnouncer2 != "") {
                YagaBot.instance().sendMessage(varAnnouncer2);
            }
        }

        private void button7_Click(object sender, EventArgs e) {
            string directory = Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(directory + "/settings.ini");
        }

        private void buttonOpenVariables_Click(object sender, EventArgs e) {
            string directory = Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(directory + "/variables.ini");
        }

        private void buttonOpenRabite_Click(object sender, EventArgs e) {
            string directory = Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(directory + "/rabites.ini");
        }

        private void buttonOpenFoodbox_Click(object sender, EventArgs e) {
            string directory = Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(directory + "/foodbox.ini");
        }

        private void buttonOpenQuotes_Click(object sender, EventArgs e) {
            string directory = Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(directory + "/quotes.ini");
        }

        private void buttonAnnouncerEnable_Click(object sender, EventArgs e) {

            if (labelConnection.Text == "Connected") {
                AnnouncerTimerEnable();
            }
        }

        private void AnnouncerTimerEnable() {
            if (timerAnnouncer.Enabled == false) {
                timerAnnouncer.Enabled = true;
                labelAnnouncerEnabled.Text = "Enabled";
            }
            else {
                timerAnnouncer.Enabled = false;
                labelAnnouncerEnabled.Text = "Disabled";
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            System.Media.SoundPlayer startSoundPlayer = new System.Media.SoundPlayer(@"C:\Windows\Media\chord.wav");
            startSoundPlayer.Play();
        }

    }
}
