BananaKeeper
============

����� �� ������ � Banana Keeper. 
����� � �������� �� �� ������ ���� ������ �� ����������� ������. ������ ���� �� ������ �� 40 �����, ����� �� �������� ������.
������ �������� ��� �� ��������, ���������� ������� �����
* �������� �� ��� ����� ����� ������� New Game

* ������� ���� ������ ����� ����� ������� Load Game �� �� ������ ������ �� ������� �� ���� �� �������� ���� ������� �������� � �� �� �������� ����������� ����

* ����� ������� Exit �� �� ������ ������������

������������ ������ � ����������, ������� ��� ������� �� �� ������� ������� �� �� �� ������.

![alt tag](http://s28.postimg.org/c3chtki59/menu.jpghttp://url/to/img.png)

![alt tag](http://s11.postimg.org/gk1ie0vgj/new.jpg)

![alt tag](http://s17.postimg.org/7br853ozz/load.jpg)



�� �������� �� ������������ �� ��� �������� 4 �����: Item, Level, LevelSet � Players.
* �� ������� Item �� ������ x � y ��������� �� �������� �������� �� ������������ �� �������� �� ������, ��� �� �� ��� ItemType � ���� �� ����� Wall, Floor, Package, Goal, Minion, PackageOnGoal, MinionOnGoal, Space. 
* �� ������� Level �� ���������� ����������� ����, ����� ������� �� ������������ ��  �������� �� �������� ������ � �� �������� ����� �� �������� �������� �� �� �� ������ �������� �� ����� ���� �� ������.
* �� ������� Level �� ���������� ����������� ����, ����� ������� �� ������������ ��  �������� �� �������� ������ (�� ����� �� ��������� �� �����������), ������ ������� �� ���� ����� ����� (�� ����� �� ������� U �� �����������) � �� �������� ����� �� �������� �������� �� �� �� ������ �������� �� ����� ���� �� ������.
* �� ������� Players �� ������ ������������ �� ���������� ������, ������� ������� ��� � ������ �� ������ �� ��� ��� ��������.


�� ���������� CreatePlayer(LevelSet levelSet)


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

�������� ��� ����� ��� �� ������ ��  xml �������� � �� ���������� ������ �� ��� ��� � ������� ��� �����.


 �� ���������� GetPlayers()


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

�� ������� ������� �� ������� LoadGame �� �������� ��� �� ��������� ��������.


Board � �������� ����� �� ������������ ���� �� �������� ���� ����� ��� ��������, ����� ��� �� ����� �� ����������� ����� �� ������� � �������, ������ �� ����������� �� ������� ����� ����������, �� ���������� ���� ������� ��� �� ����� �������, ������� �������������� �� ��������, ������ �� ������ ��� �� �������� ������� � ������.




![alt tag](http://s10.postimg.org/fcwo2fd61/board.jpg)



����� ��������� 112068
��� ���������� 111233
������� ������������ 111232
