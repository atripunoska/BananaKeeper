using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BananaKeeper
{

    public partial class Board : Form
    {
        private Players playerData;
        private Players playerData1;
        private LevelSet levelSet;
        private Level level;

        private PictureBox screen;
        private Image img;

        


        public Board()
        {

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeComponent();
            screen = new PictureBox();
            Controls.AddRange(new Control[] { screen });

            InitializeGame();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void InitializeGame()
        {
            levelSet = new LevelSet();
            
			StartGif form = new StartGif();
            form.ShowDialog();    
            Menu menu = new Menu();
            menu.ShowDialog();
            NewGame n = new NewGame();
            LoadGame l = new LoadGame();
            if (menu.NewG)
            {                
                n.ShowDialog();
            }
            else if (menu.LoadG == true)
            {                
                l.ShowDialog();
            }
            if (n.BackB)
            {
                InitializeGame1();
            }
            else if (l.BackB1)
            {
                InitializeGame1();
            }
            else
            {
                
                    playerData = new Players(n.PlayerName);
                    playerData1 = new Players(l.PlayerName);

                    if (n.ContinueExistingGame)
                    {
                        playerData1.LoadLastGameInfo();
                        levelSet.SetLevelSet(playerData1.LastPlayedSet);

                        levelSet.CurrentLevel = playerData1.LastFinishedLevel + 1;
                        if (levelSet.CurrentLevel > levelSet.NrOfLevelsInSet)
                            levelSet.CurrentLevel = levelSet.NrOfLevelsInSet;

                        levelSet.LastFinishedLevel = playerData1.LastFinishedLevel;
                        playerData = playerData1;
                    }
                    else
                    {

                        levelSet.SetLevelSet(n.FilenameLevelSet);
                        levelSet.CurrentLevel = 1;

                        if (n.NewPlayer)
                            playerData.CreatePlayer(levelSet);
                        else
                        {
                            playerData.LoadPlayerInfo(levelSet);
                            playerData.SaveLevelSet(levelSet);
                        }
                    }

                    lblPlayerName.Text = playerData.Name;

                    levelSet.SetLevelsInLevelSet(levelSet.Filename);
                    level = (Level)levelSet[levelSet.CurrentLevel - 1];

                    DrawLevel();
                
            }
		}
        private void InitializeGame1()
        {
            levelSet = new LevelSet();

            Menu menu = new Menu();
            menu.ShowDialog();
            NewGame n = new NewGame();
            LoadGame l = new LoadGame();
            if (menu.NewG)
            {
                n.ShowDialog();
            }
            else if (menu.LoadG == true)
            {
                l.ShowDialog();
            }

            if (n.BackB)
            {
                InitializeGame1();
            }
            else if (l.BackB1)
            {
                InitializeGame1();
            }
            else
            {
                
                    playerData = new Players(n.PlayerName);
                    playerData1 = new Players(l.PlayerName);

                    if (n.ContinueExistingGame)
                    {
                        playerData1.LoadLastGameInfo();
                        levelSet.SetLevelSet(playerData1.LastPlayedSet);

                        levelSet.CurrentLevel = playerData1.LastFinishedLevel + 1;
                        if (levelSet.CurrentLevel > levelSet.NrOfLevelsInSet)
                            levelSet.CurrentLevel = levelSet.NrOfLevelsInSet;

                        levelSet.LastFinishedLevel = playerData1.LastFinishedLevel;
                        playerData = playerData1;
                    }
                    else
                    {

                        levelSet.SetLevelSet(n.FilenameLevelSet);
                        levelSet.CurrentLevel = 1;

                        if (n.NewPlayer)
                            playerData.CreatePlayer(levelSet);
                        else
                        {
                            playerData.LoadPlayerInfo(levelSet);
                            playerData.SaveLevelSet(levelSet);
                        }
                    }

                    lblPlayerName.Text = playerData.Name;

                    levelSet.SetLevelsInLevelSet(levelSet.Filename);
                    level = (Level)levelSet[levelSet.CurrentLevel - 1];

                    DrawLevel();
                
            }
        }
        private void DrawLevel()
        {
            int levelWidth = (level.Width + 2) * Level.ITEM_SIZE;
            int levelHeight = (level.Height + 2) * Level.ITEM_SIZE;

            this.ClientSize = new Size(levelWidth+150 , levelHeight);
            screen.Size = new System.Drawing.Size(levelWidth, levelHeight);

            img = level.DrawLevel();
            screen.Image = img;
            

            lblPlayerName.Location = new Point(levelWidth, 25);
            lblLevelNr.Location = new Point(levelWidth, 65);

            grpMoves.Location = new Point(levelWidth + 15, 90); 
            lblMvs.Location = new Point(15, 20);
            lblMoves.Location = new Point(70, 20);
           

            lblMoves.Text = "0";
            lblLevelNr.Text = "Level: " + level.LevelNr;
        }


        private void DrawChanges()
        {
            img = level.DrawChanges();
            screen.Image = img;

           
            lblMoves.Text = level.Moves.ToString();
           
        }



        private void DrawUndo()
        {
            if (level.IsUndoable)
            {
                img = level.Undo();
                screen.Image = img;

               
                lblMoves.Text = level.Moves.ToString();
                
            }
        }


        private void AKeyDown(object sender, KeyEventArgs e)
        {
            string result = e.KeyData.ToString();

            switch (result)
            {
                case "Up":
                    MoveMinion(MoveDirection.Up);
                    break;
                case "Down":
                    MoveMinion(MoveDirection.Down);
                    break;
                case "Right":
                    MoveMinion(MoveDirection.Right);
                    break;
                case "Left":
                    MoveMinion(MoveDirection.Left);
                    break;
                case "U":
                    DrawUndo();
                    break;
            }
        }


        private void MoveMinion(MoveDirection direction)
        {
            if (direction == MoveDirection.Up)
                level.MoveMinion(MoveDirection.Up);
            else if (direction == MoveDirection.Down)
                level.MoveMinion(MoveDirection.Down);
            else if (direction == MoveDirection.Right)
                level.MoveMinion(MoveDirection.Right);
            else if (direction == MoveDirection.Left)
                level.MoveMinion(MoveDirection.Left);

            DrawChanges();

         
            if (level.IsFinished())
            {
                levelSet.LastFinishedLevel = levelSet.CurrentLevel;
                playerData.SaveLevel(level);

                if (levelSet.CurrentLevel < levelSet.NrOfLevelsInSet)
                {
                    MessageBox.Show("Well done!!");
                    levelSet.CurrentLevel++;
                    level = (Level)levelSet[levelSet.CurrentLevel - 1];
                    DrawLevel();
                }
                else
                {
                    MessageBox.Show("That was the last level!");
                    this.Close();
                }
            }
        }

    }
}
