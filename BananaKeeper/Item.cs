using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BananaKeeper
{
    public class Item
    {
        private ItemType itemType;  
        private int xPos;           
        private int yPos;

        public Item(ItemType aItemType, int aXPos, int aYPos)
        {
            itemType = aItemType;
            xPos = aXPos;
            yPos = aYPos;
        }

        public ItemType ItemType
        {
            get { return itemType; }
        }

        public int XPos
        {
            get { return xPos; }
        }

        public int YPos
        {
            get { return yPos; }
        }
    }
}
