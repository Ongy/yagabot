using System;

namespace TwitchBot {
    class SecretManager {
        private static SecretManager obj = null;
        private int secret = 5400;

        public delegate void SecretUpdate(int value);
        public event SecretUpdate secretUpdated = null;

        private SecretManager() { }

        private void init() {
            Config.instance().addModule("secret", this.changeEnabled);
        }

        private void changeEnabled(bool enabled)
        {
            if (enabled)
            {
                CommandRegistry.instance().registerCommand("secret", doSecret);
                CommandRegistry.instance().registerHiddenCmd("setsecret", setSecret);
            } else
            {
                CommandRegistry.instance().unregisterCommand("secret");
                CommandRegistry.instance().unregisterCommand("setsecret");
            }
        }

        public static SecretManager instance()
        {
            if (obj == null) {
                obj = new SecretManager();
                obj.init();
            }

            return obj;
        }

        public void setSecret(int value)
        {
            this.secret = value;
        }

        private string doSecret(Message m, string c) {
            if (this.secret >= 10000) {
                /* We are hromparading */
                return String.Format("OH NO! THE HROMPARADE!\n{0}\n{0}\n{0}\n{0}", Constants.hromPARADE);
            }
            
            this.secret += 57;
            if (this.secret > 10000)
                this.secret = 10000;

            if (this.secretUpdated != null)
                this.secretUpdated(this.secret);

            return String.Format("{0}.{1}%", secret / 100, secret % 100);
        }

        private string setSecret(Message msg, string content) {
            if (msg.isMod()) {
                try {
                    this.secret = Convert.ToInt32(content) - 57;
                } catch {
                    if (this.secret == 0)
                        this.secret = 5400;
                }
                return this.doSecret(msg, content);
            }
            return null;
        }

    }
}
