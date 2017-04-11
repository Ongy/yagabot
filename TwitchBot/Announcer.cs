﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    class Announcement
    {
        public string message;
        public string game;
        public int weight;
        public bool active;
    }

    class Announcer
    {
        private Random rand;
        private static Announcer obj;

        private Announcer()
        {
            Settings s = Config.instance().settings;
            this.rand = new Random();
            if (s.modules.announce)
                this.changeState(true);

            s.modules.announceChanged += this.changeState;
            s.timings.announceTimerChanged += this.changeInterval;

            YagaBot.instance().chatConnected += this.connectSpam;
        }

        private void connectSpam() {
            List<Announcement> actives = Config.instance().announces.FindAll((Announcement x) => x.active);
            foreach (Announcement announcement in actives)
                YagaBot.instance().sendMessage(announcement.message);
        }

        private void doAnnounce()
        {
            List<Announcement> actives = Config.instance().announces.FindAll((Announcement x) => x.active);
            int sumOfWeights = 0;
            foreach (Announcement a in actives)
                sumOfWeights += a.weight;

            if (sumOfWeights <= 0)
                return;

            int chosen = this.rand.Next(sumOfWeights) + 1;
            Announcement pick = null;

            for(int i = 0; i < actives.Count; ++i)
            {
                chosen -= actives[i].weight;
                if (chosen <= 0)
                {
                    pick = actives[i];
                    break;
                }
            }

            if (pick != null)
                YagaBot.instance().sendMessage(pick.message);
        }

        private void changeInterval(int interval)
        {
            TimingManager.instance().setInterval("announcer", interval, this.doAnnounce);
        }

        private void changeState(bool active)
        {
            if (active)
            {
                int interval = Config.instance().settings.timings.announceTimer;
                TimingManager.instance().addPeriodic("announcer", interval, this.doAnnounce);
            } else
            {
                TimingManager.instance().removeTimer("announcer");
            }
        }

        public static Announcer instance()
        {
            if (obj == null)
                obj = new Announcer();

            return obj;
        }
    }
}
