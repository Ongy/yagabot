using System;
using System.Timers;
using System.Collections.Generic;

namespace TwitchBot {
    class TimingManager {
        Dictionary<string, DateTime> times;
        private static TimingManager obj = null;

        private TimingManager() {
            this.times = new Dictionary<string, DateTime>();
            this.timers = new Dictionary<string, Timer>();
        }

        public static TimingManager instance() {
            if (obj == null)
                obj = new TimingManager();

            return obj;
        }

        public void protect(string name, int secs, Action act) {
            if (this.times.ContainsKey(name)) {
                if (DateTime.Now  - this.times[name] > TimeSpan.FromSeconds(secs)) {
                    this.times[name] = DateTime.Now;
                    act();
                }
            } else {
                this.times.Add(name, DateTime.Now);
                act();
            }
        }

        public T protect<T>(string name, int secs, Func<T> func) {
            if (this.times.ContainsKey(name)) {
                if (DateTime.Now  - this.times[name] > TimeSpan.FromSeconds(secs)) {
                    this.times[name] = DateTime.Now;
                    return func();
                }
                return default(T);
            } else {
                this.times.Add(name, DateTime.Now);
                return func();
            }

        }

        private Dictionary<string, Timer> timers;

        public bool addPeriodic(string name, int secs, Action act) {
            if (this.timers.ContainsKey(name))
                return false;

            Timer timer = new Timer(secs * 1000);

            Console.WriteLine("Added timer: {0} every {1} secs", name, secs);
            timer.Elapsed += (Object source, ElapsedEventArgs e) => act();
            timer.AutoReset = timer.Enabled = true;

            this.timers[name] = timer;
            return true;
        }

        public void removeTimer(string name)
        {
            if (!this.timers.ContainsKey(name))
                return;

            Timer timer = this.timers[name];
            this.timers.Remove(name);
            timer.Stop();
        }

        public void setInterval(string name, int secs, Action act) {
            if (this.timers.ContainsKey(name)) {
                this.timers[name].Stop();
                this.timers.Remove(name);
                this.addPeriodic(name, secs, act);
            }
        }

    }
}
