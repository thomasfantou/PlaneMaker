using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;

namespace PlaneMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static public int windowWidth = 1024; //Des variables accéssible dans les menus, et dans le jeu
        static public int windowHeight = 768;
        static public bool godMode = false; 
        static public Game1 game = new Game1(); //instance de la classe game
        static public AvionHero avionHero = new AvionHero(); //On en aura besoin pour charger un avion du fichier save

        static public void renewGame()
        {
            game = new Game1();
            avionHero = new AvionHero();
        }

        public class GameStateManagementGame : Microsoft.Xna.Framework.Game
        {
            #region Fields

            public GraphicsDeviceManager graphics;
            ScreenManager screenManager;

            #endregion

            #region Initialization


            /// <summary>
            /// The main game constructor.
            /// </summary>
            public GameStateManagementGame()
            {
                Content.RootDirectory = "Content";

                graphics = new GraphicsDeviceManager(this);

                graphics.PreferredBackBufferWidth = windowWidth;
                graphics.PreferredBackBufferHeight = windowHeight;
                graphics.ToggleFullScreen();

                // Create the screen manager component.
                screenManager = new ScreenManager(this);

                Components.Add(screenManager);

                // Activate the first screens.
                screenManager.AddScreen(new BackgroundScreen(), null);
                screenManager.AddScreen(new MainMenuScreen(), null);
            }


            #endregion

            #region Draw


            /// <summary>
            /// This is called when the game should draw itself.
            /// </summary>
            protected override void Draw(GameTime gameTime)
            {
                graphics.GraphicsDevice.Clear(Color.Black);

                // The real drawing happens inside the screen manager component.
                base.Draw(gameTime);
            }


            #endregion
        }

        static void Main(string[] args)
        {
            //using (Game1 game = new Game1())
            //{
            //    game.Run();
            //}
            using (GameStateManagementGame game = new GameStateManagementGame())
            {
                game.Run();
            }
        }

    }
}

