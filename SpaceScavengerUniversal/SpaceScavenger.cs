using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation.Metadata;
using Windows.Media.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceScavengerUniversal;

namespace Space_Scavenger
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SpaceScavenger : Game
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();
        private readonly List<PowerUp> _powerups = new List<PowerUp>();
        public readonly List<BombEnemy> bombships = new List<BombEnemy>();
        public readonly List<BossEnemy> bosses = new List<BossEnemy>();
        public readonly List<Shot> BossShots = new List<Shot>();
        public readonly List<TreasureShip> treasureShips = new List<TreasureShip>();
        private Camera _camera;
        private GameOverScreen _gameOverScreen;
        private string _inRangeToBuyString = "";
        private Texture2D _powerUpHealth, _Shield;
        public KeyboardState _previousKbState;
        public GamePadState _previousGpState;
        public Shop _shop;
        private int _shoptimer;
        private StartMenu _startMenu;
        private UserInterface _ui;
        private WinScreen _winScreen;
        //public SoundEffect Assault;
        private AsteroidComponent asteroid;
        //public Song BackgroundSong;
        private Texture2D backgroundTexture;
        public BombEnemy BombEnemy;
        private Texture2D BombEnemyTexture;
        public Boost boost;
        public BossCompass bosscompass;
        public bool BossKill;
        public Texture2D BossShotTexture;
        public Texture2D BossShotTexture2;
        private Texture2D bossTexture;
        public Compass compass;
        public int defeatedEnemies;
        private Effects effects;
        private int enemyAmountTimer = 600;
        private Texture2D enemyDamage;
        private bool enemyHit;
        private Texture2D enemyLaserTexture, turorialTexture2D;
        private Vector2 enemyPositionExplosion = new Vector2(0, 0);

       
        //public SoundEffect EnemyShootEffect,
        //    PlayerHitAsteoid,
        //    PlayerDamage,
        //    ShieldDestroyed,
        //    ShieldRegenerating,
        //    ShieldUp,
        //    HealthPickup,
        //    MeteorExplosion,
        //    ShieldDamage,
        //    deathSound;

        public List<Shot> EnemyShots = new List<Shot>();
        private Texture2D enemyTexture;
        public Exp Exp;
        public GameObject gameObject;
        public GameState gamestate;
        private readonly GraphicsDeviceManager graphics;
        //private SoundEffect laserEffect;
        private Texture2D laserTexture;
        public Money Money;
        private Texture2D moneyTexture;
        private int playerInvincibilityTimer = 100;
        private int playerShieldCooldown;
        private int playerShieldTimer;
        private readonly Random rand = new Random();
        private float reloadTime;
        private int shieldTime;
        public List<Shot> Shots = new List<Shot>();
        //public SoundEffect Sound, Agr;
        public int soundEffectTimer;
        private int soundTime = 0;
        private Texture2D spaceStation;
        private bool spawnBossCompass = true;
        private bool spawnCompass = true;
        private SpriteBatch spriteBatch, backgrSpriteBatch;
        public float startY, startX;
        private TreasureShip TreasureShip;
        private Texture2D treasureShipTexture;
        private int wantedEnemies = 5;
        private readonly int wantedPowerUps = 5;


        public SpaceScavenger()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                //PreferredBackBufferHeight = Globals.ScreenHeight,
                //PreferredBackBufferWidth = Globals.ScreenWidth,
                IsFullScreen = true
            };
            Content.RootDirectory = "Content";
        }

        public ShopItem _shopItem { get; private set; }
        public bool fasterLaser { get; set; }
        public bool multiShot { get; set; }
        public float spaceStationRotation { get; set; }
        public float EnemyRotation { get; set; }
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        public BossEnemy BossEnemy { get; private set; }
        public PowerUp PowerUp { get; private set; }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Exp = new Exp();
            BombEnemy = new BombEnemy();
            Player = new Player(this);
            Enemy = new Enemy();
            TreasureShip = new TreasureShip();
            BossEnemy = new BossEnemy();
            PowerUp = new PowerUp();
            compass = new Compass();
            bosscompass = new BossCompass();
            Exp = new Exp();
            Money = new Money();
            _camera = new Camera(GraphicsDevice.Viewport);
            Components.Add(Player);
            asteroid = new AsteroidComponent(this, Player, gameObject);
            //Components.Add(asteroid);
            _ui = new UserInterface(this);
            effects = new Effects(this);
            Components.Add(_ui);
            boost = new Boost(this);
            Components.Add(boost);
            Components.Add(effects);
            _startMenu = new StartMenu(this);
            Components.Add(_startMenu);
            _gameOverScreen = new GameOverScreen(this);
            _winScreen = new WinScreen(this);
            Components.Add(_winScreen);
            Components.Add(_gameOverScreen);
            gamestate = GameState.Menu;
            _shop = new Shop(this);
            Components.Add(_shop);
            _shopItem = new ShopItem(this);
            Components.Add(_shopItem);

            fasterLaser = false;


            gameObject = gameObject;
            // TODO: Add your initialization logic here
            //graphics.IsFullScreen = true;
            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgrSpriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = Content.Load<Texture2D>("backgroundNeon");
            laserTexture = Content.Load<Texture2D>("laserBlue");
            BombEnemyTexture = Content.Load<Texture2D>("ufoGreen");
            //deathSound = Content.Load<SoundEffect>("DeathSound");
            _powerUpHealth = Content.Load<Texture2D>("powerupRedPill");
            enemyTexture = Content.Load<Texture2D>("EnemyShipNeon");
            moneyTexture = Content.Load<Texture2D>("Money");
            _Shield = Content.Load<Texture2D>("Shield");
            treasureShipTexture = Content.Load<Texture2D>("TreasureShip");
            BossShotTexture = Content.Load<Texture2D>("BossShotNeon");
            BossShotTexture2 = Content.Load<Texture2D>("TreasureShot");
            //laserEffect = Content.Load<SoundEffect>("laserShoot");
            enemyDamage = Content.Load<Texture2D>("burst");
            spaceStation = Content.Load<Texture2D>("spaceStation");
            //EnemyShootEffect = Content.Load<SoundEffect>("enemyShoot");
            enemyLaserTexture = Content.Load<Texture2D>("laserRed");
            bossTexture = Content.Load<Texture2D>("ufoBlue");
            asteroid.asterTexture2D1 = Content.Load<Texture2D>("Meteor1Neon");
            asteroid.asterTexture2D2 = Content.Load<Texture2D>("Meteor2Neon");
            asteroid.asterTexture2D3 = Content.Load<Texture2D>("Meteor3Neon");
            asteroid.asterTexture2D4 = Content.Load<Texture2D>("Meteor4Neon");
            asteroid.MinitETexture2D1 = Content.Load<Texture2D>("tMeteorNeon");
            //PlayerDamage = Content.Load<SoundEffect>("PlayerDamage");
            //PlayerHitAsteoid = Content.Load<SoundEffect>("PlayerHitAsteroid");
            //ShieldRegenerating = Content.Load<SoundEffect>("ShieldRegenerating");
            //ShieldDestroyed = Content.Load<SoundEffect>("ShieldDestroyed");
            //ShieldUp = Content.Load<SoundEffect>("ShieldUp");
            //HealthPickup = Content.Load<SoundEffect>("HealthPickup");
            //MeteorExplosion = Content.Load<SoundEffect>("ExplosionMeteor");
            //ShieldDamage = Content.Load<SoundEffect>("ShieldDamage");

            //Assault = Content.Load<SoundEffect>("oblivion3");
            //BackgroundSong = Content.Load<Song>("backgroundMusicNeon");
            //agr = Content.Load<SoundEffect>("AGR");
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(BackgroundSong);
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.F11))
                graphics.ToggleFullScreen();

            

            switch (gamestate)
            {
                case GameState.Menu:

                    #region Menu

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                        gamestate = GameState.Playing;

                    #endregion

                    break;
                case GameState.Playing:

                    #region Playing

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
                    if (BossKill)
                        gamestate = GameState.Winscreen;
                    if (Player.Position.X <= new Vector2(400, 0).X && Player.Position.X >= new Vector2(-400, 0).X)
                        if (Player.Position.Y <= new Vector2(0, 400).Y + 400 &&
                            Player.Position.Y >= new Vector2(0, -400).Y)
                        {
                            char button = App.IsXbox() ? 'Y' : 'E';
                            _inRangeToBuyString = $"Press {button} to buy";

                            if (Keyboard.GetState().IsKeyDown(Keys.E) && _shoptimer <= 0 || 
                                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Y) && _shoptimer <= 0)
                            {
                                gamestate = GameState.Shopping;
                                _shoptimer = 10;
                            }
                            else
                            {
                                _shoptimer--;
                            }
                        }
                        else
                        {
                            _inRangeToBuyString = "";
                        }
                    else
                        _inRangeToBuyString = "";

                    #region ControlInputs

                    //Keyboard + mouse
                    if (state.IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickUp))
                        Player.Accelerate();
                    if(state.IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickDown))
                        Player.Decelerate();
                    if (state.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickLeft))
                        Player.StrafeLeft();
                    if (state.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickRight))
                        Player.StrafeRight();


                    var mouse = Mouse.GetState();
                    var mouseLoc = new Vector2(mouse.X, mouse.Y);
                    var direction = mouseLoc - Player.Position;
                    float GetRotationFromVector(Vector2 vector) => (float)Math.Atan2(vector.Y, vector.X);

                    Player.Rotation = GetRotationFromVector(direction);




                    if (state.IsKeyDown(Keys.P) && _previousKbState.IsKeyUp(Keys.P) || 
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && _previousGpState.IsButtonUp(Buttons.Start))
                        gamestate = GameState.Paused;

                    _previousKbState = Keyboard.GetState();
                    _previousGpState = GamePad.GetState(PlayerIndex.One);
                    if (state.IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightTrigger))
                        if (reloadTime <= 0)
                            if (multiShot)
                            {
                                //laserEffect.Play(0.2f, 0.0f, 0.0f);
                                var s = Player.multiShot();
                                if (s != null)
                                    Shots.Add(s);
                                var s2 = Player.multiShot();
                                if (s2 != null)
                                    Shots.Add(s2);
                                reloadTime = 10;
                            }
                            else
                            {
                                //laserEffect.Play(0.2f, 0.0f, 0.0f);
                                var s = Player.Shoot();
                                if (s != null)
                                    Shots.Add(s);
                                reloadTime = 10;
                            }
                    if (state.IsKeyDown(Keys.C) && playerShieldCooldown <= 0 || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.X) && playerShieldCooldown <= 0)
                    {
                        playerShieldTimer = 60;
                        playerShieldCooldown = 300;
                    }
                    //if (state.IsKeyDown(Keys.B))
                    //    Player.Speed = new Vector2(0, 0);

                    #endregion

                    #region Playermovement

                    Player.Position += Player.Speed;
                    if (Player.Speed.LengthSquared() > 120)
                        Player.Speed = Player.Speed * 0.97f;

                    if (Player.Speed.LengthSquared() <= 120 && !Player.Accelerating || Player.Speed.LengthSquared() <= 120 && !Player.Decelerating)
                        Player.Speed = Player.Speed * 0.99f;
                    Player.Accelerating = false;
                    Player.Decelerating = false;
                    base.Update(gameTime);

                    #endregion

                    enemyAmountTimer--;
                    if (enemyAmountTimer <= 0)
                    {
                        wantedEnemies++;
                        enemyAmountTimer = 600;
                    }

                    #region Collision

                    foreach (var enemy in _enemies)
                    {
                        var xDiffPlayer = Math.Abs(enemy.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(enemy.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                            enemy.IsDead = true;
                    }
                    foreach (var enemy in bombships)
                    {
                        var xDiffPlayer = Math.Abs(enemy.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(enemy.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                            enemy.IsDead = true;
                    }
                    foreach (var powerup in _powerups)
                    {
                        var xDiffPlayer = Math.Abs(powerup.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(powerup.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                            powerup.IsDead = true;
                    }

                    if (spawnCompass)
                    {
                        spawnCompass = false;
                        compass = compass.compassSpawn();
                    }
                    if (spawnBossCompass)
                    {
                        spawnBossCompass = false;
                        bosscompass = bosscompass.bossCompassSpawn();
                    }
                    if (bosses.Count > 0)
                        bosscompass.Update(gameTime, this);
                    compass.Update(gameTime, this);


                    if (_enemies.Count + bombships.Count < wantedEnemies)
                        switch (rand.Next(1, 3))
                        {
                            case 1:
                                var e = Enemy.enemySpawn(this);
                                if (e != null)
                                    _enemies.Add(e);
                                break;
                            case 2:
                                var be = BombEnemy.BombEnemySpawn(this);
                                if (be != null)
                                    bombships.Add(be);
                                break;
                        }
                    if (Exp.CurrentEnemiesKilled > 10)
                    {
                        var be = BossEnemy.SpawnBoss(this);
                        if (be != null)
                            bosses.Add(be);
                        Exp.CurrentEnemiesKilled = 0;
                    }
                    if (treasureShips.Count < 1)
                        if (rand.Next(0, 240) == 120)
                        {
                            var te = TreasureShip.SpawnTreasureShip(this);
                            if (te != null)
                                treasureShips.Add(te);
                        }

                    if (_powerups.Count < wantedPowerUps)
                    {
                        var p = PowerUp.PowerUpSpawn(this);
                        if (p != null)
                            _powerups.Add(p);
                    }

                    asteroid.Update(gameTime);
                    foreach (var BigAsteroid in asteroid._nrofAsteroids)
                    {
                        var hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(Player));
                        if (hitasteroid != null)
                        {
                            //PlayerHitAsteoid.Play();
                            if (playerInvincibilityTimer <= 0 && playerShieldTimer <= 0)
                            {
                                if (Player.Shield <= 0)
                                {
                                    Player.Health -= 1;
                                    shieldTime = 200;
                                }
                                else
                                {
                                    Player.Shield--;
                                    shieldTime = 200;
                                }

                                playerInvincibilityTimer = 10;
                            }
                            hitasteroid.IsDead = true;
                            for (var k = 0; k < 10; k++)
                                asteroid.miniStroid(hitasteroid.Position);
                        }
                        break;
                    }
                    foreach (var currentMiniAsteroid in asteroid._MiniStroids)
                    {
                        if (currentMiniAsteroid.Timer <= 0)
                            currentMiniAsteroid.IsDead = true;
                        currentMiniAsteroid.Timer--;
                    }
                    asteroid.Timer--;
                    foreach (var pu in _powerups)
                    {
                        var hitPowerup = _powerups.FirstOrDefault(p => p.CollidesWith(Player));
                        if (hitPowerup == null) continue;
                        //HealthPickup.Play();
                        Player.Health += 1;
                        Player.Health = Player.MaxHealth;
                        hitPowerup.IsDead = true;
                        break;
                    }
                    foreach (var bu in bombships)
                    {
                        var buHit = bombships.FirstOrDefault(p => p.CollidesWith(Player));
                        if (buHit == null) continue;
                        if (playerInvincibilityTimer <= 0 && playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                Player.Health -= 3;
                                shieldTime = 500;
                            }
                            else
                            {
                                Player.Shield -= 3;
                                shieldTime = 500;
                            }
                            if (Player.Shield < 0)
                                Player.Shield = 0;


                            playerInvincibilityTimer = 10;
                        }
                        buHit.IsDead = true;
                        break;
                    }
                    _powerups.RemoveAll(powerup => powerup.IsDead);
                    bombships.RemoveAll(powerup => powerup.IsDead);
                    foreach (var bs in BossShots)
                    {
                        bs.Timer--;
                        if (bs.Timer <= 0)
                            bs.IsDead = true;
                    }
                    foreach (var shot in Shots)
                    {
                        shot.Update(gameTime);
                        var enemy = _enemies.FirstOrDefault(d => d.CollidesWith(shot));
                        var hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));
                        var hitBoss = bosses.FirstOrDefault(e => e.CollidesWith(shot));
                        var treasureHit = treasureShips.FirstOrDefault(te => te.CollidesWith(shot));
                        var bomb = bombships.FirstOrDefault(beb => beb.CollidesWith(shot));

                        if (enemy != null)
                        {
                            enemy.Health -= 1;
                            if (enemy.Health <= 0)
                            {
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                enemy.IsDead = true;
                                defeatedEnemies += 1;
                                Exp.CurrentScore += enemy.ScoreReward;
                                for (var i = 0; i < rand.Next(1, 5); i++)
                                    Money.MoneyRoid(enemy.Position + new Vector2(rand.Next(-50, 50)));
                                Exp.CurrentEnemiesKilled++;
                            }
                            enemyHit = true;
                            enemyPositionExplosion = enemy.Position;
                            shot.IsDead = true;
                        }
                        if (treasureHit != null)
                        {
                            treasureHit.Health -= 1;
                            for (var i = 0; i < rand.Next(1, 4); i++)
                                Money.MoneyRoid(treasureHit.Position + new Vector2(rand.Next(-50, 50)));
                            if (treasureHit.Health <= 0)
                            {
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                treasureHit.IsDead = true;
                                Exp.CurrentScore += treasureHit.ScoreReward;
                                Exp.CurrentEnemiesKilled++;
                            }
                            enemyHit = true;
                            enemyPositionExplosion = treasureHit.Position;
                            Debug.WriteLine(enemyPositionExplosion);
                            shot.IsDead = true;
                        }
                        if (hitBoss != null)
                        {
                            hitBoss.Health -= 1;
                            if (hitBoss.Health <= 0)
                            {
                                spawnBossCompass = true;
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                hitBoss.IsDead = true;
                                BossKill = true;
                                Exp.CurrentScore += hitBoss.ScoreReward;
                                for (var i = 0; i < rand.Next(100, 150); i++)
                                    Money.MoneyRoid(hitBoss.Position + new Vector2(rand.Next(-100, 100)));
                            }
                            enemyHit = true;
                            enemyPositionExplosion = hitBoss.Position;
                            Debug.WriteLine(enemyPositionExplosion);
                            shot.IsDead = true;
                        }
                        if (hitasteroid != null)
                        {
                            var xDiffPlayer = Math.Abs(hitasteroid.Position.X - Player.Position.X);
                            var yDiffPlayer = Math.Abs(hitasteroid.Position.Y - Player.Position.Y);
                            if (yDiffPlayer < 1300 && xDiffPlayer < 1300)
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                            asteroid.miniStroid(hitasteroid.Position);
                            asteroid.miniStroid(hitasteroid.Position);
                            asteroid.miniStroid(hitasteroid.Position);
                            asteroid._nrofAsteroids.Remove(hitasteroid);
                            Exp.CurrentScore += hitasteroid.ScoreReward;
                            Debug.WriteLine(Exp.CurrentScore);
                            shot.IsDead = true;
                        }
                        shot.Timer--;
                        if (shot.Timer <= 0)
                            shot.IsDead = true;
                        if (bomb != null)
                        {
                            bomb.Health -= 1;
                            if (bomb.Health <= 0)
                            {
                                for (var i = 0; i < rand.Next(1, 4); i++)
                                    Money.MoneyRoid(bomb.Position + new Vector2(rand.Next(-100, 100)));
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                bomb.IsDead = true;
                                Exp.CurrentScore += bomb.ScoreReward;
                                Exp.CurrentEnemiesKilled++;
                            }
                            enemyHit = true;
                            enemyPositionExplosion = bomb.Position;
                            shot.IsDead = true;
                        }
                    }
                    foreach (var shot in EnemyShots)
                    {
                        var hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));

                        if (hitasteroid != null)
                        {
                            asteroid.miniStroid(hitasteroid.Position);
                            asteroid.miniStroid(hitasteroid.Position);
                            asteroid.miniStroid(hitasteroid.Position);
                            asteroid._nrofAsteroids.Remove(hitasteroid);

                            shot.IsDead = true;
                        }

                        shot.Update(gameTime);
                        shot.Timer--;
                        if (shot.Timer <= 0)
                            shot.IsDead = true;
                    }
                    foreach (var e in _enemies)
                        e.Update(gameTime, this);
                    foreach (var be in bombships)
                        be.Update(gameTime, this);

                    foreach (var be in bosses)
                        be.Update(gameTime, this);
                    foreach (var te in treasureShips)
                    {
                        te.Update(gameTime, this);
                        te.Timer--;
                        if (te.Timer <= 0)
                            te.IsDead = true;
                    }

                    foreach (var s in BossShots)
                        s.Update(gameTime);

                    var shotHit = EnemyShots.FirstOrDefault(e => e.CollidesWith(Player));
                    if (shotHit != null)
                    {
                        if (playerInvincibilityTimer <= 0 && playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                //PlayerDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Health -= 1;
                                shieldTime = 500;
                            }
                            else
                            {
                                //ShieldDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Shield--;
                                shieldTime = 500;
                            }

                            playerInvincibilityTimer = 10;
                        }
                        shotHit.IsDead = true;
                    }
                    var bossShot = BossShots.FirstOrDefault(be => be.CollidesWith(Player));
                    if (bossShot != null)
                    {
                        if (playerInvincibilityTimer <= 0 && playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                //PlayerDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Health -= 2;
                                shieldTime = 500;
                            }
                            else
                            {
                                //ShieldDamage.Play(0.5f, 0.0f, 0.0f);
                                if (Player.Shield > 4)
                                    Player.Shield -= 5;
                                else
                                    Player.Shield = 0;

                                shieldTime = 500;
                            }

                            playerInvincibilityTimer = 10;
                        }
                        bossShot.IsDead = true;
                    }


                    var moneyHit = Money.Moneyroids.FirstOrDefault(m => m.CollidesWith(Player));
                    if (moneyHit != null)
                    {
                        moneyHit.IsDead = true;
                        Exp.CurrentExp += 50;
                    }


                    if (Player.Health <= 0)
                    {
                        Player.Position = new Vector2(0, 0);
                        Player.Health = Player.MaxHealth;
                        Player.Shield = Player.MaxShield;
                        //deathSound.Play();
                        MediaPlayer.Stop();
                        gamestate = GameState.GameOver;
                    }


                    if (Player.Shield < 5 && shieldTime <= 0)
                    {
                        Player.Shield++;
                        shieldTime = 40;
                        //if (Player.Shield == 10)
                        //    ShieldUp.Play(0.5f, 0.0f, 0.0f);
                        //else
                        //    ShieldRegenerating.Play(0.5f, 0.0f, 0.0f);
                    }
                    if (shieldTime >= 0)
                        shieldTime--;
                    if (playerInvincibilityTimer >= 0)
                        playerInvincibilityTimer--;

                    #endregion

                    Shots.RemoveAll(s => s.IsDead);
                    treasureShips.RemoveAll(treasure => treasure.IsDead);
                    BossShots.RemoveAll(bs => bs.IsDead);
                    EnemyShots.RemoveAll(shot => shot.IsDead);
                    _enemies.RemoveAll(enemy => enemy.IsDead);

                    asteroid._MiniStroids.RemoveAll(n => n.IsDead);
                    asteroid._nrofAsteroids.RemoveAll(j => j.IsDead);
                    Money.Moneyroids.RemoveAll(money => money.IsDead);
                    bosses.RemoveAll(b => b.IsDead);
                    Player.Update(gameTime);
                    _ui.Update(gameTime);
                    _previousKbState = state;
                    boost.Update(gameTime);
                    _camera.Update(gameTime, Player);
                    Money.Update(gameTime, this);
                    _gameOverScreen.Update(gameTime);
                    if (reloadTime >= 0)
                        if (fasterLaser)
                            reloadTime -= 1.6f;
                        else
                            reloadTime--;
                    if (playerShieldCooldown >= 0)
                        playerShieldCooldown--;
                    if (playerShieldTimer >= 0)
                        playerShieldTimer--;
                    if (soundEffectTimer > 0)
                        soundEffectTimer--;

                    #endregion playing

                    break;
                case GameState.Paused:

                    #region Paused

                    if (state.IsKeyDown(Keys.P) && _previousKbState.IsKeyUp(Keys.P) 
                        || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && _previousGpState.IsButtonUp(Buttons.Start))
                        gamestate = GameState.Playing;
                    _previousKbState = Keyboard.GetState();
                    _previousGpState = GamePad.GetState(PlayerIndex.One);

                    #endregion Paused

                    break;
                case GameState.Shopping:

                    #region Shopping

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (Keyboard.GetState().IsKeyDown(Keys.E) && _shoptimer <= 0 
                        || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Y) && _shoptimer <= 0)
                    {
                        gamestate = GameState.Playing;
                        _shoptimer = 10;
                    }
                    else if (_shoptimer > 0)
                    {
                        _shoptimer--;
                    }
                    Player.Speed = new Vector2(0, 0);

                    _shopItem.Update(gameTime);
                    _shop.Update(gameTime);

                    #endregion Shopping

                    break;
                case GameState.GameOver:
                    _gameOverScreen.Update(gameTime);
                    asteroid._nrofAsteroids.Clear();
                    asteroid._MiniStroids.Clear();
                    _enemies.Clear();
                    bosses.Clear();

                    wantedEnemies = 5;
                    treasureShips.Clear();
                    BossShots.Clear();
                    Shots.Clear();
                    break;
                case GameState.Winscreen:
                    _winScreen.Update(gameTime);
                    asteroid._nrofAsteroids.Clear();
                    asteroid._MiniStroids.Clear();
                    _enemies.Clear();
                    bosses.Clear();
                    wantedEnemies = 5;
                    treasureShips.Clear();
                    BossShots.Clear();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (gamestate)
            {
                case GameState.Menu:

                    #region Menu

                    GraphicsDevice.Clear(Color.Black);
                    _startMenu.Draw(gameTime);

                    #endregion

                    break;
                case GameState.Playing:

                    #region Playing

                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    base.Draw(gameTime);


                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        _camera.transformn);

                    backgrSpriteBatch.Begin();

                    startX = Player.Position.X % backgroundTexture.Width;
                    startY = Player.Position.Y % backgroundTexture.Height;

                    for (var y = -startY - backgroundTexture.Height;
                        y < Globals.ScreenHeight;
                        y += backgroundTexture.Width)
                    for (var x = -startX - backgroundTexture.Width;
                        x < Globals.ScreenWidth;
                        x += backgroundTexture.Width)
                        backgrSpriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);

                    backgrSpriteBatch.End();

                    foreach (var mini in asteroid._MiniStroids)
                    {
                        spriteBatch.Draw(asteroid.MinitETexture2D1, mini.Position, null, Color.White,
                            asteroid.Rotation + mini.RotationCounter,
                            new Vector2(asteroid.MinitETexture2D1.Width / 2f, asteroid.MinitETexture2D1.Height / 2f), 1f,
                            SpriteEffects.None, 0f);
                        mini.RotationCounter += mini.addCounter;
                    }
                    foreach (var money in Money.Moneyroids)
                        spriteBatch.Draw(moneyTexture, money.Position, null, Color.White,
                            money.Rotation + money.RotationCounter,
                            new Vector2(moneyTexture.Width / 2f, moneyTexture.Height / 2f), 0.7f, SpriteEffects.None, 0f);


                    for (var i = 0; i < asteroid._nrofAsteroids.Count; i++)
                        switch (asteroid._nrofAsteroids[i].chosenTexture)
                        {
                            case 1:
                                spriteBatch.Draw(asteroid.asterTexture2D1, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D1.Width / 2f,
                                        asteroid.asterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 2:
                                spriteBatch.Draw(asteroid.asterTexture2D2, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D2.Width / 2f,
                                        asteroid.asterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 3:
                                spriteBatch.Draw(asteroid.asterTexture2D3, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D3.Width / 2f,
                                        asteroid.asterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 4:
                                spriteBatch.Draw(asteroid.asterTexture2D4, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D4.Width / 2f,
                                        asteroid.asterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                        }

                    foreach (var s in Shots)
                        spriteBatch.Draw(laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2,
                            new Vector2(laserTexture.Width / 2f, laserTexture.Height / 2f), 1.0f, SpriteEffects.None, 0f);

                    foreach (var s in EnemyShots)
                        spriteBatch.Draw(enemyLaserTexture, s.Position, null, Color.White, s.Rotation,
                            new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);

                    foreach (var s in BossShots)
                        spriteBatch.Draw(s.chosenTexture2D, s.Position, null, Color.White, s.Rotation,
                            new Vector2(BossShotTexture.Width / 2, BossShotTexture.Height / 2), 1.0f, SpriteEffects.None,
                            0f);


                    foreach (var e in _enemies)
                        spriteBatch.Draw(enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2,
                            new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 0.4f, SpriteEffects.None, 0f);

                    foreach (var be in bombships)
                        spriteBatch.Draw(BombEnemyTexture, be.Position, null, Color.White, be.Rotation + 0.01f,
                            new Vector2(BombEnemyTexture.Width / 2, BombEnemyTexture.Height / 2), 0.2f,
                            SpriteEffects.None, 0f);

                    foreach (var p in _powerups)
                        spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White, p.Rotation + MathHelper.PiOver2,
                            new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 0.15f, SpriteEffects.None,
                            0f);

                    foreach (var boss in bosses)
                        spriteBatch.Draw(bossTexture, boss.Position, null, Color.White, EnemyRotation,
                            new Vector2(bossTexture.Width / 2f, bossTexture.Height / 2f), 1f, SpriteEffects.None, 0f);

                    foreach (var treasureShip in treasureShips)
                        spriteBatch.Draw(treasureShipTexture, treasureShip.Position, null, Color.White, EnemyRotation,
                            new Vector2(treasureShipTexture.Width / 2f, treasureShipTexture.Height / 2f), 1f,
                            SpriteEffects.None, 0f);

                    spriteBatch.Draw(spaceStation, new Vector2(0, 0), null, Color.White, spaceStationRotation,
                        new Vector2(spaceStation.Width / 2f, spaceStation.Height / 2f), 2f, SpriteEffects.None, 0f);

                    if (playerShieldTimer > 0)
                        spriteBatch.Draw(_Shield, new Vector2(Player.Position.X, Player.Position.Y), null, Color.White,
                            0f, new Vector2(_Shield.Width / 2f, _Shield.Height / 2f), 0.5f, SpriteEffects.None, 0f);


                    Player.Draw(spriteBatch);

                    EnemyRotation += 0.02f;
                    spaceStationRotation += 0.001f;

                    if (enemyHit)
                    {
                        spriteBatch.Draw(enemyDamage, enemyPositionExplosion, null, Color.White, 1f,
                            new Vector2(enemyDamage.Width / 2f, enemyDamage.Height / 2f), 0.5f, SpriteEffects.None, 0f);
                        enemyHit = false;
                    }

                    if (_inRangeToBuyString.Length > 0)
                        spriteBatch.DrawString(_ui._scoreFont, _inRangeToBuyString, new Vector2(-120, -300), Color.White);


                    spriteBatch.End();

                    _ui.Draw(gameTime);

                    #endregion state playing

                    break;
                case GameState.Paused:
                    break;
                case GameState.Shopping:

                    #region Shopping

                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    base.Draw(gameTime);
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        _camera.transformn);

                    backgrSpriteBatch.Begin();

                    startX = Player.Position.X % backgroundTexture.Width;
                    startY = Player.Position.Y % backgroundTexture.Height;

                    for (var y = -startY - backgroundTexture.Height;
                        y < Globals.ScreenHeight;
                        y += backgroundTexture.Width)
                    for (var x = -startX - backgroundTexture.Width;
                        x < Globals.ScreenWidth;
                        x += backgroundTexture.Width)
                        backgrSpriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);

                    backgrSpriteBatch.End();

                    foreach (var mini in asteroid._MiniStroids)
                        spriteBatch.Draw(asteroid.MinitETexture2D1, mini.Position, Color.White);
                    for (var i = 0; i < asteroid._nrofAsteroids.Count; i++)
                        switch (asteroid._nrofAsteroids[i].chosenTexture)
                        {
                            case 1:
                                spriteBatch.Draw(asteroid.asterTexture2D1, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D1.Width / 2f,
                                        asteroid.asterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 2:
                                spriteBatch.Draw(asteroid.asterTexture2D2, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D2.Width / 2f,
                                        asteroid.asterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 3:
                                spriteBatch.Draw(asteroid.asterTexture2D3, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D3.Width / 2f,
                                        asteroid.asterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 4:
                                spriteBatch.Draw(asteroid.asterTexture2D4, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D4.Width / 2f,
                                        asteroid.asterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                        }

                    foreach (var s in Shots)
                        spriteBatch.Draw(laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2,
                            new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);

                    foreach (var s in EnemyShots)
                        spriteBatch.Draw(enemyLaserTexture, s.Position, null, Color.White, s.Rotation,
                            new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);

                    foreach (var e in _enemies)
                        spriteBatch.Draw(enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2,
                            new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 0.4f, SpriteEffects.None, 0f);

                    foreach (var p in _powerups)
                        spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White, p.Rotation + MathHelper.PiOver2,
                            new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 0.15f, SpriteEffects.None,
                            0f);

                    spriteBatch.Draw(spaceStation, new Vector2(0, 0), null, Color.White, spaceStationRotation,
                        new Vector2(spaceStation.Width / 2f, spaceStation.Height / 2f), 2f, SpriteEffects.None, 0f);

                    Player.Draw(spriteBatch);

                    spaceStationRotation += 0.001f;

                    if (enemyHit)
                    {
                        spriteBatch.Draw(enemyDamage, enemyPositionExplosion, null, Color.White, 1f,
                            new Vector2(enemyDamage.Width / 2f, enemyDamage.Height / 2f), 0.5f, SpriteEffects.None, 0f);
                        enemyHit = false;
                    }


                    spriteBatch.End();

                    _ui.Draw(gameTime);
                    _shop.Draw(gameTime);
                    _shopItem.Draw(gameTime);

                    #endregion Shopping

                    break;
                case GameState.GameOver:

                    #region GameOver

                    _gameOverScreen.Draw(gameTime);

                    #endregion

                    break;
                case GameState.Winscreen:
                    _winScreen.Draw(gameTime);
                    break;
            }
        }
    }
}