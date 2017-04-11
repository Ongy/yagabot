using System;
using System.Text;
using System.Collections.Generic;

namespace TwitchBot {

    class FoodItem {
        public FoodItem(string foodName, string foodGive, string foodTake) {
            this.foodName = foodName;
            this.foodGive = foodGive;
            this.foodTake = foodTake;
        }

        public readonly string foodName;
        public readonly string foodGive;
        public readonly string foodTake;
    }

    class FoodBox {
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
            Console.WriteLine("Someone wants to put food into the box");
            return TimingManager.instance().protect(String.Format("foodbox@{0}", msg.user), 5, () => {
                FoodItem item = findItem(itemName.ToLower());
                if (item == null)
                    return "No such item used. Contact a mod to get it added";

                this.contents.Add(item);
                return item.foodGive;
            });
        }

        private string addFood(Message msg, string definition) {
            if (!msg.isMod())
                return "You do not have the required permissions to add a food";

            string[] parts = definition.Split(splitArray);
            if (parts.Length != 3)
                return "The format of the food item didn't match.\nRequired format: name;give;take";

            Config.instance().addFood(new FoodItem(parts[0], parts[1], parts[2]));
            this.foods = null;
            return String.Format("\"{0}\" has been added", parts[0]);
        }

        private void doTake() {
            if (this.contents.Count == 0)
                YagaBot.instance().sendMessage("The rabite opened the foodbox, but it was empty. yagaScare");

            Random rand = new Random();
            FoodItem item = this.contents[rand.Next(this.contents.Count)];
            this.contents = new List<FoodItem>();
            YagaBot.instance().sendMessage(item.foodTake);
        }


        public FoodBox() {
            this.contents = new List<FoodItem>();

            CommandRegistry.instance().registerCmdGroup(new string[] { "foodbox", "food", "box" }, printFoods);

            CommandRegistry.instance().registerCommand("delicious", addItem);
            CommandRegistry.instance().registerCommand("addfood", addFood);

            TimingManager.instance().addPeriodic("foodbox", 300, this.doTake);
        }
    }

}
