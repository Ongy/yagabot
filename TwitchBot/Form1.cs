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

namespace Database {
    public partial class Form1 :Form {

        private string channel = Variables.settingsGetData("general", "channel");

        private TwitchIRC.TwitchIRC irc = new TwitchIRC.TwitchIRC();
	/*
	 * The reason for singletons is not to have to do this, but it doesn't
	 * hurt either, since the commands singleton is never reloaded
	 */
        private Commands cmds = Commands.Instance();
        



        public Form1() {
            InitializeComponent();

            //irc.connect();
            //irc.join();

            //var c = chat();
            getData();
            checkBoxes();




        }

        private async Task chat() {

            try {

                string data;

                string user;
                bool mod;
                string message;
                string roomid;
                string userid;
                DateTime lastMessage;
                lastMessage = DateTime.Now;
                Dictionary<string, DateTime> hrompCooldown = new Dictionary<string, DateTime>();
                Dictionary<string, DateTime> foodboxCooldown = new Dictionary<string, DateTime>();


                while ((data = await irc.receive()) != null) {

                    chatBoxRaw.AppendText(data + Environment.NewLine);





                    if (data.Contains("PING")) {
                        irc.send("PONG", ":tmi.twitch.tv");
                    }

                    //sub and resub announcments
                    if ((data.Contains("twitchnotify")) && (data.Contains("subscribed"))) {

                        char[] subSep = { ':' };
                        string subMsg = data.Split(subSep)[2];

                        if (subMsg.Contains("months")) {
                            char[] subSep2 = { ' ' };
                            string subUser = subMsg.Split(subSep2)[0];
                            string subMonths = subMsg.Split(subSep2)[3];

                            int subMonths2 = Convert.ToInt32(subMonths);
                            string subHromps = "";
                            for (int x = 0; x < subMonths2; x++) {
                                subHromps = subHromps + "yagaHROMP ";
                            }

                            irc.sendMsg("THANK YOU " + subUser + " FOR " + subMonths + " HROMPs IN A ROW! " + subHromps);
                        }
                        else {
                            char[] subSep2 = { ' ' };
                            string subUser = subMsg.Split(subSep2)[0];

                            irc.sendMsg("THANK YOU " + subUser + " FOR SUPPORTING THE CHANNEL yagaHROMP yagaHROMP yagaHROMP");
                        }
			    /* We can exit here with continue? Would be worth a * test */

                    }


                    if ((data.Contains("PRIVMSG")) && (!data.Contains("twitchnotify"))) {

                        char[] dataSep = { '=', ';' };
                        user = data.Split(dataSep)[5];
                        roomid = data.Split(dataSep)[11];
                        userid = data.Split(dataSep)[17];
                        mod = data.Split(dataSep)[9].Equals("1");
                        if ((mod == false) && (roomid == userid))
                            mod = true;
                        message = data.Split(new string[] { channel + " :" }, StringSplitOptions.None)[1];
                        if (user == "") {
                            char[] dataSep2 = { '!', '@' };
                            user = data.Split(dataSep2)[2];
                        }
                        if ((message.Contains("yag")) || (message.Contains("Yag"))) {
                            chatBox.SelectionColor = Color.White;
                            chatBox.SelectionBackColor = Color.Red;
                        }
                        chatBox.AppendText(DateTime.Now + " " + user + ": " + message + Environment.NewLine);

                        chatBox.SelectionStart = chatBox.Text.Length;
                        chatBox.ScrollToCaret();

                        message = message.ToLower();

			/* This should be preseeded with a test message[0] == '!'
			 * And drop the !
			 */
                        if (DateTime.Now - lastMessage > TimeSpan.FromSeconds(3)) {
				/* So, I saw it being used, we have runtime
				 * creatable commands.\
				 * All static comands should (imo) be moved
				 * into the runtime comands config file!
				 * Probably with some {user} like markup, to
				 * make the semi-static commands usable aswell.
				 *
				 * For the others, I think a hashmap from
				 * string to void function(string) would make
				 * sense.
				 * then go with something like:
				 *
				 * splits = message.split(' ')
				 * map[splits[0]](splits[1])
				 *
				 * This would force users to ad ' ', which is
				 * currently not required, but imo worth it.
				 */
                            switch (message) {

                                case "!help":
                                    irc.sendMsg("Use !foodbox for info on how to feed the rabite, use !commands for info about additional commands");
                                    break;

				    /* This should be a coded command, but read
				     * from the mentioned list and hashmap, not
				     * use a hardcoded list
				     */
                                case "!commands":
                                    irc.sendMsg("Available commands: !foodbox, !quote, !meow, !lore%, !hug, !hromp, !pet, !hype, !megahype, !enoughhype, !legend, !d2guide, !moo, !secret, !glitchless");
                                    break;

                                case "!cuddle":
                                    irc.sendMsg("The rabite cuddles up to " + user + " yagaHappy");
                                    break;

                                case "!quote":
                                    string quote = Variables.quoteRandom();
                                    irc.sendMsg(quote);
                                    break;

                                case "!meow":
                                    irc.sendMsg("Yagameowth to the rescue! CoolCat");
                                    break;

                                case "!discord":
                                    string discord = Variables.varRead("discord");
                                    irc.sendMsg("We are on discord! " + discord);
                                    break;

                                case "!lore%":
                                    irc.sendMsg("Yagamoth tells the story of the game while also explaining the speedrun. This leaves little time to respond to chat. Thank you for understanding, and feel free to just whisper or PM him :)");
                                    break;

                                case "!hug":
                                    irc.sendMsg(user + " sends some hugs into the chat");
                                    break;

                                case "!pet":
                                    string petHromp = Commands.petRandom();
                                    irc.sendMsg(user + " tries to pet the Rabite. " + petHromp);
                                    break;
                                    
                                case "!legend":
                                    irc.sendMsg("Who is a legend? YOU ARE A LEGEND!!");
                                    break;

                                case "!hype":
                                    irc.sendMsg("ʰʸᵖᵉ ʰʸᵖᵉ ʰʸᵖᵉ ʰʸᵖᵉ ʰʸᵖᵉ ");
                                    break;

                                case "!megahype":
                                    irc.sendMsg("ʰʸᵖᵉ... hype.. Hype.. HYPE! " + user + " SENDS ALL THE HYPE TO THE CHAT!");
                                    break;

                                case "!enoughhype":
                                    irc.sendMsg("There can never be enough hype!");
                                    break;

                                case "!d2guide":
                                    irc.sendMsg("Diablo 2 speedrun guides: http://www.speedrun.com/d2lod/guide/849dl also check out mrllama's youtube guides: https://www.youtube.com/playlist?list=PLchTMRijDSBzG-0ZiXPj_YuV9W9j1Ldj5");
                                    break;

                                case "!moo":
                                    irc.sendMsg("There is no cow level! Everything you believe you have heard about it is false!");
                                    break;

                                case "!glitchless":
                                    irc.sendMsg("Rules concerning the \"Glitchless\" category can be found here: http://pastebin.com/m2AHCxFf");
                                    break;

                                case "!secret":
                                    string secret = Variables.varRead("secret");
                                    int secretint = Convert.ToInt32(secret);
                                    secretint++;
                                    double percent = ((10000 / 173) * secretint);
                                    percent = percent / 100;
                                    string percentstring = percent.ToString("0.0#");
                                    secret = "" + secretint;
                                    if (secret == "173") {
                                        irc.sendMsg("OH NO! THE HROMPARADE!");
                                        irc.sendMsg("yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP");
                                        irc.sendMsg("yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP");
                                        irc.sendMsg("yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP");
                                        irc.sendMsg("yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP");
                                        secret = "0";
                                    }
                                    else {
                                        irc.sendMsg(percent + "%");
                                        Variables.varWrite("secret", secret);

                                    }

                                    break;
                                    
                                case "!hromp":
                                    if (hrompCooldown.ContainsKey(user)) {
                                        string hrompCooldownSpanString = Variables.settingsGetData("timer", "timerHrompCooldown");
                                        int hrompCooldownSpan = Convert.ToInt32(hrompCooldownSpanString);
                                        if (DateTime.Now - (hrompCooldown[user]) > TimeSpan.FromMinutes(hrompCooldownSpan)) {
                                            irc.sendMsg("The rabite plays around and trains with Yagamoth yagaHappy It earned 1 Exp!");
                                            string hrompLevelUp = Commands.hromp(1);
                                            if (hrompLevelUp != "Nein") {
                                                irc.sendMsg("The rabite leveled up! yagaHappy It is now level " + hrompLevelUp);
                                            }
                                            hrompCooldown[user] = DateTime.Now;
                                        }
                                    }
                                    else {
                                        irc.sendMsg("The rabite plays around and trains with " + user + " yagaHappy It earned 1 Exp!");
                                        string hrompLevelUp = Commands.hromp(1);
                                        if (hrompLevelUp != "Nein") {
                                            irc.sendMsg("The rabite leveled up! yagaHappy It is now level " + hrompLevelUp + " yagaHROMP");
                                        }
                                        hrompCooldown.Add(user, DateTime.Now);
                                    }
                                    
                                    
                                    break;

                                case "!hromp new":
                                    if (mod == true) {
                                        bool hrompNew = Variables.hrompNew(user);
                                        if (hrompNew == true) {
                                            irc.sendMsg("A new rabite was born! " + user + " adopted it!");
                                        }
                                        else {
                                            irc.sendMsg("A new rabite was born! But " + user + " already owns one, so the little guy wanders off into the wild yagaDerp");
                                        }
                                        
                                        
                                    }
                                    break;

                               

                                default:
                                    break;

                            }

                            /*foodbox starts with food btw.*/
                            if (message.StartsWith("!foodbox") || message.StartsWith("!food") || message.StartsWith("!box")) {
                                string foodboxCmd = Variables.foodList();
                                string foodboxAmount = Variables.foodAmount();
                                irc.sendMsg("type \"!delicious {food}\" into chat! The box currently contains " + foodboxAmount + " items.");
                                irc.sendMsg(foodboxCmd);

                            }
                            





                            if (cmds.CheckCommand(message)) {
                                irc.sendMsg(cmds.GetCommand(message));
                            }

                            //adding items to the list of eatable things
                            if (message.StartsWith("!foodboxadd")) {

                                char[] foodboxSep = { '#' };
                                string[] foodboxChecker = message.Split(foodboxSep, 5);

                                if (foodboxChecker.Length == 5 && foodboxChecker[0].Equals("!foodboxadd") && mod == true) {
                                    Variables.foodNew(foodboxChecker[1], foodboxChecker[2], foodboxChecker[3], foodboxChecker[4]);
                                    irc.sendMsg(foodboxChecker[1] + " added as possibility to the Foodbox");
                                }
                                
                            }

                            //increasing foodbox Quantity of a chosen type by 1
                            if (message.StartsWith("!delicious")) {
                                if((message.StartsWith("!delicious ") == false)) {
                                    message = message.Replace("!delicious", "!delicious ");
                                }


                                char[] foodboxSep = { ' ' };
                                string[] foodboxChecker = message.Split(foodboxSep, 2);
                                string foodboxOK = Variables.foodCheck(foodboxChecker[1]);
                                if (foodboxChecker.Length == 2 && foodboxChecker[0].Equals("!delicious")) {
                                    if (foodboxOK == "OK") {

                                        if (foodboxCooldown.ContainsKey(user)) {
                                            string foodboxCooldownSpanString = Variables.settingsGetData("timer", "timerfoodboxCooldown");
                                            int foodboxCooldownSpan = Convert.ToInt32(foodboxCooldownSpanString);
                                            if (DateTime.Now - (foodboxCooldown[user]) > TimeSpan.FromMinutes(foodboxCooldownSpan)) {
                                                string foodboxText = Variables.foodInc(foodboxChecker[1]);
                                                string foodboxCount = Variables.foodQuantity(foodboxChecker[1]);
                                                if (foodboxText.Contains("{user}")) {
                                                    foodboxText = foodboxText.Replace("{user}", user);
                                                }
                                                irc.sendMsg(foodboxText);
                                                foodboxCooldown[user] = DateTime.Now;
                                            }
                                        }
                                        else {

                                            string foodboxText = Variables.foodInc(foodboxChecker[1]);
                                            string foodboxCount = Variables.foodQuantity(foodboxChecker[1]);
                                            if (foodboxText.Contains("{user}")) {
                                                foodboxText = foodboxText.Replace("{user}", user);
                                            }
                                            irc.sendMsg(foodboxText);
                                            foodboxCooldown.Add(user, DateTime.Now);
                                        }

                                    }
                                    else {
                                        irc.sendMsg("No such item used. Contact a mod to get it added");
                                    }
                                }
                                

                            }

                            //add a quote
                            if (message.StartsWith("!addquote")) {

                                char[] quoteSep = { ' ' };
                                string[] quoteChecker = message.Split(quoteSep, 2);

                                if (quoteChecker.Length == 2 && quoteChecker[0].Equals("!addquote")) {
                                    Variables.quoteNew(quoteChecker[1]);
                                    irc.sendMsg("Quote added: " + quoteChecker[1]);
                                }

                            }


                            //pure textbased commands
                            char[] messageSep = { ' ' };
                            string[] cmdChecker = message.Split(messageSep, 3);

                            if (cmdChecker.Length == 3 && cmdChecker[0].Equals("!addcmd") && mod == true) {
                                bool check = cmds.AddCommand(cmdChecker[1], cmdChecker[2]);

                                if (check)
                                    irc.sendMsg("Command Added");
                                else
                                    irc.sendMsg("Command exists already");
                            }

                            if (cmdChecker.Length == 2 && cmdChecker[0].Equals("!delcmd") && mod == true) {

                                bool check = cmds.RemoveCommand(cmdChecker[1]);

                                if (check)
                                    irc.sendMsg("Command Removed");
                                else
                                    irc.sendMsg("Command doesn't exist");
                            }

                            

                            if (message.StartsWith("!")) {
                                lastMessage = DateTime.Now;
                            }
                             

                        }
                    }

                    if (data.Contains("WHISPER")) {

                        char[] dataSep = { '=', ';' };
                        user = data.Split(dataSep)[5];
                        
                        message = data.Split(new string[] { "yagabot :" }, StringSplitOptions.None)[1];

                        
                        chatBox.SelectionColor = Color.Blue;                         
                        chatBox.AppendText(DateTime.Now + " " + user + ": " + message + Environment.NewLine);

                        if (message == "hi")
                            irc.sendWhisper("Hello " + user, user);

                        if (message == "!hype")
                            irc.sendWhisper("secret hype!", user);

                    }




                }
            }
            catch {

            }
        }

        private void chatInput_KeyPress(object sender, KeyPressEventArgs e) {

            if (labelConnection.Text == "Connected") {

                if (e.KeyChar == (char)13) {

                    irc.sendMsg(chatInput.Text);

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

                irc.sendMsg(chatInput.Text);

                chatBox.AppendText(DateTime.Now + " Bot: " + chatInput.Text + Environment.NewLine);

                chatInput.Clear();

            }
            else {
                System.Windows.Forms.MessageBox.Show("You have to connect before sending Messages!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {

            

            bool foodboxCheck = Variables.foodEmpty();

            if (foodboxCheck == false) {
                
                string timerFoodboxFilled = Variables.settingsGetData("timer", "timerFoodboxFilled");
                int timerFoodboxFilledInt = Convert.ToInt32(timerFoodboxFilled);
                timer1.Interval = (timerFoodboxFilledInt * 60000);

                int foodboxMax = Variables.foodVariety();
                string foodboxResult = "0";            
                foodboxMax++;
                string foodboxQuantity = "0";
               /*This should really be here */
                Random rnd = new Random();
                while (foodboxQuantity == "0") {

                    int foodboxRnd = rnd.Next(1, foodboxMax);
                    foodboxResult = "" + foodboxRnd;

                    foodboxQuantity = Variables.foodQuantityNum(foodboxResult);
                    
                }  

                
                string foodboxTaken = Variables.foodRandom(foodboxResult);

                irc.sendMsg(foodboxTaken);
                
                int foodExp = Variables.foodGetExp(foodboxResult);
		/* looking at this, hromp should be Bool? */
                string lvlUp = Commands.hromp(foodExp);
                if (lvlUp != "Nein") {
                    irc.sendMsg("The rabite leveled up! yagaHappy It is now level " + lvlUp + " yagaHROMP");
                }

            }
            else {
                string timerFoodboxEmpty = Variables.settingsGetData("timer", "timerFoodboxEmpty");
                int timerFoodboxEmptyInt = Convert.ToInt32(timerFoodboxEmpty);
                timer1.Interval = (timerFoodboxEmptyInt * 60000);
                irc.sendMsg("The foodbox is Empty! We require more food!");
            }
            


        }

        private void buttonConnect_Click(object sender, EventArgs e) {
            connectToTwitch();
        }

        private void connectToTwitch() {
            irc.connect();
            irc.join();

            var c = chat();
            labelConnection.Text = "Connected";
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
            string settingsUser = textBoxConnectionInfoUser.Text;
            string settingsChannel = textBoxConnectionInfoChannel.Text;
            string settingsOAuth = textBoxConnectionInfoOAuth.Text;
            Variables.settingsSaveData("general", "user", settingsUser);
            Variables.settingsSaveData("general", "channel", settingsChannel);
            Variables.settingsSaveData("general", "oauth", settingsOAuth);
        }

        private void buttonTimerFoodbox_Click(object sender, EventArgs e) {
            string timerFoodboxEmpty = textBoxTimerFoodboxEmpty.Text;
            string timerFoodboxFilled = textBoxTimerFoodboxFilled.Text;
            Variables.settingsSaveData("timer", "timerFoodboxEmpty", timerFoodboxEmpty);
            Variables.settingsSaveData("timer", "timerFoodboxFilled", timerFoodboxFilled);
        }

        private void buttonTimerFoodboxEnable_Click(object sender, EventArgs e) {

            if (labelConnection.Text == "Connected") {

                if (timer1.Enabled == false) {
                    timer1.Enabled = true;
                    labelTimerEnabled.Text = "Enabled";
                }
                else {
                    timer1.Enabled = false;
                    labelTimerEnabled.Text = "Disabled";
                }
            }
            

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

                if (checkboxAutoEmoteState == "true") {
                    checkBoxMeEmote.Checked = true;
                }
            }
            

            //causees if checkboxes are checked
            if (checkBoxAutoconnect.Checked == true) {
                connectToTwitch();
            }

            if (checkBoxAutotimer.Checked == true) {
                timer1.Enabled = true;
                labelTimerEnabled.Text = "Enabled";
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
            if (checkBoxMeEmote.Checked == true) {
                checkboxState = "true";
            }
            else {
                checkboxState = "false";
            }
            Variables.settingsSaveData("general", "emote_me", checkboxState);
        }

        private void timerAnnouncer_Tick(object sender, EventArgs e) {

            string timerAnnouncerVar = Variables.settingsGetData("timer", "timerAnnouncer");
            int timerAnnouncerInt = Convert.ToInt32(timerAnnouncerVar);
            timerAnnouncer.Interval = (timerAnnouncerInt * 60000 + 1);

            string varDiscord = Variables.varRead("discord");
            string varAnnouncer1 = Variables.varRead("announcer1");
            string varAnnouncer2 = Variables.varRead("announcer2");
            if (varDiscord != "") {
                irc.sendMsg(varDiscord);
            }
            if (varAnnouncer1 != "") {
                irc.sendMsg(varAnnouncer1);
            }
            if (varAnnouncer2 != "") {
                irc.sendMsg(varAnnouncer2);
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
