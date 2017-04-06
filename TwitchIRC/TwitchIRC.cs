using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Nocksoft.IO.ConfigFiles;
using DataBase;

namespace TwitchIRC {

    public class TwitchIRC {

        private TcpClient IRCConnection = null;
        private NetworkStream ns = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;

        private string name = Database.Variables.settingsGetData("general", "user");
        private string server = "irc.chat.twitch.tv";
        private int port = 6667;
        private string oauth = Database.Variables.settingsGetData("general", "oauth");

        private string channel = Database.Variables.settingsGetData("general", "channel");

        public bool useSlashMe = Database.Variables.settingsGetData("general", "emote_me").Equals("true");


        public void send(string cmd, string data) {
            sw.WriteLine(cmd + " " + data);
        }

        public void sendMsg(string text) {
            string cmd = "";
            if (useSlashMe) {
                cmd = "/me ";
            }

            send("PRIVMSG", channel + " :" + cmd + text);
        }

        public void sendWhisper(string text, string user) {
            send("PRIVMSG", "#jtv :/w " + user + " " + text);
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

        public Task<string> receive() {
            return sr.ReadLineAsync();
        }

    }
}
