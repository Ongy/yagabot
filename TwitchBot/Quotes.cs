using System;
using System.Collections.Generic;

namespace TwitchBot {
    class QuoteManager {
        private Random quoteRand = new Random();

        private string sendQuote(Message m, string c) {
            List<string> quotes = Config.instance().getList("quotes");

            if (quotes != null && quotes.Count > 0) {
                int num = quoteRand.Next(quotes.Count);
                string quote = quotes[num];

                return String.Format("Quote #{0}: {1}", num + 1, quote);
            } else {
                return "Sadly I don't have any quotes (yet).";
            }
        }

        private string addQuote(Message msg, string quote) {
            if (msg.isMod()) {
                Config.instance().addToList("quotes", quote);
                return "Added the quote";
            } else {
                return "You don't have the required permissions to add a qutoe";
            }
        }

        public QuoteManager() {
            CommandRegistry.instance().registerCommand("quote", this.sendQuote);
            CommandRegistry.instance().registerCommand("addquote", this.addQuote);
        }
    }
}
