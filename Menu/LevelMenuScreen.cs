#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PlaneMaker;
using System.Xml.Linq;
using System.IO;

#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class LevelMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry level1MenuEntry;
        MenuEntry level2MenuEntry;
        MenuEntry level3MenuEntry;
        MenuEntry level4MenuEntry;
        public bool lvl2locked = true;
        public bool lvl3locked = true;
        public bool lvl4locked = true;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelMenuScreen()
            : base("Choix du niveau")
        {
            // Create our menu entries.

            try
            {
                XElement xElement = XElement.Load("Content\\Save\\save.xml");

                if (xElement.Element("lvl2locked").Value == "false")
                    lvl2locked = false;
                if (xElement.Element("lvl3locked").Value == "false")
                    lvl3locked = false;
                if (xElement.Element("lvl4locked").Value == "false")
                    lvl4locked = false;

            }
            catch (FileNotFoundException fnfe)
            { }
            catch (DirectoryNotFoundException dnfe)
            { }

            level1MenuEntry = new MenuEntry(string.Empty);
            level2MenuEntry = new MenuEntry(string.Empty);
            level3MenuEntry = new MenuEntry(string.Empty);
            level4MenuEntry = new MenuEntry(string.Empty);
            SetMenuEntryText();

            MenuEntry backMenuEntry = new MenuEntry("Retour");

            // Hook up menu event handlers.
            level1MenuEntry.Selected += Level1MenuEntrySelected;
            level2MenuEntry.Selected += Level2MenuEntrySelected;
            level3MenuEntry.Selected += Level3MenuEntrySelected;
            level4MenuEntry.Selected += Level4MenuEntrySelected;
            backMenuEntry.Selected += RemoveLevelMenuScreen;
            
            // Add entries to the menu.
            MenuEntries.Add(level1MenuEntry);
            MenuEntries.Add(level2MenuEntry);
            MenuEntries.Add(level3MenuEntry);
            MenuEntries.Add(level4MenuEntry);
            MenuEntries.Add(backMenuEntry);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            level1MenuEntry.Text = "Niveau 1";
            if (lvl2locked)
                level2MenuEntry.Text = "Niveau 2 (Locked)";
            else
                level2MenuEntry.Text = "Niveau 2";
            if (lvl3locked)
                level3MenuEntry.Text = "Niveau 3 (Locked)";
            else
                level3MenuEntry.Text = "Niveau 3";
            if (lvl4locked)
                level4MenuEntry.Text = "Niveau 4 (Locked)";
            else
                level4MenuEntry.Text = "Niveau 4";
            
            
        }


        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Accelerer menu entry is selected.
        /// </summary>
        void Level1MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            // GO level 1
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
            if (MediaPlayer.State == MediaState.Stopped)
                MediaPlayer.Play(MainMenuScreen.stageSong1);

            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Play(MainMenuScreen.stageSong1);
                MediaPlayer.Pause();
            }

            Program.game.setStage(1);
            if (Game1.pause)
                Game1.pause = false;
            
            ScreenManager.RemoveScreen(this);
            ScreenManager.AddScreen(Program.game, e.PlayerIndex);
        }

        void Level2MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (!lvl2locked)
            {
                // GO level 2
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Stop();
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(MainMenuScreen.stageSong2);

                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Play(MainMenuScreen.stageSong2);
                    MediaPlayer.Pause();
                }
                Program.game.setStage(2);
                if (Game1.pause)
                    Game1.pause = false;
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(Program.game, e.PlayerIndex);
            }
        }

        void Level3MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (!lvl3locked)
            {
                // GO level 3
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Stop();
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(MainMenuScreen.stageSong3);

                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Play(MainMenuScreen.stageSong3);
                    MediaPlayer.Pause();
                }
                Program.game.setStage(3);
                if (Game1.pause)
                    Game1.pause = false;
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(Program.game, e.PlayerIndex);
            }
        }

        void Level4MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (!lvl4locked)
            {
                // GO level 4
                if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Stop();
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(MainMenuScreen.stageSong4);

                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Play(MainMenuScreen.stageSong4);
                    MediaPlayer.Pause();
                }
                Program.game.setStage(4);
                if (Game1.pause)
                    Game1.pause = false;
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(Program.game, e.PlayerIndex);
            }
        }

        void RemoveLevelMenuScreen(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.RemoveScreen(this);

        }

        #endregion


        public void debloquerNiveau(int stage)
        {
            switch (stage)
            {
                case 1:
                    lvl2locked = false;
                    level2MenuEntry.Text = "Niveau 2";
                    break;
                case 2:
                    lvl3locked = false;
                    level3MenuEntry.Text = "Niveau 3";
                    break;
                case 3:
                    lvl4locked = false;
                    level4MenuEntry.Text = "Niveau 4";
                    break;
            }
        }
    }
}
