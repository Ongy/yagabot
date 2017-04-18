using System;
using System.Text;
using System.Collections.Generic;

namespace TwitchBot {
    class CommandRegistry {
        private static CommandRegistry obj = null;
        private Dictionary<string, Func<Message, string, string>> commands;
        private Dictionary<string, Func<Message, string, string>> hiddenCmds;

        private CommandRegistry() { }

        private void init() {
            this.commands = new Dictionary<string, Func<Message, string, string>>();
            this.hiddenCmds = new Dictionary<string, Func<Message, string, string>>();

            this.commands.Add("commands", this.listCommands);
            this.commands.Add("addcommand", this.addCommand);

            YagaBot.instance().chatReceived += this.handleMsg;
        }

        public static CommandRegistry instance() {
            if (obj == null) {
                obj = new CommandRegistry();
                obj.init();
            }

            return obj;
        }

        private void handleMsg(Message msg, string message) {
            if (message[0] == '!') {
                int timeout = Config.instance().settings.timings.commandTimeout;
                Action act = () => this.handleCommand(msg, message.Substring(1));
                TimingManager.instance().protect("command", timeout, act);
            }
        }

        private string listCommands(Message m, string c) {
            StringBuilder builder = new StringBuilder("The possible commands are: ");

            foreach (KeyValuePair<string, Func<Message, string, string>> kv in this.commands) {
                builder.Append('!');
                builder.Append(kv.Key);
                builder.Append(' ');
            }

            foreach (Command cmd in Config.instance().commands) {
                builder.Append('!');
                builder.Append(cmd.cmdStr);
                builder.Append(' ');
            }

            return builder.ToString();
        }

        private string addCommand(Message msg, string command) {
            if (!msg.isMod())
                return "You need moderator privileges to add a command";

            int lvl = 0;
            string[] splits = command.Split(Constants.spaceArray, 3);
            try {
                lvl = Convert.ToInt32(splits[1]);
            } catch {
                return "Failed to parse the permission level.";
            }

            Command cmd = new Command(splits[0].ToLower(), splits[2], lvl);
            Config.instance().addCommand(cmd);
            return String.Format("Added the new command: {0}", cmd.cmdStr);
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
            if (!this.commands.ContainsKey(name))
                this.commands.Add(name, func);
        }

        public void registerHiddenCmd(string name, Func<Message, string, string> func) {
            if (!this.hiddenCmds.ContainsKey(name))
                this.hiddenCmds.Add(name, func);
        }

        public void registerCmdGroup(string[] names, Func<Message, string, string> func) {
            this.registerCommand(names[0], func);

            for (int i = 1; i < names.Length; ++i)
                this.registerHiddenCmd(names[i], func);
        }

        public void unregisterCommand(string name)
        {
            if (this.commands.ContainsKey(name))
                this.commands.Remove(name);

            if (this.hiddenCmds.ContainsKey(name))
                this.hiddenCmds.Remove(name);
        }

    }
}
