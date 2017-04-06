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
        
	/* Move this into long living scope, no reason to load it each time
	 * (reduce IO, the SSD will thank you)
	 */
        private string emote = Database.Variables.settingsGetData("general", "emote_me");

        public void send(string cmd, string data) {
            sw.WriteLine(cmd + " " + data);
        }

        public void sendMsg(string text) {

            //string emote = Database.Variables.settingsGetData("general", "emote_me");
            string emote_me = "";
            if (emote == "true") {
                emote_me = "/me ";
            }
            else {
                emote_me = "";
            }

	    /* we got it, so use it */
	    send("PRIVMSG", channel + " :" + emote_me + text);
            //sw.WriteLine("PRIVMSG " + channel + " :" + emote_me + text);
       
        }

        public void sendWhisper(string text, string user) {
	    /* we got it, so use it */
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
            }
            catch {

            }

            try {
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
