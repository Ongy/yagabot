using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace TwitchBot {

    class FoodItem {
        public FoodItem(string foodName, string foodGive, string foodTake, List<string> foodTakes) {
            this.foodName = foodName;
            this.foodGive = foodGive;
            this.foodTake = foodTake;
            this.foodTakes = foodTakes;

            if (foodTakes == null) {
                this.foodTakes = new List<string>();
                this.foodTakes.Add(this.foodTake);
                this.foodTake = null;
            }
        }

        public readonly string foodName;
        public readonly string foodGive;
        public readonly string foodTake;
        public readonly List<string> foodTakes;
    }

    class FoodBox {
        int emptyTry = 0;
        private static readonly char[] splitArray = { ';' };
        private string foods = null;
        private List<FoodItem> contents;

        private string getFoods() {
            if (foods == null) {
                StringBuilder builder = new StringBuilder("Foods: ");
                foreach (FoodItem item in Config.instance().foods) {
                    builder.Append(item.foodName);
                    builder.Append(' ');
                }
                this.foods = builder.ToString();
            }

            return this.foods;
        }

        private string printFoods(Message m, string c) {
            return String.Format("type \"!delicious <food>\" into chat! The box currently contains {0} items.\n{1}", this.contents.Count, this.getFoods());
        }

        private static FoodItem findItem(string itemName) {
            Console.WriteLine("Searching for food: " + itemName);
            foreach(FoodItem item in Config.instance().foods) {
                Console.WriteLine("Considering food: " + item.foodName);
                if (item.foodName.Equals(itemName))
                    return item;
            }

            return null;
        }

        private string addItem(Message msg, string itemName) {
            return TimingManager.instance().protect(String.Format("foodbox@{0}", msg.user), 5, () => {
                FoodItem item = findItem(itemName.ToLower());
                if (item == null)
                    return "No such item used. Contact a mod to get it added";

                this.contents.Add(item);
                this.emptyTry = 0;
                return item.foodGive;
            });
        }

        private string addFrog(Message msg, string itemname)
        {
            return this.addItem(msg, "frog");
        }

        private string addFood(Message msg, string definition) {
            if (!msg.isMod())
                return "You do not have the required permissions to add a food";

            string[] parts = definition.Split(splitArray);
            if (parts.Length < 3)
                return "The format of the food item didn't match.\nRequired format: name;give;take";

            List<string> answers = new List<string>();

            for (int i = 2; i < parts.Length; ++i)
                answers.Add(parts[i]);

            Config.instance().addFood(new FoodItem(parts[0], parts[1], null, answers));
            this.foods = null;
            return String.Format("\"{0}\" has been added", parts[0]);
        }

        private void doTake() {
            if (this.contents.Count == 0 && this.emptyTry++ % 4 == 0)
                YagaBot.instance().sendMessage("The rabite opened the foodbox, but it was empty. yagaScare");

            Random rand = new Random();
            FoodItem item = this.contents[rand.Next(this.contents.Count)];

            /* Remove all duplicates, then remove the item we just ate */
            this.contents = this.contents.Distinct().ToList().FindAll((FoodItem x) => !x.foodName.Equals(item.foodName));

            int choice = rand.Next(item.foodTakes.Count);;
            YagaBot.instance().sendMessage(item.foodTakes[choice]);
        }

        private void changeActive(bool active)
        {
            if (active)
            {
                CommandRegistry.instance().registerCmdGroup(new string[] { "foodbox", "food", "box" }, printFoods);

                CommandRegistry.instance().registerCommand("delicious", addItem);
                CommandRegistry.instance().registerCommand("addfood", addFood);
                CommandRegistry.instance().registerHiddenCmd("deliciousfrog", this.addFrog);

                int seconds = Config.instance().settings.timings.foodboxTimer;
                TimingManager.instance().addPeriodic("foodbox", seconds, this.doTake);
            } else
            {
                CommandRegistry.instance().unregisterCommand("deliciousfrog");
                CommandRegistry.instance().unregisterCommand("foodbox");
                CommandRegistry.instance().unregisterCommand("food");
                CommandRegistry.instance().unregisterCommand("box");
                CommandRegistry.instance().unregisterCommand("delicious");
                CommandRegistry.instance().unregisterCommand("addfood");

                TimingManager.instance().removeTimer("foodbox");
            }
        }

        private void timerChanged(int value)
        {
            TimingManager.instance().setInterval("foodbox", value, this.doTake);
        }

        public FoodBox() {
            this.contents = new List<FoodItem>();

            if (Config.instance().settings.modules.foodbox)
                this.changeActive(true);

            Config.instance().settings.modules.foodboxChanged += this.changeActive;
            Config.instance().settings.timings.foodboxTimerChanged += timerChanged;
        }
    }
}
