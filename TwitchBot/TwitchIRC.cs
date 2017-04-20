using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TwitchIRC {

    public class TwitchIRC {

        private TcpClient IRCConnection = null;
        private NetworkStream ns = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;

        private string channel = null;
        private string oauth = null;
        private string name = null;

        public TwitchIRC(string channel, string name, string oauth) {
            this.channel = channel;
            this.name = name;
            this.oauth = oauth;
        }

        private string server = "irc.chat.twitch.tv";
        private int port = 6667;
        public bool useSlashMe = false;


        public void send(string cmd, string data) {
            lock(this) {
                if (sw != null)
                        sw.WriteLine(cmd + " " + data);
            }
        }

        public void sendMsg(string text) {
            string cmd = "";
            if (useSlashMe) {
                cmd = "/me ";
            }

            send("PRIVMSG", channel + " :" + cmd + text);
        }

        public void sendWhisper(string text, string user) {
            send("PRIVMSG", String.Format("#jtv :/w {0} {1}", user, text));
            /* This is the format we currently get from the server */
            //send("WHISPER", String.Format("{0} :{1}", user, text));
        }

        public void join() {
            send("JOIN", channel);

            sw.WriteLine("CAP REQ :twitch.tv/tags");
            sw.WriteLine("CAP REQ :twitch.tv/commands");
        }

        public void connect() {
            try {
                IRCConnection = new TcpClient(server, port);
                ns = IRCConnection.GetStream();
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                send("PASS", oauth);
                send("USER", name + " 0 * " + name);
                send("NICK", name);

            }
            catch {

            }
        }

        public string receive() {
            return sr.ReadLine();
        }

    }
}
