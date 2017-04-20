using System;
using System.Text;
using System.Collections.Generic;

namespace TwitchBot {

    class Rabite {
        public string owner;
        public int exp;
        public int level;

        public Rabite(string owner, int exp, int level) {
            this.owner = owner;
            this.exp = exp;
            this.level = level;
        }

        public static Rabite getDefault() {
            return new Rabite("the channel", 1, 1);
        }
    }


    class HrompManager {
        private Random petRand;
        private Random expRand;
        private Rabite newBorn;

        private Rabite current;
        private string currentOwner;

        private string rabiteLevel(Rabite rabite, bool isOwner, int exp) {
            string owner = rabite.owner + "'s";
            if (isOwner)
                owner = "their";

            string ret = String.Format("{2} played and trained with {0} rabite. It earned {1} exp. yagaHappy", owner, exp, "{user}");
            rabite.exp += exp;
            int newLvl = (int) (3 * Math.Log(rabite.exp));

            if (newLvl > rabite.level) {
                rabite.level = newLvl;
                Config.instance().saveRabites();
                ret += String.Format("\n{1}'s rabite leveled up! yagaHappy It is now level {0} yagaHROMP", newLvl, rabite.owner);
            }

            return ret;
        }

        private string lvlRabite(Message msg, int amount) {
            Tuple<bool, Rabite> tuple = Config.instance().getRabite(msg.user);

            return rabiteLevel(tuple.Item2, tuple.Item1, amount);
        }

        private string doCreate(Message msg) {
            if (!msg.isBotOp())
                return "You do not have the required permissions to create a new yagaHROMP yagaSad";

            this.newBorn = new Rabite(null, 1, 1);
            return "A new yagaHROMP has been born. Hurry and !adopt it yagaScare";
        }

        private string doAdopt(Message msg, string c) {
            if (this.newBorn == null || !msg.isNormal())
                return null;

            Rabite adoptee = this.newBorn;

            adoptee.owner = msg.getName();
            if (Config.instance().addRabite(msg.user, adoptee)) {
                this.newBorn = null;
                return "Congratulation {user}. You just adopted your own rabite yagaHappy Take good care of the little guy.";
            }

            return "Bad move {user}. You already have a rabite. yagaScare";
        }

        private string doHromp(Message msg, string c) {
            if ("create".Equals(c))
                return doCreate(msg);

            int amount = this.expRand.Next(4) + 1;

            Func<string> func = () => this.lvlRabite(msg, amount);
            return TimingManager.instance().protect<string>("hromp@" + msg.user, 5, func);
        }

        private string doPet(Message msg, string c) {
            List<string> answers = Config.instance().getList("pet");

            if (answers != null && answers.Count > 0) {
                string resp = answers[this.petRand.Next(answers.Count)];
                string owner = this.current.owner + "'s";

                if (this.currentOwner.Equals(msg.user))
                    owner = "their";

                return resp.Replace("{owner}", owner);

            }

            return "Sorry can't pet without a valid response";
        }

        private string listRabites(Message msg, string c) {
            if (!msg.isMod())
                return "This command is currently limited to mods.";

            List<Rabite> rabites = Config.instance().getRabites();

            StringBuilder builder = new StringBuilder();
            foreach (Rabite rabite in rabites) {
                builder.Append("Owner: ");
                builder.Append(rabite.owner);
                builder.Append(", Exp: ");
                builder.Append(rabite.exp);
                builder.Append(", Lvl: ");
                builder.Append(rabite.level);
                builder.Append('\n');
            }

            return builder.ToString();
        }

        private void setCurrent(Message msg, string c) {
            Tuple<bool, Rabite> tuple = Config.instance().getRabite(msg.user);
            this.current = tuple.Item2;

            if (tuple.Item1)
                this.currentOwner = msg.user;
            else
                this.currentOwner = "";
        }

        private string whisperRabites(Message msg, string c) {
            List<Rabite> rabites = Config.instance().getRabites();
            
            StringBuilder builder = new StringBuilder();
            foreach (Rabite rabite in rabites) {
                builder.Append("Owner: ");
                builder.Append(rabite.owner);
                if (msg.isMod()) {
                    builder.Append(", Exp: ");
                    builder.Append(rabite.exp);
                }
                builder.Append(", Lvl: ");
                builder.Append(rabite.level);
                builder.Append('\n');
            }

            return builder.ToString();
        }

        private void changeActive(bool active) {
            if (active) {
                CommandRegistry.instance().registerCommand("hromp", this.doHromp);
                CommandRegistry.instance().registerCommand("pet", this.doPet);
                CommandRegistry.instance().registerHiddenCmd("adopt", this.doAdopt);
                CommandRegistry.instance().registerHiddenCmd("rabiteden", this.listRabites);
                WhisperCommands.instance().addCommand("rabites", this.whisperRabites);
            } else {
                CommandRegistry.instance().unregisterCommand("hromp");
                CommandRegistry.instance().unregisterCommand("pet");
                CommandRegistry.instance().unregisterCommand("adopt");
                CommandRegistry.instance().unregisterCommand("rabiteden");
                WhisperCommands.instance().delCommand("rabites");
            }
        }

        public HrompManager() {
            this.current = Config.instance().getRabite("channel").Item2;
            this.currentOwner = "";
            this.expRand = new Random();
            this.petRand = new Random();
            this.newBorn = null;

            Config.instance().addModule("hromp", this.changeActive);

            YagaBot.instance().chatReceived += this.setCurrent;
        }
    }
}
