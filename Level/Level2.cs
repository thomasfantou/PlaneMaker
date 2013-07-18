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
    class Level2 : GeneralLevel
    {
        AttackSequence aseq;
        ContentManager contentManager;
        Mouvement mov;

        private const int maxList = 5;

        public const int numberOfSequence = 50;
        public bool[] seq; // verifie si la sequence a été activé
        public bool[] seqdead; // verifie si la sequence est terminé
        public bool[] allowNext; // donne la possibilité a la séquence d'aprés d'être lu
        public bool[] seqStop; //arrete completement la lecture

        //enumération permetant de définir le mouvement de l'avion avec des mot plutot que des nombres
        public enum stateMovement { none, down, up, forward, backward };
        public enum side { top, right, bottom, left };

        public Level2(ContentManager content)
        {
            resetTimer();
            getLevel = 2;
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
            allowNext[0] = true;

        }

        #region variable de update
        bool check61 = false;
        bool check62 = false;
        bool check63 = false;
        float timeToLive;
        float timeToLive2;
        float timeToLive3;

        #endregion

        #region var Boss
        bool incoming = true;
        bool normalActing = false;
        bool missileActing = false;
        bool ecraseActing = false;
        bool break1 = false;
        bool break2 = false;
        private float timerMissile1;
        private float timerMissile2;
        private float timerMissile3;
        private float timerMissile4;
        private float timerMove1;
        private float timerMove2;
        private float timerBreak1;
        private float timerBreak2;
        Random random = new Random();
        #endregion

        public override void update()
        {
            base.avionEnemies = avionEnemies;

            #region Sequence 1
            //*1 Tank en bas, stop puis repars vers le bas
            //*
            if (allowNext[0])
            {
                if (!seqStop[0])
                {
                    if (timer > 2000 && !seq[0])
                    {
                        timeToLive = timer + 8500;
                        allowNext[1] = true;
                        createListAvionEnemi(0, 1, 201);
                        createListAvionEnemi(2, 9, 101);
                        aseq.attackBottomRight101(tabAvionEnemi[0], 650);
                        aseq.attackTopRight1(tabAvionEnemi[2], 150);
                        seq[0] = true;
                    }
                    if (seq[0] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        check61 = true;
                    }
                    if (seq[0] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        check62 = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi1 in tabAvionEnemi[2])
                        {
                            if (!stillAlive(avionEnemi1, side.left))
                            {
                                avionEnemi1.alive = false;
                            }
                        }
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[0])
                        {
                            if (timer > timeToLive)
                            {

                                if (avionEnemi.position.X > 500 && avionEnemi.position.X < 510)
                                {
                                    mov.changeSpeed(avionEnemi, -2f);
                                }
                                if (avionEnemi.position.X > 550 && avionEnemi.position.X < 560)
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
                                avionEnemi.state = (int)stateMovement.none;
                                if (avionEnemi.position.X > 500 && avionEnemi.position.X < 510)
                                {
                                    mov.setSpeed(avionEnemi, new Vector2(0, 0));
                                    hitChances = 2;
                                }
                                if (avionEnemi.position.X > 550 && avionEnemi.position.X < 560)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                    hitChances = 4;
                                }
                            }
                            if (check61 && check62)
                            {
                                seqStop[0] = true;
                            }
                        }
                    }
                }
            }
            //*/
            #endregion

            #region Sequence 2
            //* avion de haut en bas
            if (allowNext[1])
            {
                if (!seqStop[1])
                {
                    if (timer > 12000 & !seq[1])
                    {
                        allowNext[2] = true;
                        createListAvionEnemi(1, 4, 102);
                        aseq.attackBottomRight102(tabAvionEnemi[1]);
                        seq[1] = true;
                    }
                    if (seq[1] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[1] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            if (avionEnemi.position.X < 1024 && avionEnemi.position.X > 1012)
                            {
                                float rotation = 11 * MathHelper.Pi / 7;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 18);
                                avionEnemi.state = (int)stateMovement.up;
                            }

                            if (avionEnemi.position.Y < 150)
                            {
                                float rotation = 5 * MathHelper.Pi / 7;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 8);
                                avionEnemi.state = (int)stateMovement.down;
                            }
                            if (avionEnemi.position.Y > 600)
                            {
                                float rotation = 11 * MathHelper.Pi / 7;
                                Vector2 angle = new Vector2(
                                    (float)Math.Cos(rotation),
                                    (float)Math.Sin(rotation));

                                mov.changeTrajectoire(avionEnemi, angle, 18);
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

            #region Sequence 3
            //Tank en bas, invincible
            if (allowNext[2])
            {
                if (!seqStop[2])
                {
                    if (timer > 25000 & !seq[2])
                    {
                        allowNext[3] = true;
                        createListAvionEnemi(0, 7, 202);
                        aseq.attackBottomRight101(tabAvionEnemi[0], 650);
                        seq[2] = true;
                    }
                    if (seq[2] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[2] = true;
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
            #endregion

            #region Sequence 4
            if (allowNext[3])
            {
                if (!seqStop[3])
                {
                    if (timer > 37000 && !seq[3])
                    {
                        timeToLive2 = timer + 9000;
                        allowNext[4] = true;
                        createListAvionEnemi(1, 1, 103);
                        aseq.attackMiddleRight101(tabAvionEnemi[1]);
                        seq[3] = true;
                    }
                    if (seq[3] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[3] = true;
                        hitChances = 1;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
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
            #endregion

            #region Sequence 5
            if (allowNext[4])
            {
                if (!seqStop[4])
                {
                    if (timer > 45000 && !seq[4])
                    {
                        allowNext[5] = true;
                        createListAvionEnemi(0, 6, 105);
                        createListAvionEnemi(2, 6, 105);
                        aseq.attackTopRight102(tabAvionEnemi[0]);
                        aseq.attackBottomRight103(tabAvionEnemi[2]);
                        seq[4] = true;
                    }
                    if (seq[4] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        check61 = true;
                    }
                    if (seq[4] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        check62 = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi1 in tabAvionEnemi[2])
                        {
                            if (!stillAlive(avionEnemi1, side.left))
                            {
                                avionEnemi1.alive = false;
                            }
                        }
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
            #endregion

            #region Sequence 6
            //*9  EN BAS A GAUCHE ARC DE CERCLE
            if (allowNext[5])
            {
                if (!seqStop[5])
                {
                    if (timer > 55000 && !seq[5])
                    {
                        allowNext[6] = true;
                        createListAvionEnemi(1, 6, 104);
                        aseq.attackTopLeft1(tabAvionEnemi[1]);
                        seq[5] = true;
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            avionEnemi.changeSprite("Sprites\\avionEnemy4bis");
                            hitChances = 0.8f;
                        }
                    }
                    if (seq[5] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[5] = true;

                        hitChances = 4;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            if (avionEnemi.position.X > 450)
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
                            if (avionEnemi.position.X < 450 && avionEnemi.velocity.X < 0)
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

            #region Sequence 7
            if (allowNext[6])
            {
                if (!seqStop[6])
                {
                    if (timer > 70000 & !seq[6])
                    {
                        allowNext[7] = true;
                        createListAvionEnemi(4, 5, 105);
                        aseq.attackBottomLeft101(tabAvionEnemi[4]);
                        seq[6] = true;
                    }
                    if (seq[6] && isSeqDead(tabAvionEnemi[4]))
                    {
                        tabAvionEnemi[4].Clear();
                        seqStop[6] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[4])
                        {
                            if (!stillAlive(avionEnemi, side.right))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Sequence 8
            //Deux helico fixes plus file avion
            if (allowNext[7])
            {
                if (!seqStop[7])
                {
                    if (timer > 82000 && !seq[7])
                    {
                        timeToLive = timer + 8500;
                        timeToLive2 = timer + 8500;
                        timeToLive3 = timer + 9000;
                        allowNext[8] = true;
                        createListAvionEnemi(3, 1, 103);
                        aseq.attackTopMiddle1(tabAvionEnemi[3]);

                        createListAvionEnemi(2, 9, 301);
                        aseq.attackMiddleRight1(tabAvionEnemi[2]);

                        createListAvionEnemi(1, 1, 103);
                        aseq.attackBottomMiddle1(tabAvionEnemi[1]);

                        seq[7] = true;
                    }
                    if (seq[7] && isSeqDead(tabAvionEnemi[3]))
                    {
                        tabAvionEnemi[3].Clear();
                        check63 = true;
                        hitChances = 1;
                    }
                    if (seq[7] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        check62 = true;
                        hitChances = 1;
                    }
                    if (seq[7] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        check61 = true;
                        hitChances = 1;
                    }
                    else
                    {
                        #region foreach tabAvion[3]
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
                                    mov.setSpeed(avionEnemi, new Vector2(0, 2));
                                }
                                if (avionEnemi.position.Y > 150 && avionEnemi.position.Y < 160)
                                {
                                    mov.changeSpeed(avionEnemi, 0);
                                    avionEnemi.state = (int)stateMovement.none;
                                    hitChances = 4;
                                }
                            }
                            if (!stillAlive(avionEnemi, side.top))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                        #endregion

                        #region foreach tabAvion[2]
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
                                if (!stillAlive(avionEnemi, side.left))
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
                                    hitChances = 4;
                                }
                            }
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                        #endregion

                        #region foreach tabAvion[1]
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
                                    hitChances = 4;
                                }
                            }
                            if (check61 && check62 && check63)
                            {
                                seqStop[7] = true;
                            }
                        }
                        #endregion
                    }
                }

            }
            #endregion

            #region Sequence 9
            //Mur d'avion
            if (allowNext[8])
            {
                if (!seqStop[8])
                {
                    if (timer > 94000 && !seq[8])
                    {
                        allowNext[9] = true;
                        createListAvionEnemi(0, 6, 104);
                        aseq.attackRight101(tabAvionEnemi[0]);
                        seq[8] = true;
                    }
                    if (seq[8] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[8] = true;
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
            #endregion

            #region Sequence 10
            //Mur d'avion
            if (allowNext[9])
            {
                if (!seqStop[9])
                {
                    if (timer > 104000 && !seq[9])
                    {
                        allowNext[10] = true;
                        createListAvionEnemi(1, 6, 105);
                        aseq.attackRight102(tabAvionEnemi[1]);
                        seq[9] = true;
                    }
                    if (seq[9] && isSeqDead(tabAvionEnemi[1]))
                    {
                        tabAvionEnemi[1].Clear();
                        seqStop[9] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[1])
                        {
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Sequence 11
            // Mur d'avion
            if (allowNext[10])
            {
                if (!seqStop[10])
                {
                    if (timer > 118000 && !seq[10])
                    {
                        allowNext[11] = true;
                        createListAvionEnemi(2, 6, 104);
                        aseq.attackRight103(tabAvionEnemi[2]);
                        seq[10] = true;
                        hitChances = 3;
                    }
                    if (seq[10] && isSeqDead(tabAvionEnemi[2]))
                    {
                        tabAvionEnemi[2].Clear();
                        seqStop[10] = true;
                    }
                    else
                    {
                        foreach (AvionEnemi avionEnemi in tabAvionEnemi[2])
                        {
                            if (!stillAlive(avionEnemi, side.left))
                            {
                                avionEnemi.alive = false;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Sequence 12
            //Attaque en V
            if (allowNext[11])
            {
                if (!seqStop[11])
                {
                    if (timer > 125000 && !seq[11])
                    {
                        allowNext[12] = true;
                        createListAvionEnemi(0, 9, 105);
                        aseq.attackRight104(tabAvionEnemi[0]);
                        seq[11] = true;
                    }
                    if (seq[11] && isSeqDead(tabAvionEnemi[0]))
                    {
                        tabAvionEnemi[0].Clear();
                        seqStop[11] = true;
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
            #endregion

            #region Boss



            if (allowNext[12])
            {
                if (!seqStop[12])
                {
                    if (timer > 135000 && !seq[12])
                    {
                        this.bossFight = true;
                        this.bossInComing = true;
                        timerSeq = 0;
                        createBoss(2);
                        aseq.bossIncoming2(boss);
                        seq[12] = true;
                    }
                    if (bossFight)
                    {
                        if (boss.alive)
                        {
                            boss.position += boss.velocity;
                            boss.currentHealthPoint -= 1;
                            if (incoming)
                            {
                                if (boss.position.X <= 750)
                                {
                                    incoming = false;
                                    normalActing = true;
                                    timerZero();
                                }
                            }
                            else
                            {
                                if (boss.position.X <= 750)
                                {
                                    boss.velocity.X = 0.5f;
                                }
                                if (boss.position.X >= 800)
                                {
                                    boss.velocity.X = -0.5f;
                                }

                                #region normal acting
                                if (normalActing)
                                {
                                    if (timerMove1 >= 5000)
                                    {
                                        normalActing = false;
                                        break1 = true;
                                        timerZero();
                                    }
                                    if (timerMissile1 > 50) //tirer missile 1 au milieu
                                    {
                                        foreach (Missile missile in boss.missile1)
                                        {
                                            if (!missile.alive)
                                            {
                                                missile.alive = true;
                                                int k = random.Next(50, 80);

                                                float rotation = k * MathHelper.Pi / 80;
                                                Vector2 angle = new Vector2(
                                                    (float)Math.Cos(rotation),
                                                    (float)Math.Sin(rotation));

                                                missile.velocity = angle * 5f;

                                                missile.position = new Vector2(boss.position.X + 20, boss.position.Y + boss.height / 2);
                                                timerMissile1 = 0f;
                                                break;
                                            }

                                        }
                                        foreach (Missile missile in boss.missile1)
                                        {
                                            if (!missile.alive)
                                            {
                                                missile.alive = true;
                                                int k = random.Next(80, 110);

                                                float rotation = k * MathHelper.Pi / 80;
                                                Vector2 angle = new Vector2(
                                                    (float)Math.Cos(rotation),
                                                    (float)Math.Sin(rotation));

                                                missile.velocity = angle * 5f;

                                                missile.position = new Vector2(boss.position.X + 20, boss.position.Y + boss.height / 2);
                                                timerMissile1 = 0f;
                                                break;
                                            }

                                        }
                                    }
                                    if (timerMissile2 > 100) //tirer missile 2 en bas
                                    {
                                        foreach (Missile missile in boss.missile2)
                                        {
                                            if (!missile.alive)
                                            {
                                                missile.alive = true;
                                                int k = random.Next(5, 30);

                                                float rotation = k * MathHelper.Pi / 80;
                                                Vector2 angle = new Vector2(
                                                    (float)Math.Cos(rotation),
                                                    (float)Math.Sin(rotation));

                                                missile.velocity = angle * 5f;

                                                missile.position = new Vector2(boss.position.X + 50, boss.position.Y + boss.height - 10);
                                                timerMissile2 = 0f;
                                                return;
                                            }

                                        }
                                    }
                                    if (timerMissile3 > 100) //tirer missile 3 en haut
                                    {
                                        foreach (Missile missile in boss.missile3)
                                        {
                                            if (!missile.alive)
                                            {
                                                missile.alive = true;
                                                int k = random.Next(90, 165);

                                                float rotation = k * MathHelper.Pi / 80;
                                                Vector2 angle = new Vector2(
                                                    (float)Math.Cos(rotation),
                                                    (float)Math.Sin(rotation));

                                                missile.velocity = angle * 5f;

                                                missile.position = new Vector2(boss.position.X + 80, boss.position.Y);
                                                timerMissile3 = 0f;
                                                return;
                                            }

                                        }
                                    }
                                }
                                #endregion

                                #region break1
                                if (break1)
                                {
                                    if (timerBreak1 >= 2500)
                                    {
                                        break1 = false;
                                        missileActing = true;
                                        timerZero();
                                        timerMissile4 = 0;
                                    }

                                }
                                #endregion

                                #region missile acting
                                if (missileActing)
                                {
                                    if (timerMove2 >= 6000)
                                    {
                                        missileActing = false;
                                        break2 = true;
                                        timerZero();
                                    }
                                    if (timerMissile4 >= 50)
                                    {
                                        foreach (Missile missile in boss.missile4)
                                        {
                                            if (!missile.alive)
                                            {
                                                missile.alive = true;
                                                int k = random.Next(35, 70);

                                                float rotation = k * MathHelper.Pi / 80;
                                                Vector2 angle = new Vector2(
                                                    (float)Math.Cos(rotation),
                                                    (float)Math.Sin(rotation));

                                                missile.velocity = angle * 1f;

                                                missile.position = new Vector2(boss.position.X + 20, boss.position.Y + boss.height / 2);
                                                timerMissile4 = 0f;
                                                break;
                                            }

                                        }
                                        foreach (Missile missile in boss.missile4)
                                        {
                                            if (!missile.alive)
                                            {
                                                missile.alive = true;
                                                int k = random.Next(70, 110);

                                                float rotation = k * MathHelper.Pi / 80;
                                                Vector2 angle = new Vector2(
                                                    (float)Math.Cos(rotation),
                                                    (float)Math.Sin(rotation));

                                                missile.velocity = angle * 1f;

                                                missile.position = new Vector2(boss.position.X + 20, boss.position.Y + boss.height / 2);
                                                timerMissile4 = 0f;
                                                break;
                                            }

                                        }
                                    }
                                    foreach (Missile missile in boss.missile4)
                                    {
                                        if(missile.alive)
                                            missile.velocity *= 1.02f;
                                    }
                                    if (timerMissile3 > 100) //tirer missile 4 en haut
                                    {
                                        foreach (Missile missile in boss.missile4)
                                        {
                                            if (!missile.alive)
                                            {
                                                missile.alive = true;
                                                int k = random.Next(90, 165);

                                                float rotation = k * MathHelper.Pi / 80;
                                                Vector2 angle = new Vector2(
                                                    (float)Math.Cos(rotation),
                                                    (float)Math.Sin(rotation));

                                                missile.velocity = angle * 5f;

                                                missile.position = new Vector2(boss.position.X + 80, boss.position.Y);
                                                timerMissile3 = 0f;
                                                return;
                                            }

                                        }
                                    }
                                  

                                }
                                #endregion

                                #region break 2
                                if (break2)
                                {
                                    if (timerBreak2 >= 3000)
                                    {
                                        break2 = false;
                                        ecraseActing = true;
                                        timerZero();
                                    }
                                    foreach (Missile missile in boss.missile4)
                                    {
                                        if (missile.alive)
                                            missile.velocity *= 1.02f;
                                    }
                                }

                                #endregion

                                #region ecraser
                                if (ecraseActing)
                                {
                                    if (boss.velocity.Y == 0)
                                    {
                                        boss.velocity.Y = 3;
                                    }

                                    if (boss.height + boss.position.Y >= 764)
                                    {
                                        boss.velocity.Y = -1;
                                    }
                                    if (boss.position.Y < 80 + ((768 - 80) / 2) - (boss.height / 2) + 50)
                                    {
                                        boss.position.Y = 80 + ((768 - 80) / 2) - (boss.height / 2) + 50;
                                        boss.velocity.Y = 0;
                                        ecraseActing = false;
                                        normalActing = true;
                                    }
                                }

                                #endregion



                            }
                            
                        }

                    }

                }

            }

            #endregion
        }

        public void createBoss(int type)
        {
            boss = new Boss(this.contentManager, type);
            boss.alive = true;
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
            timerMissile2 += timerSeq;
            timerMissile3 += timerSeq;
            timerMissile4 += timerSeq;

            timerMove1 += timerSeq;
            timerMove2 += timerSeq;
            timerBreak1 += timerSeq;
            timerBreak2 += timerSeq;
        }

        private void timerZero()
        {
            timerMove1 = 0;
            timerMove2 = 0;
            timerBreak1 = 0;
            timerBreak2 = 0;
        }

        public override void resetTimer()
        {
            timer = 0f;
            timerSeq = 0f;
            timerMissile1 = 0f;
            timerMissile2 = 0f;
            timerMissile3 = 0f;
            timerMissile4 = 0f;
            timerZero();

        }
    }
}
