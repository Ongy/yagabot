using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchBot {

    class Settings {
        public string channel;
        public string oauth;
        public string username;

        public Settings(string channel, string oauth, string username) {
            this.channel = channel;
            this.oauth = oauth;
            this.username = username;
        }
    }

    class Config {
        private readonly List<string> defaultMods = new List<string>();
        private Dictionary<string, List<string>> lists = null;
        private Dictionary<string, Rabite> rabites;
        public List<FoodItem> foods = null;
        public List<Command> commands = null;
        public Settings settings;
        private static Config obj = null;


        private Config() {
            Console.WriteLine("Reading in config");
            this.loadCommands();
            this.loadLists();
            this.loadFoods();
            this.loadRabites();
            this.loadSettings();

            /**
             * NOTE: This is a default list of people allowed to bot-maintance
             * This can be overwritten by the "mods" list.
             */
            this.defaultMods.Add("ongyerth");
        }

        public static Config instance() {
            if (obj == null) {
                obj = new Config();
            }

            return obj;
        }

        private static T loadFromFile<T>(string file) {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sw = new StreamReader(file))
            using (JsonReader reader = new JsonTextReader(sw))
            {
                return serializer.Deserialize<T>(reader);
            }
        }

        private static void writeToFile<T>(string file, T obj) {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(file))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        private static T loadData<T>(string file) {
            try {
                return loadFromFile<T>(Path.Combine(".", "data", file));
            } catch (Exception e) {
                Console.WriteLine("Couldn't load data: {0}.\n{1}", file, e);
                return default(T);
            }
        }

        private static void writeData<T>(string file, T obj) {
            string dataPath = Path.Combine(".", "data");
            Directory.CreateDirectory(dataPath);

            writeToFile<T>(Path.Combine(dataPath, file), obj);
        }

        private void loadSettings() {
            this.settings = loadFromFile<Settings>(@"settings.json");
        }

        public void saveSettings() {
            writeToFile<Settings>(@"settings.json", this.settings);
        }

        private void loadRabites() {
            this.rabites = loadData<Dictionary<string, Rabite>>(@"rabites.json");

            if (this.rabites == null) {
                this.rabites = new Dictionary<string, Rabite>();
                this.addRabite("channel", Rabite.getDefault());
            }
        }

        public void saveRabites() {
            writeData<Dictionary<string, Rabite>>(@"rabites.json", this.rabites);
        }

        public bool addRabite(string user, Rabite rabite) {
            if (!this.rabites.ContainsKey(user)) {
                this.rabites.Add(user, rabite);
                this.saveRabites();
                return true;
            }

            return false;
        }

        public Rabite getRabite(string user) {
            if (this.rabites.ContainsKey(user))
                return this.rabites[user];

            return this.rabites["channel"];
        }

        private void loadCommands() {
            this.commands = loadData<List<Command>>(@"commands.json");

            if (this.commands == null)
                this.commands = new List<Command>();
        }

        private void loadLists() {
            this.lists = loadData<Dictionary<string, List<string>>>(@"lists.json");

            if (this.lists == null)
                this.lists = new Dictionary<string, List<string>>();
        }

        public List<string> getList(string name) {
            List<string> ret = null;
            this.lists.TryGetValue(name, out ret);
            return ret;
        }

        public void addToList(string name, string val) {
            if (!this.lists.ContainsKey(name))
                this.lists.Add(name, new List<string>());
            this.lists[name].Add(val);

            writeData<Dictionary<string, List<string>>>(@"lists.json", this.lists);
        }

        private void loadFoods() {
            this.foods = loadData<List<FoodItem>>(@"foods.json");

            if (this.foods == null)
                this.foods = new List<FoodItem>();
        }

        public void addFood(FoodItem item) {
            this.foods.Add(item);
            writeData<List<FoodItem>>(@"foods.json", this.foods);
        }

        public List<string> getBotMods() {
            List<string> ret = this.getList("mods");

            if (ret == null)
                return defaultMods;

            return ret;
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

