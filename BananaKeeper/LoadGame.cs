using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BananaKeeper
{
    public partial class LoadGame : Form
    {
        private string playerName = string.Empty;
        private bool continueExistingGame = true;
        private bool backB1 = false;
        private ArrayList levelSets = new ArrayList();
        private string filenameLevelSet = string.Empty;
        private string nameLevelSet = string.Empty;
        public LoadGame()
        {

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;

            InitializeComponent();

            ArrayList players = Players.GetPlayers();

          
            if (players.Count == 0)
                play.Enabled = false;
            else
                listPlayers.DataSource = players;

            levelSets = LevelSet.GetAllLevelSetInfos();
        }

        private void play_Click(object sender, EventArgs e)
        {
            playerName = listPlayers.SelectedItem.ToString();
            continueExistingGame = true;
            
            this.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            backB1 = true;
            this.Close();
            
        }

        public bool BackB1
        {
            get { return backB1; }
        }
        public bool ContinueExistingGame
        {
            get { return continueExistingGame; }
        }

        public string PlayerName
        {
            get { return playerName; }
        }
        private void listPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
