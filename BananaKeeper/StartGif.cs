using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace BananaKeeper
{
    public partial class StartGif : Form
    {
        private Timer tm;
        public SoundPlayer player;
        
        
        public StartGif()
        {
            InitializeComponent();
            tm = new Timer();
            player = new SoundPlayer("Resources/gif.wav");
            player.Play();
            tm.Interval = 11 * 1000; 
            tm.Tick += new EventHandler(tm_Tick);           
            tm.Start();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
        }
        private void tm_Tick(object sender, EventArgs e)
        {
            tm.Stop();            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tm.Stop();
            this.Close();
        }
    }
}
