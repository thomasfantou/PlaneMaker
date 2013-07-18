using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneMaker
{
    public class AvionHero : Avion
    {
        public int level;
        public int lives;
        public int currentExp;
        public bool newLvl = false;
        public int nextLvl;
        public int oldLvlExp;
        public int[] tabLife = new int[21] { 0, 50, 80, 120, 180, 250, 300, 400, 500, 700, 900, 1000, 1200, 1500, 1800, 2000, 2400, 2800, 3300, 3500, 4000 };
        public int[] tabExp = new int[22] { 0, 0, 110, 400, 900, 1800, 3000, 8000, 15000, 25000, 40000, 65000, 100000, 150000, 250000, 500000, 750000, 1100000, 1450000, 2000000, 3000000, -1 };
        public float timeToRecovery;
        public bool invincible;

        public AvionHero()
        {
            //this.invincible = true; //debug
            this.alive = false;
            this.position = Vector2.Zero;
            
            this.velocity = Vector2.Zero;
            this.healthPoint = tabLife[1];
            this.currentHealthPoint = this.healthPoint;
            this.lives = 3;
            this.level = 1;
            this.currentExp = 0;
            this.nextLvl = tabExp[2];
            this.oldLvlExp = tabExp[1];
            //this.currentExp = 1800;
            //this.nextLvl = tabExp[6];
            //this.oldLvlExp = tabExp[5];
            this.state = 1; //voir enumération Game1.cs
            this.timeToRecovery = 0;
        }

        public AvionHero(Texture2D texture2D)
        {
            //this.invincible = true; //debug
            this.alive = false;
            this.position = Vector2.Zero;
            this.sprite = texture2D;
            this.width = this.sprite.Width;
            this.height = this.sprite.Height / 3;
            this.center = new Vector2(this.width / 2, this.height / 2);
            this.velocity = Vector2.Zero;
            this.healthPoint = tabLife[1];
            this.currentHealthPoint = this.healthPoint;
            this.lives = 3;
            this.level = 1;
            this.currentExp = 0;
            this.nextLvl = tabExp[2];
            this.oldLvlExp = tabExp[1];
            //this.currentExp = 1800;
            //this.nextLvl = tabExp[6];
            //this.oldLvlExp = tabExp[5];
            this.state = 1; //voir enumération Game1.cs
            this.timeToRecovery = 0;
        }

        public void setSprite(Texture2D texture2D)
        {
            this.sprite = texture2D;
            this.width = this.sprite.Width;
            this.height = this.sprite.Height / 3;
            this.center = new Vector2(this.width / 2, this.height / 2);
        }

        public void gainExp(int expFromEnemi)
        {
            currentExp += expFromEnemi;
            checkForLvl();
        }

        private void checkForLvl()
        {
            switch (level)
            {
                case 1:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 2:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 3:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 4:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 5:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 6:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 7:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 8:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 9:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 10:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 11:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 12:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 13:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 14:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 15:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 16:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 17:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 18:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;
                case 19:
                    if (currentExp >= this.nextLvl)
                    {
                        lvlUp();
                    }
                    break;


            }
        }

        public void setNewVar(int lvl)
        {
            float percentangeHP = 1 - ((float)(this.healthPoint - this.currentHealthPoint) / (float)this.healthPoint);
            this.healthPoint = tabLife[lvl];
            this.currentHealthPoint = (int)(this.healthPoint * (percentangeHP));
            this.currentHealthPoint += (int)(this.healthPoint * ((1 - percentangeHP)/2));
            this.oldLvlExp = tabExp[lvl];
            this.nextLvl = tabExp[lvl + 1];

        }

        private void lvlUp()
        {
            level += 1;
            newLvl = true;
        }

        public void setFullHp()
        {
            currentHealthPoint = this.healthPoint;
        }

        
    }
}
