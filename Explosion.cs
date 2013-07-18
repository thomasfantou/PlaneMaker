using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PlaneMaker
{
    public class Explosion
    {
        public Texture2D sprite;
        public float width;
        public float height;
        public int frameCount;
        public int currentFrame;
        public Rectangle sourceRect;
        public Vector2 destination;
        public bool alive;
        int current = 0;
        int delay;

        public Explosion(Texture2D texture2D, int spriteCount, int delay)
        {
            this.alive = true;
            this.sprite = texture2D;
            this.width = this.sprite.Width / spriteCount;
            this.height = this.sprite.Height;
            this.frameCount = spriteCount;
            this.currentFrame = 0;
            this.sourceRect = new Rectangle((int)(this.width * currentFrame), 0, (int)this.width, (int)this.height);
            this.destination = new Vector2();
            this.delay = delay;

        }

        public void update()
        {
            if (current == 0)
            {
                if (currentFrame < frameCount)
                {
                    this.currentFrame += 1;
                    sourceRect = new Rectangle((int)(this.width * currentFrame), 0, (int)this.width, (int)this.height);
                }
                else
                {
                    this.currentFrame = 0;
                    sourceRect = new Rectangle((int)(this.width * currentFrame), 0, (int)this.width, (int)this.height);
                    this.alive = false;
                }
            }
            current += 1;
            if (current == delay)
            {
                current = 0;
            }
        }
    }
}
