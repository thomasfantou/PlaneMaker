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
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry infoDies;
        MenuEntry infoKills;
        MenuEntry infoTime;
        int DeadCount;
        int EnemiKilled;
        float TimePlayed;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("")
        {
            infoDies = new MenuEntry(string.Empty);
            infoKills = new MenuEntry(string.Empty);
            infoTime = new MenuEntry(string.Empty);



            DeadCount = Program.game.deadCount;
            EnemiKilled = Program.game.enemiKilled;
            TimePlayed = Program.game.timePlayed;




            MenuEntry backMenuEntry = new MenuEntry("Retour");

            backMenuEntry.Selected += RemoveOptionsMenuScreen;


            SetMenuEntryText();

            MenuEntries.Add(infoDies);
            MenuEntries.Add(infoKills);
            MenuEntries.Add(infoTime);
            MenuEntries.Add(backMenuEntry);
        }

        

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            if (Program.game.deadCount != 0)
                infoDies.Text = "Vous etes mort " + Program.game.deadCount + " fois.";
            else
                infoDies.Text += "Vous etes mort 0 fois";
            if (Program.game.enemiKilled != 0)
                infoKills.Text += "Vous avez abattu " + Program.game.enemiKilled + " enemies.";
            else
                infoKills.Text += "Vous avez abattu 0 enemies ";
            if (Program.game.timePlayed != 0f)
            {
                int tempSec = (int)(Program.game.timePlayed / 1000);
                int tempMin = tempSec / 60;
                tempSec = (tempSec % 60);
                int tempH = tempMin / 60;
                tempMin = (tempMin % 60);
                infoTime.Text += "Temps en jeu : " + tempH + "h " + tempMin + "min " + tempSec + "sec.";
            }
            else
                infoTime.Text += "Temps en jeu : ";
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Accelerer menu entry is selected.
        /// </summary>
        void AccelererMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            
        }

        void RemoveOptionsMenuScreen(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.RemoveScreen(this);

        }

        /// <summary>
        /// Event handler for when the Ralentir menu entry is selected.
        /// </summary>
        void RalentirMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            
        }

        /// <summary>
        /// Event handler for when the Arme1 menu entry is selected.
        /// </summary>
        



        #endregion
    }
}
