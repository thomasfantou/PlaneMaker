using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneMaker
{
    public class Missile
    {
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 center;
        public Vector2 velocity;
        public bool alive;
        public float damage;
        public bool visible;
        public float scale;
        public int type;
        public bool direction;

        public Missile(Texture2D texture2D, float damage)
        {
            this.alive = false;
            this.sprite = texture2D;
            this.position = Vector2.Zero;
            this.center = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.velocity = Vector2.Zero;
            this.damage = damage;
            this.scale = 1;
            this.direction = false;
            this.type = 0; // sert uniquement pour valve, définir si les missiles sont plus puissant
        }
    }
}
