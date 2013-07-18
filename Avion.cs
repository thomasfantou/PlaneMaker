using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneMaker
{
    public class Avion
    {
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 center;
        public Vector2 velocity;
        public bool alive;
        public int healthPoint;
        public int currentHealthPoint;
        public int state;
        public int width;
        public int height;

        public void changeSprite(Texture2D texture2D)
        {
            this.sprite = texture2D;
        }
    }
}
