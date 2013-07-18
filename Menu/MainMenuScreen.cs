#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using PlaneMaker;
using System.Xml.Linq;
using System.IO;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization

        static public Song menuSong;
        static public Song bossSong1;
        static public Song bossSong2;
        static public Song stageSong1;
        static public Song stageSong2;
        static public Song stageSong3;
        static public Song stageSong4;
        static public LevelMenuScreen lms;



        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Menu Principal")
        {
            string dir = "Content\\Save";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string path = "Content\\Save\\save.xml";

            // Delete the file if it exists.
            if (!File.Exists(path)) 
            {   
                // Create the file.
                FileStream fs = File.Create(path);
                fs.Close();
                XElement xElement =
                    new XElement("Save",
                        new XElement("lvl2locked"),
                        new XElement("lvl3locked"),
                        new XElement("lvl4locked"),
                        new XElement("XP"),
                        new XElement("Lvl"),
                        new XElement("Godmode"),
                        new XElement("DeadCount"),
                        new XElement("EnemiKilled"),
                        new XElement("TimePlayed")

                        );

                xElement.Save("Content\\Save\\save.xml");
            }


            loadGame();




            // Create our menu entries.
            //MenuEntry playGameMenuEntry = new MenuEntry("Choix du niveau");
            MenuEntry levelGameMenuEntry = new MenuEntry("Choix du niveau");
            MenuEntry optionsMenuEntry = new MenuEntry("Configuration des touches");
            MenuEntry statistiqueMenuEntry = new MenuEntry("Statistiques");
            MenuEntry resetGameEntry = new MenuEntry("Effacer ma sauvegarde");
            MenuEntry exitMenuEntry = new MenuEntry("Quitter");

            // Hook up menu event handlers.
            //playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            levelGameMenuEntry.Selected += LevelGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            resetGameEntry.Selected += resetGameEntrySelected;
            statistiqueMenuEntry.Selected += statistiqueMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            //MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(levelGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(statistiqueMenuEntry);
            MenuEntries.Add(resetGameEntry);
            MenuEntries.Add(exitMenuEntry);

            lms = new LevelMenuScreen();



            
        }

        public void loadGame()
        {
            string path = "Content\\Save\\save.xml";

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                XElement xElement = XElement.Load(path);

                if (xElement.Element("Godmode").Value == "true")
                    Program.godMode = true;
                else
                    Program.godMode = false;

                if (xElement.Element("DeadCount").Value != "")
                    Program.game.deadCount = int.Parse(xElement.Element("DeadCount").Value);
                else
                    Program.game.deadCount = 0;

                if (xElement.Element("XP").Value != "")
                    Program.avionHero.currentExp = int.Parse(xElement.Element("XP").Value);
                else
                    Program.avionHero.currentExp = 0;

                if (xElement.Element("Lvl").Value != "")
                    Program.avionHero.level = int.Parse(xElement.Element("Lvl").Value);
                else
                    Program.avionHero.level = 1;

                if (xElement.Element("EnemiKilled").Value != "")
                    Program.game.enemiKilled = int.Parse(xElement.Element("EnemiKilled").Value);
                else
                    Program.game.enemiKilled = 0;

                if (xElement.Element("TimePlayed").Value != "")
                    Program.game.timePlayed = float.Parse(xElement.Element("TimePlayed").Value.Replace('.', ','));
                else
                    Program.game.timePlayed = 0f;


            }
        }

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            menuSong = content.Load<Song>("Musics\\heart-of-fire");
            bossSong1 = content.Load<Song>("Musics\\Final Fantasy VII - Boss Battle Theme");
            stageSong1 = content.Load<Song>("Musics\\01-front-line-base");
            stageSong2 = content.Load<Song>("Musics\\08-project-4");
            stageSong3 = content.Load<Song>("Musics\\07-air-and-ground");
            stageSong4 = content.Load<Song>("Musics\\02-air-force");
            bossSong2 = content.Load<Song>("Musics\\gallantry-round1-4");
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(menuSong);
            }
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        //void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
        //                       new GameplayScreen());
        //}

        void LevelGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(lms, e.PlayerIndex);
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new GameplayScreen(), e.PlayerIndex);
        }

        void resetGameEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Etes-vous sur de vouloir effacer votre sauvegarde?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmDeleteMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, PlayerIndex.One);
        }

        void statistiqueMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Etes-vous sur de vouloir quitter le jeu ?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            Program.game.saveGame();
            ScreenManager.Game.Exit();
        }

        void ConfirmDeleteMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            XElement xElement =
                 new XElement("Save",
                     new XElement("lvl2locked"),
                     new XElement("lvl3locked"),
                     new XElement("lvl4locked"),
                     new XElement("XP"),
                     new XElement("Lvl"),
                     new XElement("Godmode"),
                     new XElement("DeadCount"),
                     new XElement("EnemiKilled"),
                     new XElement("TimePlayed")

                     );

            xElement.Save("Content\\Save\\save.xml");

            lms = new LevelMenuScreen();
            Program.renewGame();
            loadGame();
        }


        #endregion
    }
}
