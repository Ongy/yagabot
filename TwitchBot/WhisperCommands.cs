using System;
using System.Collections.Generic;

namespace TwitchBot {
    class WhisperCommands {
        private static WhisperCommands obj;
        private Dictionary<string, Func<Message, string, string>> funcs;

        void handleIncomming(Message msg, string content) {
            if (content[0] != '!')
                return;

            string[] parts = content.Split(Constants.spaceArray, 2);
            string cmd = parts[0].Substring(1);
            if (!this.funcs.ContainsKey(cmd))
                return;

            string arg = null;
            if (parts.Length > 1)
                arg = parts[1];

            var func = this.funcs[cmd];
            string ret = func(msg, arg);


            if (ret != null) {
                YagaBot.instance().answerWhisper(msg, ret);
            }
        }

        private void init() {
            YagaBot.instance().recvWhisper += this.handleIncomming;
        }

        private WhisperCommands() {
            this.funcs = new Dictionary<string, Func<Message, string, string>>();
        }

        public void delCommand(string name) {
            if (this.funcs.ContainsKey(name))
                this.funcs.Remove(name);
        }

        public void addCommand(string name, Func<Message, string, string> func) {
            if (!this.funcs.ContainsKey(name))
                this.funcs.Add(name, func);
            else
                this.funcs[name] = func;
        }

        public static WhisperCommands instance() {
            if (obj == null) {
                obj = new WhisperCommands();
                obj.init();
            }

            return obj;
        }
    }
}
