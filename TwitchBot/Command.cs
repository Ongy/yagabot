namespace TwitchBot {
    class Command {
        public string cmdStr;
        public string response;
        public int lvl;

        Command()
        { }

        public Command(string cmd, string resp, int lvl) {
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

        public void updateFrom(Command cmd) {
            this.response = cmd.response;
            this.lvl = cmd.lvl;
        }
    }
}
