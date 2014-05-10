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
    public partial class Menu : Form
    {
        private bool newG=false;
        private bool loadG=false;
        private bool exitB = false;
        public SoundPlayer player;
        private bool music=true;
        public bool NewG
        {
            get { return newG; }
            
        }
        public SoundPlayer Player1
        {
            get { return player; }
        }
        public bool ExitB
        {
            get { return exitB; }

        }
        public bool LoadG
        {
            get { return loadG; }

        }

        public Menu()
        {

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            player = new SoundPlayer("Resources/menu.wav");
            InitializeComponent();
            player.PlayLooping();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            exitB = true;
            System.Environment.Exit(0);
            
                
        }

        private void newgame_Click(object sender, EventArgs e)
        {

            newG = true;
            this.Close();


        }

        private void load_Click(object sender, EventArgs e)
        {

            loadG = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (music)
            {
                music = false;
                player.Stop();
            }
            else if (!music)
            {
                music = true;
                player.PlayLooping();
            }

        }
    }
}
