using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace TwitchBot {

    public class Constants {
        public static readonly char[] spaceArray = new [] { ' ' };
        public static readonly char[] equalArray = new [] { '=' };
        public static readonly char[] colonArray = new [] { ';' };
        public static readonly char[] exclaArray = new [] { '!' };
        public static readonly char[] newliArray = new [] { '\n' };

        public static readonly string hromPARADE = "yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP yagaHROMP";
    }

    class Message {
        public readonly Dictionary<string, string> tags = null;
        public readonly string user = null;
        public readonly string host = null;
        public readonly string command;
        public readonly string content;

        private Dictionary<string, string> parseTags(string tags) {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            string[] parts = tags.Split(Constants.colonArray);
            foreach (string part in parts) {
                /* TODO: In theory we *should* escape tag values */
                string[] kv = part.Split(Constants.equalArray, 2);
                ret.Add(kv[0], kv[1]);
            }
            return ret;
        }

        internal Message(string msg) {
            string current = msg;
            while (true) {
                string[] splits = current.Split(Constants.spaceArray, 2);
                switch (splits[0][0]) {
                    case '@':
                    this.tags = parseTags(splits[0].Substring(1));
                    break;
                    case ':':
                    string[] tmp = splits[0].Substring(1).Split(Constants.exclaArray, 2);
                    this.user = tmp[0];
                    if (tmp.Length > 1)
                        this.host = tmp[1];
                    break;
                    default:
                    this.command = splits[0];
                    this.content = splits[1];
                    return;
                }
                current = splits[1];
            }
        }

        public string getName() {
            if (this.tags.ContainsKey("display-name"))
                return this.tags["display-name"];

            if (this.tags.ContainsKey("login"))
                return this.tags["login"];

            return this.user;
        }

        public bool isBroadcaster() {
            Console.WriteLine("Checking if broadcaster");
            if (this.tags.ContainsKey("badges") && this.tags["badges"].Contains("broadcaster/1"))
                Console.WriteLine("Is broadcaster");
            return this.tags.ContainsKey("badges") && this.tags["badges"].Contains("broadcaster/1");
        }

        public bool isBotOp() {
            return Config.instance().getBotMods().Contains(this.user) || this.isBroadcaster();
        }

        public bool isMod() {
            return (this.tags.ContainsKey("mod") && this.tags["mod"].Equals("1"))
                    || this.isBotOp();
        }

        /* This should be replaced at some point for spamming/botting detection */
        public bool isNormal() {
            /* TODO: check if user is following */
            return true;
        }

        public int getLvl() {
            if (this.isBroadcaster())
                return 100;

            if (this.isBotOp())
                return 75;

            if (this.isMod())
                return 50;

            if (this.isNormal())
                return 25;

            return 0;
        }
    }


    class YagaBot {
        private static YagaBot obj;
        private TwitchIRC.TwitchIRC irc;
        Thread thread;
        private bool run = true;

        public delegate void ConnectedChat();
        public event ConnectedChat chatConnected;

        public void connect(string channel, string name, string oauth) {
            this.irc = new TwitchIRC.TwitchIRC(channel, name, oauth);
            irc.connect();
            irc.join();
            Kraken.instance().connect();

            if (this.chatConnected != null)
                this.chatConnected();

            thread.Start();
        }

        public void stop() {
            this.run = false;
            this.thread.Abort();
        }

        private void ircLoop() {
            while (run) {
                try {
                    string line = this.irc.receive();
                    this.handleMessage(line);
                } catch (ThreadAbortException) {
                } catch (Exception e) {
                    Console.WriteLine(String.Format("Exception in ircLoop: {0}", e));
                }
            }
        }

        private YagaBot() {
            this.irc = null;
            thread = new Thread(this.ircLoop);
        }

        private static void redirectOutput() {
            StreamWriter file = new StreamWriter("errors.log");
            file.AutoFlush = true;
            Console.SetError(file);
            Console.SetOut(file);
        }

        private static void createModules() {
            if (!Config.instance().settings.spectator) {
                redirectOutput();
            }

            /* call instance() method once to make sure it is created */
            SecretManager.instance();
            Announcer.instance();
            Kraken.instance();
            WhisperCommands.instance();
            new FoodBox();
            new QuoteManager();
            new HrompManager();
        }

        public static YagaBot instance() {
            if (obj == null) {
                obj = new YagaBot();
                createModules();
            }

            return obj;
        }

        public delegate void LineReceive(string line);
        public event LineReceive lineReceived;

        public delegate void UserSubscribe(string displayName, int months, string userId);
        public event UserSubscribe userSubscribed;

        public delegate void ChatLine(Message msg, string line);
        public event ChatLine chatReceived;

        public delegate void ReceivedWhisper(Message msg, string content);
        public event ReceivedWhisper recvWhisper;

        public void sendMessage(string message) {
            if (this.irc == null)
                return;

            if (Config.instance().settings.spectator) {
                Console.Write("Sending: ");
                Console.WriteLine(message);
                return;
            }

            string[] splits = message.Split(Constants.newliArray);
            foreach (string split in splits) {
                if (!"".Equals(split))
                    this.irc.sendMsg(split);
            }
        }

        public void sendMessage(Message cxt, string message) {
            this.sendMessage(message.Replace("{user}", cxt.getName()));
        }

        public void answerWhisper(Message cxt, string message) {
            string[] splits = message.Split(Constants.newliArray);

            foreach (string split in splits)
                this.irc.sendWhisper(split, cxt.user);
        }

        private void handleNotify(string message) {
            char[] subSep = { ':' };
            string subMsg = message.Split(subSep)[2];

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
                if (this.userSubscribed != null)
                    userSubscribed(subUser, 0, null);

                irc.sendMsg("THANK YOU " + subUser + " FOR SUPPORTING THE CHANNEL yagaHROMP yagaHROMP yagaHROMP");
            }
        }

        private void handleResub(Message msg) {
            /* We know the command is USERNOTICE here */
            int months = Convert.ToInt32(msg.tags["msg-param-months"]);

            if (this.userSubscribed != null)
                userSubscribed(msg.getName(), months, msg.user);
            /* 'yagaHROMP ' is 11 chars */
            StringBuilder builder = new StringBuilder(11 * months + 2);
            for (int i = 0; i < months; ++i)
                builder.Append("yagaHROMP ");

            string react = String.Format("THANK YOU {0} FOR {1} HROMPs IN A ROW! {2}", msg.getName(), months, builder);

            irc.sendMsg(react);
        }

        private void handleWHISPER(Message msg) {
            string[] lines = msg.content.Split(Constants.spaceArray, 2);
            string line = lines[1].Substring(1);

            if (this.recvWhisper != null)
                        recvWhisper(msg, line);
        }

        private void handlePRIVMSG(Message msg) {
            string[] lines = msg.content.Split(Constants.spaceArray, 2);
            string line = lines[1].Substring(1);

            chatReceived(msg, line);
        }

        private void handleMessage(string message) {
            if (lineReceived != null)
                lineReceived(message);

            Message msg = new Message(message);
            if (msg.command.Equals("PING")) {
                irc.send("PONG", ":tmi.twitch.tv");
            } else if (msg.command.Equals("USERNOTICE")) {
                this.handleResub(msg);
            } else if (msg.command.Equals("WHISPER")) {
                this.handleWHISPER(msg);
            } else if (msg.command.Equals("PRIVMSG")) {
                if (msg.user.Equals("twitchnotify"))
                    this.handleNotify(message);
                else
                    this.handlePRIVMSG(msg);
            }
        }
    }

}
