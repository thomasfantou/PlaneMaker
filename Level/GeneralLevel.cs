using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneMaker.Level
{
    abstract class GeneralLevel
    {
        static public float timer;
        static public float timerSeq;
        public bool bossFight;
        public bool bossInComing = false;
        public List<AvionEnemi>[] tabAvionEnemi;
        public List<AvionEnemi> avionEnemies = new List<AvionEnemi>();
        public Boss boss;
        public float hitChances = 0; //2 pour 2 fois plus, 0.5 pour 2 fois moin
        public abstract void updateTimer();
        public Vector2 target;
        public int getLevel = 0;
        public int bAlphaValue = 0;

        public GeneralLevel()
        {

        }

        public abstract void update();
        public abstract void resetTimer();
    }
}
