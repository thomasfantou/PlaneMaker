using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace PlaneMaker
{
    public class Boss : Avion
    {
        public int type;
        public int damageMissile;
        public int damageCollision;
        public int dropedExp;
        public List<Missile[]> missiles;
        public Missile[] missile1;
        public Missile[] missile2;
        public Missile[] missile3;
        public Missile[] missile4;
        public Missile bouclie;
        private ContentManager content;
        public Vector2 targetHero;
        public bool invincible;

        public Boss(ContentManager content, int type)
        {
            this.type = type;
            this.alive = false;
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            this.content = content;
            missiles = new List<Missile[]>();
            if (this.type == 1) //BOSS
            {

                this.sprite = content.Load<Texture2D>("Sprites\\Boss1");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 6000;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 500;
                this.damageCollision = 45;
                this.damageMissile = 20;
                this.invincible = true;
                

                missile1 = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile1[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss1"), this.damageMissile * 5);
                }

                missile2 = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile2[i] = new Missile(content.Load<Texture2D>("Sprites\\missile2Boss1"), this.damageMissile * 3);
                    missile2[i].direction = true;
                }

                missile3 = new Missile[60];
                for (int i = 0; i < 60; i++)
                {
                    missile3[i] = new Missile(content.Load<Texture2D>("Sprites\\missile3Boss1"), this.damageMissile);
                }

                missile4 = new Missile[150];
                for (int i = 0; i < 150; i++)
                {
                    missile4[i] = new Missile(content.Load<Texture2D>("Sprites\\missile4Boss1"), this.damageMissile / 2);
                }

                bouclie = new Missile(content.Load<Texture2D>("Sprites\\bouclieBoss1"), 0);

                missiles.Add(missile1);
                missiles.Add(missile2);
                missiles.Add(missile3);
                missiles.Add(missile4);
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 2) //BOSS
            {

                this.sprite = content.Load<Texture2D>("Sprites\\Boss2");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 11000;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 9000;
                this.damageCollision = 150;
                this.damageMissile = 80;

                missile1 = new Missile[80];
                for (int i = 0; i < 80; i++)
                {
                    missile1[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss2"), this.damageMissile);
                }
                missile2 = new Missile[20];
                for (int i = 0; i < 20; i++)
                {
                    missile2[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss2"), this.damageMissile);
                }
                missile3 = new Missile[20];
                for (int i = 0; i < 20; i++)
                {
                    missile3[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss2"), this.damageMissile);
                }

                missile4 = new Missile[80];
                for (int i = 0; i < 80; i++)
                {
                    missile4[i] = new Missile(content.Load<Texture2D>("Sprites\\missile4Boss2"), this.damageMissile * 1.5f);
                }


                missiles.Add(missile1);
                missiles.Add(missile2);
                missiles.Add(missile3);
                missiles.Add(missile4);
               
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 3) //BOSS
            {

                this.sprite = content.Load<Texture2D>("Sprites\\Boss3");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 37000;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 80000;
                this.damageCollision = 400;
                this.damageMissile = 60;
                this.invincible = true;

                missile1 = new Missile[3];
                for (int i = 0; i < 3; i++)
                {
                    missile1[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss1"), this.damageMissile * 5);
                }

                missile2 = new Missile[3];
                for (int i = 0; i < 3; i++)
                {
                    missile2[i] = new Missile(content.Load<Texture2D>("Sprites\\missile2Boss1"), this.damageMissile * 3);
                    missile2[i].direction = true;
                }

                missile3 = new Missile[120];
                for (int i = 0; i < 120; i++)
                {
                    missile3[i] = new Missile(content.Load<Texture2D>("Sprites\\missile3Boss1"), this.damageMissile);
                }

                missile4 = new Missile[150];
                for (int i = 0; i < 150; i++)
                {
                    missile4[i] = new Missile(content.Load<Texture2D>("Sprites\\missile4Boss1"), this.damageMissile / 2);
                }

                bouclie = new Missile(content.Load<Texture2D>("Sprites\\bouclieBoss1"), 0);

                missiles.Add(missile1);
                missiles.Add(missile2);
                missiles.Add(missile3);
                missiles.Add(missile4);
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 4) //BOSS
            {

                this.sprite = content.Load<Texture2D>("Sprites\\Boss4");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 11000;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 130000;
                this.damageCollision = 700;
                this.damageMissile = 300;

                missile1 = new Missile[80];
                for (int i = 0; i < 80; i++)
                {
                    missile1[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss2"), this.damageMissile);
                }
                missile2 = new Missile[20];
                for (int i = 0; i < 20; i++)
                {
                    missile2[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss2"), this.damageMissile);
                }
                missile3 = new Missile[20];
                for (int i = 0; i < 20; i++)
                {
                    missile3[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1Boss2"), this.damageMissile);
                }

                missile4 = new Missile[80];
                for (int i = 0; i < 80; i++)
                {
                    missile4[i] = new Missile(content.Load<Texture2D>("Sprites\\missile4Boss2"), this.damageMissile * 1.5f);
                }


                missiles.Add(missile1);
                missiles.Add(missile2);
                missiles.Add(missile3);
                missiles.Add(missile4);

                this.state = 0; //voir enumération Game1.cs
            }

        }
    }
}
