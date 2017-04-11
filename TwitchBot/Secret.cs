using System;

namespace TwitchBot {
    class SecretManager {
        private int secret = 5400;

        private string doSecret(Message m, string c) {
            this.secret += 57;
            if (this.secret >= 10000) {
                /* We are hromparading */
                return String.Format("OH NO! THE HROMPARADE!\n{0}\n{0}\n{0}\n{0}", Constants.hromPARADE);
            }

            return String.Format("{0}.{1}%", secret / 100, secret % 100);
        }

        private string setSecret(Message msg, string content) {
            if (msg.isMod()) {
                try {
                    this.secret = Convert.ToInt32(content) - 57;
                } finally {
                    if (this.secret == 0)
                        this.secret = 5400;
                }
                this.doSecret(msg, content);
            }
            return null;
        }

        public SecretManager() {
            CommandRegistry.instance().registerCommand("secret", doSecret);
            CommandRegistry.instance().registerHiddenCmd("setsecret", doSecret);
        }

    }
}
