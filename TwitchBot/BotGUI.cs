using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using TwitchBot;

namespace TwitchBot
{
    public partial class BotGUI :Form {

        public BotGUI() {
            InitializeComponent();
            
            setFromConfig();

            this.Closing += this.closing;

            YagaBot.instance().lineReceived += this.appendRaw;
            YagaBot.instance().chatReceived += this.appendChat;
            SecretManager.instance().secretUpdated += this.secretUpdate;

            if (Config.instance().settings.autoconnect)
                this.connectToTwitch();
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

            this.setTimingsFromConfig();
            this.setBoxesFromConfig();
            this.setAnnouncesFromConfig();
        }

        private void setTimingsFromConfig()
        {
            Timings timing = Config.instance().settings.timings;
            this.AnnounceTimerText.Text = timing.announceTimer.ToString();
            this.FoodboxTimerText.Text = timing.foodboxTimer.ToString();
            this.FoodboxTimeoutText.Text = timing.foodboxTimeout.ToString();
            this.HrompTimeoutText.Text = timing.hrompTimeout.ToString();
            this.CommandTimeoutText.Text = timing.commandTimeout.ToString();
        }

        private void setBoxesFromConfig()
        {
            Modules modules = Config.instance().settings.modules;
            this.AnnounceBox.Checked = modules.announce;
            this.Secretbox.Checked = modules.secret;
            this.FoodboxBox.Checked = modules.foodbox;
        }

        private void setAnnouncesFromConfig()
        {
            foreach(Announcement a in Config.instance().announces)
            {
                AnnounceControl control = new AnnounceControl();
                control.weight = a.weight;
                control.game = a.game;
                control.active = a.active;
                control.message = a.message;

                this.AnnouncerList.Controls.Add(control);
            }
            this.AnnouncerList.Update();
        }

        private void buttonConnectionDataSave_Click(object sender, EventArgs e) {
            Settings s = Config.instance().settings;

            s.username = textBoxConnectionInfoUser.Text;
            s.channel = textBoxConnectionInfoChannel.Text;
            s.oauth = textBoxConnectionInfoOAuth.Text;

            Config.instance().saveSettings();
        }

        private void Secretbar_Mouse_Up(object sender, EventArgs e)
        {
            int value = this.Secretbar.Value;

            SecretManager.instance().setSecret(value * 100);
        }

        private void secretUpdate(int value)
        {
            this.Secretbar.Value = value / 100;
        }

        private void timerButtonClick(object sender, EventArgs e)
        {
            TwitchBot.Timings timings = new Timings();

            try
            {
                timings.foodboxTimeout = Convert.ToInt32(this.FoodboxTimeoutText.Text);
                timings.hrompTimeout = Convert.ToInt32(this.HrompTimeoutText.Text);
                timings.foodboxTimer = Convert.ToInt32(this.FoodboxTimerText.Text);
                timings.hrompTimeout = Convert.ToInt32(this.HrompTimeoutText.Text);
                timings.commandTimeout = Convert.ToInt32(this.CommandTimeoutText.Text);
                Config.instance().setTimings(timings);
            } catch (Exception exp) {
                Console.WriteLine("Caught exception while setting times: {0}", exp);
                this.setTimingsFromConfig();
            }
        }

        private void AnnounceBox_CheckedChanged(object sender, EventArgs e)
        {
            Config.instance().settings.modules.announce = this.AnnounceBox.Checked;
        }

        private void FoodboxBox_CheckedChanged(object sender, EventArgs e)
        {
            Config.instance().settings.modules.foodbox = this.FoodboxBox.Checked;
        }

        private void Secretbox_CheckedChanged(object sender, EventArgs e)
        {
            Config.instance().settings.modules.secret = this.Secretbox.Checked;
        }

        private void checkBoxAutoconnect_CheckedChanged(object sender, EventArgs e)
        {
            Config.instance().settings.autoconnect = this.checkBoxAutoconnect.Checked;
            Config.instance().saveSettings();
        }

        private void AnnouncersSet_Click(object sender, EventArgs e)
        {
            List<Announcement> list = new List<Announcement>();
            foreach (AnnounceControl control in this.AnnouncerList.Controls)
            {
                Announcement announce = new Announcement();
                announce.game = control.game;
                announce.active = control.active;
                announce.message = control.message;
                announce.weight = control.weight;

                list.Add(announce);
            }

            Config.instance().setAnnounces(list);
        }

        private void AddAnnounce_Click(object sender, EventArgs e)
        {
            this.AnnouncerList.Controls.Add(new AnnounceControl());
        }
    }
}
