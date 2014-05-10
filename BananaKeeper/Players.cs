using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Collections;
using System.Windows.Forms;

namespace BananaKeeper
{
    public class Players
    {
        private string name = string.Empty;             
        private string filename = string.Empty;         
        private int lastFinishedLevel = 0;
        private string lastPlayedSet = string.Empty;

        public string Name
        {
            get { return name; }
        }

        public int LastFinishedLevel
        {
            get { return lastFinishedLevel; }
        }

        public string LastPlayedSet
        {
            get { return lastPlayedSet; }
        }

        public Players(string aName)
		{
		    name = aName;
		    
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            filename = path + "/savegames/" + aName + ".xml";
		}

        public void CreatePlayer(LevelSet levelSet)
        {
            XmlDocument doc = new XmlDocument();
                        
            XmlTextWriter writer = new XmlTextWriter(filename, null);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteProcessingInstruction("xml","version='1.0' encoding='ISO-8859-1'");
            writer.WriteStartElement("savegame");
            writer.Close();

            doc.Load(filename);

            XmlNode root = doc.DocumentElement;
            XmlElement playerName = doc.CreateElement("playerName");
            playerName.InnerText = name;
            XmlElement lastPlayedNameSet = doc.CreateElement("lastPlayedNameSet");
            lastPlayedNameSet.InnerText = levelSet.Filename;
            XmlElement lastFinishedLevel = doc.CreateElement("lastFinishedLevel");
            lastFinishedLevel.InnerText = "0";
            XmlElement levelSets = doc.CreateElement("levelSets");

            XmlElement nodeLevelSet = doc.CreateElement("levelSet");
            XmlAttribute xa = doc.CreateAttribute("title");
            xa.Value = levelSet.Title;
            nodeLevelSet.Attributes.Append(xa);
            XmlElement lastFinishedLevelInSet = doc.CreateElement("lastFinishedLevelInSet");
            lastFinishedLevelInSet.InnerText = "0";

            nodeLevelSet.AppendChild(lastFinishedLevelInSet);
            levelSets.AppendChild(nodeLevelSet);
            root.AppendChild(playerName);
            root.AppendChild(lastPlayedNameSet);
            root.AppendChild(lastFinishedLevel);
            root.AppendChild(levelSets);

            doc.Save(filename);
            
        }
        public void LoadPlayerInfo(LevelSet levelSet)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode lastPlayedNameSet =
                doc.SelectSingleNode("//lastPlayedNameSet");
            lastPlayedNameSet.InnerText = levelSet.Filename;

            doc.Save(filename);
            lastFinishedLevel = 0;

        }
        public void LoadLastGameInfo()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            
            XmlNode lastPlayedNameSet =
                doc.SelectSingleNode("//lastPlayedNameSet");
            lastPlayedSet = lastPlayedNameSet.InnerText;
            XmlNode lastFinishedLvl =
                doc.SelectSingleNode("//lastFinishedLevel");
            lastFinishedLevel = int.Parse(lastFinishedLvl.InnerText);
        }
        public void SaveLevelSet(LevelSet levelSet)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode lastFilename = doc.SelectSingleNode("//lastPlayedNameSet");
            lastFilename.InnerText = levelSet.Filename;
            XmlNode lastFinishedLvl = doc.SelectSingleNode("//lastFinishedLevel");
            lastFinishedLvl.InnerText = "0";

            XmlNode setName = doc.SelectSingleNode("/savegame/levelSets/" +
                "levelSet[@title = \"" + levelSet.Title + "\"]");

            if (setName == null) 
            {
                XmlNode levelSets = doc.GetElementsByTagName("levelSets")[0];

                XmlElement newLevelSet = doc.CreateElement("levelSet");
                XmlAttribute xa = doc.CreateAttribute("title");
                xa.Value = levelSet.Title;
                newLevelSet.Attributes.Append(xa);
                XmlElement lastFinishedLevelInSet = doc.CreateElement("lastFinishedLevelInSet");
                lastFinishedLevelInSet.InnerText = "0";

                newLevelSet.AppendChild(lastFinishedLevelInSet);
                levelSets.AppendChild(newLevelSet);
            }

            doc.Save(filename);
        }
        public void SaveLevel(Level level)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode lastFinishedLvl = doc.SelectSingleNode("//lastFinishedLevel");
            lastFinishedLvl.InnerText = level.LevelNr.ToString();

            XmlNode setName = doc.SelectSingleNode("/savegame/levelSets/" +
                "levelSet[@title = \"" + level.LevelSetName + "\"]");
            XmlNode nodeLevel = setName.SelectSingleNode("level[@levelNr = " +
                level.LevelNr + "]");

            if (nodeLevel == null)
            {
                XmlElement nodeNewLevel = doc.CreateElement("level");
                XmlAttribute xa = doc.CreateAttribute("levelNr");
                xa.Value = level.LevelNr.ToString();
                nodeNewLevel.Attributes.Append(xa);
                XmlElement moves = doc.CreateElement("moves");
                moves.InnerText = level.Moves.ToString();

                nodeNewLevel.AppendChild(moves);
                setName.AppendChild(nodeNewLevel);
            }
            else
            {
                XmlElement moves = nodeLevel["moves"];
                int nrOfMoves = int.Parse(moves.InnerText);
            }

            doc.Save(filename);
        }
        public static ArrayList GetPlayers()
        {
            ArrayList playerNames = new ArrayList();
        
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().
                GetName().CodeBase).Substring(6);
            
            string[] fileEntries = Directory.GetFiles(path + "/savegames");
           
            foreach (string filename in fileEntries)
            {
                FileInfo fileInfo = new FileInfo(filename);
                if (fileInfo.Extension.Equals(".xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);

                    XmlNode playerName = doc.SelectSingleNode("//playerName");
                    if (playerName != null)
                        playerNames.Add(playerName.InnerText);
                }
            }

            return playerNames;
        }
    }
}
