#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using PlaneMaker;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class PauseMenuScreen : MenuScreen
    {
        #region Initialization

        LevelMenuScreen lms;
        /// <summary>
        /// Constructor.
        /// </summary>
        public PauseMenuScreen()
            : base("Paused")
        {
            // Flag that there is no need for the game to transition
            // off when the pause menu is on top of it.
            IsPopup = true;

            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Continuer");
            MenuEntry quitGameMenuEntry = new MenuEntry("Revenir au menu");
            
            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += RemovePauseMenuScreen;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        public PauseMenuScreen(LevelMenuScreen _lms)
            : base("Paused")
        {
            // Flag that there is no need for the game to transition
            // off when the pause menu is on top of it.
            IsPopup = true;
            lms = _lms;

            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Continuer");
            MenuEntry quitGameMenuEntry = new MenuEntry("Revenir au menu");

            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += RemovePauseMenuScreen;
            quitGameMenuEntry.Selected += GoToLevelMenu;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Etes-vous sur de vouloir quitter la partie ?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void RemovePauseMenuScreen(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.RemoveScreen(this);
            Game1.pause = false;
        }

        void GoToLevelMenu(object sender, PlayerIndexEventArgs e)
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
            if (MediaPlayer.State == MediaState.Stopped)
                MediaPlayer.Play(MainMenuScreen.menuSong);

            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Play(MainMenuScreen.menuSong);
                MediaPlayer.Pause();
            }

            
            ScreenManager.RemoveScreen(this);
            ScreenManager.RemoveScreen(Program.game);
            ScreenManager.AddScreen(lms, PlayerIndex.One);
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the pause menu screen. This darkens down the gameplay screen
        /// that is underneath us, and then chains to the base MenuScreen.Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            base.Draw(gameTime);
        }


        #endregion
    }
}
