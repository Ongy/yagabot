using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Nocksoft.IO.ConfigFiles;

namespace Database {
	/* IMO This entire thing should be a hashmap from String to Int and
	 * only read/write the ini files on startup/exit of the bot.
	 * So, make this a singleton (like Comamnds) and have it read the file
	 * in the constructor.
	 * Only write the file, on exit
	 *
	 * or, imo better: don't keep count and only write if a new item is added
	 */
    public class Variables {

	    /* This should be "./" if supported or rather ".\\" for windows */
        static string iniPath = "";

        public static void varWrite(string varName, string varValue) {
            INIFile iniFile = new INIFile(iniPath + "variables.ini", true);
            iniFile.SetValue("Variables", varName, varValue);
            return;

        }

        public static string varRead(string varName) {
            INIFile iniFile = new INIFile(iniPath + "variables.ini", true);
            string iniTmp = iniFile.GetValue("Variables", varName);
            return iniTmp;

        }

        public static void foodNew(string foodName, string foodGive, string foodTake, string foodExp) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            if (iniFile.GetValue("Settings", "Counter") == null) {
                iniFile.SetValue("Settings", "Counter", "0");
            }
            string counter = iniFile.GetValue("Settings", "Counter");
            int newcounter = Convert.ToInt32(counter);
            newcounter++; counter = "" + newcounter;
            iniFile.SetValue("Settings", "Counter", counter);

            iniFile.SetValue(counter, "Name", foodName);
            iniFile.SetValue(counter, "Quantity", "0");
            iniFile.SetValue(counter, "Give", foodGive);
            iniFile.SetValue(counter, "Take", foodTake);
            iniFile.SetValue(counter, "Exp", foodExp);

            return;

        }

        public static string foodList() {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);
            string counter = iniFile.GetValue("Settings", "Counter");
            int maxCounter = Convert.ToInt32(counter);
            int currentCounter = 1;

	    /* Small restructure here to clean something up */
	    /* Also C# library probably has a stringbuilder
	     * String concat usually has quadratic behaviour when used like
	     * this, because each time we add an item, the entire current
	     * string has to be copied around
	     */
            //string foodboxCmd = "Food commands: ";
            string foodboxCmd = "";

            while (currentCounter < (maxCounter + 1)) {

                counter = "" + currentCounter;
                string food = iniFile.GetValue(counter, "Name");

                foodboxCmd = (foodboxCmd + ", " + food);

                currentCounter++;
            }

            return "Food commands: " + foodboxCmd;

        }

        public static string foodInc(string foodName) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            string counter = "1";
            int currentCounter;

            while ((iniFile.GetValue(counter, "Name")) != foodName) {
                currentCounter = Convert.ToInt32(counter);
                currentCounter++; counter = ("" + currentCounter);
            }

            string iniTmp = iniFile.GetValue(counter, "Quantity");
            int iniTmp2 = Convert.ToInt32(iniTmp);
            iniTmp2++;
            iniTmp = ""; iniTmp = (iniTmp + iniTmp2);
            iniFile.SetValue(counter, "Quantity", iniTmp);
            iniTmp = iniFile.GetValue(counter, "Give");
            return iniTmp;

        }

        public static string foodQuantity(string foodName) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            string counter = "1";
            int currentCounter;

            while ((iniFile.GetValue(counter, "Name")) != foodName) {
                currentCounter = Convert.ToInt32(counter);
                currentCounter++; counter = ("" + currentCounter);
            }

            string iniTmp = iniFile.GetValue(counter, "Quantity");
            return iniTmp;

        }

        public static string foodQuantityNum(string foodNumber) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            string iniTmp = iniFile.GetValue(foodNumber, "Quantity");
            return iniTmp;

        }

        public static string foodTaken(string foodName) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            string counter = "1";
            int currentCounter;

            while ((iniFile.GetValue(counter, "Name")) != foodName) {
                currentCounter = Convert.ToInt32(counter);
                currentCounter++; counter = ("" + currentCounter);
            }

            string iniTmp = iniFile.GetValue(counter, "Quantity");
            int iniTmp2 = Convert.ToInt32(iniTmp);
            iniTmp2--;
            iniTmp = ""; iniTmp = (iniTmp + iniTmp2);
            iniFile.SetValue(counter, "Quantity", iniTmp);
            iniTmp = iniFile.GetValue(counter, "Take");
            return iniTmp;

        }

        public static string foodRandom(string foodNumber) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            string counter = foodNumber;

            string iniTmp = iniFile.GetValue(counter, "Quantity");
            int iniTmp2 = Convert.ToInt32(iniTmp);
            iniTmp2--;
            iniTmp = ""; iniTmp = (iniTmp + iniTmp2);
            iniFile.SetValue(counter, "Quantity", iniTmp);
            iniTmp = iniFile.GetValue(counter, "Take");
            return iniTmp;

        }

        public static int foodVariety() {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            string counterMax = iniFile.GetValue("settings", "Counter");
            int counter = Convert.ToInt32(counterMax);

            return counter;
        }

        public static string foodCheck(string food) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);
            Dictionary<string, string> foodboxOK = new Dictionary<string, string>();
            string foodOK = "";

            string counterMaxS = iniFile.GetValue("settings", "Counter");
            int counterMax = Convert.ToInt32(counterMaxS);
            counterMax++;
            int counterCurrent = 1;
            string counter = counterCurrent + "";
            string foodName = "";

            while (counterCurrent < counterMax) {

                foodName = iniFile.GetValue(counter, "Name");
                //foodboxOK.Add(foodName, counter);

                counterCurrent++;
                counter = "" + counterCurrent;
                if (foodName == food) {
                    foodOK = "OK";
                }

            }
            
            //if (foodboxOK.ContainsKey(food)) {
            //    foodOK = "OK";
            //}

            return foodOK;
        }

        public static bool foodEmpty() {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);

            string counterMaxS = iniFile.GetValue("settings", "Counter");
            int counterMax = Convert.ToInt32(counterMaxS);
            counterMax++;
            string foodboxQuantity = "0";
            bool foodboxEmpty = true;

            string counter = "1";
            int counterCurrent = 1;

            while (counterCurrent < counterMax) {

                foodboxQuantity = iniFile.GetValue(counter, "Quantity");
                counterCurrent++;
                counter = "" + counterCurrent;

                if (foodboxQuantity != "0")
                    foodboxEmpty = false;
            }

            return foodboxEmpty;

        }

        public static string foodAmount() {

            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);
            string counterMaxS = iniFile.GetValue("settings", "Counter");
            int counterMax = Convert.ToInt32(counterMaxS);
            counterMax++;
            int counterCurrent = 1;
            string foodboxQuantity = "0";
            string counter = "1";
            int foodboxQuantityNum = 0;
            int foodboxTotal = 0;

            while (counterCurrent < counterMax) {

                foodboxQuantity = iniFile.GetValue(counter, "Quantity");
                counterCurrent++;
                counter = "" + counterCurrent;
                foodboxQuantityNum = Convert.ToInt32(foodboxQuantity);
                foodboxTotal = foodboxTotal + foodboxQuantityNum;

            }

            foodboxQuantity = "" + foodboxTotal;

            return foodboxQuantity;

        }

        public static int foodGetExp(string foodNumber) {
            INIFile iniFile = new INIFile(iniPath + "foodbox.ini", true);
            string foodExpTmp = iniFile.GetValue(foodNumber, "Exp");
            int foodExp = Convert.ToInt32(foodExpTmp);
            return foodExp;
        }

        public static string settingsGetData(string category, string element) {
            INIFile iniFile = new INIFile(iniPath + "settings.ini", true);
            string settingsElement = iniFile.GetValue(category, element);
            return settingsElement;
        }

        public static string settingsSaveData(string category, string element, string value) {
            INIFile iniFile = new INIFile(iniPath + "settings.ini", true);
            iniFile.SetValue(category, element, value);
            return "saved";

        }


        public static string hrompGetExp(string owner) {
            INIFile iniFile = new INIFile(iniPath + "rabites.ini", true);
            string rabiteExp = iniFile.GetValue("Experience", owner);
            return rabiteExp;
        }

        public static void hrompSetExp(string owner, string exp) {
            INIFile iniFile = new INIFile(iniPath + "rabites.ini", true);
            iniFile.SetValue("Experience", owner, exp);
        }

        public static bool hrompNew(string owner) {
            INIFile iniFile = new INIFile(iniPath + "rabites.ini", true);
            string hrompExists = iniFile.GetValue("Experience", owner);
            bool hrompNew = false;
            if (hrompExists == null) {
                iniFile.SetValue("Experience", owner, "0");
                hrompNew = true;
            }
            return hrompNew;    
        }

        public static string quoteRandom() {
            INIFile iniFile = new INIFile(iniPath + "quotes.ini", true);

            string iniTmp = iniFile.GetValue("settings", "Counter");

            int iniTmp2 = Convert.ToInt32(iniTmp);
            iniTmp2++;
            Random rnd = new Random();
            int quoteRnd = rnd.Next(1, iniTmp2);
            iniTmp = "" + quoteRnd;
            iniTmp = "Quote #" + iniTmp + ": " + iniFile.GetValue("quotes", iniTmp);
            return iniTmp;

        }

        public static void quoteNew(string newquote) {
            INIFile iniFile = new INIFile(iniPath + "quotes.ini", true);
            string iniTmp1 = iniFile.GetValue("settings", "Counter");
            int iniTmp2 = Convert.ToInt32(iniTmp1);
            iniTmp2++;
            iniTmp1 = "" + iniTmp2;
            iniFile.SetValue("quotes", iniTmp1, newquote);
            iniFile.SetValue("settings", "Counter", iniTmp1);

        }


    }
}
