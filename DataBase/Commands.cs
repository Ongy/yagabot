using System.IO;
using System;

namespace DataBase {
    public class Commands {

        
        private DataSet1 ds = new DataSet1();
        private static readonly string xmlfile = "commands.xml";
        private static Commands instance;

        private Commands() {
		/* XML yagaSad */
            if (File.Exists(xmlfile))
                ds.ReadXml(xmlfile);
        }

        public static Commands Instance() {
            if (instance == null)
                instance = new Commands();

            return instance;
        }

        public bool AddCommand(string cmdName, string cmdString) {
            if (CheckCommand(cmdName))
                return false;
            else {
                ds.Commands.Rows.Add(cmdName, cmdString);
                ds.WriteXml(xmlfile);
                return true;
            }
        }
        public bool RemoveCommand(string cmdName) {

            for (int i = 0; i < ds.Commands.Rows.Count; i++) {
                if (cmdName.Equals(ds.Commands.Rows[i]["cmdName"].ToString())) {
                    ds.Commands.Rows[i].Delete();
                    ds.WriteXml(xmlfile);
                    return true;
                }
            }
            return false;
        }

        public string GetCommand(string cmdName) {

            //string tmp = "";

            for (int i = 0; i < ds.Commands.Rows.Count; i++) {

                if (cmdName.Equals(ds.Commands.Rows[i]["cmdName"].ToString())) {
		/* we can early exit here, also the toStringed thingy is equal
		 * to cmdName, so unless we want it to make a copy, we can just
		 * return cmdName here
		 */
                    return cmdName;
                    //tmp = ds.Commands.Rows[i]["cmdString"].ToString();
		    /* for single return statement (coding style!) I would go
		     * with break for early exit out of the loop:
		     *
		     * tmp = cmdName;
		     * break;
		     */
                }
            }

            return "";
            //return tmp;
        }

        public bool CheckCommand(string cmdName) {
		/* This is equal to GetCommand in the body, so we can use the
		 * already existing code
		 * If GetCommand does not duplicate, == should also be ok to
		 * compare the results
		 */
            return !"".Equals(GetCommand(cmdName));
//            bool check = false;
//
//            for (int i = 0; i < ds.Commands.Rows.Count; i++) {
//                if (cmdName.Equals(ds.Commands.Rows[i]["cmdName"].ToString()))
//                    check = true;
//            }
//                return check;
            

        }

        

        public static string petRandom() {
		/* Put this into an array, so we can simply add another line at
		 * the end without modifying other code.
		 * Also It may be a good idea to move this into a store lilke
		 * the commands, so it can be modified without touching source
		 */
            string [] sentences =
                { "The rabite is happy! yagaHappy"
                , "It cuddles up to your hand and purrs"
                , "It hromps your hand! yagaHROMP ouch!"
                , "It looks at you in confusion, thinking that you would feed it yagaDerp "
                };
            Random rnd = new Random();

            return sentences[rnd.Next(0, sentences.Length)];
            //int rndNo = rnd.Next(1, 5);
            //string rndTake = "" + rndNo;
            //switch (rndTake) {
            //    case "1":
            //        petRabite = "The rabite is happy! yagaHappy";
            //        break;
            //    case "2":
            //        petRabite = "It cuddles up to your hand and purrs";
            //        break;
            //    case "3":
            //        petRabite = "It hromps your hand! yagaHROMP ouch!";
            //        break;
            //    case "4":
            //        petRabite = "It looks at you in confusion, thinking that you would feed it yagaDerp ";
            //        break;
            //    
            //}
            //return petRabite;
        }

        public static string hromp(int hrompExpPlus) {
            string hrompLevelUp;
            int hrompLevelBefore = hrompGetLevel("system");
            string hrompExp = Database.Variables.hrompGetExp("System");
            int tmpExp = Convert.ToInt32(hrompExp);
            tmpExp = tmpExp + hrompExpPlus;
            hrompExp = "" + tmpExp;
            Database.Variables.hrompSetExp("system", hrompExp);
            int hrompLevelAfter = hrompGetLevel("system");
            if (hrompLevelBefore == hrompLevelAfter) {
                hrompLevelUp = "Nein";
            }
            else {
                hrompLevelUp =  "" + hrompLevelAfter;
            }
            return hrompLevelUp;
                
        }

        private static int hrompGetLevel(string owner) {
            string hrompExp = Database.Variables.hrompGetExp(owner);
            int tmpExp = Convert.ToInt32(hrompExp);
            int hrompLevel = 0;

	    /* All of this should be moved into some mathematic expression. I
	     * can't think of one just from looking at it though
	     * Maybe plotting it (max/min values) could give a better intution.
	     * If moving around the borders isn't to much of a problem
	     * I'd say sqrt or some log, probably with constant factor.
	     */
            if (0 <= tmpExp && tmpExp <= 9) {
                hrompLevel = 1;
            }

            if (10 <= tmpExp && tmpExp <= 24) {
                hrompLevel = 2;
            }

            if (25 <= tmpExp && tmpExp <= 49) {
                hrompLevel = 3;
            }

            if (50 <= tmpExp && tmpExp <= 99) {
                hrompLevel = 4;
            }

            if (100 <= tmpExp && tmpExp <= 199) {
                hrompLevel = 5;
            }

            if (200 <= tmpExp && tmpExp <= 349) {
                hrompLevel = 6;
            }

            if (350 <= tmpExp && tmpExp <= 499) {
                hrompLevel = 7;
            }

            if (500 <= tmpExp && tmpExp <= 699) {
                hrompLevel = 8;
            }

            if (700 <= tmpExp && tmpExp <= 999) {
                hrompLevel = 9;
            }

            if (1000 <= tmpExp && tmpExp <= 1499) {
                hrompLevel = 10;
            }

            if (1500 <= tmpExp && tmpExp <= 2099) {
                hrompLevel = 11;
            }

            if (2100 <= tmpExp && tmpExp <= 2799) {
                hrompLevel = 12;
            }

            if (2800 <= tmpExp && tmpExp <= 3599) {
                hrompLevel = 13;
            }

            if (3600 <= tmpExp && tmpExp <= 4499) {
                hrompLevel = 14;
            }

            if (4500 <= tmpExp && tmpExp <= 5499) {
                hrompLevel = 15;
            }

            if (5500 <= tmpExp && tmpExp <= 6599) {
                hrompLevel = 16;
            }

            if (6600 <= tmpExp && tmpExp <= 7699) {
                hrompLevel = 17;
            }

            if (7700 <= tmpExp && tmpExp <= 8799) {
                hrompLevel = 18;
            }

            if (8800 <= tmpExp && tmpExp <= 9999) {
                hrompLevel = 19;
            }

            if (10000 <= tmpExp && tmpExp <= 20000) {
                hrompLevel = 20;
            }

            return hrompLevel;
        }

        



    }
}
