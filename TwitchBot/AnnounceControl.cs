using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchBot
{
    public partial class AnnounceControl : UserControl
    {
        public string message
        {
            get { return this.AnnounceText.Text; }
            set { this.AnnounceText.Text = value; }
        }
        public string game
        {
            get { return this.AnnounceGame.Text; }
            set { this.AnnounceGame.Text = value; }
        }
        public bool active
        {
            get { return this.Active.Checked; }
            set { this.Active.Checked = value; }
        }
        public int weight
        {
            get { try { return Convert.ToInt32(this.Weight.Text); } catch { return 0; }  }
            set { this.Weight.Text = value.ToString(); }
        }
        public AnnounceControl()
        {
            InitializeComponent();
            this.message = "message";
            this.game = "game";
            this.weight = 0;
            this.active = false;
        }
    }
}
