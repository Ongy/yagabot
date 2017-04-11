using System;
using System.Text;
using System.Collections.Generic;

namespace TwitchBot {
    class CommandRegistry {
        private static CommandRegistry obj = null;
        private Dictionary<string, Func<Message, string, string>> commands;
        private Dictionary<string, Func<Message, string, string>> hiddenCmds;

        private CommandRegistry() {
            this.commands = new Dictionary<string, Func<Message, string, string>>();
            this.hiddenCmds = new Dictionary<string, Func<Message, string, string>>();

            this.commands.Add("commands", listCommands);

            YagaBot.instance().chatReceived += this.handleMsg;
        }

        public static CommandRegistry instance() {
            if (obj == null)
                obj = new CommandRegistry();

            return obj;
        }

        private void handleMsg(Message msg, string message) {
            if (message[0] == '!') {
                Action act = () => this.handleCommand(msg, message.Substring(1).ToLower());
                TimingManager.instance().protect("command", 3, act);
            }
        }

        private string listCommands(Message m, string c) {
            StringBuilder builder = new StringBuilder("The possible commands are: ");

            foreach (KeyValuePair<string, Func<Message, string, string>> kv in this.commands) {
                builder.Append(kv.Key);
                builder.Append(' ');
            }

            foreach (Command cmd in Config.instance().commands) {
                builder.Append(cmd.cmdStr);
                builder.Append(' ');
            }

            return builder.ToString();
        }


        private void handleCommand(Message msg, string message) {
            string[] splits = message.Split(Constants.spaceArray, 2);
            string command = splits[0].ToLower();
            string param = null;
            if (splits.Length > 1)
                param = splits[1];

            string ret = null;
            Func<Message, string, string> func = null;
            this.commands.TryGetValue(command, out func);
            if (func == null)
                this.hiddenCmds.TryGetValue(command, out func);

            if (func != null)
                ret = func(msg, param);

            if (ret != null) {
                YagaBot.instance().sendMessage(msg, ret);
                return;
            }

            Command cmd = Config.instance().getCommand(command);
            if (cmd != null) {
                if (msg.getLvl() >= cmd.getLvl()) {
                    YagaBot.instance().sendMessage(msg, cmd.getResponse());
                }
            }
        }

        public void registerCommand(string name, Func<Message, string, string> func) {
            this.commands.Add(name, func);
        }

        public void registerHiddenCmd(string name, Func<Message, string, string> func) {
            this.hiddenCmds.Add(name, func);
        }

        public void registerCmdGroup(string[] names, Func<Message, string, string> func) {
            this.commands.Add(names[0], func);

            for (int i = 1; i < names.Length; ++i)
                this.hiddenCmds.Add(names[i], func);
        }

    }
}
