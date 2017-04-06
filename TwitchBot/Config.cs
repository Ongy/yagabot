using System.IO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchBot {

    class Config {
        private List<Command> commands = null;
        private static Config obj = null;


        private Config() {
            Console.WriteLine("Reading in config");
            this.loadCommands();

        }

        public static Config instance() {
            if (obj == null) {
                obj = new Config();
            }

            return obj;
        }

        private void loadCommands() {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sw = new StreamReader(@"commands.json"))
            using (JsonReader reader = new JsonTextReader(sw))
            {
                this.commands = serializer.Deserialize<List<Command>>(reader);
            }
        }

        public Command getCommand(string str) {
            foreach (Command cmd in this.commands) {
                if (cmd.getCmd().Equals(str))
                    return cmd;
            }

            return null;
        }
    }
}

