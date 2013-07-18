using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PlaneMaker.Level
{
    class Level1 : GeneralLevel
    {
        AttackSequence aseq;
        ContentManager contentManager;
        Mouvement mov;

        private const int maxList = 4;

        public const int numberOfSequence = 50;
        public bool[] seq; // verifie si la sequence a été activé
        public bool[] seqdead; // verifie si la sequence est terminé
        public bool[] allowNext; // donne la possibilité a la séquence d'aprés d'être lu
        public bool[] seqStop; //arrete completement la lecture

        //enumération permetant de définir le mouvement de l'avion avec des mot plutot que des nombres
        public enum stateMovement { none, down, up, forward, backward };
        public enum side { top, right, bottom, left };
        private Random random = new Random();

        private float timerMissile1;
        private float timerMissile1Explode;
        private float timerMissile2;
        private float timerMissile3;
        private float timerMissile4;
        private float timerMove1;
        private float timerMove2;

        public Level1(ContentManager content)
        {
            resetTimer();
            getLevel = 1;
            aseq = new AttackSequence(content);
            mov = new Mouvement();
            contentManager = content;
            tabAvionEnemi = new List<AvionEnemi>[maxList];

            for (int i = 0; i < maxList; i++)
            {
                List<AvionEnemi> temp = new List<AvionEnemi>();
                tabAvionEnemi[i] = temp;
            }

            seq = new bool[numberOfSequence];
            seqdead = new bool[numberOfSequence];
            allowNext = new bool[numberOfSequence];
            seqStop = new bool[numberOfSequence];
            for (int i = 0; i < numberOfSequence; i++)
            {
                seq[i] = false;
                seqdead[i] = false;
                allowNext[i] = false;
                seqStop[i] = false;
            }
            this.bossFight = false;
            allowNext[0] = true;

        }

        #region variable de update
        bool check61 = false;
        bool check62 = false;
        float timeToLive;
        float timeToLive2;
        float timeToLive3;

        #endregion

        #region variable du boss
        bool positionStopedRight = false;
        bool positionStopedLeft = false;
        bool readyToExplode = false;
        double cercle = 0;
        int cycle = 0;
        bool move1 = false;
        bool move2 = false;
        int boucle = 0;
        int horslimite = 0;
        bool finBoucle = true;
        int rand;
        bool stopBoucle = true;
        bool mitraillage = false;
        const int missile4Max = 12;
        int currentMissile4 = 1;
        
        #endregion

        public override void update()
        {
            base.avionEnemies = avionEnemies;
            #region 1
            //*1 EN HAUT
            if (allowNext[0])
            {
                if(!seqStop[0])
                {
                    if (timer > 2000 && !seq[0])
                    {
                        allowNext[1] = true;
                        createListAvionEnemi(0, 5, 1);
                        aseq.attackTopRight1(tabAvionEnemi[0]);
                        seq[0] = true;
                    }
                    if (seq[0] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[0] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }
            //*/
            #endregion

            #region 2
            //*2 EN BAS
            if (allowNext[1])
            {
                if (!seqStop[1])
                {
                    if (timer > 6000 && !seq[1])
                    {
                        allowNext[2] = true;
                        createListAvionEnemi(1, 5, 1);
                        aseq.attackBottomRight1(tabAvionEnemi[1]);
                        seq[1] = true;
                    }
                    if (seq[1] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[1] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }
            //*/
            #endregion

            #region 3
            //*3  ZIG ZAG D EN HAUT
            if (allowNext[2])
            {
                if (!seqStop[2])
                {
                    if (timer > 10000 && !seq[2])
                    {
                        allowNext[3] = true;
                        createListAvionEnemi(2, 3, 1);
                        aseq.attackTopRight1(tabAvionEnemi[2]);
                        seq[2] = true;
                    }
                    if (seq[2] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        seqStop[2] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            if (avionEnemi.position.X < 950 && avionEnemi.position.X > 920)
                            {
                                float rotation = 7 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 6);
                                mov.changeSpeed(avionEnemi, 2);
                                avionEnemi.state = (int)stateMovement.down;
                            }
                            if (avionEnemi.position.X < 600 && avionEnemi.position.X > 570)
                            {
                                float rotation = 9 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 6);
                                avionEnemi.state = (int)stateMovement.up;
                            }
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }
            //*/
            #endregion

            #region 4
            //*4   ZIG ZAG DU MILIEU
            if (allowNext[3])
            {
                if (!seqStop[3])
                {
                    if (timer > 13000 && !seq[3])
                    {
                        allowNext[4] = true;
                        createListAvionEnemi(0, 3, 1);
                        aseq.attackMiddleRight1(tabAvionEnemi[0]);
                        seq[3] = true;
                    }
                    if (seq[3] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[3] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            if (avionEnemi.position.X < 950 && avionEnemi.position.X > 920)
                            {
                                float rotation = 7 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 6);
                                mov.changeSpeed(avionEnemi, 2);
                                avionEnemi.state = (int)stateMovement.down;
                            }
                            if (avionEnemi.position.X < 600 && avionEnemi.position.X > 570)
                            {
                                float rotation = 9 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 6);
                                avionEnemi.state = (int)stateMovement.up;
                            }
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }
            
            //*/

            #endregion

            #region 5
            //*5   HAUT BAS HAUT BAS PETIT AVION

            if (allowNext[4])
            {
                if (!seqStop[4])
                {
                    if (timer > 17000 & !seq[4])
                    {
                        allowNext[5] = true;
                        createListAvionEnemi(1, 4, 2);
                        aseq.attackBottomRight1(tabAvionEnemi[1]);
                        seq[4] = true;
                    }
                    if (seq[4] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[4] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            if (avionEnemi.position.X < 1024 && avionEnemi.position.X > 1012)
                            {
                                float rotation = 11 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 12);
                                avionEnemi.state = (int)stateMovement.up;
                            }

                            if (avionEnemi.position.Y < 150)
                            {
                                float rotation = 5 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 8);
                                avionEnemi.state = (int)stateMovement.down;
                            }
                            if (avionEnemi.position.Y > 600)
                            {
                                float rotation = 11 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 12);
                                avionEnemi.state = (int)stateMovement.up;
                            }
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }

            //*/

            #endregion

            #region 6
            //*6    HAUT + BAS petit avion



            if (allowNext[5])
            {
                if (!seqStop[5])
                {
                    if (timer > 21500 && !seq[5])
                    {
                        allowNext[6] = true;
                        createListAvionEnemi(2, 7, 2);
                        createListAvionEnemi(0, 7, 2);
                        aseq.attackBottomRight1(tabAvionEnemi[2], 650);
                        aseq.attackTopRight1(tabAvionEnemi[0], 150);
                        seq[5] = true;
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            mov.setSpeed(avionEnemi, new Vector2(-2.5f, 0));
                        }
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            mov.setSpeed(avionEnemi, new Vector2(-2.5f, 0));
                        }
                    }
                    if (seq[5] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        check61 = true;
                    }
                    if (seq[5] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        check62 = true;
                    }
                    foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                    {
                        if (!stillAlive(avionEnemi, side.left))
                        {
                            avionEnemi.alive = false;
                        }
                    }
                    foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                    {
                        if (!stillAlive(avionEnemi, side.left))
                        {
                            avionEnemi.alive = false;
                        }
                    }

                    if (check61 && check62)
                    {
                        seqStop[5] = true;
                    }
                }
            }

            //*/

            #endregion

            #region 7
            //*7   AVION IMMOBILE EN HAUT
            if (allowNext[6])
            {
                if (!seqStop[6])
                {
                    if (timer > 33000 && !seq[6])
                    {
                        timeToLive = timer + 6000;
                        allowNext[7] = true;
                        createListAvionEnemi(3, 1, 3);
                        aseq.attackTopMiddle1(tabAvionEnemi[3]);
                        seq[6] = true;
                    }
                    if (seq[6] && isSeqDead(tabAvionEnemi[3]))
                    {
                        tabAvionEnemi[3].Clear();
                        seqStop[6] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[3])
                        {
                            if (timer > timeToLive)
                            {
                                
                                if (avionEnemi.position.Y > 100 && avionEnemi.position.Y < 110)
                                {
                                    mov.changeSpeed(avionEnemi, 2f);
                                }
                                if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 160)
                                {
                                    avionEnemi.state = (int)stateMovement.up;
                                    mov.setSpeed(avionEnemi, new Vector2(0, -2f));
                                }
                                if (!stillAlive(avionEnemi, side.top))
                                {
                                    avionEnemi.alive = false;
                                }
                            }
                            else
                            {
                                avionEnemi.state = (int)stateMovement.down;
                                if (avionEnemi.position.Y > 100 && avionEnemi.position.Y < 110)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(0,2));
                                }
                                if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 160)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                    hitChances = 3;
                                }
                            }






                        }
                    }
                }
               
            }
            //*/
            #endregion

            #region 8
            //*8    EN HAUT A GAUCHE ARC DE CERLCE
            if (allowNext[7])
            {
                if (!seqStop[7])
                {
                    if (timer > 38000 && !seq[7]) //if (timer > 31000 && !seq[7])
                    {
                        allowNext[8] = true;
                        createListAvionEnemi(1, 4, 4);
                        aseq.attackBottomLeft1(tabAvionEnemi[1]);
                        seq[7] = true;
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            avionEnemi.changeSprite("Sprites\\avionEnemy4bis");
                            hitChances = 0.8f;
                        }
                    }
                    if(seq[7] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[7] = true;

                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            if (avionEnemi.position.X > 250)
                            {
                                if (avionEnemi.position.Y > 470)
                                {
                                    avionEnemi.state = (int)stateMovement.up;
                                    avionEnemi.velocity += new Vector2(-0.1f, -0.1f);
                                }
                                else if (avionEnemi.position.Y < 420)
                                {
                                    avionEnemi.changeSprite("Sprites\\avionEnemy4");
                                    avionEnemi.velocity += new Vector2(-0.1f, 0f);

                                }
                                else
                                {
                                    avionEnemi.velocity.X = 0;
                                }
                            }
                            if (avionEnemi.position.X < 250 && avionEnemi.velocity.X < 0)
                            {
                                avionEnemi.velocity = new Vector2(-5, 0);
                            }

                            if (!stillAlive(avionEnemi, side.left) && avionEnemi.position.Y < 420)
                            {
                                avionEnemi.alive = false;
                            }

                        }
                        
                    }
                }
            }
            //*/
            #endregion

            #region 9
            //*9  EN BAS A GAUCHE ARC DE CERCLE
            if (allowNext[8])
            {
                if (!seqStop[8])
                {
                    if (timer > 47000 && !seq[8])
                    {
                        allowNext[9] = true;
                        createListAvionEnemi(2, 4, 4);
                        aseq.attackTopLeft1(tabAvionEnemi[2]);
                        seq[8] = true;
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            avionEnemi.changeSprite("Sprites\\avionEnemy4bis");
                            hitChances = 0.8f;
                        }
                    }
                    if(seq[8] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        seqStop[8] = true;

                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            if (avionEnemi.position.X > 250)
                            {
                                if (avionEnemi.position.Y < 320)
                                {
                                    avionEnemi.state = (int)stateMovement.down;
                                    avionEnemi.velocity += new Vector2(-0.1f, 0.1f);
                                }
                                else if (avionEnemi.position.Y > 370)
                                {
                                    avionEnemi.changeSprite("Sprites\\avionEnemy4");
                                    avionEnemi.velocity += new Vector2(-0.1f, 0f);

                                }
                                else
                                {
                                    avionEnemi.velocity.X = 0;
                                }
                            }
                            if (avionEnemi.position.X < 250 && avionEnemi.velocity.X < 0)
                            {
                                avionEnemi.velocity = new Vector2(-6, 0);
                            }

                            if (!stillAlive(avionEnemi, side.left) && avionEnemi.position.Y > 420)
                            {
                                avionEnemi.alive = false;
                            }

                        }
                        
                    }
                }
            }
            //*/
            #endregion

            #region 10
            //*10  AVION IMMOBILE EN HAUT
            if (allowNext[9])
            {
                if (!seqStop[9])
                {
                    if (timer > 56000 && !seq[9])
                    {
                        timeToLive = timer + 8000;
                        allowNext[10] = true;
                        createListAvionEnemi(3, 1, 3);
                        aseq.attackTopMiddle1(tabAvionEnemi[3]);
                        seq[9] = true;
                    }
                    if (seq[9] && isSeqDead(tabAvionEnemi[3]))
                    {
                        tabAvionEnemi[3].Clear();
                        seqStop[9] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[3])
                        {
                            if (timer > timeToLive)
                            {
                                
                                if (avionEnemi.position.Y > 100 && avionEnemi.position.Y < 110)
                                {
                                    mov.changeSpeed(avionEnemi, 2f);
                                }
                                if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 160)
                                {
                                    avionEnemi.state = (int)stateMovement.up;
                                    mov.setSpeed(avionEnemi, new Vector2(0, -2f));
                                }
                                if (!stillAlive(avionEnemi, side.top))
                                {
                                    avionEnemi.alive = false;
                                }
                            }
                            else
                            {
                                avionEnemi.state = (int)stateMovement.down;
                                if (avionEnemi.position.Y > 100 && avionEnemi.position.Y < 110)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(0,2));
                                }
                                if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 160)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                    hitChances = 3;
                                }
                            }


                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }



                        }
                    }
                }
               
            }
            //*/
            #endregion

            #region 11
            //*11  AVION IMMOBILE AU MILIEU
            if (allowNext[10])
            {
                if (!seqStop[10])
                {
                    if (timer > 63000 && !seq[10])
                    {
                        timeToLive2 = timer + 8000;
                        allowNext[11] = true;
                        createListAvionEnemi(0, 1, 3);
                        aseq.attackMiddleRight1(tabAvionEnemi[0]);
                        seq[10] = true;
                    }
                    if (seq[10] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[10] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            if (timer > timeToLive2)
                            {

                                if (avionEnemi.position.X > 850 && avionEnemi.position.X < 860)
                                {
                                    mov.changeSpeed(avionEnemi, 2f);
                                }
                                if (avionEnemi.position.X > 800 && avionEnemi.position.X < 810)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(2f, 0));
                                }
                                if (!stillAlive(avionEnemi, side.right))
                                {
                                    avionEnemi.alive = false;
                                }
                            }
                            else
                            {
                                if (avionEnemi.position.X > 850 && avionEnemi.position.X < 860)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(-2f, 0));
                                }
                                if (avionEnemi.position.X > 800 && avionEnemi.position.X < 810)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                    hitChances = 3;
                                }
                            }


                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }



                        }
                    }
                }

            }
            //*/
            #endregion

            #region 12
            //*12  AVION IMMOBLE EN BAS
            if (allowNext[11])
            {
                if (!seqStop[11])
                {
                    if (timer > 70000 && !seq[11])
                    {
                        timeToLive3 = timer + 8000;
                        allowNext[12] = true;
                        createListAvionEnemi(1, 1, 3);
                        aseq.attackBottomMiddle1(tabAvionEnemi[1]);
                        seq[11] = true;
                    }
                    if (seq[11] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[11] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            if (timer > timeToLive3)
                            {

                                if (avionEnemi.position.Y > 768 - 110 && avionEnemi.position.Y < 768 - 100)
                                {
                                    mov.changeSpeed(avionEnemi, 2f);
                                }
                                if (avionEnemi.position.Y > 768 - 160 && avionEnemi.position.Y < 768 - 150)
                                {
                                    avionEnemi.state = (int)stateMovement.down;
                                    mov.setSpeed(avionEnemi, new Vector2(0, 2f));
                                }
                                if (!stillAlive(avionEnemi, side.bottom))
                                {
                                    avionEnemi.alive = false;
                                }
                            }
                            else
                            {
                                avionEnemi.state = (int)stateMovement.up;
                                if (avionEnemi.position.Y > 768 - 110 && avionEnemi.position.Y < 768 - 100)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(0, -2));
                                }
                                if (avionEnemi.position.Y > 768 - 160 && avionEnemi.position.Y < 768 - 150)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                    hitChances = 3;
                                }
                            }




                        }
                    }
                }

            }
            //*/
            #endregion

            #region 13
            //*13  ATTAQUE CROISE DE HAUT
            if (allowNext[12])
            {
                if (!seqStop[12])
                {
                    if (timer > 77000 && !seq[12])
                    {
                        allowNext[13] = true;
                        createListAvionEnemi(2, 10, 5);
                        aseq.attackTopMiddle1(tabAvionEnemi[2]);
                        seq[12] = true;
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            avionEnemi.state = (int)stateMovement.down;
                        }
                    }
                    if (seq[12] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        seqStop[12] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 155)
                            {
                                float rotation = 29 * MathHelper.Pi / 32;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 9);
                            }

                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }

            //*/

            #endregion

            #region 14
            //*14  ATTAQUE CROISE DE BAS
            if (allowNext[13])
            {
                if (!seqStop[13])
                {
                    if (timer > 85000 && !seq[13])
                    {
                        allowNext[14] = true;
                        createListAvionEnemi(3, 10, 5);
                        aseq.attackBottomMiddle1(tabAvionEnemi[3]);
                        seq[13] = true;
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[3])
                        {
                            avionEnemi.state = (int)stateMovement.up;
                        }
                    }
                    if (seq[13] && isSeqDead(tabAvionEnemi[3]))
                    {
                        tabAvionEnemi[3].Clear();
                        seqStop[13] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[3])
                        {
                            if (avionEnemi.position.Y > 768 - 155 && avionEnemi.position.Y < 768 - 150)
                            {
                                float rotation = 35 * MathHelper.Pi / 32;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 9);
                            }

                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }

            //*/

            #endregion

            #region 15
            //*15  AVION HAUT GAUCHE
            if (allowNext[14])
            {
                if (!seqStop[14])
                {
                    if (timer > 93000 && !seq[14])
                    {
                        allowNext[15] = true;
                        createListAvionEnemi(0, 2, 5);
                        aseq.attackTopLeft2(tabAvionEnemi[0]);
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            avionEnemi.changeSprite("Sprites\\avionEnemy5bis");
                        }
                        seq[14] = true;
                    }
                    if (seq[14] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[14] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            
                            if (!stillAlive(avionEnemi, side.right))
                            {
                                avionEnemi.alive = false;
                            }
                           

                        }
                    }
                }

            }
            //*/
            #endregion

            #region 16
            //*15  AVION HAUT GAUCHE 2
            if (allowNext[15])
            {
                if (!seqStop[15])
                {
                    if (timer > 96000 && !seq[15])
                    {
                        allowNext[16] = true;
                        createListAvionEnemi(1, 2, 5);
                        aseq.attackTopLeft3(tabAvionEnemi[1]);
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            avionEnemi.changeSprite("Sprites\\avionEnemy5bis");
                        }
                        seq[15] = true;
                    }
                    if (seq[15] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[15] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {

                            if (!stillAlive(avionEnemi, side.right))
                            {
                                avionEnemi.alive = false;
                            }


                        }
                    }
                }

            }
            //*/
            #endregion

            #region 17
            //*17  AVION HAUT GAUCHE 3
            if (allowNext[16])
            {
                if (!seqStop[16])
                {
                    if (timer > 99000 && !seq[16])
                    {
                        allowNext[17] = true;
                        createListAvionEnemi(2, 3, 5);
                        aseq.attackBottomLeft2(tabAvionEnemi[2]);
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            avionEnemi.changeSprite("Sprites\\avionEnemy5bis");
                        }
                        seq[16] = true;
                    }
                    if (seq[16] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        seqStop[16] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {

                            if (!stillAlive(avionEnemi, side.right))
                            {
                                avionEnemi.alive = false;
                            }


                        }
                    }
                }

            }
            //*/
            #endregion

            #region 18
            //*18  AVION HAUT GAUCHE 4
            if (allowNext[17])
            {
                if (!seqStop[17])
                {
                    if (timer > 102000 && !seq[17])
                    {
                        allowNext[15] = true;
                        createListAvionEnemi(3, 3, 5);
                        aseq.attackBottomLeft3(tabAvionEnemi[3]);
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[3])
                        {
                            avionEnemi.changeSprite("Sprites\\avionEnemy5bis");
                        }
                        seq[17] = true;
                    }
                    if (seq[17] && isSeqDead(tabAvionEnemi[3]))
                    {
                        tabAvionEnemi[3].Clear();
                        seqStop[17] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[3])
                        {

                            if (!stillAlive(avionEnemi, side.right))
                            {
                                avionEnemi.alive = false;
                            }


                        }
                    }
                }

            }
            //*/
            #endregion

            #region 19
            //*19  AVION IMMOBILE EN HAUT
            if (allowNext[0])
            {
                if (!seqStop[18])
                {
                    if (timer > 106000 && !seq[18])
                    {
                        timeToLive = timer + 12000;
                        allowNext[19] = true;
                        createListAvionEnemi(0, 1, 3);
                        aseq.attackTopMiddle1(tabAvionEnemi[0]);
                        seq[18] = true;
                    }
                    if (seq[18] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[18] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            if (timer > timeToLive)
                            {

                                if (avionEnemi.position.Y > 100 && avionEnemi.position.Y < 110)
                                {
                                    mov.changeSpeed(avionEnemi, 2f);
                                }
                                if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 160)
                                {
                                    avionEnemi.state = (int)stateMovement.up;
                                    mov.setSpeed(avionEnemi, new Vector2(0, -2f));
                                }
                                if (!stillAlive(avionEnemi, side.top))
                                {
                                    avionEnemi.alive = false;
                                }
                            }
                            else
                            {
                                avionEnemi.state = (int)stateMovement.down;
                                if (avionEnemi.position.Y > 100 && avionEnemi.position.Y < 110)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(0, 2));
                                }
                                if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 160)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                }
                            }


                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }



                        }
                    }
                }

            }
            //*/
            #endregion

            #region 20
            //*20  AVION IMMOBLE EN BAS
            if (allowNext[19])
            {
                if (!seqStop[19])
                {
                    if (timer > 106000 && !seq[19])
                    {
                        timeToLive2 = timer + 12000;
                        allowNext[20] = true;
                        createListAvionEnemi(1, 1, 3);
                        aseq.attackBottomMiddle1(tabAvionEnemi[1]);
                        seq[19] = true;
                    }
                    if (seq[19] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[19] = true;
                        hitChances = 0;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            if (timer > timeToLive2)
                            {

                                if (avionEnemi.position.Y > 768 - 110 && avionEnemi.position.Y < 768 - 100)
                                {
                                    mov.changeSpeed(avionEnemi, 2f);
                                }
                                if (avionEnemi.position.Y > 768 - 160 && avionEnemi.position.Y < 768 - 150)
                                {
                                    avionEnemi.state = (int)stateMovement.down;
                                    mov.setSpeed(avionEnemi, new Vector2(0, 2f));
                                }
                                if (!stillAlive(avionEnemi, side.bottom))
                                {
                                    avionEnemi.alive = false;
                                }
                            }
                            else
                            {
                                avionEnemi.state = (int)stateMovement.up;
                                if (avionEnemi.position.Y > 768 - 110 && avionEnemi.position.Y < 768 - 100)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(0, -2));
                                }
                                if (avionEnemi.position.Y > 768 - 160 && avionEnemi.position.Y < 768 - 150)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                }
                            }




                        }
                    }
                }

            }
            //*/
            #endregion

            #region 21
            //*21   HAUT BAS HAUT BAS PETIT AVION

            if (allowNext[20])
            {
                if (!seqStop[20])
                {
                    if (timer > 107000 & !seq[20])
                    {
                        createListAvionEnemi(2, 8, 2);
                        aseq.attackBottomRight1(tabAvionEnemi[2]);
                        seq[20] = true;
                    }
                    if (seq[20] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        seqStop[20] = true;
                        allowNext[21] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            if (avionEnemi.position.X < 1024 && avionEnemi.position.X > 1012)
                            {
                                float rotation = 11 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 12);
                                avionEnemi.state = (int)stateMovement.up;
                            }

                            if (avionEnemi.position.Y < 150)
                            {
                                float rotation = 5 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 8);
                                avionEnemi.state = (int)stateMovement.down;
                            }
                            if (avionEnemi.position.Y > 600)
                            {
                                float rotation = 11 * MathHelper.Pi / 8;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 12);
                                avionEnemi.state = (int)stateMovement.up;
                            }
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }

            //*/

            #endregion

            #region BOSS
            //*22 BOSS
            if (allowNext[21])
            {
                if(!seqStop[21])
                {
                    if (timer > 122000 && !seq[21])
                    {
                        this.bossFight = true;
                        this.bossInComing = true;
                        timerSeq = 0;
                        allowNext[22] = true;
                        createBoss(1);
                        aseq.bossIncoming1(boss);
                        seq[21] = true;
                    }
                    if (bossFight)
                    {
                        if (boss.alive)
                        {
                            boss.position += boss.velocity;
                            if (boss.position.X == 800 && positionStopedRight == false && move2 == false) //arret a droite
                            {
                                mov.changeSpeed(boss, 0);
                                timerMissile2 = 0;
                                positionStopedRight = true;
                                timerMove1 = 0;
                            }
                            if (timerMissile3 > 400 && positionStopedRight == true) //tirer missile 3
                            {
                                foreach (Missile missile in boss.missile3)
                                {
                                    if (!missile.alive)
                                    {
                                        missile.alive = true;
                                        int k = random.Next(77, 110);

                                        float rotation = k * MathHelper.Pi / 80;
                                        Vector2 angle = new Vector2(
                                            (float)Math.Cos(rotation),
                                            (float)Math.Sin(rotation));

                                        missile.velocity = angle * 5f;

                                        missile.position = new Vector2(boss.position.X, boss.position.Y + boss.height);
                                        timerMissile3 = 0f;
                                        return;
                                    }

                                }
                            }
                            if (timerMissile2 > 3000 && positionStopedRight == true) //tirer missile 2
                            {
                                foreach (Missile missile in boss.missile2)
                                {
                                    if (!missile.alive)
                                    {
                                        double newX;
                                        double newY;
                                        double hypothenuse;
                                        Vector2 directionHero;

                                        missile.position = new Vector2(boss.position.X + boss.center.X, boss.position.Y + boss.center.Y);
                                        directionHero = target - missile.position;
                                        boss.targetHero = directionHero;

                                        missile.alive = true;

                                        hypothenuse = Math.Sqrt(
                                        (directionHero.X * directionHero.X) + (directionHero.Y * directionHero.Y));
                                        newX = (directionHero.X * 20) / hypothenuse;
                                        newY = (directionHero.Y * 20) / hypothenuse;
                                        missile.velocity = new Vector2((float)newX, (float)newY);


                                        timerMissile2 = 0f;
                                        return;
                                    }

                                }
                            }

                            if (timerMissile1 > 5000 && positionStopedRight == true) //tirer missile 1
                            {
                                foreach (Missile missile in boss.missile1)
                                {
                                    if (!missile.alive)
                                    {
                                        readyToExplode = true;
                                        missile.alive = true;
                                        int k = random.Next(55, 81);

                                        float rotation = k * MathHelper.Pi / 80;
                                        Vector2 angle = new Vector2(
                                            (float)Math.Cos(rotation),
                                            (float)Math.Sin(rotation));

                                        int vitesse = random.Next(1, 5);
                                        missile.velocity = angle * vitesse;

                                        missile.position = new Vector2(boss.position.X, boss.position.Y + 10);
                                        timerMissile1 = 0f;
                                        timerMissile1Explode = 0f;
                                    }
                                }
                            }
                            if (readyToExplode)
                            {
                                if (timerMissile1Explode > 2000)
                                {
                                    foreach (Missile missile in boss.missile1)
                                    {
                                        missile.alive = false;
                                        foreach (Missile missile3 in boss.missile3)
                                        {
                                            if (!missile3.alive)
                                            {
                                                missile3.alive = true;
                                                missile3.position = missile.position + missile.center - missile3.center;
                                                missile3.velocity = new Vector2(
                                                    (float)Math.Cos(cercle) * 5,
                                                    (float)Math.Sin(cercle) * 5);
                                                cercle += 2 * MathHelper.Pi / 20;
                                                cycle++;
                                                if (cycle == 20)
                                                {
                                                    cycle = 0;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    readyToExplode = false;
                                }
                            }

                            if (timerMove1 > 16000 && positionStopedRight == true)
                            {
                                positionStopedRight = false;
                                move1 = true;
                                timerMove1 = 0;
                                boss.position += new Vector2(-1, 0); // car si il est en x=800, il va s'arreter
                                boss.velocity = new Vector2(-3, -7);
                                boss.state = (int)stateMovement.forward;
                            }
                            if (boss.position.X <= 50 && move1 == true)
                            {
                                mov.changeSpeed(boss, 0);
                            }
                            if (move1)
                            {
                                if (boss.position.X > 50)
                                    boss.velocity += new Vector2(0, 0.05f);
                                else
                                {
                                    if (boss.position.Y >= 80 + ((768 - 80) / 2) - (boss.height / 2))
                                    {
                                        mov.changeSpeed(boss, 0);
                                        move1 = false;
                                        timerMove2 = 0;
                                        positionStopedLeft = true;
                                        stopBoucle = false;
                                        timerMissile4 = 0;
                                        boss.state = (int)stateMovement.none;
                                    }
                                    else
                                    {
                                        boss.velocity = new Vector2(0, 3);
                                    }
                                }
                            }

                            // mouvement de retour
                            if (timerMove2 > 8000 && positionStopedLeft == true)
                            {
                                if (finBoucle)
                                {
                                    stopBoucle = true;
                                    boss.velocity = new Vector2(0, 0);
                                }
                                if (boss.position.Y >= 194 || boss.position.Y <= 192)
                                {
                                    boss.state = (int)stateMovement.backward;
                                    boss.invincible = true;
                                    if (boss.position.Y >= 194)
                                    {
                                        boss.position.Y += -1;
                                    }
                                    if (boss.position.Y <= 192)
                                    {
                                        boss.position.Y += 1;
                                    }
                                }
                                else
                                {
                                    if (timerMove2 > 12000)
                                    {
                                        positionStopedLeft = false;
                                        move2 = true;
                                        timerMove2 = 0;
                                        boss.velocity = new Vector2(3, 7);
                                    }
                                }



                            }
                            if (boss.position.X >= 800 && move2 == true)
                            {
                                mov.changeSpeed(boss, 0);

                            }
                            if (move2)
                            {
                                if (boss.position.X <= 800)
                                    boss.velocity += new Vector2(0, -0.05f);
                                else
                                {
                                    if (boss.position.Y <= 80 + ((768 - 80) / 2) - (boss.height / 2))
                                    {
                                        mov.changeSpeed(boss, 0);
                                        move2 = false;
                                        timerMove2 = 0;
                                        positionStopedRight = true;
                                        timerMove1 = 0;
                                        boss.state = (int)stateMovement.none;
                                        stopBoucle = false;
                                    }
                                    else
                                    {
                                        boss.velocity = new Vector2(0, -3);
                                    }
                                }
                            }
                            if (positionStopedLeft)
                            {
                                if (!stopBoucle)
                                {
                                    if (timerMove2 > 1000)
                                    {
                                        boss.invincible = false;
                                        if (finBoucle)
                                        {
                                            if (horslimite == 2)
                                            {
                                                rand = 2;
                                            }
                                            else if (horslimite == -2)
                                            {
                                                rand = 1;
                                            }
                                            else
                                            {
                                                rand = random.Next(1, 3);
                                                finBoucle = false;
                                            }
                                        }
                                        if (rand == 1)
                                        {
                                            if (boucle > 0 && boucle < 120)
                                            {
                                                boucle++;
                                            }
                                            else if (boucle == 0)
                                            {
                                                boss.velocity = new Vector2(0, -1);
                                                boucle++;
                                            }
                                            else if (boucle == 120)
                                            {
                                                horslimite++;
                                                boucle = 0;
                                                finBoucle = true;
                                            }
                                        }
                                        if (rand == 2)
                                        {
                                            if (boucle > 0 && boucle < 120)
                                            {
                                                boucle++;
                                            }
                                            else if (boucle == 0)
                                            {
                                                boss.velocity = new Vector2(0, 1);
                                                boucle++;
                                            }
                                            else if (boucle == 120)
                                            {
                                                horslimite--;
                                                boucle = 0;
                                                finBoucle = true;
                                            }
                                        }

                                        while (currentMissile4 <= missile4Max)
                                        {
                                            foreach (Missile missile in boss.missile4)
                                            {
                                                if (!missile.alive)
                                                {
                                                    missile.alive = true;
                                                    missile.velocity = new Vector2(14, 0);
                                                    switch (currentMissile4)
                                                    {
                                                        case 1:
                                                            missile.position = boss.position + new Vector2(85, 17);
                                                            break;
                                                        case 2:
                                                            missile.position = boss.position + new Vector2(102, 46);
                                                            break;
                                                        case 3:
                                                            missile.position = boss.position + new Vector2(124, 75);
                                                            break;
                                                        case 4:
                                                            missile.position = boss.position + new Vector2(140, 104);
                                                            break;
                                                        case 5:
                                                            missile.position = boss.position + new Vector2(153, 134);
                                                            break;
                                                        case 6:
                                                            missile.position = boss.position + new Vector2(164, 165);
                                                            break;
                                                        case 7:
                                                            missile.position = boss.position + new Vector2(164, 281);
                                                            break;
                                                        case 8:
                                                            missile.position = boss.position + new Vector2(153, 312);
                                                            break;
                                                        case 9:
                                                            missile.position = boss.position + new Vector2(140, 342);
                                                            break;
                                                        case 10:
                                                            missile.position = boss.position + new Vector2(124, 371);
                                                            break;
                                                        case 11:
                                                            missile.position = boss.position + new Vector2(102, 400);
                                                            break;
                                                        case 12:
                                                            missile.position = boss.position + new Vector2(85, 429);
                                                            break;
                                                    }
                                                    currentMissile4++;
                                                    break;

                                                }
                                            }
                                        }

                                        if (timerMissile4 > 100)
                                        {
                                            currentMissile4 = 1;
                                            timerMissile4 = 0;
                                        }

                                    }
                                }
                            }
                        }
    
                        
                    }

                }
            }
            //*/
            #endregion

        }

        public void createListAvionEnemi(int index, int nombre)
        {
            for (int i = 0; i < nombre; i++)
            {
                AvionEnemi temp = new AvionEnemi(contentManager);
                tabAvionEnemi[index].Add(temp);
            }
            
        }

        public void createListAvionEnemi(int index, int nombre, int type)
        {
            tabAvionEnemi[index].Clear();
            for (int i = 0; i < nombre; i++)
            {
                AvionEnemi temp = new AvionEnemi(contentManager, type);
                temp.alive = true;
                tabAvionEnemi[index].Add(temp);
            }

        }

        public void createBoss(int type)
        {
            boss = new Boss(this.contentManager, type);
            boss.alive = true;
        }

        public void addToList(List<AvionEnemi> avionEnemi)
        {

        }

        public void deleteFromList(int index)
        {

        }

        public bool isSeqDead(List<AvionEnemi> avionEnemies)
        {
            bool seqdead = true;
            foreach (AvionEnemi avionEnemi in avionEnemies)
            {
                foreach (Missile missile in avionEnemi.missile)
                {
                    if (missile.alive)
                    {
                        seqdead = false;
                    }
                }

                if (avionEnemi.alive)
                {
                    avionEnemi.position += avionEnemi.velocity;

                    seqdead = false; //si il reste un enemi alive, le bool redevient a "non terminé"
                }
            }
            return seqdead;

        }

        public bool stillAlive(AvionEnemi avionEnemi, side s)
        {
            bool stillAlive = true;
            if (avionEnemi.alive == true)
            {
                if (s == side.top)
                {
                    if (avionEnemi.position.Y < -avionEnemi.height)
                    {
                        stillAlive = false;
                    }
                }
                if (s == side.right)
                {
                    if (avionEnemi.position.X > 1024)
                    {
                        stillAlive = false;
                    }
                }
                if (s == side.bottom)
                {
                    if (avionEnemi.position.Y > 768)
                    {
                        stillAlive = false;
                    }
                }
                if (s == side.left)
                {
                    if (avionEnemi.position.X < -avionEnemi.width)
                    {
                        stillAlive = false;
                    }
                }
            }
            return stillAlive;
        }

        public override void updateTimer()
        {
            timerMissile1 += timerSeq;
            timerMissile1Explode += timerSeq;
            timerMissile2 += timerSeq;
            timerMissile3 += timerSeq;
            timerMissile4 += timerSeq;
            timerMove1 += timerSeq;
            timerMove2 += timerSeq;
        }

        public override void resetTimer()
        {
            timer = 0f;
            timerSeq = 0f;
            timerMissile1 = 0f;
            timerMissile1Explode = 0f;
            timerMissile2 = 0f;
            timerMissile3 = 0f;
            timerMissile4 = 0f;
            timerMove1 = 0f;
            timerMove2 = 0f;
        
        }

    }

    
}
