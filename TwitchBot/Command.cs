namespace TwitchBot {
    class Command {
        public string cmdStr;
        public string response;
        public int lvl;

        Command()
        {
        }

        Command(string cmd, string resp, int lvl) {
            this.cmdStr = cmd;
            this.response = resp;
            this.lvl = lvl;
        }

        public string getCmd() {
            return cmdStr;
        }

        public string getResponse() {
            return response;
        }

        public int getLvl() {
            return lvl;
        }
    }
}
