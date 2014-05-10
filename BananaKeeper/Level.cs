using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BananaKeeper
{
    public enum ItemType
    {
        Wall,
        Floor,
        Package,
        Goal,
        Minion,
        PackageOnGoal,
        MinionOnGoal,
        Space
    }
    public enum MoveDirection
    {
        Right,
        Up,
        Down,
        Left
    }
    public class Level
    {
        private string name = string.Empty; 
        private ItemType[,] levelMap;       
        private int nrOfGoals = 0;          
        private int levelNr = 0;
        private int width = 0;              
        private int height = 0;            
                
        private string levelSetName = string.Empty;

        private int moves = 0;              // Sokoban number of moves        

        private int MinionPosX;              
        private int MinionPosY;               

        private bool isUndoable = false;    

       private MoveDirection minionDirection = MoveDirection.Right;
        public const int ITEM_SIZE = 30;
        private Item item1, item2, item3;
        private Item item1U, item2U, item3U;
        private int movesBeforeUndo = 0;       

        private Bitmap img;
        private Graphics g;

        public string Name
        {
            get { return name; }
        }

        public int LevelNr
        {
            get { return levelNr; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public string LevelSetName
        {
            get { return levelSetName; }
        }

        public int Moves
        {
            get { return moves; }
        }

        public bool IsUndoable
        {
            get { return isUndoable; }
        }

        public Level(string aName, ItemType[,] aLevelMap, int aWidth,
            int aHeight, int aNrOfGoals, int aLevelNr, string aLevelSetName)
        {
            name = aName;
            width = aWidth;
            height = aHeight;
            levelMap = aLevelMap;
            nrOfGoals = aNrOfGoals;
            levelNr = aLevelNr;
            levelSetName = aLevelSetName;
        }
        public Image DrawLevel()
        {
            int levelWidth = (width + 2) * Level.ITEM_SIZE;
            int levelHeight = (height + 2) * Level.ITEM_SIZE;

            img = new Bitmap(levelWidth, levelHeight);
            g = Graphics.FromImage(img);

            Font statusText = new Font("Tahoma", 10, FontStyle.Bold);
            g.Clear(Color.FromArgb(27, 33, 61));            
            for (int i = 0; i < width + 2; i++)
            {
                g.DrawImage(ImgSpace, ITEM_SIZE * i, 0,
                    ITEM_SIZE, ITEM_SIZE);
                g.DrawImage(ImgSpace, ITEM_SIZE * i,
                    (height + 1) * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }
            for (int i = 1; i < height + 1; i++)
                g.DrawImage(ImgSpace, 0, ITEM_SIZE * i,
                    ITEM_SIZE, ITEM_SIZE);
            for (int i = 1; i < height + 1; i++)
                g.DrawImage(ImgSpace, (width + 1) * ITEM_SIZE,
                    ITEM_SIZE * i, ITEM_SIZE, ITEM_SIZE);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Image image = GetLevelImage(levelMap[i, j], minionDirection);

                    g.DrawImage(image, ITEM_SIZE + i * ITEM_SIZE,
                        ITEM_SIZE + j * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);

                    if (levelMap[i, j] == ItemType.Minion ||
                        levelMap[i, j] == ItemType.MinionOnGoal)
                    {
                        MinionPosX = i;
                        MinionPosY = j;
                    }
                }
            }
            return img;
        }
        public Image DrawChanges()
        {
            Image image1 = GetLevelImage(item1.ItemType, minionDirection);
            g.DrawImage(image1, ITEM_SIZE + item1.XPos * ITEM_SIZE,
                ITEM_SIZE + item1.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);

            Image image2 = GetLevelImage(item2.ItemType, minionDirection);
            g.DrawImage(image2, ITEM_SIZE + item2.XPos * ITEM_SIZE,
                ITEM_SIZE + item2.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);

            if (item3 != null)
            {
                Image image3 = GetLevelImage(item3.ItemType, minionDirection);
                g.DrawImage(image3, ITEM_SIZE + item3.XPos * ITEM_SIZE,
                    ITEM_SIZE + item3.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }
            return img;
        }
        public bool IsFinished()
        {
            int nrOfPackagesOnGoal = 0;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (levelMap[i, j] == ItemType.PackageOnGoal)
                        nrOfPackagesOnGoal++;

            return nrOfPackagesOnGoal == nrOfGoals ? true : false;
        }
        public Image Undo()
        {
            Image image1 = GetLevelImage(item1U.ItemType, minionDirection);
            g.DrawImage(image1, ITEM_SIZE + item1U.XPos * ITEM_SIZE,
                ITEM_SIZE + item1U.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            levelMap[item1U.XPos, item1U.YPos] = item1U.ItemType;

            Image image2 = GetLevelImage(item2U.ItemType, minionDirection);
            g.DrawImage(image2, ITEM_SIZE + item2U.XPos * ITEM_SIZE,
                ITEM_SIZE + item2U.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            levelMap[item2U.XPos, item2U.YPos] = item2U.ItemType;

            Image image3 = GetLevelImage(item3U.ItemType, minionDirection);
            g.DrawImage(image3, ITEM_SIZE + item3U.XPos * ITEM_SIZE,
                ITEM_SIZE + item3U.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            levelMap[item3U.XPos, item3U.YPos] = item3U.ItemType;

            if (!(MinionPosX == item1U.XPos && MinionPosY == item1U.YPos))
            {
                if (levelMap[MinionPosX, MinionPosY] == ItemType.Minion)
                {
                    levelMap[MinionPosX, MinionPosY] = ItemType.Floor;
                    g.DrawImage(GetLevelImage(ItemType.Floor, MoveDirection.Up),
                        ITEM_SIZE + MinionPosX * ITEM_SIZE, ITEM_SIZE +
                        MinionPosY * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
                }
                else if (levelMap[MinionPosX, MinionPosY] == ItemType.MinionOnGoal)
                {
                    levelMap[MinionPosX, MinionPosY] = ItemType.Goal;
                    g.DrawImage(GetLevelImage(ItemType.Goal, MoveDirection.Up),
                        ITEM_SIZE + MinionPosX * ITEM_SIZE, ITEM_SIZE +
                        MinionPosY * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
                }
            }
            MinionPosX = item1U.XPos;
            MinionPosY = item1U.YPos;

            moves = movesBeforeUndo;
            isUndoable = false;

            return img;
        }

        public void MoveMinion(MoveDirection direction)
        {
            minionDirection = direction;

            switch (direction)
            {
                case MoveDirection.Up:
                    MoveUp();
                    break;
                case MoveDirection.Down:
                    MoveDown();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
                case MoveDirection.Left:
                    MoveLeft();
                    break;
            }
        }
        private void MoveUp()
        {
            if ((levelMap[MinionPosX, MinionPosY - 1] == ItemType.Package ||
                levelMap[MinionPosX, MinionPosY - 1] == ItemType.PackageOnGoal) &&
                (levelMap[MinionPosX, MinionPosY - 2] == ItemType.Floor ||
                levelMap[MinionPosX, MinionPosY - 2] == ItemType.Goal))
            {
                item3U = new Item(levelMap[MinionPosX, MinionPosY - 2], MinionPosX, MinionPosY - 2);
                item2U = new Item(levelMap[MinionPosX, MinionPosY - 1], MinionPosX, MinionPosY - 1);
                item1U = new Item(levelMap[MinionPosX, MinionPosY], MinionPosX, MinionPosY);

                if (levelMap[MinionPosX, MinionPosY - 2] == ItemType.Floor)
                {
                    levelMap[MinionPosX, MinionPosY - 2] = ItemType.Package;
                    item3 = new Item(ItemType.Package, MinionPosX, MinionPosY - 2);
                }
                else if (levelMap[MinionPosX, MinionPosY - 2] == ItemType.Goal)
                {
                    levelMap[MinionPosX, MinionPosY - 2] = ItemType.PackageOnGoal;
                    item3 = new Item(ItemType.PackageOnGoal, MinionPosX, MinionPosY - 2);
                }
                if (levelMap[MinionPosX, MinionPosY - 1] == ItemType.Package)
                {
                    levelMap[MinionPosX, MinionPosY - 1] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX, MinionPosY - 1);
                }
                else if (levelMap[MinionPosX, MinionPosY - 1] == ItemType.PackageOnGoal)
                {
                    levelMap[MinionPosX, MinionPosY - 1] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX, MinionPosY - 1);
                }

                isUndoable = true;
                UpdateCurrentMinionPosition();
                movesBeforeUndo = moves;                
                moves++;
                MinionPosY--;
            }
            else if (levelMap[MinionPosX, MinionPosY - 1] == ItemType.Floor ||
                levelMap[MinionPosX, MinionPosY - 1] == ItemType.Goal)
            {
                if (levelMap[MinionPosX, MinionPosY - 1] == ItemType.Floor)
                {
                    levelMap[MinionPosX, MinionPosY - 1] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX, MinionPosY - 1);
                }
                else if (levelMap[MinionPosX, MinionPosY - 1] == ItemType.Goal)
                {
                    levelMap[MinionPosX, MinionPosY - 1] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX, MinionPosY - 1);
                }

                item3 = null;
                UpdateCurrentMinionPosition();
                moves++;
                MinionPosY--;
            }
        }
        private void MoveDown()
        {
            if ((levelMap[MinionPosX, MinionPosY + 1] == ItemType.Package ||
                levelMap[MinionPosX, MinionPosY + 1] == ItemType.PackageOnGoal) &&
                (levelMap[MinionPosX, MinionPosY + 2] == ItemType.Floor ||
                levelMap[MinionPosX, MinionPosY + 2] == ItemType.Goal))
            {
                item3U = new Item(levelMap[MinionPosX, MinionPosY + 2], MinionPosX, MinionPosY + 2);
                item2U = new Item(levelMap[MinionPosX, MinionPosY + 1], MinionPosX, MinionPosY + 1);
                item1U = new Item(levelMap[MinionPosX, MinionPosY], MinionPosX, MinionPosY);

                if (levelMap[MinionPosX, MinionPosY + 2] == ItemType.Floor)
                {
                    levelMap[MinionPosX, MinionPosY + 2] = ItemType.Package;
                    item3 = new Item(ItemType.Package, MinionPosX, MinionPosY + 2);
                }
                else if (levelMap[MinionPosX, MinionPosY + 2] == ItemType.Goal)
                {
                    levelMap[MinionPosX, MinionPosY + 2] = ItemType.PackageOnGoal;
                    item3 = new Item(ItemType.PackageOnGoal, MinionPosX, MinionPosY + 2);
                }

                if (levelMap[MinionPosX, MinionPosY + 1] == ItemType.Package)
                {
                    levelMap[MinionPosX, MinionPosY + 1] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX, MinionPosY + 1);
                }
                else if (levelMap[MinionPosX, MinionPosY + 1] == ItemType.PackageOnGoal)
                {
                    levelMap[MinionPosX, MinionPosY + 1] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX, MinionPosY + 1);
                }

                isUndoable = true;
                UpdateCurrentMinionPosition();
                movesBeforeUndo = moves;
                moves++;
                MinionPosY++;
            }
            else if (levelMap[MinionPosX, MinionPosY + 1] == ItemType.Floor ||
                levelMap[MinionPosX, MinionPosY + 1] == ItemType.Goal)
            {
                if (levelMap[MinionPosX, MinionPosY + 1] == ItemType.Floor)
                {
                    levelMap[MinionPosX, MinionPosY + 1] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX, MinionPosY + 1);
                }
                else if (levelMap[MinionPosX, MinionPosY + 1] == ItemType.Goal)
                {
                    levelMap[MinionPosX, MinionPosY + 1] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX, MinionPosY + 1);
                }

                item3 = null;
                UpdateCurrentMinionPosition();
                moves++;
                MinionPosY++;
            }
        }
        private void MoveRight()
        {
            if ((levelMap[MinionPosX + 1, MinionPosY] == ItemType.Package ||
                levelMap[MinionPosX + 1, MinionPosY] == ItemType.PackageOnGoal) &&
                (levelMap[MinionPosX + 2, MinionPosY] == ItemType.Floor ||
                levelMap[MinionPosX + 2, MinionPosY] == ItemType.Goal))
            {
                item3U = new Item(levelMap[MinionPosX + 2, MinionPosY], MinionPosX + 2, MinionPosY);
                item2U = new Item(levelMap[MinionPosX + 1, MinionPosY], MinionPosX + 1, MinionPosY);
                item1U = new Item(levelMap[MinionPosX, MinionPosY], MinionPosX, MinionPosY);

                if (levelMap[MinionPosX + 2, MinionPosY] == ItemType.Floor)
                {
                    levelMap[MinionPosX + 2, MinionPosY] = ItemType.Package;
                    item3 = new Item(ItemType.Package, MinionPosX + 2, MinionPosY);
                }
                else if (levelMap[MinionPosX + 2, MinionPosY] == ItemType.Goal)
                {
                    levelMap[MinionPosX + 2, MinionPosY] = ItemType.PackageOnGoal;
                    item3 = new Item(ItemType.PackageOnGoal, MinionPosX + 2, MinionPosY);
                }
                if (levelMap[MinionPosX + 1, MinionPosY] == ItemType.Package)
                {
                    levelMap[MinionPosX + 1, MinionPosY] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX + 1, MinionPosY);
                }
                else if (levelMap[MinionPosX + 1, MinionPosY] == ItemType.PackageOnGoal)
                {
                    levelMap[MinionPosX + 1, MinionPosY] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX + 1, MinionPosY);
                }

                isUndoable = true;
                UpdateCurrentMinionPosition();
                movesBeforeUndo = moves;
                moves++;
                MinionPosX++;
            }
            else if (levelMap[MinionPosX + 1, MinionPosY] == ItemType.Floor ||
                levelMap[MinionPosX + 1, MinionPosY] == ItemType.Goal)
            {
                if (levelMap[MinionPosX + 1, MinionPosY] == ItemType.Floor)
                {
                    levelMap[MinionPosX + 1, MinionPosY] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX + 1, MinionPosY);
                }
                else if (levelMap[MinionPosX + 1, MinionPosY] == ItemType.Goal)
                {
                    levelMap[MinionPosX + 1, MinionPosY] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX + 1, MinionPosY);
                }

                item3 = null;
                UpdateCurrentMinionPosition();
                moves++;
                MinionPosX++;
            }
        }
        private void MoveLeft()
        {
            if ((levelMap[MinionPosX - 1, MinionPosY] == ItemType.Package ||
                levelMap[MinionPosX - 1, MinionPosY] == ItemType.PackageOnGoal) &&
                (levelMap[MinionPosX - 2, MinionPosY] == ItemType.Floor ||
                levelMap[MinionPosX - 2, MinionPosY] == ItemType.Goal))
            {
                item3U = new Item(levelMap[MinionPosX - 2, MinionPosY], MinionPosX - 2, MinionPosY);
                item2U = new Item(levelMap[MinionPosX - 1, MinionPosY], MinionPosX - 1, MinionPosY);
                item1U = new Item(levelMap[MinionPosX, MinionPosY], MinionPosX, MinionPosY);

                if (levelMap[MinionPosX - 2, MinionPosY] == ItemType.Floor)
                {
                    levelMap[MinionPosX - 2, MinionPosY] = ItemType.Package;
                    item3 = new Item(ItemType.Package, MinionPosX - 2, MinionPosY);
                }
                else if (levelMap[MinionPosX - 2, MinionPosY] == ItemType.Goal)
                {
                    levelMap[MinionPosX - 2, MinionPosY] = ItemType.PackageOnGoal;
                    item3 = new Item(ItemType.PackageOnGoal, MinionPosX - 2, MinionPosY);
                }
                if (levelMap[MinionPosX - 1, MinionPosY] == ItemType.Package)
                {
                    levelMap[MinionPosX - 1, MinionPosY] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX - 1, MinionPosY);
                }
                else if (levelMap[MinionPosX - 1, MinionPosY] == ItemType.PackageOnGoal)
                {
                    levelMap[MinionPosX - 1, MinionPosY] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX - 1, MinionPosY);
                }

                isUndoable = true;
                UpdateCurrentMinionPosition();
                movesBeforeUndo = moves;
                moves++;
                MinionPosX--;
            }
            else if (levelMap[MinionPosX - 1, MinionPosY] == ItemType.Floor ||
                levelMap[MinionPosX - 1, MinionPosY] == ItemType.Goal)
            {
                if (levelMap[MinionPosX - 1, MinionPosY] == ItemType.Floor)
                {
                    levelMap[MinionPosX - 1, MinionPosY] = ItemType.Minion;
                    item2 = new Item(ItemType.Minion, MinionPosX - 1, MinionPosY);
                }
                else if (levelMap[MinionPosX - 1, MinionPosY] == ItemType.Goal)
                {
                    levelMap[MinionPosX - 1, MinionPosY] = ItemType.MinionOnGoal;
                    item2 = new Item(ItemType.MinionOnGoal, MinionPosX - 1, MinionPosY);
                }

                item3 = null;
                UpdateCurrentMinionPosition();
                moves++;
                MinionPosX--;
            }
        }
        private void UpdateCurrentMinionPosition()
        {
            if (levelMap[MinionPosX, MinionPosY] == ItemType.Minion)
            {
                levelMap[MinionPosX, MinionPosY] = ItemType.Floor;
                item1 = new Item(ItemType.Floor, MinionPosX, MinionPosY);
            }
            else if (levelMap[MinionPosX, MinionPosY] == ItemType.MinionOnGoal)
            {
                levelMap[MinionPosX, MinionPosY] = ItemType.Goal;
                item1 = new Item(ItemType.Goal, MinionPosX, MinionPosY);
            }
        }




        public Image GetLevelImage(ItemType itemType, MoveDirection direction)
        {
            Image image;

            if (itemType == ItemType.Wall)
                image = ImgWall;
            else if (itemType == ItemType.Floor)
                image = ImgFloor;
            else if (itemType == ItemType.Package)
                image = ImgPackage;
            else if (itemType == ItemType.Goal)
                image = ImgGoal;
            else if (itemType == ItemType.Minion)
            {
                if (direction == MoveDirection.Up)
                    image = ImgMinionUp;
                else if (direction == MoveDirection.Down)
                    image = ImgMinionDown;
                else if (direction == MoveDirection.Right)
                    image = ImgMinionRight;
                else
                    image = ImgMinionLeft;
            }
            else if (itemType == ItemType.PackageOnGoal)
                image = ImgPackageGoal;
            else if (itemType == ItemType.MinionOnGoal)
            {
                if (direction == MoveDirection.Up)
                    image = ImgMinionUpGoal;
                else if (direction == MoveDirection.Down)
                    image = ImgMinionDownGoal;
                else if (direction == MoveDirection.Right)
                    image = ImgMinionRightGoal;
                else
                    image = ImgMinionLeftGoal;
            }
            else
                image = ImgSpace;

            return image;
        }

        public Image ImgWall
        {
            get
            {
                return Image.FromFile("Pictures/wall.jpg");
            }
        }

        public Image ImgFloor
        {
            get
            {
                return Image.FromFile("Pictures/floor.jpg");
            }
        }

        public Image ImgPackage
        {
            get
            {
                return Image.FromFile("Pictures/package-normal.jpg");
            }
        }

        public Image ImgPackageGoal
        {
            get
            {
                return
                    Image.FromFile("Pictures/package-goal.jpg");
            }
        }

        public Image ImgGoal
        {
            get
            {
                return Image.FromFile("Pictures/goal.jpg");
            }
        }

        public Image ImgMinionUp
        {
            get
            {
                return Image.FromFile("Pictures/up-normal.jpg");
            }
        }

        public Image ImgMinionDown
        {
            get
            {
                return
                    Image.FromFile("Pictures/down-normal.jpg");
            }
        }

        public Image ImgMinionRight
        {
            get
            {
                return Image.FromFile("Pictures/right-normal.jpg");
            }
        }

        public Image ImgMinionLeft
        {
            get
            {
                return Image.FromFile("Pictures/left-normal.jpg");
            }
        }

        public Image ImgMinionUpGoal
        {
            get
            {
                return Image.FromFile("Pictures/up-goal.jpg");
            }
        }

        public Image ImgMinionDownGoal
        {
            get
            {
                return Image.FromFile("Pictures/down-goal.jpg");
            }
        }

        public Image ImgMinionRightGoal
        {
            get
            {
                return
                    Image.FromFile("Pictures/right-goal.jpg");
            }
        }

        public Image ImgMinionLeftGoal
        {
            get
            {
                return Image.FromFile("Pictures/left-goal.jpg");
            }
        }

        public Image ImgSpace
        {
            get
            {
                return Image.FromFile("Pictures/space.jpg");
            }
        }
    }
}
