using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneMaker
{
    class Mouvement
    {
        public Mouvement()
        {

        }

        public void changeTrajectoire(AvionEnemi avionEnemi, Vector2 angle, int vitesse)
        {
            avionEnemi.velocity = new Vector2(angle.X * vitesse, angle.Y * vitesse);

        }

        public void changeSpeed(AvionEnemi avionEnemi, float speed)
        {
            avionEnemi.velocity *= speed;
        }

        public void changeSpeed(Boss boss, float speed)
        {
            boss.velocity *= speed;
        }

        public void setSpeed(AvionEnemi avionEnemi, Vector2 velocity)
        {
            avionEnemi.velocity = velocity;
        }

    }
}
