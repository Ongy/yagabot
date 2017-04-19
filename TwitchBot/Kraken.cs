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

        private void changeActive(bool active)
        {
            if (active)
            {
                CommandRegistry.instance().registerCommand("game", this.getGame);
                CommandRegistry.instance().registerCommand("title", this.getTitle);
            } else
            {
                CommandRegistry.instance().unregisterCommand("game");
                CommandRegistry.instance().unregisterCommand("title");
            }
        }

        public void connect() {
            string channel = Config.instance().settings.channel;
            if (channel[0] == '#') {
                channel = channel.Substring(1);
            }

           this. api = new TwitchApi(channel);
           this.channelId = api.getChannelId();

            if (Config.instance().settings.modules.kraken)
                this.changeActive(true);

            Config.instance().settings.modules.krakenChanged += this.changeActive;
        }

        public static Kraken instance() {
            if (obj == null) {
                obj = new Kraken();
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