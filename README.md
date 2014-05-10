BananaKeeper
============

Името на играта е Banana Keeper. 
Целта е минионот да ги донесе сите банани на затемнетите полиња. Целата игра се состои од 40 нивоа, секое со различна тежина.
Првиот прозорец кој се појавува, овозможува неколку опции
* Креирање на нов играч преку копчето New Game

* Доколку веќе постои играч преку копчето Load Game да се избере истиот од листата во која се зачувани сите дотогаш креирани и да се продолжи соодветното ниво

* Преку копчето Exit да се изгаси апликацијата

Позадинската музика е опционална, играчот има можност да ја исклучи доколку не му се допаѓа.

![alt tag](http://s28.postimg.org/c3chtki59/menu.jpghttp://url/to/img.png)

![alt tag](http://s11.postimg.org/gk1ie0vgj/new.jpg)

![alt tag](http://s17.postimg.org/7br853ozz/load.jpg)



За креирање на апликацијата ни беа потребни 4 класи: Item, Level, LevelSet и Players.
* Во класата Item ги чуваме x и y позициите на деловите потребни за конструирање на изгледот на играта, кои се од тип ItemType и може да бидат Wall, Floor, Package, Goal, Minion, PackageOnGoal, MinionOnGoal, Space. 
* Во класата Level го исцртуваме соодветното ниво, имаме функции за придвижување на  минионот во четирите насоки и ги додаваме слики на деловите потребни за да се исцрта изгледот на секое ниво од играта.
* Во класата Level го исцртуваме соодветното ниво, имаме функции за придвижување на  минионот во четирите насоки (со помош на стрелките од тастатурата), негово враќање за еден потег назад (со помош на копчето U од тастатурата) и ги додаваме слики на деловите потребни за да се исцрта изгледот на секое ниво од играта.
* Во класата Players ги чуваме информациите за креираните играчи, односно нивното име и бројот на нивото до кое што стигнале.


Со функцијата CreatePlayer(LevelSet levelSet)


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

креираме нов играч кој го чуваме во  xml документ и го зачувуваме нивото до кое што е стигнат тој играч.


 Со функцијата GetPlayers()


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

ја полниме листата од формата LoadGame со играчите кои се претходно креирани.


Board е главната форма во апликацијата која ги повикува сите класи кои постојат, преку неа на екран се прикажуваат името на играчот и нивоата, истите се променуваат по успешно нивно завршување, се исцртуваат сите промени кои ги прави играчот, односно придвижувањето на минионот, бројот на потези кој ги направил играчот и слично.




![alt tag](http://s10.postimg.org/fcwo2fd61/board.jpg)



Ѓорѓи Настовски 112068
Ана Трипуноска 111233
Даниела Трендафилова 111232
