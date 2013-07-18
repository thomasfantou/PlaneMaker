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
    public class AvionEnemi: Avion
    {
        public int type;
        public int damageMissile;
        public int damageCollision;
        public int dropedExp;
        public Missile[] missile;
        private ContentManager content;

        public AvionEnemi(ContentManager content, int type)
        {
            this.type = type;
            this.alive = false;
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            this.content = content;

            if (this.type == 1) //trés facil
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy1");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 10;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 10;
                this.damageCollision = 16;
                this.damageMissile = 8;

                missile = new Missile[1];
                for (int i = 0; i < 1; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 2) //très rapide, dégats peu importants
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy2");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 15;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 18;
                this.damageCollision = 10;
                this.damageMissile = 5;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 3) //gros tank, grosse vitesse de tire, très peu de dégats
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy3");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 300;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 80;
                this.damageCollision = 40;
                this.damageMissile = 3;

                missile = new Missile[30];
                for (int i = 0; i < 30; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 4) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy4");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 30;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 40;
                this.damageCollision = 15;
                this.damageMissile = 10;

                missile = new Missile[1];
                for (int i = 0; i < 1; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 5) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy5");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 80;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 75;
                this.damageCollision = 30;
                this.damageMissile = 13;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            #region Tanks Level 2
            else if (this.type == 201) // Tank de base
            {
                this.sprite = content.Load<Texture2D>("Sprites\\tankEnemy1");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 2;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 800;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 150;
                this.damageCollision = 55;
                this.damageMissile = 35;

                missile = new Missile[3];
                for (int i = 0; i < 3; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            else if (this.type == 202) // Tank evolué
            {
                this.sprite = content.Load<Texture2D>("Sprites\\tankEnemy1");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 2;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 20000;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 80;
                this.damageCollision = 60;
                this.damageMissile = 50;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            #endregion
            #region Hélico Level 2
            else if (this.type == 301) // Helico de base
            {
                this.sprite = content.Load<Texture2D>("Sprites\\helico01");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 100;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 135;
                this.damageCollision = 55;
                this.damageMissile = 95;

                missile = new Missile[3];
                for (int i = 0; i < 3; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            #endregion
            #region Avions Level 2
            else if (this.type == 101) //
            {
                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy6");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 100;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 80;
                this.damageCollision = 55;
                this.damageMissile = 40;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            else if (this.type == 102) //
            {
                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy6");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 75;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 80;
                this.damageCollision = 55;
                this.damageMissile = 40;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            else if (this.type == 103) //gros tank, grosse vitesse de tire, très peu de dégats
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy3");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 800;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 90;
                this.damageCollision = 70;
                this.damageMissile = 45;

                missile = new Missile[30];
                for (int i = 0; i < 30; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            else if (this.type == 104) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy4");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 350;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 90;
                this.damageCollision = 45;
                this.damageMissile = 45;

                missile = new Missile[1];
                for (int i = 0; i < 1; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            else if (this.type == 105) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy5");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 250;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 75;
                this.damageCollision = 55;
                this.damageMissile = 65;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi2"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            #endregion

            #region level3

            else if (this.type == 31) //trés facil
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy31");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 400;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 600;
                this.damageCollision = 200;
                this.damageMissile = 120;

                missile = new Missile[1];
                for (int i = 0; i < 1; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 32) //très rapide, dégats peu importants
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy32");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 500;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 1400;
                this.damageCollision = 150;
                this.damageMissile = 100;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 33) //gros tank, grosse vitesse de tire, très peu de dégats
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy33");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 3800;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 6400;
                this.damageCollision = 500;
                this.damageMissile = 50;

                missile = new Missile[30];
                for (int i = 0; i < 30; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 34) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy34");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 900;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 2100;
                this.damageCollision = 180;
                this.damageMissile = 150;

                missile = new Missile[1];
                for (int i = 0; i < 1; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            else if (this.type == 35) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy35");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 500;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 4700;
                this.damageCollision = 300;
                this.damageMissile = 200;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }

            #endregion

            #region Tanks Level 4
            if (this.type == 241) // Tank de base
            {
                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy34");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 3, this.height / 3);
                this.healthPoint = 1050;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 3000;
                this.damageCollision = 720;
                this.damageMissile = 300;

                missile = new Missile[3];
                for (int i = 0; i < 3; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi3"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            if (this.type == 242) // Tank evolué
            {
                this.sprite = content.Load<Texture2D>("Sprites\\vaisseauEnemi1");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 3, this.height / 3);
                this.healthPoint = 6000000;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 6000;
                this.damageCollision = 14000;
                this.damageMissile = 180;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi3"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            #endregion
            #region Hélico Level 4
            if (this.type == 341) // Helico de base
            {
                this.sprite = content.Load<Texture2D>("Sprites\\helico02");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 1400;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 5050;
                this.damageCollision = 650;
                this.damageMissile = 285;

                missile = new Missile[3];
                for (int i = 0; i < 3; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi3"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            #endregion
            #region Avions Level 4
            if (this.type == 142) //
            {
                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy101");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 350;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 5500;
                this.damageCollision = 300;
                this.damageMissile = 155;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi3"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            if (this.type == 143) //gros tank, grosse vitesse de tire, très peu de dégats
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy104");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 4600;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 6150;
                this.damageCollision = 800;
                this.damageMissile = 65;

                missile = new Missile[30];
                for (int i = 0; i < 30; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi3"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            if (this.type == 144) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy102");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 1400;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 7000;
                this.damageCollision = 320;
                this.damageMissile = 200;

                missile = new Missile[1];
                for (int i = 0; i < 1; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi3"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            if (this.type == 145) //polyvalent
            {

                this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy105");
                this.width = this.sprite.Width;
                this.height = this.sprite.Height / 3;
                this.center = new Vector2(this.width / 2, this.height / 2);
                this.healthPoint = 950;
                this.currentHealthPoint = this.healthPoint;
                dropedExp = 9000;
                this.damageCollision = 500;
                this.damageMissile = 400;

                missile = new Missile[2];
                for (int i = 0; i < 2; i++)
                {
                    missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi3"), this.damageMissile);
                }
                this.state = 0; //voir enumération Game1.cs
            }
            #endregion


        }

        public AvionEnemi(ContentManager content)
        {
            this.alive = false;
            this.position = Vector2.Zero;
            this.sprite = content.Load<Texture2D>("Sprites\\avionEnemy1");
            this.center = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.velocity = Vector2.Zero;
            missile = new Missile[1];
            this.damageCollision = 0;
            for (int i = 0; i < 1; i++)
            {
                missile[i] = new Missile(content.Load<Texture2D>("Sprites\\missile_enemi"), this.damageCollision);
            }
            this.state = 0; //voir enumération Game1.cs
        }

        public void startSimpleAttack(Vector2 deplacementTrajectoireVitesse)
        {
            this.velocity = deplacementTrajectoireVitesse;
        }

        public void changeSprite(String path)
        {
            this.sprite = content.Load<Texture2D>(path);
        }
    }
}
