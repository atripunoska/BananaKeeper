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
    public partial class NewGame : Form
    {
        private string playerName = string.Empty;
        private bool newPlayer = false;
        private bool continueExistingGame = true;
        private bool backB = false;
        private ArrayList levelSets = new ArrayList();
        private string filenameLevelSet = string.Empty;
        private string nameLevelSet = "BananaKeeper";
        
        public NewGame()
        {

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;

            InitializeComponent();
            levelSets = LevelSet.GetAllLevelSetInfos();
            
            
        }
       
        
        private void play_Click(object sender, EventArgs e)
        {

                playerName = name.Text;
                continueExistingGame = false;
                newPlayer = true;
                

                foreach (LevelSet levelSet in levelSets)
                {
                    if (levelSet.Title == nameLevelSet)
                    {

                        filenameLevelSet = levelSet.Filename;
                        break;
                    }
                }

                this.Close();
            
        }
        public string FilenameLevelSet
        {
            get { return filenameLevelSet; }
        }
        public bool BackB
        {
            get { return backB; }
        }
        public string PlayerName
        {
            get { return playerName; }
        }
        public bool NewPlayer
        {
            get { return newPlayer; }
        }
        public bool ContinueExistingGame
        {
            get { return continueExistingGame; }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            backB = true;
            this.Close();
           
        }

        private void name_Validating(object sender, CancelEventArgs e)
        {
            if (name.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(name, "Please enter your name.");
            }
            else
                errorProvider1.SetError(name, null);
        }

        


        

    }
}
