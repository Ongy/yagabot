using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchBot {

    class Timings
    {
        private int _foodboxTimer;
        private int _announceTimer;
        public int foodboxTimer
        {
            get { return this._foodboxTimer; }
            set
            {
                this._foodboxTimer = value;
                if (this.foodboxTimerChanged != null)
                    this.foodboxTimerChanged(value);
            }
        }
        public int announceTimer
        {
            get { return this._announceTimer; }
            set
            {
                this._announceTimer = value;
                if (this.announceTimerChanged != null)
                    this.announceTimerChanged(value);
            }
        }
        public int foodboxTimeout;
        public int hrompTimeout;
        public int commandTimeout;

        public delegate void TimerChanged(int value);

        public event TimerChanged foodboxTimerChanged;
        public event TimerChanged announceTimerChanged;

        public Timings()
        {
            this.foodboxTimer = 300;
            this.announceTimer = 140;

            this.foodboxTimeout = 15;
            this.hrompTimeout = 10;
            this.commandTimeout = 3;
        }
    }

    class Modules
    {
        private bool _announce;
        private bool _secret;
        private bool _foodbox;
        public bool announce
        {
            get { return this._announce; }
            set { this._announce = value;
                if (this.announceChanged !=null)
                    this.announceChanged(value); }
        }
        public bool secret
        {
            get { return this._secret; }
            set { this._secret = value;
                if (this.secretChanged != null)
                    this.secretChanged(value); }
        }
        public bool foodbox
        {
            get { return this._foodbox; }
            set { this._foodbox = value;
                if (this.foodboxChanged != null)
                    this.foodboxChanged(value); }
        }

        public delegate void ModuleStateChanged(bool newState);
        public event ModuleStateChanged announceChanged;
        public event ModuleStateChanged secretChanged;
        public event ModuleStateChanged foodboxChanged;

        public Modules()
        {
            this.announce = this.secret = this.foodbox = true;
        }

        public Modules(bool announce, bool secret, bool foodbox)
        {
            this.announce = announce;
            this.secret = secret;
            this.foodbox = foodbox;
        }
    }

    class Settings {
        public string channel;
        public string oauth;
        public string username;
        public bool autoconnect;
        public Timings timings;
        public Modules modules;

        public Settings()
        {
            this.channel = this.oauth = this.username = "";
            this.autoconnect = false;
            this.timings = new Timings();
            this.modules = new Modules();
        }

        public Settings(string channel, string oauth, string username, bool autoconnect, Timings timings, Modules modules) {
            this.channel = channel;
            this.oauth = oauth;
            this.username = username;
            this.autoconnect = autoconnect;
            this.timings = timings;
            this.modules = modules;
        }
    }

    class Config {
        private readonly List<string> defaultMods = new List<string>();
        private Dictionary<string, List<string>> lists = null;
        private Dictionary<string, Rabite> rabites;
        public List<FoodItem> foods = null;
        public List<Command> commands = null;
        public List<Announcement> announces = null;
        public Settings settings;
        private static Config obj = null;

        public delegate void TimingChange(Timings timings);
        public event TimingChange newTimings;

        private Config() {
            Console.WriteLine("Reading in config");
            this.loadCommands();
            this.loadLists();
            this.loadFoods();
            this.loadRabites();
            this.loadSettings();
            this.loadAnnounces();

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
            try
            {
                this.settings = loadFromFile<Settings>(@"settings.json");
            } catch
            {
                this.settings = new Settings();
                this.saveSettings();
            }
        }

        public void saveSettings() {
            writeToFile<Settings>(@"settings.json", this.settings);
        }

        private void loadAnnounces()
        {
            this.announces = loadData<List<Announcement>>(@"announcements.json");

            if (this.announces == null)
                this.announces = new List<Announcement>();
        }

        private void saveAnnounces()
        {
            writeData<List<Announcement>>(@"announcements.json", this.announces);
        }

        public void setAnnounces(List<Announcement> list)
        {
            this.announces = list;
            this.saveAnnounces();
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

        public void setTimings(Timings newTimings)
        {
            this.settings.timings = newTimings;
            this.saveSettings();
            this.newTimings(newTimings);
        }
    }
}

