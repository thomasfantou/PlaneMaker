using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneMaker
{
    public class Font
    {
        public Texture2D sprite;
        public bool alive;
        public Vector2 velocity;
        public Vector2 position;

        public Font(Texture2D texture2D)
        {
            this.alive = false;
            this.sprite = texture2D;
            velocity = Vector2.Zero;
            position = Vector2.Zero;
        }

        public Font()
        {

        }
    }
}
