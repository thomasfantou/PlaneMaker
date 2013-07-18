using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using PlaneMaker.Level;
using GameStateManagement;
using System.Xml.Linq;
using System.IO;


namespace PlaneMaker
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : GameScreen
    {
        ContentManager content;
        SpriteFont gameFont;
        SpriteBatch spriteBatch;
        Rectangle viewportRect;
        AvionHero avionHero;
        Font backgroundLvl1;
        Queue<Font> backgroundLvl2;
        Queue<Font> backgroundLvl3;
        Queue<Font> backgroundLvl4;
        Font backgroundLvl2One;
        Font backgroundLvl3One;
        Font backgroundLvl4One;
        Font thisbackgroundLvl2;
        Font thisbackgroundLvl3;
        Font thisbackgroundLvl4;

        int backgroundToUse;
        Font fontMenu;
        Texture2D heroHealthBar;
        Texture2D bossHealthBar;
        Texture2D expBar;
        Texture2D bouclieBoss;
        Random random = new Random();

        GeneralLevel currentLevel;

        KeyboardState previousKeyboardState = Keyboard.GetState();
        Missile[] missile1;
        Missile[] missile2;
        Missile[] missile3;
        Missile[] missile4;
        Missile[] missile5;
        Missile[] missile6;
        Missile[] missile7;
        Missile[] missile8;
        Missile[] missile9;
        Missile[] currentMissile;
        Missile[] temp;
        Missile currentFeu;
        Missile feu1;
        Missile feu2;
        Missile feu3;
        Missile feu4;
        Missile currentNova;
        Missile nova1;
        Missile nova2;
        Missile nova3;
        Missile nova4;
        Texture2D valveDroite;
        Texture2D valveGauche;
        Missile[] valve;
        Missile[] valve2;
        Missile[] currentUltimValve;
        Missile[] currentValve;
        Missile[] ondeUp;
        Missile[] ondeDown;

        Queue<Explosion> explosions;


        GameTime gameTime = new GameTime();

        public float currentPuissance;
        public float currentDps;
        SpriteFont lvlFont;
        SpriteFont statFont;
        SpriteFont info;
        SpriteFont infoBoss;
        Texture2D armesMissileActif;
        Texture2D armesMissileInactif;
        Texture2D armesFeuActif;
        Texture2D armesFeuInactif;
        Texture2D armesFeuNA;
        Texture2D armesNovaActif;
        Texture2D armesNovaInactif;
        Texture2D armesNovaNA;
        Texture2D armesValveActif;
        Texture2D armesValveInactif;
        Texture2D armesValveNA;
        Texture2D armesOndeActif;
        Texture2D armesOndeInactif;
        Texture2D armesOndeNA;
        Texture2D shining;
        Texture2D armeShining;

        SoundEffect soundAvionHExplosion;
        SoundEffect soundAvionHMissile;
        SoundEffect soundAvionHFeu;
        SoundEffectInstance soundAvionFeuInstance;
        SoundEffect soundAvionHNova;
        SoundEffectInstance soundAvionNovaInstance;
        SoundEffect soundAvionHValve;
        SoundEffect soundAvionHWave;
        SoundEffect soundAvionEMissile;
        SoundEffect soundImpact;
        SoundEffect soundImpactAvions;
        SoundEffect soundAvionEExplosion;
        SoundEffect soundBossShield;
        SoundEffect soundLevelUp;
        SoundEffect soundBossExplosion;

        public float novaMouvement;
        //public bool = false;
        public bool unlockFeu = false;
        public bool unlockNova = false;
        public bool unlockValve = false;
        public bool openedValve = false;
        public bool parywave = false;
        public bool unlockOnde = false;
        public bool ultimValve = false;
        public bool bossDead = false;
        public float exitToMenu = 0;
        public int deadCount = 0;
        public float timePlayed = 0f;
        public int enemiKilled = 0;
        public enum Anim
        {
            victory,
            defeat
        };
        static public bool pause = false;
        public bool avionHeroAnimation = false; 
        public Anim anim;
        public bool firstInitialized = true; // pour que l'initialisation se passe qu'une seul foie sur certaines variables
        public bool addedQueueDone2 = false;
        public bool addedQueueDone3 = false;
        public bool addedQueueDone4 = false; // pour l'états des background

        enum CursorWeapon
        {
            missile,
            feu,
            nova,
            valve,
            onde
        }
        int currentCursor;

        int mAlphaValue = 0;
        int mFadeIncrement = 18;
        int aAlphaValue = 0;
        int aFadeIncrement = 9;
        int[] armeShiningTab;

        float timerSeq = 0.0f;
        float timer = 0.0f;
        float timerMissile = 0.0f;
        const int maxMissile = 15;
        const int maxValveMissile = 30;
        const int maxOnde = 5;
        const float vitesseDeplacement = 8f;
        float vitesseMissile = 20f;
        float vitesseOnde = 3f;
        const int slowedSpeed = 3;
        const float upgradedSpeed = 0.5f;
        float intervalMissile = 120.0f;
        float intervalNova = 200.0f;
        float intervalValve = 30.0f;
        float intervalOnde = 500.0f;
        const int vitesseMissileEnemi = 8;
        float vitesseMissileAmp;
        int level;
        int expInBuffer;
        int stage;

        const float puissanceMissile1 = 5; //lvl 1
        const float puissanceMissile2 = 15; //lvl 2
        const float puissanceMissile3 = 35; //lvl 4
        const float puissanceMissile4 = 60; //lvl 7
        const float puissanceMissile5 = 85; //lvl 9
        const float puissanceMissile6 = 125; //lvl 11
        const float puissanceMissile7 = 165; //lvl 14
        const float puissanceMissile8 = 225; //lvl 17
        const float puissanceMissile9 = 275; //lvl 19
        const float puissanceFeu1 = 5f; //lvl 3
        const float puissanceFeu2 = 10f; //lvl 6
        const float puissanceFeu3 = 25f; //lvl 12
        const float puissanceFeu4 = 40f; //lvl 16
        const float puissanceNova1 = 75f; //lvl 5
        const float puissanceNova2 = 130f; //lvl 8
        const float puissanceNova3 = 175f; //lvl 13
        const float puissanceNova4 = 440f; //lvl 18
        const float puissanceValve1 = 30; //lvl 10
        const float puissanceValve2 = 60; //lvl 15
        const float puissanceValve3 = 70; //lvl 18
        const float puissanceOnde1 = 60f; //lvl 15
        const float puissanceOnde2 = 100f; //lvl 20

        /*
         const float puissanceMissile1 = 5; //lvl 1
        const float puissanceMissile2 = 15; //lvl 2
        const float puissanceFeu1 = 5f; //lvl 3
        const float puissanceMissile3 = 35; //lvl 4
        const float puissanceNova1 = 75f; //lvl 5
        const float puissanceFeu2 = 10f; //lvl 6
        const float puissanceMissile4 = 60; //lvl 7
        const float puissanceNova2 = 130f; //lvl 8
        const float puissanceMissile5 = 75; //lvl 9
        const float puissanceValve1 = 30; //lvl 10
        const float puissanceMissile6 = 110; //lvl 11
        const float puissanceFeu3 = 25f; //lvl 12
        const float puissanceNova3 = 175f; //lvl 13
        const float puissanceMissile7 = 150; //lvl 14
        const float puissanceValve2 = 60; //lvl 15
        const float puissanceOnde1 = 50f; //lvl 15
        const float puissanceFeu4 = 40f; //lvl 16
        const float puissanceMissile8 = 200; //lvl 17
        const float puissanceNova4 = 440f; //lvl 18
        const float puissanceValve3 = 70; //lvl 18
        const float puissanceMissile9 = 250; //lvl 19
        const float puissanceOnde2 = 100f; //lvl 20
          
        */


        //enumération permetant de définir le mouvement de l'avion avec des mot plutot que des nombres
        public enum stateMovement { none, down, up, forward, backward };

        public Game1()
        {
            stage = 0;
        }


        //public override void Initialize()
        //{
        //    this.graphics.PreferredBackBufferHeight = 768;
        //    this.graphics.PreferredBackBufferWidth = 1024;
        //    this.graphics.ApplyChanges();
        //    base.Initialize();
        //}

        public void setStage(int _stage)
        {
            stage = _stage;
        }
        

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
                content.RootDirectory = "content";
            }



            spriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);

            fontMenu = new Font(content.Load<Texture2D>("Sprites\\fontMenu"));

            //currentLevel = new Level3(this.content);                                       /* <-------------------------  LEVEL  */
            switch (stage)
            {
                case 1:
                    currentLevel = new Level1(this.content);
                    break;
                case 2:
                    currentLevel = new Level2(this.content);
                    break;
                case 3:
                    currentLevel = new Level3(this.content);
                    break;
                case 4:
                    currentLevel = new Level4(this.content);
                    break;
            }


            thisbackgroundLvl2 = new Font();
            thisbackgroundLvl3 = new Font();
            thisbackgroundLvl4 = new Font();
            //backgroundLvl1 = new Font(content.Load<Texture2D>("Sprites\\background1"));
            if (currentLevel.getLevel == 1)
            {
                backgroundToUse = 1;
                backgroundLvl2 = new Queue<Font>();

                thisbackgroundLvl2.sprite = content.Load<Texture2D>("Sprites\\background22");
                thisbackgroundLvl2.velocity = new Vector2(-1f, 0);
                thisbackgroundLvl2.position = new Vector2(0, 0);

                backgroundLvl2One = new Font(thisbackgroundLvl2.sprite);
                addToQueueFont(backgroundLvl2, backgroundLvl2One, thisbackgroundLvl2.position, thisbackgroundLvl2.velocity);
            }
            if (currentLevel.getLevel == 2)
            {
                backgroundToUse = 3;
                backgroundLvl2 = new Queue<Font>();
                backgroundLvl3 = new Queue<Font>();
                backgroundLvl4 = new Queue<Font>();

                thisbackgroundLvl2.sprite = content.Load<Texture2D>("Sprites\\background22");
                thisbackgroundLvl3.sprite = content.Load<Texture2D>("Sprites\\background32");
                thisbackgroundLvl4.sprite = content.Load<Texture2D>("Sprites\\background33");

                backgroundLvl2One = new Font(thisbackgroundLvl2.sprite);
                backgroundLvl3One = new Font(thisbackgroundLvl3.sprite);
                backgroundLvl4One = new Font(thisbackgroundLvl4.sprite);

                thisbackgroundLvl2.position = new Vector2(0, 0);
                thisbackgroundLvl3.position = new Vector2(0, 768 - thisbackgroundLvl3.sprite.Height);
                thisbackgroundLvl4.position = new Vector2(0, 768 - thisbackgroundLvl4.sprite.Height);

                thisbackgroundLvl2.velocity = new Vector2(-0.5f, 0);
                thisbackgroundLvl3.velocity = new Vector2(-0.6f, 0);
                thisbackgroundLvl4.velocity = new Vector2(-3f, 0);

                addToQueueFont(backgroundLvl2, backgroundLvl2One, thisbackgroundLvl2.position, thisbackgroundLvl2.velocity);
                addToQueueFont(backgroundLvl3, backgroundLvl3One, thisbackgroundLvl3.position, thisbackgroundLvl3.velocity);
                addToQueueFont(backgroundLvl4, backgroundLvl4One, thisbackgroundLvl4.position, thisbackgroundLvl4.velocity);
            }
            if (currentLevel.getLevel == 3)
            {
                backgroundToUse = 3;
                backgroundLvl2 = new Queue<Font>();
                backgroundLvl3 = new Queue<Font>();
                backgroundLvl4 = new Queue<Font>();

                thisbackgroundLvl2.sprite = content.Load<Texture2D>("Sprites\\background41");
                thisbackgroundLvl3.sprite = content.Load<Texture2D>("Sprites\\background2j");
                thisbackgroundLvl4.sprite = content.Load<Texture2D>("Sprites\\background3j");

                backgroundLvl2One = new Font(thisbackgroundLvl2.sprite);
                backgroundLvl3One = new Font(thisbackgroundLvl3.sprite);
                backgroundLvl4One = new Font(thisbackgroundLvl4.sprite);

                thisbackgroundLvl2.velocity = new Vector2(-2.5f, 0);
                thisbackgroundLvl3.velocity = new Vector2(-7f, 0);
                thisbackgroundLvl4.velocity = new Vector2(-4.5f, 0);

                thisbackgroundLvl2.position = new Vector2(0, 0);
                thisbackgroundLvl3.position = new Vector2(0, -100);
                thisbackgroundLvl4.position = new Vector2(0, 768 - thisbackgroundLvl4.sprite.Height);

                backgroundLvl2One = new Font(thisbackgroundLvl2.sprite);
                addToQueueFont(backgroundLvl2, backgroundLvl2One, thisbackgroundLvl2.position, thisbackgroundLvl2.velocity);
                addToQueueFont(backgroundLvl3, backgroundLvl3One, thisbackgroundLvl3.position, thisbackgroundLvl3.velocity);
                addToQueueFont(backgroundLvl4, backgroundLvl4One, thisbackgroundLvl4.position, thisbackgroundLvl4.velocity);
            }
            if (currentLevel.getLevel == 4)
            {
                backgroundToUse = 2;
                backgroundLvl2 = new Queue<Font>();
                backgroundLvl3 = new Queue<Font>();

                thisbackgroundLvl2.sprite = content.Load<Texture2D>("Sprites\\background22");
                thisbackgroundLvl3.sprite = content.Load<Texture2D>("Sprites\\background32");

                backgroundLvl2One = new Font(thisbackgroundLvl2.sprite);
                backgroundLvl3One = new Font(thisbackgroundLvl3.sprite);

                thisbackgroundLvl2.position = new Vector2(0, 0);
                thisbackgroundLvl3.position = new Vector2(0, 768 - thisbackgroundLvl3.sprite.Height);

                thisbackgroundLvl2.velocity = new Vector2(-3.5f, 0);
                thisbackgroundLvl3.velocity = new Vector2(-4.8f, 0);

                addToQueueFont(backgroundLvl2, backgroundLvl2One, thisbackgroundLvl2.position, thisbackgroundLvl2.velocity);
                addToQueueFont(backgroundLvl3, backgroundLvl3One, thisbackgroundLvl3.position, thisbackgroundLvl3.velocity);
            }


            explosions = new Queue<Explosion>();

            viewportRect = new Rectangle(0, 0,
               Program.windowWidth,
               Program.windowHeight);

            if (firstInitialized)
            {
                
                avionHero = Program.avionHero;
                if(avionHero.level <= 15 && !Program.godMode)
                    avionHero.setSprite(content.Load<Texture2D>("Sprites\\avion"));
                else if (!Program.godMode)
                    avionHero.setSprite(content.Load<Texture2D>("Sprites\\avion2"));
                else
                    avionHero.setSprite(content.Load<Texture2D>("Sprites\\avion3"));

            }
            if (Program.godMode)
            {
                avionHero.sprite = content.Load<Texture2D>("Sprites\\avion3");
                avionHero.width = avionHero.sprite.Width;
                avionHero.height = avionHero.sprite.Height / 3;
            }
            avionHero.setFullHp();
            avionHero.position = new Vector2(20, (Program.windowHeight / 2) - (avionHero.height / 2));
            avionHero.alive = true;
            avionHero.velocity = Vector2.Zero;
            heroHealthBar = content.Load<Texture2D>("Sprites\\healthbar");
            bossHealthBar = content.Load<Texture2D>("Sprites\\bosshealthbar");
            expBar = content.Load<Texture2D>("Sprites\\expbar");


            missile1 = new Missile[maxMissile];
            for (int i = 0; i < maxMissile; i++)
            {
                missile1[i] = new Missile(content.Load<Texture2D>("Sprites\\missile1"), puissanceMissile1);
            }
            
            temp = new Missile[maxMissile];
            feu1 = new Missile(content.Load<Texture2D>("Sprites\\feu1"), puissanceFeu1);
            feu2 = new Missile(content.Load<Texture2D>("Sprites\\feu2"), puissanceFeu2);
            feu3 = new Missile(content.Load<Texture2D>("Sprites\\feu3"), puissanceFeu3);
            feu4 = new Missile(content.Load<Texture2D>("Sprites\\feu4"), puissanceFeu4);
            nova1 = new Missile(content.Load<Texture2D>("Sprites\\nova1"), puissanceNova1);
            nova2 = new Missile(content.Load<Texture2D>("Sprites\\nova2"), puissanceNova2);
            nova3 = new Missile(content.Load<Texture2D>("Sprites\\nova3"), puissanceNova3);
            nova4 = new Missile(content.Load<Texture2D>("Sprites\\nova4"), puissanceNova4);
           

            valveDroite = content.Load<Texture2D>("Sprites\\valveDroite");
            valveGauche = content.Load<Texture2D>("Sprites\\valveGauche");
            valve = new Missile[maxValveMissile];
            for (int i = 0; i < maxValveMissile; i++)
            {
                valve[i] = new Missile(content.Load<Texture2D>("Sprites\\valveMissile"), puissanceValve1);
            }
            valve2 = new Missile[maxValveMissile];
            for (int i = 0; i < maxValveMissile; i++)
            {
                valve2[i] = new Missile(content.Load<Texture2D>("Sprites\\valveMissile3"), puissanceValve3);
            }
           

            ondeUp = new Missile[maxOnde];
            for (int i = 0; i < maxOnde; i++)
            {
                ondeUp[i] = new Missile(content.Load<Texture2D>("Sprites\\ondeUp"), puissanceOnde1);
                ondeUp[i].scale = 0.0f;
            }

            ondeDown = new Missile[maxOnde];
            for (int i = 0; i < maxOnde; i++)
            {
                ondeDown[i] = new Missile(content.Load<Texture2D>("Sprites\\ondeDown"), puissanceOnde1);
                ondeDown[i].scale = 0.0f;
            }

            if (firstInitialized)
            {
                currentFeu = feu1;
                currentNova = nova1;
                currentValve = valve;
                currentUltimValve = valve2;
                currentMissile = missile1;
                currentPuissance = 5;
                currentDps = (1000 / intervalMissile) * currentPuissance;

                loadArmory();
                firstInitialized = false; //on ne réinitiliserai plus ces valeur
            }

            if (Program.godMode)
                vitesseMissileAmp = 1.5f;
            else
                vitesseMissileAmp = 1f;

            bossDead = false; //réinitialisation de quelques variables lorsqu'on recommence un stage
            addedQueueDone2 = false;
            addedQueueDone3 = false;
            addedQueueDone4 = false;
            openedValve = false;
            avionHeroAnimation = false; 

            lvlFont = content.Load<SpriteFont>("Fonts\\LevelSpriteFont");
            statFont = content.Load<SpriteFont>("Fonts\\StatSpriteFont");
            info = content.Load<SpriteFont>("Fonts\\InfoSpritefont");
            infoBoss = content.Load<SpriteFont>("Fonts\\InfoBossSpritefont");
            armesMissileActif = content.Load<Texture2D>("Sprites\\armesMissileActif");
            armesMissileInactif = content.Load<Texture2D>("Sprites\\armesMissileInactif");
            armesFeuActif = content.Load<Texture2D>("Sprites\\armesFeuActif");
            armesFeuInactif = content.Load<Texture2D>("Sprites\\armesFeuInactif");
            armesFeuNA = content.Load<Texture2D>("Sprites\\armesFeuNA");
            armesNovaActif = content.Load<Texture2D>("Sprites\\armesNovaActif");
            armesNovaInactif = content.Load<Texture2D>("Sprites\\armesNovaInactif");
            armesNovaNA = content.Load<Texture2D>("Sprites\\armesNovaNA");
            armesValveActif = content.Load<Texture2D>("Sprites\\armesValveActif");
            armesValveInactif = content.Load<Texture2D>("Sprites\\armesValveInactif");
            armesValveNA = content.Load<Texture2D>("Sprites\\armesValveNA");
            armesOndeActif = content.Load<Texture2D>("Sprites\\armesOndeActif");
            armesOndeInactif = content.Load<Texture2D>("Sprites\\armesOndeInactif");
            armesOndeNA = content.Load<Texture2D>("Sprites\\armesOndeNA");
            shining = content.Load<Texture2D>("Sprites\\shining");
            armeShining = content.Load<Texture2D>("Sprites\\armeShining");

            armeShiningTab = new int[5];

            currentCursor = (int)CursorWeapon.missile;

            //SOUNDS


            soundAvionHExplosion = content.Load<SoundEffect>("Sounds\\avionHeroExplosion");
            soundAvionHMissile = content.Load<SoundEffect>("Sounds\\avionHeroMissile");
            soundAvionHFeu = content.Load<SoundEffect>("Sounds\\avionHeroFeu");
            soundAvionFeuInstance = soundAvionHFeu.CreateInstance();
            soundAvionHNova = content.Load<SoundEffect>("Sounds\\avionHeroNova");
            soundAvionNovaInstance = soundAvionHNova.CreateInstance();
            soundAvionHValve = content.Load<SoundEffect>("Sounds\\avionHeroValve");
            soundAvionHWave = content.Load<SoundEffect>("Sounds\\avionHeroWave");

            soundAvionEMissile = content.Load<SoundEffect>("Sounds\\avionEnemiMissile");
            soundImpact = content.Load<SoundEffect>("Sounds\\Impact");
            soundImpactAvions = content.Load<SoundEffect>("Sounds\\ImpactAvions");
            soundAvionEExplosion = content.Load<SoundEffect>("Sounds\\avionEnemiExplosion");
            soundBossShield = content.Load<SoundEffect>("Sounds\\bossShield");
            soundLevelUp = content.Load<SoundEffect>("Sounds\\lvlUp");
            soundBossExplosion = content.Load<SoundEffect>("Sounds\\bossExplosion");
        }



        public void addToQueueFont(Queue<Font> qFont, Font font, Vector2 position, Vector2 speed)
        {
            font.position = position;
            font.velocity = speed;
            qFont.Enqueue(font);
        }


        public override void UnloadContent()
        {
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                      bool coveredByOtherScreen)
        {
            if (!pause)
            {

                KeyboardState keyboardState = Keyboard.GetState();


                timerMissile += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                GeneralLevel.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                GeneralLevel.timerSeq = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                timePlayed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                avionHero.state = (int)stateMovement.none;

                float speed = 1;
                if (keyboardState.IsKeyDown(Keys.C))
                    speed = (int)slowedSpeed;

                if (keyboardState.IsKeyDown(Keys.V))
                    speed = upgradedSpeed;



                if (avionHero.velocity != Vector2.Zero)
                {
                    avionHero.position += avionHero.velocity;
                    if (avionHero.timeToRecovery > 0)//si on est en phase de récupération d'une collision, la vélocité diminu et devient nul en dessous de 1
                    {
                        avionHero.velocity *= 0.9f;
                        if (Math.Abs(avionHero.velocity.X) <= 1 ||
                            Math.Abs(avionHero.velocity.Y) <= 1)
                        {
                            avionHero.velocity = Vector2.Zero;
                        }
                    }
                }
                else
                {
                    if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        avionHero.position += new Vector2(0, -vitesseDeplacement / speed);
                        avionHero.state = (int)stateMovement.up;
                    }
                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        avionHero.position += new Vector2(0, vitesseDeplacement / speed);
                        avionHero.state = (int)stateMovement.down;
                    }
                    if (keyboardState.IsKeyDown(Keys.Left))
                    {
                        avionHero.position += new Vector2(-vitesseDeplacement / speed, 0);
                    }
                    if (keyboardState.IsKeyDown(Keys.Right))
                    {
                        avionHero.position += new Vector2(vitesseDeplacement / speed, 0);
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (currentLevel.getLevel == 2 || currentLevel.getLevel == 4) //au boss du level 2 on ne peut plus tirer
                    {
                        if (!currentLevel.bossFight)
                            fireMissile();
                    }
                    else
                    {
                        fireMissile();
                    }

                }

                if (keyboardState.IsKeyUp(Keys.Space)) // si on lache espace le feu s'arrete
                {
                    if (currentCursor == (int)CursorWeapon.feu)
                    {
                        currentFeu.alive = false;

                        soundAvionFeuInstance.Stop();
                    }
                    if (currentCursor == (int)CursorWeapon.nova)
                    {
                        currentNova.alive = false;
                        currentNova.visible = false;

                        soundAvionNovaInstance.Stop();
                    }
                }
                if (currentCursor != (int)CursorWeapon.feu) // on ettein le feu si on change d'arme
                {
                    currentFeu.alive = false;
                }
                if (currentCursor != (int)CursorWeapon.nova) // on ettein la nova si on change d'arme
                {
                    currentNova.alive = false;
                    currentNova.visible = false;
                }

                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    currentCursor = (int)CursorWeapon.missile;
                    currentPuissance = currentMissile.ElementAt(0).damage;
                    currentDps = (1000 / intervalMissile) * currentPuissance;
                    openedValve = false;
                    changeArme();
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (unlockFeu)
                    {
                        currentCursor = (int)CursorWeapon.feu;
                        currentPuissance = currentFeu.damage;
                        currentDps = 60 * currentPuissance;

                        openedValve = false;
                        changeArme();
                    }
                }

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (unlockNova)
                    {
                        currentCursor = (int)CursorWeapon.nova;
                        currentPuissance = currentNova.damage;
                        currentDps = (1000 / intervalNova) * currentPuissance;
                        openedValve = false;
                        changeArme();
                    }
                }
                if (keyboardState.IsKeyDown(Keys.F))
                {
                    if (unlockValve)
                    {
                        currentCursor = (int)CursorWeapon.valve;
                        currentPuissance = currentValve.ElementAt(0).damage;
                        currentDps = (1000 / intervalValve) * currentPuissance;
                        openedValve = true;
                        changeArme();
                    }
                }
                if (keyboardState.IsKeyDown(Keys.G))
                {
                    if (unlockOnde)
                    {
                        currentCursor = (int)CursorWeapon.onde;
                        currentPuissance = ondeUp.ElementAt(0).damage;
                        currentDps = 0;
                        openedValve = false;
                        changeArme();
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    if (exitToMenu == 0)
                    {
                        //if (MediaPlayer.State == MediaState.Playing)
                        //    MediaPlayer.Stop();
                        //if (MediaPlayer.State == MediaState.Stopped)
                        //    MediaPlayer.Play(MainMenuScreen.menuSong);

                        //ScreenManager.RemoveScreen(this);
                        //ScreenManager.AddScreen(MainMenuScreen.lms, PlayerIndex.One);
                        pause = true;
                        ScreenManager.AddScreen(new PauseMenuScreen(MainMenuScreen.lms), PlayerIndex.One);
                    }
                }

                //temporaire pour lvl up
                //if (keyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A))
                //{
                //    expInBuffer += avionHero.tabExp[avionHero.level + 1] - avionHero.tabExp[avionHero.level];
                //}

                if (!bossDead)
                {
                    avionHero.position.Y = MathHelper.Clamp(avionHero.position.Y, 0 + fontMenu.sprite.Height, Program.windowHeight - avionHero.height);
                    avionHero.position.X = MathHelper.Clamp(avionHero.position.X, 0, Program.windowWidth - avionHero.width);
                }

                if (currentLevel.getLevel == 2 || currentLevel.getLevel == 4) //au boss du level 2 on ne peut plus tirer
                {

                    if (currentLevel.boss != null)
                    {
                        if (currentLevel.boss.alive)
                        {
                            if (currentLevel.boss.currentHealthPoint <= 0)
                            {
                                bossIsDead();
                            }
                        }
                    }
                }

                updateMissile();
                if (backgroundToUse >= 3)
                {
                    updateBackground2();
                    updateBackground3();
                    updateBackground4();
                }
                else if (backgroundToUse >= 2)
                {
                    updateBackground2();
                    updateBackground3();
                }
                else if (backgroundToUse >= 1)
                {
                    updateBackground2();
                }

                updateExp();
                updateAvionEnemies();
                autoFireMissileEnemi();
                updateMissileEnemi();
                updateAvionHero();
                previousKeyboardState = keyboardState;

                if (avionHero.timeToRecovery > 0) // on baisse le temps qu'il reste a etre invincible aux collisions
                {
                    avionHero.timeToRecovery -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (avionHero.timeToRecovery < 0)
                    {
                        avionHero.timeToRecovery = 0;
                    }
                }
                if (anim == Anim.victory)
                {
                    if (exitToMenu != 0)
                    {
                        if (exitToMenu == 4000)
                        {
                            avionHero.velocity = new Vector2(-1, 0);
                        }
                        if (exitToMenu > 0 && exitToMenu < 1000)
                        {
                            avionHero.velocity += new Vector2(1.5f, 0);
                        }
                        if (exitToMenu > 0) // on baisse le temps qu'il reste avant que l'avion fuse sa victoire et de rejoindre le menu
                        {
                            exitToMenu -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (exitToMenu < 0)
                            {
                                if (MediaPlayer.State == MediaState.Playing)
                                    MediaPlayer.Stop();
                                if (MediaPlayer.State == MediaState.Stopped)
                                    MediaPlayer.Play(MainMenuScreen.menuSong);

                                if (MediaPlayer.State == MediaState.Paused)
                                {
                                    MediaPlayer.Play(MainMenuScreen.menuSong);
                                    MediaPlayer.Pause();
                                }

                                if (currentLevel.getLevel == 4)
                                {
                                    Program.godMode = true;
                                    ScreenManager.RemoveScreen(this);
                                    Program.renewGame();
                                }
                                exitToMenu = 0;
                                //retourner au menu 
                                MainMenuScreen.lms.debloquerNiveau(stage);
                                ScreenManager.RemoveScreen(this);
                                ScreenManager.AddScreen(MainMenuScreen.lms, PlayerIndex.One);
                            }
                        }
                    }
                }
                else if (anim == Anim.defeat)
                {
                    if (exitToMenu != 0)
                    {
                        if (exitToMenu == 3000)
                        {
                            avionHero.velocity = new Vector2(0.5f, 0.5f);
                            int randx = random.Next((int)(avionHero.position.X - 40), (int)(avionHero.position.X + avionHero.width + 30));
                            int randw = random.Next((int)(avionHero.position.Y - 10), (int)(avionHero.position.Y + avionHero.height + 10));
                            Explosion exp = new Explosion(content.Load<Texture2D>("Sprites\\explosion"), 18, 3);
                            exp.destination = avionHero.position - new Vector2(0, exp.sprite.Height / 3);
                            explosions.Enqueue(exp);
                        }
                        if ((int)exitToMenu % 12 == 0)
                        {
                            int randx = random.Next((int)(avionHero.position.X - 30), (int)(avionHero.position.X + avionHero.width + 10));
                            int randy = random.Next((int)(avionHero.position.Y - 20), (int)(avionHero.position.Y + avionHero.height - 30));
                            Explosion exp = new Explosion(content.Load<Texture2D>("Sprites\\explosion"), 18, 3);
                            exp.destination = new Vector2(randx, randy); ;
                            explosions.Enqueue(exp);
                        }
                        if (exitToMenu > 0) // on baisse le temps qu'il reste avant que l'avion fuse sa victoire et de rejoindre le menu
                        {
                            exitToMenu -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (exitToMenu < 0)
                            {
                                if (MediaPlayer.State == MediaState.Playing)
                                    MediaPlayer.Stop();
                                if (MediaPlayer.State == MediaState.Stopped)
                                    MediaPlayer.Play(MainMenuScreen.menuSong);

                                if (MediaPlayer.State == MediaState.Paused)
                                {
                                    MediaPlayer.Play(MainMenuScreen.menuSong);
                                    MediaPlayer.Pause();
                                }
                                exitToMenu = 0;
                                ScreenManager.RemoveScreen(this);
                                ScreenManager.AddScreen(MainMenuScreen.lms, PlayerIndex.One);
                            }

                        }
                    }
                }

                currentLevel.update();
                currentLevel.updateTimer();
                currentLevel.target = avionHero.position + avionHero.center;
                if (!avionHero.alive)
                {
                    if (!avionHeroAnimation)
                    {
                        deadCount++;
                        avionHeroAnimation = true;
                        soundAvionHExplosion.Play();
                        exitToMenu = 3000;
                        anim = Anim.defeat;
                    }
                }


                if (currentLevel.bossInComing)
                {
                    if (currentLevel.getLevel == 2 || currentLevel.getLevel == 4)
                    {
                        if (MediaPlayer.State == MediaState.Playing)
                            MediaPlayer.Stop();
                        if (MediaPlayer.State == MediaState.Stopped)
                            MediaPlayer.Play(MainMenuScreen.bossSong1);

                        if (MediaPlayer.State == MediaState.Paused)
                        {
                            MediaPlayer.Play(MainMenuScreen.bossSong1);
                            MediaPlayer.Pause();
                        }
                        currentLevel.bossInComing = false;
                    }
                    else if (currentLevel.getLevel == 1 || currentLevel.getLevel == 3)
                    {
                        if (MediaPlayer.State == MediaState.Playing)
                            MediaPlayer.Stop();
                        if (MediaPlayer.State == MediaState.Stopped)
                            MediaPlayer.Play(MainMenuScreen.bossSong2);

                        if (MediaPlayer.State == MediaState.Paused)
                        {
                            MediaPlayer.Play(MainMenuScreen.bossSong2);
                            MediaPlayer.Pause();
                        }
                        currentLevel.bossInComing = false;
                    }
                }
            }
        }


        public void changeArme()
        {
            soundAvionNovaInstance.Stop();
            soundAvionFeuInstance.Stop();
        }

        public void bossIsDead()
        {
            currentLevel.boss.alive = false;
            soundBossExplosion.Play();
            foreach (Missile[] missiles in currentLevel.boss.missiles)
            {
                foreach (Missile missile in missiles)
                    missile.alive = false;
            }
            expInBuffer += currentLevel.boss.dropedExp;
            for (int i = 0; i < 10; i++)
            {
                Explosion exp = new Explosion(content.Load<Texture2D>("Sprites\\explosion"), 18, 10);
                int randw = random.Next(0, (int)currentLevel.boss.width + 50);
                int randh = random.Next(0, (int)currentLevel.boss.height + 50);
                exp.destination = currentLevel.boss.position + new Vector2(randw - 50, randh - 50);
                explosions.Enqueue(exp);
            }

            anim = Anim.victory;
            exitToMenu = 4000;
            bossDead = true;

            
        }

        public void fireMissile()
        {
            if (avionHero.alive)
            {
                if (currentCursor == (int)CursorWeapon.missile)
                {
                    if (timerMissile > intervalMissile)
                    {
                        foreach (Missile missile in currentMissile)
                        {
                            if (!missile.alive)
                            {
                                soundAvionHMissile.Play();
                                missile.alive = true;
                                missile.velocity = new Vector2(vitesseMissile, 0.0f);
                                missile.position = avionHero.position;
                                missile.position.X += (avionHero.width / 1.2f);
                                missile.position.Y += (avionHero.height / 2) - (missile.center.Y) + 5;

                                timerMissile = 0f;
                                return;
                            }
                        }
                    }
                }
                if (currentCursor == (int)CursorWeapon.feu)
                {
                    currentFeu.alive = true;
                    soundAvionFeuInstance.Play();

                    currentFeu.position = avionHero.position + new Vector2(6 - currentFeu.sprite.Width, 26 - currentFeu.center.Y);
                    //currentFeu.position = avionHero.position + new Vector2(-132, -30);
                    //feu1.position = avionHero.position + new Vector2(6, 26);
                }
                if (currentCursor == (int)CursorWeapon.nova)
                {
                    currentNova.visible = true;
                    soundAvionNovaInstance.Play();
                    currentNova.position = (avionHero.position + avionHero.center) - currentNova.center;
                    if (timerMissile > intervalNova)
                    {
                        currentNova.alive = true;
                        timerMissile = 0;
                    }
                }
                if (currentCursor == (int)CursorWeapon.valve)
                {
                    if (timerMissile > intervalValve)
                    {
                        foreach (Missile missile in currentValve)
                        {
                            if (!missile.alive)
                            {
                                soundAvionHValve.Play();
                                if (missile.type == 0)
                                {

                                    if (parywave)
                                    {
                                        missile.sprite = content.Load<Texture2D>("Sprites\\valveMissile");
                                        missile.alive = true;
                                        missile.velocity = new Vector2(vitesseMissile, 0.0f);
                                        missile.position = new Vector2(
                                            avionHero.position.X + avionHero.width - 15,
                                            avionHero.position.Y + avionHero.center.Y - valveDroite.Height / 2);
                                        if (!ultimValve)
                                            timerMissile = 0f;
                                        parywave = false;
                                        break;
                                    }
                                    else // on crée un décalage entre les missiles
                                    {
                                        missile.sprite = content.Load<Texture2D>("Sprites\\valveMissileW2");
                                        missile.alive = true;
                                        missile.velocity = new Vector2(vitesseMissile, 0.0f);
                                        missile.position = new Vector2(
                                            avionHero.position.X + avionHero.width - 15,
                                            avionHero.position.Y + avionHero.center.Y - (valveDroite.Height / 2) + 2);
                                        if (!ultimValve)
                                            timerMissile = 0f;
                                        parywave = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (parywave)
                                    {
                                        missile.sprite = content.Load<Texture2D>("Sprites\\valveMissile2");
                                        missile.alive = true;
                                        missile.velocity = new Vector2(vitesseMissile, 0.0f);
                                        missile.position = new Vector2(
                                            avionHero.position.X + avionHero.width - 15,
                                            avionHero.position.Y + avionHero.center.Y - valveDroite.Height / 2);
                                        if (!ultimValve)
                                            timerMissile = 0f;
                                        parywave = false;
                                        break;
                                    }
                                    else // on crée un décalage entre les missiles
                                    {
                                        missile.sprite = content.Load<Texture2D>("Sprites\\valveMissile2W2");
                                        missile.alive = true;
                                        missile.velocity = new Vector2(vitesseMissile, 0.0f);
                                        missile.position = new Vector2(
                                            avionHero.position.X + avionHero.width - 15,
                                            avionHero.position.Y + avionHero.center.Y - (valveDroite.Height / 2) + 2);
                                        if (!ultimValve)
                                            timerMissile = 0f;
                                        parywave = true;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }

                if (currentCursor == (int)CursorWeapon.valve && ultimValve)
                {

                    if (ultimValve)
                    {
                        if (timerMissile > intervalValve)
                        {
                            foreach (Missile missile in currentUltimValve)
                            {
                                if (!missile.alive)
                                {
                                    if (parywave)
                                    {
                                        missile.sprite = content.Load<Texture2D>("Sprites\\valveMissile3");
                                        missile.alive = true;
                                        missile.velocity = new Vector2(-vitesseMissile, 0.0f);
                                        missile.position = new Vector2(
                                            avionHero.position.X - 5,
                                            avionHero.position.Y + avionHero.center.Y - valveGauche.Height / 2);
                                        timerMissile = 0f;
                                        break;
                                    }
                                    else // on crée un décalage entre les missiles
                                    {
                                        missile.sprite = content.Load<Texture2D>("Sprites\\valveMissile3W2");
                                        missile.alive = true;
                                        missile.velocity = new Vector2(-vitesseMissile, 0.0f);
                                        missile.position = new Vector2(
                                            avionHero.position.X - 5,
                                            avionHero.position.Y + avionHero.center.Y - (valveGauche.Height / 2) - 4);
                                        timerMissile = 0f;
                                        break;
                                    }

                                }
                            }
                        }
                    }
                }

                if (currentCursor == (int)CursorWeapon.onde)
                {
                    if (timerMissile > intervalOnde)
                    {

                        soundAvionHWave.Play();
                        foreach (Missile missile in ondeUp)
                        {
                            if (!missile.alive)
                            {
                                missile.alive = true;
                                missile.velocity = new Vector2(0, -vitesseOnde);

                                missile.position = new Vector2(
                                    (avionHero.position.X + avionHero.center.X),
                                    (avionHero.position.Y + avionHero.center.Y));
                                timerMissile = 0.0f;
                                break;
                            }
                        }
                        foreach (Missile missile in ondeDown)
                        {
                            if (!missile.alive)
                            {
                                missile.alive = true;
                                missile.velocity = new Vector2(0, vitesseOnde);

                                missile.position = new Vector2(
                                    (avionHero.position.X + avionHero.center.X),
                                    (avionHero.position.Y + avionHero.center.Y));
                                timerMissile = 0.0f;
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void autoFireMissileEnemi()
        {
            foreach (List<AvionEnemi> avionEnemies in currentLevel.tabAvionEnemi)
            {
                if (avionEnemies != null)
                {
                    float temp = 1;
                    if (currentLevel.hitChances != 0)
                    {
                        temp = currentLevel.hitChances;
                    }
                    fireMissileEnemi(avionEnemies, 100f / temp);
                }
            }

        }

        public void updateExp()
        {
            int quotient = (avionHero.tabExp[avionHero.level + 1] / 100) * 1;
            if (expInBuffer > quotient)
            {
                expInBuffer -= quotient;
                avionHero.gainExp(quotient);
            }
            else if (expInBuffer > 0)
            {
                avionHero.gainExp(expInBuffer);
                expInBuffer = 0;
            }

        }

        public void fireMissileEnemi(List<AvionEnemi> avionEnemies, float chances)
        {
            foreach (AvionEnemi avionEnemi in avionEnemies)
            {
                int fireCannonBall = random.Next(0, (int)chances);

                if (fireCannonBall == 1)
                {
                    if (avionEnemi.alive)
                    {
                        foreach (Missile missile in avionEnemi.missile)
                        {
                            if (!missile.alive)
                            {
                                if (viewportRect.Contains(new Point(
                                   (int)avionEnemi.position.X,
                                   (int)avionEnemi.position.Y)))
                                    soundAvionEMissile.Play();
                                missile.alive = true;
                                missile.position = avionEnemi.position + avionEnemi.center;
                                missile.velocity = aimHero(missile.position, missile.center);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public Vector2 aimHero(Vector2 position, Vector2 center)
        {
            Vector2 directionHero;
            double newX;
            double newY;
            double hypothenuse;
            position = position + center;
            directionHero = new Vector2(
                avionHero.position.X + avionHero.center.X - position.X,
                avionHero.position.Y + avionHero.center.Y - position.Y);
            hypothenuse = Math.Sqrt(
                (directionHero.X * directionHero.X) + (directionHero.Y * directionHero.Y));
            newX = (directionHero.X * vitesseMissileEnemi * vitesseMissileAmp) / hypothenuse;
            newY = (directionHero.Y * vitesseMissileEnemi * vitesseMissileAmp) / hypothenuse;
            return new Vector2((float)newX, (float)newY);
           
        }

        public Vector2 aimHero(Vector2 position, Vector2 center, int vitesse)
        {
            Vector2 directionHero;
            double newX;
            double newY;
            double hypothenuse;
            position = position + center;
            directionHero = new Vector2(
                avionHero.position.X + avionHero.center.X - position.X,
                avionHero.position.Y + avionHero.center.Y - position.Y);
            hypothenuse = Math.Sqrt(
                (directionHero.X * directionHero.X) + (directionHero.Y * directionHero.Y));
            newX = (directionHero.X * vitesse) / hypothenuse;
            newY = (directionHero.Y * vitesse) / hypothenuse;
            return new Vector2((float)newX, (float)newY);

        }

        public void fireMissileEnemi(AvionEnemi avionEnemi, int chances)
        {
            int fireCannonBall = random.Next(0, chances);

            if (fireCannonBall == 1)
            {
                Vector2 directionHero;
                double newX;
                double newY;
                double hypothenuse;

                if (avionEnemi.alive)
                {
                    foreach (Missile missile in avionEnemi.missile)
                    {
                        if (!missile.alive)
                        {
                            soundAvionEMissile.Play();
                            missile.alive = true;
                            missile.position = avionEnemi.position + avionEnemi.center;
                            directionHero = new Vector2(
                                avionHero.position.X + avionHero.center.X - missile.position.X,
                                avionHero.position.Y + avionHero.center.Y - missile.position.Y);
                            hypothenuse = Math.Sqrt(
                                (directionHero.X * directionHero.X) + (directionHero.Y * directionHero.Y));
                            newX = (directionHero.X * vitesseMissileEnemi) / hypothenuse;
                            newY = (directionHero.Y * vitesseMissileEnemi) / hypothenuse;
                            missile.velocity = new Vector2((float)newX, (float)newY);
                            break;
                        }
                    }
                }
            }
            
        }

        public void updateMissile()
        {
            foreach(Missile missile in currentMissile)
            {
                if (missile.alive)
                {
                    missile.position += missile.velocity;

                    checkMissileCollision(missile);                    
                }

                if (!viewportRect.Contains(new Point(
                           (int)missile.position.X,
                           (int)missile.position.Y)))
                {
                    missile.alive = false;
                }


            }


            foreach (Missile missile in temp) //Si on lvl up, les ancient missiles sont tjrs vivant et sont stocké dans temp
            {
                if (missile != null)
                {
                    if (missile.alive)
                    {
                        missile.position += missile.velocity;

                        checkMissileCollision(missile);
                    }

                    if (!viewportRect.Contains(new Point(
                               (int)missile.position.X,
                               (int)missile.position.Y)))
                    {
                        missile.alive = false;
                    }
                }
            }

            if (currentFeu.alive)
            {
                checkMissileCollision(currentFeu);
            }

            if (currentNova.alive)
            {
                checkMissileCollision(currentNova);
            }

            foreach (Missile missile in currentValve)
            {
                if (missile.alive)
                {
                    missile.position += missile.velocity;
                    checkMissileCollision(missile);
                }

                if (!viewportRect.Contains(new Point(
                           (int)missile.position.X,
                           (int)missile.position.Y)))
                {
                    missile.alive = false;
                }
            }
            if (ultimValve)
            {
                foreach (Missile missile in currentUltimValve)
                {
                    if (missile.alive)
                    {
                        missile.position += missile.velocity;
                        checkMissileCollision(missile);
                    }

                    if (!viewportRect.Contains(new Point(
                               (int)(missile.position.X + missile.sprite.Width),
                               (int)missile.position.Y)))
                    {
                        missile.alive = false;
                    }
                }
            }

            foreach (Missile missile in ondeUp)
            {
                if (missile.alive)
                {
                    missile.position += missile.velocity;
                    missile.position.X -= missile.center.X * 0.01f;
                    missile.velocity *= 1.01f;
                    missile.scale += 0.01f;
                    checkMissileCollision(missile);
                }

                if (!viewportRect.Contains(new Point(
                           1,
                           (int)(missile.position.Y + (missile.sprite.Height * missile.scale)))))
                {
                    missile.alive = false;
                    missile.scale = 0.0f;
                }


            }
            foreach (Missile missile in ondeDown)
            {
                if (missile.alive)
                {
                    missile.position += missile.velocity;
                    missile.position.X -= missile.center.X * 0.01f;
                    missile.velocity *= 1.01f;
                    missile.scale += 0.01f;
                    checkMissileCollision(missile);
                }

                if (!viewportRect.Contains(new Point(
                           1,
                           (int)(missile.position.Y - (missile.sprite.Height * missile.scale)))))
                {
                    missile.alive = false;
                    missile.scale = 0.0f;
                }


            }
        }

        public void checkMissileCollision(Missile missile)
        {
            if (missile.alive)
            {
                Rectangle missileRect = new Rectangle(
                                (int)missile.position.X,
                                (int)missile.position.Y,
                                (int)(missile.sprite.Width * missile.scale),
                                (int)(missile.sprite.Height * missile.scale));

                foreach (List<AvionEnemi> avionEnemies in currentLevel.tabAvionEnemi)
                {
                    if (avionEnemies.Count() != 0)
                    {
                        foreach (AvionEnemi avionEnemi in avionEnemies)
                        {
                            if (avionEnemi.alive == true)
                            {
                                Rectangle avionEnemiRect = new Rectangle(
                                    (int)avionEnemi.position.X,
                                    (int)avionEnemi.position.Y,
                                    avionEnemi.width,
                                    avionEnemi.height);

                                if (Intersections.intersectPixel(avionEnemi.sprite, missile.sprite, avionEnemiRect, missileRect))
                                {
                                    if (currentCursor == (int)CursorWeapon.missile || currentCursor == (int)CursorWeapon.valve)
                                        missile.alive = false;

                                    avionEnemi.currentHealthPoint -= (int)missile.damage;
                                    if (avionEnemi.currentHealthPoint <= 0)
                                    {
                                        soundAvionEExplosion.Play();
                                        avionEnemi.alive = false;
                                        enemiKilled++;
                                        expInBuffer += (avionEnemi.dropedExp);
                                        avionEnemiDead(avionEnemi);
                                        Explosion exp = new Explosion(content.Load<Texture2D>("Sprites\\explosion"), 18, 2);
                                        exp.destination = avionEnemi.position - new Vector2(0, exp.sprite.Height / 3);
                                        explosions.Enqueue(exp);
                                    }
                                    if (currentCursor == (int)CursorWeapon.missile || currentCursor == (int)CursorWeapon.valve)
                                        break;

                                }
                            }
                        }
                    }
                }
                if (currentLevel.getLevel == 1 || currentLevel.getLevel == 3)
                {
                    if (currentLevel.boss != null)
                    {
                        if (currentLevel.boss.alive)
                        {
                            if (!currentLevel.boss.invincible)
                            {
                                Rectangle bossRect = new Rectangle(
                                (int)currentLevel.boss.position.X,
                                (int)currentLevel.boss.position.Y,
                                currentLevel.boss.width,
                                currentLevel.boss.height);
                                if (Intersections.intersectPixel(currentLevel.boss.sprite, missile.sprite, bossRect, missileRect))
                                {

                                    currentLevel.boss.currentHealthPoint -= (int)missile.damage;
                                    if (currentLevel.boss.currentHealthPoint <= 0)
                                    {
                                        bossIsDead();

                                    }

                                }
                            }
                            else
                            {
                                Rectangle bossRect = new Rectangle(
                                (int)currentLevel.boss.position.X - 50,
                                (int)currentLevel.boss.position.Y - 50,
                                currentLevel.boss.width + 100,
                                currentLevel.boss.height + 100);
                                if (Intersections.intersectPixel(currentLevel.boss.sprite, missile.sprite, bossRect, missileRect))
                                {
                                    soundBossShield.Play();
                                    if (currentCursor == (int)CursorWeapon.missile || currentCursor == (int)CursorWeapon.onde || currentCursor == (int)CursorWeapon.nova)
                                        missile.alive = false;
                                    currentLevel.boss.bouclie.alive = true;
                                    currentLevel.boss.bouclie.position =
                                        currentLevel.boss.position + currentLevel.boss.center - currentLevel.boss.bouclie.center;
                                    currentLevel.bAlphaValue = 1;
                                    mFadeIncrement *= -1;
                                }
                            }
                        }
                    }
                }

                if (currentLevel.getLevel == 2)
                {
                    if (currentLevel.boss != null)
                    {
                        if (currentLevel.boss.alive)
                        {
                            Rectangle bossRect = new Rectangle(
                                (int)currentLevel.boss.position.X,
                                (int)currentLevel.boss.position.Y,
                                currentLevel.boss.width,
                                currentLevel.boss.height);
                            if (Intersections.intersectPixel(currentLevel.boss.sprite, missile.sprite, bossRect, missileRect))
                            {

                                currentLevel.boss.currentHealthPoint -= (int)missile.damage;
                                if (currentLevel.boss.currentHealthPoint <= 0)
                                {
                                    currentLevel.boss.alive = false;
                                    expInBuffer += (currentLevel.boss.dropedExp);
                                    for (int i = 0; i < 10; i++)
                                    {
                                        Explosion exp = new Explosion(content.Load<Texture2D>("Sprites\\explosion"), 18, 10);
                                        int randw = random.Next(0, (int)currentLevel.boss.width + 50);
                                        int randh = random.Next(0, (int)currentLevel.boss.height + 50);
                                        exp.destination = currentLevel.boss.position + new Vector2(randw - 50, randh - 50);
                                        explosions.Enqueue(exp);
                                    }
                                    exitToMenu = 4000;
                                    bossDead = true;

                                }

                            }
                        }
                    }
                }
            }
        }

        public void avionEnemiDead(AvionEnemi avionEnemi)
        {
            //fireMissileEnemi(avionEnemi, 10);
        }

        public void updateMissileEnemi()
        {
            foreach (List<AvionEnemi> avionEnemies in currentLevel.tabAvionEnemi)
            {
                if (avionEnemies != null)
                {
                    foreach (AvionEnemi avionEnemi in avionEnemies)
                    {
                        foreach (Missile missile in avionEnemi.missile)
                        {
                            if (missile.alive)
                            {
                                missile.position += missile.velocity;

                                if (!viewportRect.Contains(new Point(
                                    (int)missile.position.X,
                                    (int)missile.position.Y)))
                                {
                                    missile.alive = false;
                                }
                            }
                        }
                    }
                }
            }

            if (currentLevel.bossFight)
            {
                foreach (Missile[] missiles in currentLevel.boss.missiles)
                {
                    foreach (Missile missile in missiles)
                    {
                        if (missile.alive)
                        {
                            missile.position += missile.velocity;

                            if (!viewportRect.Contains(new Point(
                                (int)missile.position.X,
                                (int)missile.position.Y)))
                            {
                                missile.alive = false;
                            }
                        }
                    }
                }
            }
           
        }

        #region Update des backgrounds

        public void updateBackground2()
        {
            foreach (Font font in backgroundLvl2)
            {
                font.position += font.velocity;
            }
            Font b = backgroundLvl2.First<Font>();
            Rectangle bRect = new Rectangle(
                (int)b.position.X,
                (int)b.position.Y,
                b.sprite.Width,
                b.sprite.Height);
            Rectangle bRectOver2 = new Rectangle(
                (int)b.position.X,
                (int)b.position.Y,
                b.sprite.Width - 1024,
                b.sprite.Height);

            if (!viewportRect.Intersects(bRectOver2) && !addedQueueDone2)
            {
                Font font = new Font(thisbackgroundLvl2.sprite);
                addToQueueFont(backgroundLvl2, font, thisbackgroundLvl2.position + new Vector2(1024, 0) + thisbackgroundLvl2.velocity, thisbackgroundLvl2.velocity);
                addedQueueDone2 = true;
            }
            if (!viewportRect.Intersects(bRect))
            {
                backgroundLvl2.Dequeue();
                addedQueueDone2 = false;
            }
            
        }

        public void updateBackground3()
        {
            foreach (Font font in backgroundLvl3)
            {
                font.position += font.velocity;
            }
            Font b = backgroundLvl3.First<Font>();
            Rectangle bRect = new Rectangle(
                (int)b.position.X,
                (int)b.position.Y,
                b.sprite.Width,
                b.sprite.Height);
            Rectangle bRectOver2 = new Rectangle(
                (int)b.position.X,
                (int)b.position.Y,
                b.sprite.Width - 1024,
                b.sprite.Height);
            if (!viewportRect.Intersects(bRectOver2) && !addedQueueDone3)
            {
                Font font = new Font(thisbackgroundLvl3.sprite);
                addToQueueFont(backgroundLvl3, font, thisbackgroundLvl3.position + new Vector2(1024, 0) + thisbackgroundLvl3.velocity, thisbackgroundLvl3.velocity);
                addedQueueDone3 = true;

            }
            if (!viewportRect.Intersects(bRect))
            {
                backgroundLvl3.Dequeue();
                addedQueueDone3 = false;
            }

        }

        public void updateBackground4()
        {
            foreach (Font font in backgroundLvl4)
            {
                font.position += font.velocity;
            }
            Font b = backgroundLvl4.First<Font>();
            Rectangle bRect = new Rectangle(
                (int)b.position.X,
                (int)b.position.Y,
                b.sprite.Width,
                b.sprite.Height);
            Rectangle bRectOver2 = new Rectangle(
                (int)b.position.X,
                (int)b.position.Y,
                b.sprite.Width - 1024,
                b.sprite.Height);
            if (!viewportRect.Intersects(bRectOver2) && !addedQueueDone4)
            {
                Font font = new Font(thisbackgroundLvl4.sprite);
                addToQueueFont(backgroundLvl4, font, thisbackgroundLvl4.position + new Vector2(1024, 0) + thisbackgroundLvl4.velocity, thisbackgroundLvl4.velocity);
                addedQueueDone4 = true;

            }
            if (!viewportRect.Intersects(bRect))
            {
                backgroundLvl4.Dequeue();
                addedQueueDone4 = false;
            }

        }
        #endregion

 

        public void updateAvionEnemies()
        {
            foreach (List<AvionEnemi> avion in currentLevel.tabAvionEnemi)
            {
                if (!avionHeroAnimation)
                    checkCollision(avion);
            }

            if (currentLevel.bossFight)
            {
                if (currentLevel.boss != null)
                {
                    if(!avionHeroAnimation)
                        checkCollision(currentLevel.boss);
                }
            }

            
            
        }

        public void createSequence(List<AvionEnemi> avionList, int numberOfEnemies, int type, Vector2 positionStart)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector2 positionDecalage = new Vector2();
                positionDecalage.X = positionStart.X + i * 200;
                positionDecalage.Y = positionStart.Y;
                AvionEnemi avionEnemi = new AvionEnemi(this.content, type);
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
        }

        public void checkCollision(List<AvionEnemi> avionList)
        {
            Rectangle avionHeroRect = new Rectangle(
                (int)avionHero.position.X,
                (int)avionHero.position.Y,
                avionHero.width,
                avionHero.height);

            foreach (AvionEnemi avionEnemi in avionList)
            {
                if (avionEnemi.alive && !avionHero.invincible)
                {
                    Rectangle avionEnemiRect = new Rectangle(
                        (int)avionEnemi.position.X,
                        (int)avionEnemi.position.Y,
                        avionEnemi.width,
                        avionEnemi.height);
                    if (Intersections.intersectPixel(avionEnemi.sprite, avionHero.sprite, avionEnemiRect, avionHeroRect))
                    {
                        soundImpactAvions.Play();
                        avionHero.currentHealthPoint -= avionEnemi.damageCollision;
                        avionEnemi.alive = false;
                        int rand = random.Next(2, 4);
                        Explosion exp = new Explosion(content.Load<Texture2D>("Sprites\\explosion"+rand), 12, 3);
                        exp.destination = avionEnemi.position - new Vector2(0, exp.sprite.Height / 3);
                        explosions.Enqueue(exp);
                        if (avionHero.currentHealthPoint <= 0)
                        {
                            avionHero.currentHealthPoint = 0;
                            avionHero.alive = false;
                        }
                    }
                }
                foreach (Missile missile in avionEnemi.missile)
                {
                    if (missile.alive && !avionHero.invincible)
                    {
                        Rectangle missileRect = new Rectangle(
                            (int)missile.position.X,
                            (int)missile.position.Y,
                            missile.sprite.Width,
                            missile.sprite.Height);
                        if (Intersections.intersectPixel(missile.sprite, avionHero.sprite, missileRect, avionHeroRect))
                        {
                            soundImpact.Play();
                            avionHero.currentHealthPoint -= (int)missile.damage;
                            missile.alive = false;
                        }
                        if (avionHero.currentHealthPoint <= 0)
                        {
                            avionHero.alive = false;
                        }
                    }
                }

            }
        }

        public void checkCollision(Boss boss)
        {
            Rectangle avionHeroRect = new Rectangle(
                (int)avionHero.position.X,
                (int)avionHero.position.Y,
                avionHero.width,
                avionHero.height);

            foreach (Missile[] missiles in currentLevel.boss.missiles)
            {
                foreach (Missile missile in missiles)
                {
                    if (missile.alive && !avionHero.invincible)
                    {
                        Rectangle missileRect = new Rectangle(
                            (int)missile.position.X,
                            (int)missile.position.Y,
                            missile.sprite.Width,
                            missile.sprite.Height);
                        if (Intersections.intersectPixel(missile.sprite, avionHero.sprite, missileRect, avionHeroRect))
                        {
                            avionHero.currentHealthPoint -= (int)missile.damage;
                            missile.alive = false;
                        }
                        if (avionHero.currentHealthPoint <= 0)
                        {
                            avionHero.alive = false;
                        }
                    }
                }

                
            }
            if (currentLevel.boss.alive)
            {
                if (avionHero.timeToRecovery == 0 && !avionHero.invincible)
                {
                    Rectangle bossRect = new Rectangle(
                        (int)currentLevel.boss.position.X,
                        (int)currentLevel.boss.position.Y,
                        currentLevel.boss.width,
                        currentLevel.boss.height);
                    if (Intersections.intersectPixel(currentLevel.boss.sprite, avionHero.sprite, bossRect, avionHeroRect))
                    {
                        avionHero.currentHealthPoint -= currentLevel.boss.damageCollision;
                        if (avionHero.currentHealthPoint <= 0)
                        {
                            avionHero.alive = false;
                        }
                        avionHero.timeToRecovery = 2000;
                        avionHero.velocity = aimHero(currentLevel.boss.position, currentLevel.boss.center, 15);
                    }
                }
            }

        }

        public void updateAvionHero()
        {
            if (avionHero.newLvl)
            {
                soundLevelUp.Play();
                if(mFadeIncrement < 0)
                    mFadeIncrement *= -1;
                if (aFadeIncrement < 0)
                    aFadeIncrement *= -1;
                mAlphaValue = 1;
                aAlphaValue = 1;
                switch (avionHero.level)
                {
                    case 2:
                        temp = currentMissile;
                        missile2 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile2[i] = new Missile(content.Load<Texture2D>("Sprites\\missile2"), puissanceMissile2);
                        }
                        currentMissile = missile2;
                        currentPuissance = currentMissile.ElementAt(0).damage;
                        if(currentCursor == (int)CursorWeapon.missile)
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        avionHero.setNewVar(2);
                        armeShiningTab[0] = 1;
                        break;
                    case 3:

                        unlockFeu = true;
                        avionHero.setNewVar(3);
                        armeShiningTab[1] = 1;
                        break;
                    case 4:
                        temp = currentMissile;
                        missile3 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile3[i] = new Missile(content.Load<Texture2D>("Sprites\\missile3"), puissanceMissile3);
                        }
                        currentMissile = missile3;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(4);
                        armeShiningTab[0] = 1;
                        break;
                    case 5:

                        unlockNova = true;
                        avionHero.setNewVar(5);
                        armeShiningTab[2] = 1;
                        break;
                    case 6:
                        currentFeu = feu2;
                        if (currentCursor == (int)CursorWeapon.feu)
                        {
                            currentPuissance = currentFeu.damage;
                            currentDps = 60 * currentPuissance;
                        }
                        avionHero.setNewVar(6);
                        armeShiningTab[1] = 1;
                        break;
                    case 7:
                        temp = currentMissile;
                        missile4 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile4[i] = new Missile(content.Load<Texture2D>("Sprites\\missile4"), puissanceMissile4);
                        }
                        currentMissile = missile4;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(7);
                        armeShiningTab[0] = 1;
                        break;
                    case 8:
                        currentNova = nova2;
                        if (currentCursor == (int)CursorWeapon.nova)
                        {
                            currentPuissance = currentNova.damage;
                            currentDps = (1000 / intervalNova) * currentPuissance;
                        }
                        avionHero.setNewVar(8);
                        armeShiningTab[2] = 1;
                        break;
                    case 9:
                        temp = currentMissile;
                        missile5 = new Missile[(int)(maxMissile * 1.1f)];
                        for (int i = 0; i < (int)(maxMissile * 1.1f); i++)
                        {
                            missile5[i] = new Missile(content.Load<Texture2D>("Sprites\\missile5"), puissanceMissile5);
                        }
                        currentMissile = missile5;
                        vitesseMissile = vitesseMissile * 1.1f;
                        intervalMissile = intervalMissile / 1.1f;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(9);
                        armeShiningTab[0] = 1;
                        break;
                    case 10:

                        currentFeu.damage += 6;
                        currentNova.damage += 5;
                        unlockValve = true;
                        avionHero.setNewVar(10);
                        armeShiningTab[3] = 1;
                        break;
                    case 11:
                        currentFeu.damage += 1;
                        currentNova.damage += 5;
                        temp = currentMissile;
                        missile6 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile6[i] = new Missile(content.Load<Texture2D>("Sprites\\missile6"), puissanceMissile6);
                        }
                        currentMissile = missile6;

                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(11);
                        armeShiningTab[1] = 1;
                        armeShiningTab[2] = 1;
                        armeShiningTab[0] = 1;
                        break;
                    case 12:
                        currentFeu = feu3;
                        if (currentCursor == (int)CursorWeapon.feu)
                        {
                            currentPuissance = currentFeu.damage;
                            currentDps = 60 * currentPuissance;
                        }
                        avionHero.setNewVar(12);
                        armeShiningTab[1] = 1;
                        break;
                    case 13:
                        currentNova = nova3;
                        if (currentCursor == (int)CursorWeapon.nova)
                        {
                            currentPuissance = currentNova.damage;
                            currentDps = (1000 / intervalNova) * currentPuissance;
                        }
                        avionHero.setNewVar(13);
                        armeShiningTab[2] = 1;
                        break;
                    case 14:
                        temp = currentMissile;
                        missile7 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile7[i] = new Missile(content.Load<Texture2D>("Sprites\\missile7"), puissanceMissile7);
                        }
                        currentMissile = missile7;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(14);
                        armeShiningTab[0] = 1;
                        break;
                    case 15:
                        foreach (Missile mis in currentValve)
                        {
                            mis.damage += 15;
                        }
                        currentNova.damage += 50;
                        unlockOnde = true;
                        avionHero.setNewVar(15);
                        armeShiningTab[2] = 1;
                        armeShiningTab[3] = 1;
                        armeShiningTab[4] = 1;
                        avionHero.sprite = content.Load<Texture2D>("Sprites\\avion2");
                        break;
                    case 16:
                        currentFeu = feu4;
                        if (currentCursor == (int)CursorWeapon.feu)
                        {
                            currentPuissance = currentFeu.damage;
                            currentDps = 60 * currentPuissance;
                        }
                        avionHero.setNewVar(16);
                        armeShiningTab[1] = 1;
                        break;
                    case 17:
                        
                        temp = currentMissile;
                        missile8 = new Missile[(int)(maxMissile * 1.1f)];
                        for (int i = 0; i < (int)(maxMissile * 1.1f); i++)
                        {
                            missile8[i] = new Missile(content.Load<Texture2D>("Sprites\\missile8"), puissanceMissile8);
                        }
                        currentMissile = missile8;
                        vitesseMissile = vitesseMissile * 1.1f;
                        intervalMissile = intervalMissile / 1.1f;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(17);
                        armeShiningTab[0] = 1;
                        break;
                    case 18:
                        foreach (Missile mis in currentValve)
                        {
                            mis.type = 1;
                            mis.damage = puissanceValve2;
                        }
                        currentNova = nova4;
                        if (currentCursor == (int)CursorWeapon.nova)
                        {
                            currentPuissance = currentNova.damage;
                            currentDps = (1000 / intervalNova) * currentPuissance;
                        }
                        
                        avionHero.setNewVar(18);
                        armeShiningTab[3] = 1;
                        armeShiningTab[4] = 1;
                        break;
                    case 19:
                        temp = currentMissile;
                        missile9 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile9[i] = new Missile(content.Load<Texture2D>("Sprites\\missile9"), puissanceMissile9);
                        }
                        currentMissile = missile9;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(19);
                        armeShiningTab[0] = 1;
                        break;
                    case 20:
                        ultimValve = true;
                        foreach (Missile mis in currentValve)
                        {
                            mis.damage = puissanceValve3;
                        }
                        foreach (Missile mis in ondeUp)
                        {
                            mis.damage = puissanceOnde2;
                        }
                        foreach (Missile mis in ondeDown)
                        {
                            mis.damage = puissanceOnde2;
                        }
                        avionHero.setNewVar(20);
                        armeShiningTab[3] = 1;
                        armeShiningTab[4] = 1;
                        break;

                }
                avionHero.newLvl = false;
            }
            if (avionHero.alive == false)
            {

            }
        }

        public void loadArmory()
        {
            for (int k = 0; k < avionHero.level; k++)
            {
                switch (k+1)
                {
                    case 2:
                        temp = currentMissile;
                        missile2 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile2[i] = new Missile(content.Load<Texture2D>("Sprites\\missile2"), puissanceMissile2);
                        }
                        currentMissile = missile2;
                        currentPuissance = currentMissile.ElementAt(0).damage;
                        if (currentCursor == (int)CursorWeapon.missile)
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        avionHero.setNewVar(2);
                        break;
                    case 3:

                        unlockFeu = true;
                        avionHero.setNewVar(3);
                        break;
                    case 4:
                        temp = currentMissile;
                        missile3 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile3[i] = new Missile(content.Load<Texture2D>("Sprites\\missile3"), puissanceMissile3);
                        }
                        currentMissile = missile3;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(4);
                        break;
                    case 5:

                        unlockNova = true;
                        avionHero.setNewVar(5);
                        break;
                    case 6:
                        currentFeu = feu2;
                        if (currentCursor == (int)CursorWeapon.feu)
                        {
                            currentPuissance = currentFeu.damage;
                            currentDps = 60 * currentPuissance;
                        }
                        avionHero.setNewVar(6);
                        break;
                    case 7:
                        temp = currentMissile;
                        missile4 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile4[i] = new Missile(content.Load<Texture2D>("Sprites\\missile4"), puissanceMissile4);
                        }
                        currentMissile = missile4;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(7);
                        break;
                    case 8:
                        currentNova = nova2;
                        if (currentCursor == (int)CursorWeapon.nova)
                        {
                            currentPuissance = currentNova.damage;
                            currentDps = (1000 / intervalNova) * currentPuissance;
                        }
                        avionHero.setNewVar(8);
                        break;
                    case 9:
                        temp = currentMissile;
                        missile5 = new Missile[(int)(maxMissile * 1.1f)];
                        for (int i = 0; i < (int)(maxMissile * 1.1f); i++)
                        {
                            missile5[i] = new Missile(content.Load<Texture2D>("Sprites\\missile5"), puissanceMissile5);
                        }
                        currentMissile = missile5;
                        vitesseMissile = vitesseMissile * 1.1f;
                        intervalMissile = intervalMissile / 1.1f;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(9);
                        break;
                    case 10:

                        currentFeu.damage += 6;
                        currentNova.damage += 5;
                        unlockValve = true;
                        avionHero.setNewVar(10);
                        break;
                    case 11:
                        currentFeu.damage += 1;
                        currentNova.damage += 5;
                        temp = currentMissile;
                        missile6 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile6[i] = new Missile(content.Load<Texture2D>("Sprites\\missile6"), puissanceMissile6);
                        }
                        currentMissile = missile6;

                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(11);
                        break;
                    case 12:
                        currentFeu = feu3;
                        if (currentCursor == (int)CursorWeapon.feu)
                        {
                            currentPuissance = currentFeu.damage;
                            currentDps = 60 * currentPuissance;
                        }
                        avionHero.setNewVar(12);
                        break;
                    case 13:
                        currentNova = nova3;
                        if (currentCursor == (int)CursorWeapon.nova)
                        {
                            currentPuissance = currentNova.damage;
                            currentDps = (1000 / intervalNova) * currentPuissance;
                        }
                        avionHero.setNewVar(13);
                        break;
                    case 14:
                        temp = currentMissile;
                        missile7 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile7[i] = new Missile(content.Load<Texture2D>("Sprites\\missile7"), puissanceMissile7);
                        }
                        currentMissile = missile7;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(14);
                        break;
                    case 15:
                        foreach (Missile mis in currentValve)
                        {
                            mis.damage += 15;
                        }
                        currentNova.damage += 50;
                        unlockOnde = true;
                        avionHero.setNewVar(15);
                        break;
                    case 16:
                        currentFeu = feu4;
                        if (currentCursor == (int)CursorWeapon.feu)
                        {
                            currentPuissance = currentFeu.damage;
                            currentDps = 60 * currentPuissance;
                        }
                        avionHero.setNewVar(16);
                        break;
                    case 17:

                        temp = currentMissile;
                        missile8 = new Missile[(int)(maxMissile * 1.1f)];
                        for (int i = 0; i < (int)(maxMissile * 1.1f); i++)
                        {
                            missile8[i] = new Missile(content.Load<Texture2D>("Sprites\\missile8"), puissanceMissile8);
                        }
                        currentMissile = missile8;
                        vitesseMissile = vitesseMissile * 1.1f;
                        intervalMissile = intervalMissile / 1.1f;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(17);
                        break;
                    case 18:
                        foreach (Missile mis in currentValve)
                        {
                            mis.type = 1;
                            mis.damage = puissanceValve2;
                        }
                        currentNova = nova4;
                        if (currentCursor == (int)CursorWeapon.nova)
                        {
                            currentPuissance = currentNova.damage;
                            currentDps = (1000 / intervalNova) * currentPuissance;
                        }

                        avionHero.setNewVar(18);
                        break;
                    case 19:
                        temp = currentMissile;
                        missile9 = new Missile[maxMissile];
                        for (int i = 0; i < maxMissile; i++)
                        {
                            missile9[i] = new Missile(content.Load<Texture2D>("Sprites\\missile9"), puissanceMissile9);
                        }
                        currentMissile = missile9;
                        if (currentCursor == (int)CursorWeapon.missile)
                        {
                            currentPuissance = currentMissile.ElementAt(0).damage;
                            currentDps = (1000 / intervalMissile) * currentPuissance;
                        }
                        avionHero.setNewVar(19);
                        break;
                    case 20:
                        ultimValve = true;
                        foreach (Missile mis in currentValve)
                        {
                            mis.damage = puissanceValve3;
                        }
                        foreach (Missile mis in ondeUp)
                        {
                            mis.damage = puissanceOnde2;
                        }
                        foreach (Missile mis in ondeDown)
                        {
                            mis.damage = puissanceOnde2;
                        }
                        avionHero.setNewVar(20);
                        break;
                }
            }
        }

        public void saveGame()
        {
            if (avionHero != null && MainMenuScreen.lms != null)
            {

                XElement xElement =
                    new XElement("Save",
                        new XElement("lvl2locked", MainMenuScreen.lms.lvl2locked),
                        new XElement("lvl3locked", MainMenuScreen.lms.lvl3locked),
                        new XElement("lvl4locked", MainMenuScreen.lms.lvl4locked),
                        new XElement("XP", avionHero.currentExp),
                        new XElement("Lvl", avionHero.level),
                        new XElement("Godmode",Program.godMode),
                        new XElement("DeadCount", deadCount),
                        new XElement("EnemiKilled", enemiKilled),
                        new XElement("TimePlayed", timePlayed)

                        );

                xElement.Save("Content\\Save\\save.xml");
            }

        }

        
        public override void Draw(GameTime gameTime)
        {
            if (!pause)
            {
                ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                #region Draw Background

                if (backgroundToUse >= 3)
                {
                    foreach (Font font in backgroundLvl2)
                    {
                        spriteBatch.Draw(font.sprite, font.position, Color.White);
                    }

                    foreach (Font font in backgroundLvl3)
                    {
                        spriteBatch.Draw(font.sprite, font.position, Color.White);
                    }

                    foreach (Font font in backgroundLvl4)
                    {
                        spriteBatch.Draw(font.sprite, font.position, Color.White);
                    }
                }
                else if (backgroundToUse >= 2)
                {
                    foreach (Font font in backgroundLvl2)
                    {
                        spriteBatch.Draw(font.sprite, font.position, Color.White);
                    }

                    foreach (Font font in backgroundLvl3)
                    {
                        spriteBatch.Draw(font.sprite, font.position, Color.White);
                    }
                }
                else if (backgroundToUse >= 1)
                {
                    foreach (Font font in backgroundLvl2)
                    {
                        spriteBatch.Draw(font.sprite, font.position, Color.White);
                    }
                }


                #endregion

                #region top
                spriteBatch.Draw(fontMenu.sprite, fontMenu.position, Color.White);
                spriteBatch.DrawString(lvlFont, "LVL : " + avionHero.level, new Vector2(50, 5), Color.Gold);
                spriteBatch.Draw(expBar, new Vector2(50, 35), Color.White);
                spriteBatch.Draw(expBar, new Vector2(51, 36), new Rectangle(
                    51, 36, (int)(expBar.Width * (((float)avionHero.currentExp - avionHero.oldLvlExp) / ((float)avionHero.nextLvl - avionHero.oldLvlExp))) - 2, expBar.Height - 2),
                    Color.PowderBlue);
                spriteBatch.DrawString(lvlFont, "Vie : ", new Vector2(270, 5), Color.White);
                spriteBatch.DrawString(statFont, "Puissance : " + currentPuissance + " ( DPS : " + (int)currentDps + ")", new Vector2(50, 50), Color.Black);
                #endregion

                if (mAlphaValue > 0)
                {
                    spriteBatch.Draw(shining,
                        new Vector2(avionHero.position.X + avionHero.center.X - shining.Width / 2, avionHero.position.Y + avionHero.center.Y - shining.Height / 2),
                        new Color(255, 255, 255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));


                    mAlphaValue += mFadeIncrement;
                    if (mAlphaValue >= 255)
                    {
                        mFadeIncrement *= -1;
                    }
                }

                Color color = Color.White;
                if (avionHero.timeToRecovery > 0)
                    color = Color.Red;

                if (avionHero.state == (int)stateMovement.none)
                {
                    spriteBatch.Draw(avionHero.sprite, avionHero.position,
                        new Rectangle(0, 0, avionHero.width, avionHero.height),
                        color);
                }
                if (avionHero.state == (int)stateMovement.down)
                {
                    spriteBatch.Draw(avionHero.sprite, avionHero.position,
                        new Rectangle(0, avionHero.height, avionHero.width, avionHero.height),
                        color);
                }
                if (avionHero.state == (int)stateMovement.up)
                {
                    spriteBatch.Draw(avionHero.sprite, avionHero.position,
                        new Rectangle(0, avionHero.height * 2, avionHero.width, avionHero.height),
                        color);
                }
                if (mAlphaValue > 0)
                {
                    if (avionHero.state == (int)stateMovement.none)
                    {
                        spriteBatch.Draw(avionHero.sprite, avionHero.position,
                            new Rectangle(0, avionHero.height * 0, avionHero.width, avionHero.height),
                            new Color(255, 255, 0, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                    }
                    if (avionHero.state == (int)stateMovement.down)
                    {
                        spriteBatch.Draw(avionHero.sprite, avionHero.position,
                            new Rectangle(0, avionHero.height * 1, avionHero.width, avionHero.height),
                            new Color(255, 255, 0, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                    }
                    if (avionHero.state == (int)stateMovement.up)
                    {
                        spriteBatch.Draw(avionHero.sprite, avionHero.position,
                            new Rectangle(0, avionHero.height * 2, avionHero.width, avionHero.height),
                            new Color(255, 255, 0, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
                    }
                }

                foreach (Missile missile in currentMissile)
                {
                    if (missile.alive)
                    {
                        spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                    }
                }


                foreach (Missile missile in temp)
                {
                    if (missile != null)
                    {
                        if (missile.alive)
                        {
                            spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                        }
                    }
                }

                foreach (List<AvionEnemi> list in currentLevel.tabAvionEnemi)
                {
                    foreach (AvionEnemi avionEnemi in list)
                    {
                        if (avionEnemi.alive)
                        {
                            if (avionEnemi.state == (int)stateMovement.none)
                            {
                                spriteBatch.Draw(avionEnemi.sprite, avionEnemi.position,
                                    new Rectangle(0, avionEnemi.height * 0, avionEnemi.width, avionEnemi.height),
                                    Color.White);
                            }
                            if (avionEnemi.state == (int)stateMovement.down)
                            {
                                spriteBatch.Draw(avionEnemi.sprite, avionEnemi.position,
                                    new Rectangle(0, avionEnemi.height * 1, avionEnemi.width, avionEnemi.height),
                                    Color.White);
                            }
                            if (avionEnemi.state == (int)stateMovement.up)
                            {
                                spriteBatch.Draw(avionEnemi.sprite, avionEnemi.position,
                                    new Rectangle(0, avionEnemi.height * 2, avionEnemi.width, avionEnemi.height),
                                    Color.White);
                            }
                        }

                        foreach (Missile missile in avionEnemi.missile)
                        {
                            if (missile.alive)
                            {
                                spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                            }
                        }
                    }
                }

                if (currentLevel.bossFight)
                {
                    bool endFight = false;
                    if (currentLevel.boss.alive)
                    {
                        if (currentLevel.boss.state == (int)stateMovement.none)
                        {
                            spriteBatch.Draw(currentLevel.boss.sprite, currentLevel.boss.position,
                                new Rectangle(0, currentLevel.boss.healthPoint * 0, currentLevel.boss.width, currentLevel.boss.height),
                                Color.White);
                        }
                        if (currentLevel.boss.state == (int)stateMovement.forward)
                        {
                            spriteBatch.Draw(currentLevel.boss.sprite, currentLevel.boss.position,
                                new Rectangle(0, currentLevel.boss.height * 1, currentLevel.boss.width, currentLevel.boss.height),
                                Color.White);
                        }
                        if (currentLevel.boss.state == (int)stateMovement.backward)
                        {
                            spriteBatch.Draw(currentLevel.boss.sprite, currentLevel.boss.position,
                                new Rectangle(0, currentLevel.boss.height * 2, currentLevel.boss.width, currentLevel.boss.height),
                                Color.White);
                        }
                    }
                    else
                    {
                        endFight = true;
                    }
                    foreach (Missile[] missiles in currentLevel.boss.missiles)
                    {
                        foreach (Missile missile in missiles)
                        {
                            if (missile.alive)
                            {
                                if (!missile.direction)
                                {
                                    spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                                }
                                else
                                {

                                    spriteBatch.Draw(missile.sprite, missile.position, null, Color.White,
                                        (float)Math.Atan2(currentLevel.boss.targetHero.Y, currentLevel.boss.targetHero.X),
                                        missile.center, 1, SpriteEffects.None, 0); // pour que le missile pointe vers le hero
                                }
                                endFight = false;
                            }
                        }

                        if (currentLevel.getLevel == 1)
                        {
                            if (currentLevel.boss.bouclie.alive)
                            {
                                if (currentLevel.bAlphaValue > 0)
                                {
                                    spriteBatch.Draw(currentLevel.boss.bouclie.sprite,
                                        currentLevel.boss.bouclie.position,
                                        new Color(255, 255, 255, (byte)MathHelper.Clamp(currentLevel.bAlphaValue, 0, 255)));


                                    currentLevel.bAlphaValue += mFadeIncrement;
                                    if (currentLevel.bAlphaValue >= 255)
                                    {
                                        mFadeIncrement *= -1;
                                    }
                                }
                            }
                        }


                    }
                    if (endFight)
                    {
                        currentLevel.bossFight = false;
                    }
                }


                int timesToDequeue = 0;
                foreach (Explosion exp in explosions)
                {
                    if (exp.alive)
                    {
                        spriteBatch.Draw(exp.sprite, exp.destination, exp.sourceRect, Color.White);
                        exp.update();
                    }
                    else
                    {
                        timesToDequeue += 1;
                    }
                }

                for (int i = 0; i < timesToDequeue; i++)
                {
                    explosions.Dequeue();
                }


                if (currentFeu.alive)
                {
                    //spriteBatch.Draw(feu1.sprite, feu1.position + new Vector2(-138, -56), Color.White);
                    int inclinaison = random.Next(-2, 2);
                    float inclinaison2 = (float)inclinaison / 20.0f;
                    int taille = random.Next(90, 120);
                    float taille2 = (float)taille / 100.0f;
                    spriteBatch.Draw(currentFeu.sprite, currentFeu.position +
                        new Vector2(currentFeu.sprite.Width, currentFeu.sprite.Height / 2), null, Color.White, inclinaison2,
                        new Vector2(currentFeu.sprite.Width, currentFeu.sprite.Height / 2), taille2, SpriteEffects.None, 0);
                }

                if (currentNova.visible)
                {
                    int taille = random.Next(98, 103);
                    float taille2 = (float)taille / 100.0f; // la taille varie legerement aléatoirement
                    spriteBatch.Draw(currentNova.sprite, currentNova.position + currentNova.center, null, Color.White, novaMouvement, currentNova.center, taille2, SpriteEffects.None, 0);

                    int k = random.Next(1, 100);
                    float k2 = (float)k / 100.0f;
                    novaMouvement += 0.05f * k2; // ici on génere un mouvement avec une vitesse aléatoir
                    currentNova.alive = false;
                }

                if (openedValve)
                {
                    spriteBatch.Draw(valveDroite, new Vector2(
                        avionHero.position.X + avionHero.width,
                        avionHero.position.Y + avionHero.center.Y - valveDroite.Height / 2), Color.White);
                    if (ultimValve)
                    {
                        spriteBatch.Draw(valveGauche, new Vector2(
                           avionHero.position.X - valveGauche.Width,
                           avionHero.position.Y + avionHero.center.Y - valveGauche.Height / 2), Color.White);
                    }
                }

                foreach (Missile missile in currentValve)
                {
                    if (missile.alive)
                    {
                        spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                    }

                }

                if (ultimValve)
                {
                    foreach (Missile missile in currentUltimValve)
                    {
                        if (missile.alive)
                        {
                            spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                        }
                    }
                }

                foreach (Missile missile in ondeUp)
                {
                    if (missile != null)
                    {
                        if (missile.alive)
                        {
                            //*
                            spriteBatch.Draw(missile.sprite, missile.position,
                                null, Color.White, 0,
                                //new Vector2(missile.center.X, missile.center.Y),
                                Vector2.Zero,
                                missile.scale, SpriteEffects.None, 0);
                            //*/
                            //spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                        }
                    }
                }
                foreach (Missile missile in ondeDown)
                {
                    if (missile != null)
                    {
                        if (missile.alive)
                        {
                            //*
                            spriteBatch.Draw(missile.sprite, missile.position,
                                null, Color.White, 0,
                                //new Vector2(missile.center.X, missile.center.Y),
                                Vector2.Zero,
                                missile.scale, SpriteEffects.None, 0);
                            //*/
                            //spriteBatch.Draw(missile.sprite, missile.position, Color.White);
                        }
                    }
                }


                #region Hero Health Bar

               

                if (!Program.godMode)
                {
                    spriteBatch.Draw(heroHealthBar, new Vector2(avionHero.position.X + (avionHero.width / 2) - (heroHealthBar.Width / 2),
                    avionHero.position.Y + avionHero.height),
                    Color.White);
                }

                    if (((float)avionHero.currentHealthPoint / (float)avionHero.healthPoint) >= 75.0f / 100.0f)
                    {
                        if (!Program.godMode)
                        {
                            spriteBatch.Draw(heroHealthBar, new Vector2(
                                    avionHero.position.X + (avionHero.width / 2) - (heroHealthBar.Width / 2) + 1,
                                    avionHero.position.Y + avionHero.height + 1),
                                    new Rectangle(
                                        (int)avionHero.position.X + (avionHero.width / 2) - (heroHealthBar.Width / 2) + 1,
                                        (int)avionHero.position.Y + avionHero.height + 1,
                                        (int)(heroHealthBar.Width * ((float)avionHero.currentHealthPoint / (float)avionHero.healthPoint)) - 2,
                                        heroHealthBar.Height - 2),
                                    Color.Green);
                        }

                        spriteBatch.DrawString(lvlFont, avionHero.currentHealthPoint + " / " + avionHero.healthPoint,
                            new Vector2(270, 35), Color.Green);
                    }
                    if (((float)avionHero.currentHealthPoint / (float)avionHero.healthPoint) >= 30.0f / 100.0f &&
                        ((float)avionHero.currentHealthPoint / (float)avionHero.healthPoint) < 75.0f / 100.0f)
                    {
                        if (!Program.godMode)
                        {
                            spriteBatch.Draw(heroHealthBar, new Vector2(
                                    avionHero.position.X + (avionHero.width / 2) - (heroHealthBar.Width / 2) + 1,
                                    avionHero.position.Y + avionHero.height + 1),
                                    new Rectangle(
                                        (int)avionHero.position.X + (avionHero.width / 2) - (heroHealthBar.Width / 2) + 1,
                                        (int)avionHero.position.Y + avionHero.height + 1,
                                        (int)(heroHealthBar.Width * ((float)avionHero.currentHealthPoint / (float)avionHero.healthPoint)) - 2,
                                        heroHealthBar.Height - 2),
                                    Color.Yellow);
                        }
                        spriteBatch.DrawString(lvlFont, avionHero.currentHealthPoint + " / " + avionHero.healthPoint,
                       new Vector2(270, 35), Color.Yellow);
                    }
                    if (((float)avionHero.currentHealthPoint / (float)avionHero.healthPoint) < 30.0f / 100.0f)
                    {
                        if (!Program.godMode)
                        {
                            spriteBatch.Draw(heroHealthBar, new Vector2(
                                    avionHero.position.X + (avionHero.width / 2) - (heroHealthBar.Width / 2) + 1,
                                    avionHero.position.Y + avionHero.height + 1),
                                    new Rectangle(
                                        (int)avionHero.position.X + (avionHero.width / 2) - (heroHealthBar.Width / 2) + 1,
                                        (int)avionHero.position.Y + avionHero.height + 1,
                                        (int)(heroHealthBar.Width * ((float)avionHero.currentHealthPoint / (float)avionHero.healthPoint)) - 2,
                                        heroHealthBar.Height - 2),
                                    Color.Red);
                        }
                        spriteBatch.DrawString(lvlFont, avionHero.currentHealthPoint + " / " + avionHero.healthPoint,
                       new Vector2(270, 35), Color.Red);
                    }
                
                #endregion

                #region Enemies Health bar

                foreach (List<AvionEnemi> avionEnemies in currentLevel.tabAvionEnemi)
                {
                    foreach (AvionEnemi avionEnemi in avionEnemies)
                    {
                        if (avionEnemi.alive)
                        {
                            spriteBatch.Draw(heroHealthBar, new Vector2(avionEnemi.position.X + (avionEnemi.width / 2) - (heroHealthBar.Width / 2),
                                avionEnemi.position.Y + avionEnemi.height),
                            Color.White);
                            if (((float)avionEnemi.currentHealthPoint / (float)avionEnemi.healthPoint) >= 75.0f / 100.0f)
                            {
                                spriteBatch.Draw(heroHealthBar, new Vector2(
                                        avionEnemi.position.X + (avionEnemi.width / 2) - (heroHealthBar.Width / 2) + 1,
                                        avionEnemi.position.Y + avionEnemi.height + 1),
                                        new Rectangle(
                                            (int)avionEnemi.position.X + (avionEnemi.width / 2) - (heroHealthBar.Width / 2) + 1,
                                            (int)avionEnemi.position.Y + avionEnemi.height + 1,
                                            (int)(heroHealthBar.Width * ((float)avionEnemi.currentHealthPoint / (float)avionEnemi.healthPoint)) - 2,
                                            heroHealthBar.Height - 2),
                                        Color.Green);
                            }
                            if (((float)avionEnemi.currentHealthPoint / (float)avionEnemi.healthPoint) >= 30.0f / 100.0f &&
                                ((float)avionEnemi.currentHealthPoint / (float)avionEnemi.healthPoint) < 75.0f / 100.0f)
                            {
                                spriteBatch.Draw(heroHealthBar, new Vector2(
                                        avionEnemi.position.X + (avionEnemi.width / 2) - (heroHealthBar.Width / 2) + 1,
                                        avionEnemi.position.Y + avionEnemi.height + 1),
                                        new Rectangle(
                                            (int)avionEnemi.position.X + (avionEnemi.width / 2) - (heroHealthBar.Width / 2) + 1,
                                            (int)avionEnemi.position.Y + avionEnemi.height + 1,
                                            (int)(heroHealthBar.Width * ((float)avionEnemi.currentHealthPoint / (float)avionEnemi.healthPoint)) - 2,
                                            heroHealthBar.Height - 2),
                                        Color.Yellow);
                            }
                            if (((float)avionEnemi.currentHealthPoint / (float)avionEnemi.healthPoint) < 30.0f / 100.0f)
                            {
                                spriteBatch.Draw(heroHealthBar, new Vector2(
                                        avionEnemi.position.X + (avionEnemi.width / 2) - (heroHealthBar.Width / 2) + 1,
                                        avionEnemi.position.Y + avionEnemi.height + 1),
                                        new Rectangle(
                                            (int)avionEnemi.position.X + (avionEnemi.width / 2) - (heroHealthBar.Width / 2) + 1,
                                            (int)avionEnemi.position.Y + avionEnemi.height + 1,
                                            (int)(heroHealthBar.Width * ((float)avionEnemi.currentHealthPoint / (float)avionEnemi.healthPoint)) - 2,
                                            heroHealthBar.Height - 2),
                                        Color.Red);
                            }
                        }
                    }
                }


                #endregion

                #region Boss Health Bar

                if (currentLevel.bossFight)
                {
                    spriteBatch.Draw(bossHealthBar, new Vector2(810, 25),
                        Color.White);
                    String bossName = "";
                    String bossAstuce = "";
                    if (currentLevel.getLevel == 1)
                    {
                        bossName = "Noraj";
                        bossAstuce = "Noraj est invincible quand son bouclie \nest actif";
                    }
                    else if (currentLevel.getLevel == 2)
                    {
                        bossName = "Manstre";
                        bossAstuce = "Manstre absorbe ondes et empeche d'activer \nles canons, ceci lui cause des degats a lui meme";
                    }
                    if (currentLevel.getLevel == 3)
                    {
                        bossName = "Dude";
                        bossAstuce = "Dude est invincible quand son bouclie \nest actif";
                    }
                    else if (currentLevel.getLevel == 4)
                    {
                        bossName = "Plizzz";
                        bossAstuce = "Plizzz absorbe ondes et empeche d'activer \nles canons, ceci lui cause des degats a lui meme";
                    }
                    spriteBatch.DrawString(info, "BOSS : " + bossName, new Vector2(810, 1), Color.White);
                    spriteBatch.DrawString(infoBoss, bossAstuce, new Vector2(755, 45), Color.White);

                    if (((float)currentLevel.boss.currentHealthPoint / (float)currentLevel.boss.healthPoint) >= 75.0f / 100.0f)
                    {
                        spriteBatch.Draw(bossHealthBar, new Vector2(813, 28),
                                new Rectangle(810 + 3, 45 + 3,
                                    (int)(bossHealthBar.Width * ((float)currentLevel.boss.currentHealthPoint / (float)currentLevel.boss.healthPoint)) - 6,
                                    bossHealthBar.Height - 6),
                                Color.Blue);
                    }
                    if (((float)currentLevel.boss.currentHealthPoint / (float)currentLevel.boss.healthPoint) >= 30.0f / 100.0f &&
                        ((float)currentLevel.boss.currentHealthPoint / (float)currentLevel.boss.healthPoint) < 75.0f / 100.0f)
                    {
                        spriteBatch.Draw(bossHealthBar, new Vector2(813, 28),
                                new Rectangle(810 + 3, 45 + 3,
                                    (int)(bossHealthBar.Width * ((float)currentLevel.boss.currentHealthPoint / (float)currentLevel.boss.healthPoint)) - 6,
                                    bossHealthBar.Height - 6),
                                Color.Purple);
                    }
                    if (((float)currentLevel.boss.currentHealthPoint / (float)currentLevel.boss.healthPoint) < 30.0f / 100.0f)
                    {
                        spriteBatch.Draw(bossHealthBar, new Vector2(813, 28),
                                new Rectangle(810 + 3, 45 + 3,
                                    (int)(bossHealthBar.Width * ((float)currentLevel.boss.currentHealthPoint / (float)currentLevel.boss.healthPoint)) - 6,
                                    bossHealthBar.Height - 6),
                                Color.Red);
                    }
                }

                #endregion

                #region gestion des armes
                if (currentCursor == (int)CursorWeapon.missile)
                {
                    spriteBatch.Draw(armesMissileActif, new Vector2(420, 15), Color.White);
                }
                else
                {
                    spriteBatch.Draw(armesMissileInactif, new Vector2(420, 15), Color.White);
                }

                if (unlockFeu)
                {
                    if (currentCursor == (int)CursorWeapon.feu)
                    {
                        spriteBatch.Draw(armesFeuActif, new Vector2(490, 15), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(armesFeuInactif, new Vector2(490, 15), Color.White);
                    }
                }
                else
                {
                    spriteBatch.Draw(armesFeuNA, new Vector2(490, 15), Color.White);
                }

                if (unlockNova)
                {
                    if (currentCursor == (int)CursorWeapon.nova)
                    {
                        spriteBatch.Draw(armesNovaActif, new Vector2(560, 15), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(armesNovaInactif, new Vector2(560, 15), Color.White);
                    }
                }
                else
                {
                    spriteBatch.Draw(armesNovaNA, new Vector2(560, 15), Color.White);
                }
                if (unlockValve)
                {
                    if (currentCursor == (int)CursorWeapon.valve)
                    {
                        spriteBatch.Draw(armesValveActif, new Vector2(630, 15), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(armesValveInactif, new Vector2(630, 15), Color.White);
                    }
                }
                else
                {
                    spriteBatch.Draw(armesValveNA, new Vector2(630, 15), Color.White);
                }
                if (unlockOnde)
                {
                    if (currentCursor == (int)CursorWeapon.onde)
                    {
                        spriteBatch.Draw(armesOndeActif, new Vector2(700, 15), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(armesOndeInactif, new Vector2(700, 15), Color.White);
                    }
                }
                else
                {
                    spriteBatch.Draw(armesOndeNA, new Vector2(700, 15), Color.White);
                }
                //spriteBatch.Draw(contour, new Vector2(560, 15), Color.White);
                //spriteBatch.Draw(contour, new Vector2(630, 15), Color.White);
                //spriteBatch.Draw(contour, new Vector2(690, 15), Color.White);

                if (aAlphaValue > 0)
                {
                    int[] iTemp = new int[5];
                    for (int i = 0; i < armeShiningTab.Count(); i++)
                    {
                        if (armeShiningTab[i] != 0)
                        {
                            iTemp[i] = 1;
                            int j = 420;
                            j += (70 * i);

                            spriteBatch.Draw(armeShining, new Vector2(j, 15),
                                new Color(255, 255, 255, (byte)MathHelper.Clamp(aAlphaValue, 0, 255)));
                        }

                    }
                    aAlphaValue += aFadeIncrement;
                    if (aAlphaValue >= 255)
                        aFadeIncrement *= -1;
                    if (aAlphaValue <= 0)
                    {
                        for (int k = 0; k < iTemp.Count(); k++)
                        {
                            if (iTemp[k] == 1)
                            {
                                armeShiningTab[k] = 0;
                            }
                        }
                    }


                }

                #endregion

                spriteBatch.End();

                base.Draw(gameTime);
            }
        }
    }
}
