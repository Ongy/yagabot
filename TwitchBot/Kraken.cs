namespace TwitchBot {

    public class ChannelInfo {
        public string status;
        public string game;
    }

    class Kraken {
        private ChannelInfo _channelInfo;

        private ChannelInfo channelInfo { get { this.updateInfo(); return _channelInfo;} }

        private static Kraken obj;
        private TwitchApi api;
        private string channelId;

        private Kraken() { }

        private void init() {
            string channel = Config.instance().settings.channel;
            if (channel[0] == '#') {
                channel = channel.Substring(1);
            }

           this. api = new TwitchApi(channel);
           this.channelId = api.getChannelId();

           CommandRegistry.instance().registerCommand("game", this.getGame);
           CommandRegistry.instance().registerCommand("title", this.getTitle);
        }

        public static Kraken instance() {
            if (obj == null) {
                obj = new Kraken();
                obj.init();
            }

            return obj;
        }

        private void updateInfo() {
            TimingManager.instance().protect("Kraken@channelInfo", 60,
                    () => this._channelInfo = this.api.getParsed<ChannelInfo>("channels/" + this.channelId));
        }

        private string getGame(Message m, string c) {
            return this.channelInfo.game;
        }

        private string getTitle(Message m, string c) {
            return this.channelInfo.status;
        }

    }
}
