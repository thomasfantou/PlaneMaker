using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using GameStateManagement;

namespace PlaneMaker
{
    class Music : GameScreen
    {
        static Music instance = null;
        static readonly object padlock = new object();

        Music()
        {
        }

        public static Music getInstance()
        {
            lock (padlock)
            {
                if (instance==null)
                {
                    instance = new Music();
                }
                return instance;
            }
            
        }

    }
}
