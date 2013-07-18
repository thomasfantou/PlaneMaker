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
    class AttackSequence
    {
        Mouvement mouvement;
        ContentManager contentManager;
        


        public AttackSequence(ContentManager content)
        {
            this.contentManager = content;
            mouvement = new Mouvement();

        }

        public void attackTopRight1(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 200);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 0);
            }
        }

        public void attackTopRight1(List<AvionEnemi> avionEnemies, int hauteur)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, hauteur);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 0);
            }
        }

        public void attackTopRight2(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 100, 100);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 0);
            }
        }

        public void attackMiddleRight1(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 80 + (768-80)/2);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 0);
            }
        }


        public void changeDirection(List<AvionEnemi> avionEnemies, Vector2 vector)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).velocity = vector;
            }
        }

        public void attackBottomRight1(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 550);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 0);
            }
        }

        public void attackBottomRight1(List<AvionEnemi> avionEnemies, int hauteur)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, hauteur);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 0);
            }
        }

        public void attackBottomRight2(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 500);
                avionEnemies.ElementAt(i).velocity = new Vector2(-4, -7);
            }
        }

        public void attackTopMiddle1(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(800, 0 - i * 150);
                avionEnemies.ElementAt(i).velocity = new Vector2(0, 3);
            }
        }

        public void attackBottomLeft1(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(0 - i * 400, 600);
                avionEnemies.ElementAt(i).velocity = new Vector2(5, 0);
            }
        }


        public void attackBottomLeft2(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(0 - i * 300, 400);
                avionEnemies.ElementAt(i).velocity = new Vector2(4, 0);
            }
        }

        public void attackBottomLeft3(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(0 - i * 300, 620);
                avionEnemies.ElementAt(i).velocity = new Vector2(4, 0);
            }
        }


        public void attackTopLeft1(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(0 - i * 400, 150);
                avionEnemies.ElementAt(i).velocity = new Vector2(5, 0);
            }
        }

        public void attackTopLeft2(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(0 - i * 300, 120);
                avionEnemies.ElementAt(i).velocity = new Vector2(4, 0);
            }
        }

        public void attackTopLeft3(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(0 - i * 300, 270);
                avionEnemies.ElementAt(i).velocity = new Vector2(4, 0);
            }
        }

        public void attackBottomMiddle1(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(800, 768 + i * 150);
                avionEnemies.ElementAt(i).velocity = new Vector2(0, -3);
            }
        }

        public void bossIncoming1(Boss boss)
        {
           
                boss.position = new Vector2(1024, 80 + ((768-80)/2)-(boss.height/2));
                boss.velocity = new Vector2(-1, 0);
            
        }

        public void bossIncoming2(Boss boss)
        {

            boss.position = new Vector2(1024, 80 + ((768 - 80) / 2) - (boss.height / 2) + 50);
            boss.velocity = new Vector2(-1, 0);

        }

        public void attackRight101(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024, 100 + i * 105);
                avionEnemies.ElementAt(i).velocity = new Vector2(-3, 0);
            }
        }
        public void attackRight102(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024, 120 + i * 105);
                avionEnemies.ElementAt(i).velocity = new Vector2(-4.5f, 0);
            }
        }
        public void attackRight103(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024, 140 + i * 105);
                avionEnemies.ElementAt(i).velocity = new Vector2(-3, 0);
            }
        }
        public void attackRight104(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                int modulo = i % 2;
                if (i == 0)
                {
                    avionEnemies.ElementAt(i).position = new Vector2(1024, (795 - 80) / 2);
                }
                else if (modulo == 0)
                {
                    avionEnemies.ElementAt(i).position = new Vector2(1024 + 100 + (((i - 1) / 2) * 100), ((795 - 80) / 2) + 80 + (((i - 1) / 2) * 80));
                }
                else if (modulo == 1)
                {
                    avionEnemies.ElementAt(i).position = new Vector2(1024 + 100 + (((i - 1) / 2) * 100), (795 / 2) - 80 - (((i - 1) / 2) * 80));
                }
                avionEnemies.ElementAt(i).velocity = new Vector2(-3, 0);
            }
        }
        public void attackTopRight102(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 100);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 1);
            }
        }

        public void attackMiddleRight101(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 80 + (950 - 80) / 2);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, 0);
            }
        }

        public void attackBottomRight102(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 650);
                avionEnemies.ElementAt(i).velocity = new Vector2(-4, 0.5f);
            }
        }

        public void attackBottomRight101(List<AvionEnemi> avionEnemies, int hauteur)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, hauteur);
                avionEnemies.ElementAt(i).velocity = new Vector2(-2, 0);
            }
        }

        public void attackBottomRight103(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 700);
                avionEnemies.ElementAt(i).velocity = new Vector2(-5, -1);
            }
        }

        public void attackBottomLeft101(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(0 - i * 222, 700);
                avionEnemies.ElementAt(i).velocity = new Vector2(4, -1.5f);
            }
        }

        public void attackBottomRight141(List<AvionEnemi> avionEnemies)
        {
            for (int i = 0; i < avionEnemies.Count(); i++)
            {
                avionEnemies.ElementAt(i).position = new Vector2(1024 + i * 200, 690 - i * 25);
                avionEnemies.ElementAt(i).velocity = new Vector2(-2, 0);
            }
        }

        /* public void createSequence(List<AvionEnemi> avionList, int numberOfEnemies, int type, Vector2 positionStart)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector2 positionDecalage = new Vector2();
                positionDecalage.X = positionStart.X + i * 200;
                positionDecalage.Y = positionStart.Y;
                AvionEnemi avionEnemi = new AvionEnemi(contentManager, type, positionDecalage);
                avionList.Add(avionEnemi);
            }
        }

        public void launchSequence(List<AvionEnemi> avionList, Vector2 deplacement)
        {
            if (deplacement.Y > 0)
            {
                for (int i = 0; i < avionList.Count(); i++)
                {
                    avionList.ElementAt(i).position.Y += i * -33;
                }
            }
            if (deplacement.Y < 0)
            {
                for (int i = 0; i < avionList.Count(); i++)
                {
                    avionList.ElementAt(i).position.Y += i * 33;
                }
            }
            foreach (AvionEnemi avionEnemi in avionList)
            {
                avionEnemi.startSimpleAttack(deplacement);
            }
        }

        public void deleteSequence(List<AvionEnemi> avionList)
        {
            avionList.Clear();
        }*/
    }
}
