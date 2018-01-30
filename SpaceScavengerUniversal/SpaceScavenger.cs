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
        // Start with underscore.
        #region Private fields
        private SpriteBatch _spriteBatch;
        private SpriteBatch _backgroundSpriteBatch;
        private readonly GraphicsDeviceManager _graphics;
        private readonly Random _rng = new Random();

        private readonly List<Enemy> _enemies = new List<Enemy>();
        private readonly List<PowerUp> _powerups = new List<PowerUp>();

        private Camera _camera;
        private GameOverScreen _gameOverScreen;
        private StartMenu _startMenu;
        private UserInterface _ui;
        private WinScreen _winScreen;
        private AsteroidComponent _asteroid;
        private Texture2D _backgroundTexture;
        private Texture2D _bombEnemyTexture;
        private Effects _effects;
        private Vector2 _enemyPositionExplosion = new Vector2(0, 0);
        private TreasureShip _treasureShip;

        private int _wantedEnemies = 5;
        private int _wantedPowerUps = 5;
        private int _soundTime = 0;
        private int _playerInvincibilityTimer = 100;
        private int _playerShieldCooldown;
        private int _playerShieldTimer;
        private int _shieldTime;
        private int _enemyAmountTimer = 600;
        private int _shoptimer;

        private float _reloadTime;

        private bool _spawnBossCompass = true;
        private bool _enemyHit;
        private bool _spawnCompass = true;

        private string _inRangeToBuyString = "";

        private Texture2D _treasureShipTexture;
        private Texture2D _spaceStation;
        private Texture2D _moneyTexture;
        private Texture2D _laserTexture;
        private Texture2D _enemyLaserTexture;
        private Texture2D _tutorialTexture2D;
        private Texture2D _enemyDamage;
        private Texture2D _enemyTexture;
        private Texture2D _bossTexture;
        private Texture2D _powerUpHealth;
        private Texture2D _shield;
        #endregion

        // Start with lowercase letter.
        #region Public fields
        public readonly List<BombEnemy>    bombships     = new List<BombEnemy>();
        public readonly List<BossEnemy>    bosses        = new List<BossEnemy>();
        public readonly List<Shot>         enemyShots    = new List<Shot>();
        public readonly List<Shot>         shots         = new List<Shot>();
        public readonly List<Shot>         bossShots     = new List<Shot>();
        public readonly List<TreasureShip> treasureShips = new List<TreasureShip>();

        public KeyboardState previousKbState;
        public GamePadState  previousGamePadState;
        public Shop          shop;
        public BombEnemy     bombEnemy;
        public Boost         boost;
        public BossCompass   bosscompass;
        public Compass       compass;
        public Exp           exp;
        public GameObject    gameObject;
        public Money         money;
        public GameState     gameState;

        public int defeatedEnemies;
        public int soundEffectTimer;

        public float startX;
        public float startY;

        public bool bossKill;

        public Texture2D bossShotTexture;
        public Texture2D bossShotTexture2;

        //private SoundEffect laserEffect;
        //public  SoundEffect Sound, Agr;
        //public  SoundEffect Assault;
        //public  SoundEffect EnemyShootEffect;
        //public  SoundEffect PlayerHitAsteoid;
        //public  SoundEffect PlayerDamage;
        //public  SoundEffect ShieldDestroyed;
        //public  SoundEffect ShieldRegenerating;
        //public  SoundEffect ShieldUp;
        //public  SoundEffect HealthPickup;
        //public  SoundEffect MeteorExplosion;
        //public  SoundEffect ShieldDamage;
        //public  SoundEffect deathSound;
        //public  Song        BackgroundSong; 
        #endregion

        // Start with uppercase letter.
        #region Public properties
        public ShopItem ShopItem { get; private set; }
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        public BossEnemy BossEnemy { get; private set; }
        public PowerUp PowerUp { get; private set; }
        public bool FasterLaser { get; set; }
        public bool MultiShot { get; set; }
        public float SpaceStationRotation { get; set; }
        public float EnemyRotation { get; set; } 
        #endregion

        public SpaceScavenger()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                //PreferredBackBufferHeight = Globals.ScreenHeight,
                //PreferredBackBufferWidth = Globals.ScreenWidth,
                IsFullScreen = true
            };
            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Player      = new Player(this);
            Enemy       = new Enemy();
            BossEnemy   = new BossEnemy();
            PowerUp     = new PowerUp();
            ShopItem    = new ShopItem(this);
            FasterLaser = false;

            _treasureShip   = new TreasureShip();
            _camera         = new Camera(GraphicsDevice.Viewport);
            _asteroid       = new AsteroidComponent(this, Player, gameObject);
            _ui             = new UserInterface(this);
            _effects        = new Effects(this);
            _startMenu      = new StartMenu(this);
            _gameOverScreen = new GameOverScreen(this);
            _winScreen      = new WinScreen(this);
            
            exp         = new Exp();
            bombEnemy   = new BombEnemy();
            compass     = new Compass();
            bosscompass = new BossCompass();
            money       = new Money();
            boost       = new Boost(this);
            shop        = new Shop(this);
            gameState   = GameState.Menu;

            //Components.Add(asteroid);
            Components.Add(Player);
            Components.Add(_ui);
            Components.Add(boost);
            Components.Add(_effects);
            Components.Add(_startMenu);
            
            Components.Add(_winScreen);
            Components.Add(_gameOverScreen);
            Components.Add(shop);
            Components.Add(ShopItem);
            
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
            _spriteBatch           = new SpriteBatch(GraphicsDevice);
            _backgroundSpriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroundTexture         = Content.Load<Texture2D>("backgroundNeon");
            _laserTexture              = Content.Load<Texture2D>("laserBlue");
            _bombEnemyTexture          = Content.Load<Texture2D>("ufoGreen");
            _powerUpHealth             = Content.Load<Texture2D>("powerupRedPill");
            _enemyTexture              = Content.Load<Texture2D>("EnemyShipNeon");
            _moneyTexture              = Content.Load<Texture2D>("Money");
            _shield                    = Content.Load<Texture2D>("Shield");
            _treasureShipTexture       = Content.Load<Texture2D>("TreasureShip");
            _enemyDamage               = Content.Load<Texture2D>("burst");
            _spaceStation              = Content.Load<Texture2D>("spaceStation");
            _enemyLaserTexture         = Content.Load<Texture2D>("laserRed");
            _bossTexture               = Content.Load<Texture2D>("ufoBlue");
            _asteroid.asterTexture2D1  = Content.Load<Texture2D>("Meteor1Neon");
            _asteroid.asterTexture2D2  = Content.Load<Texture2D>("Meteor2Neon");
            _asteroid.asterTexture2D3  = Content.Load<Texture2D>("Meteor3Neon");
            _asteroid.asterTexture2D4  = Content.Load<Texture2D>("Meteor4Neon");
            _asteroid.MinitETexture2D1 = Content.Load<Texture2D>("tMeteorNeon");

            bossShotTexture  = Content.Load<Texture2D>("BossShotNeon");
            bossShotTexture2 = Content.Load<Texture2D>("TreasureShot");

            //laserEffect        = Content.Load<SoundEffect>("laserShoot");
            //EnemyShootEffect   = Content.Load<SoundEffect>("enemyShoot");
            //deathSound         = Content.Load<SoundEffect>("DeathSound");
            //PlayerDamage       = Content.Load<SoundEffect>("PlayerDamage");
            //PlayerHitAsteoid   = Content.Load<SoundEffect>("PlayerHitAsteroid");
            //ShieldRegenerating = Content.Load<SoundEffect>("ShieldRegenerating");
            //ShieldDestroyed    = Content.Load<SoundEffect>("ShieldDestroyed");
            //ShieldUp           = Content.Load<SoundEffect>("ShieldUp");
            //HealthPickup       = Content.Load<SoundEffect>("HealthPickup");
            //MeteorExplosion    = Content.Load<SoundEffect>("ExplosionMeteor");
            //ShieldDamage       = Content.Load<SoundEffect>("ShieldDamage");
            //Assault            = Content.Load<SoundEffect>("oblivion3");
            //agr                = Content.Load<SoundEffect>("AGR");
            //BackgroundSong     = Content.Load<Song>("backgroundMusicNeon");

            //MediaPlayer.IsRepeating = true;
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
                _graphics.ToggleFullScreen();

            

            switch (gameState)
            {
                case GameState.Menu:

                    #region Menu

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                        gameState = GameState.Playing;

                    #endregion

                    break;
                case GameState.Playing:
                    HandleKeyboardInput();
                    HandleControllerInput();
                    HandleCollisionDetection();
                    MovePlayer();
                    RemoveDeadGameObjects();

                    #region Playing

                    
                    if (bossKill)
                        gameState = GameState.Winscreen;
                    if (Player.Position.X <= new Vector2(400, 0).X && Player.Position.X >= new Vector2(-400, 0).X)
                        if (Player.Position.Y <= new Vector2(0, 400).Y + 400 &&
                            Player.Position.Y >= new Vector2(0, -400).Y)
                        {
                            char button = App.IsXbox() ? 'Y' : 'E';
                            _inRangeToBuyString = $"Press {button} to buy";

                            if (Keyboard.GetState().IsKeyDown(Keys.E) && _shoptimer <= 0 || 
                                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Y) && _shoptimer <= 0)
                            {
                                gameState = GameState.Shopping;
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

                    _enemyAmountTimer--;
                    if (_enemyAmountTimer <= 0)
                    {
                        _wantedEnemies++;
                        _enemyAmountTimer = 600;
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

                    if (_spawnCompass)
                    {
                        _spawnCompass = false;
                        compass = compass.compassSpawn();
                    }
                    if (_spawnBossCompass)
                    {
                        _spawnBossCompass = false;
                        bosscompass = bosscompass.bossCompassSpawn();
                    }
                    if (bosses.Count > 0)
                        bosscompass.Update(gameTime, this);
                    compass.Update(gameTime, this);


                    if (_enemies.Count + bombships.Count < _wantedEnemies)
                        switch (_rng.Next(1, 3))
                        {
                            case 1:
                                var e = Enemy.enemySpawn(this);
                                if (e != null)
                                    _enemies.Add(e);
                                break;
                            case 2:
                                var be = bombEnemy.BombEnemySpawn(this);
                                if (be != null)
                                    bombships.Add(be);
                                break;
                        }
                    if (exp.CurrentEnemiesKilled > 10)
                    {
                        var be = BossEnemy.SpawnBoss(this);
                        if (be != null)
                            bosses.Add(be);
                        exp.CurrentEnemiesKilled = 0;
                    }
                    if (treasureShips.Count < 1)
                        if (_rng.Next(0, 240) == 120)
                        {
                            var te = _treasureShip.SpawnTreasureShip(this);
                            if (te != null)
                                treasureShips.Add(te);
                        }

                    if (_powerups.Count < _wantedPowerUps)
                    {
                        var p = PowerUp.PowerUpSpawn(this);
                        if (p != null)
                            _powerups.Add(p);
                    }

                    _asteroid.Update(gameTime);
                    foreach (var BigAsteroid in _asteroid._nrofAsteroids)
                    {
                        var hitasteroid = _asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(Player));
                        if (hitasteroid != null)
                        {
                            //PlayerHitAsteoid.Play();
                            if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                            {
                                if (Player.Shield <= 0)
                                {
                                    Player.Health -= 1;
                                    _shieldTime = 200;
                                }
                                else
                                {
                                    Player.Shield--;
                                    _shieldTime = 200;
                                }

                                _playerInvincibilityTimer = 10;
                            }
                            hitasteroid.IsDead = true;
                            for (var k = 0; k < 10; k++)
                                _asteroid.miniStroid(hitasteroid.Position);
                        }
                        break;
                    }
                    foreach (var currentMiniAsteroid in _asteroid._MiniStroids)
                    {
                        if (currentMiniAsteroid.Timer <= 0)
                            currentMiniAsteroid.IsDead = true;
                        currentMiniAsteroid.Timer--;
                    }
                    _asteroid.Timer--;
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
                        if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                Player.Health -= 3;
                                _shieldTime = 500;
                            }
                            else
                            {
                                Player.Shield -= 3;
                                _shieldTime = 500;
                            }
                            if (Player.Shield < 0)
                                Player.Shield = 0;


                            _playerInvincibilityTimer = 10;
                        }
                        buHit.IsDead = true;
                        break;
                    }
                    _powerups.RemoveAll(powerup => powerup.IsDead);
                    bombships.RemoveAll(powerup => powerup.IsDead);
                    foreach (var bs in bossShots)
                    {
                        bs.Timer--;
                        if (bs.Timer <= 0)
                            bs.IsDead = true;
                    }
                    foreach (var shot in shots)
                    {
                        shot.Update(gameTime);
                        var enemy = _enemies.FirstOrDefault(d => d.CollidesWith(shot));
                        var hitasteroid = _asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));
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
                                exp.CurrentScore += enemy.ScoreReward;
                                for (var i = 0; i < _rng.Next(1, 5); i++)
                                    money.MoneyRoid(enemy.Position + new Vector2(_rng.Next(-50, 50)));
                                exp.CurrentEnemiesKilled++;
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = enemy.Position;
                            shot.IsDead = true;
                        }
                        if (treasureHit != null)
                        {
                            treasureHit.Health -= 1;
                            for (var i = 0; i < _rng.Next(1, 4); i++)
                                money.MoneyRoid(treasureHit.Position + new Vector2(_rng.Next(-50, 50)));
                            if (treasureHit.Health <= 0)
                            {
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                treasureHit.IsDead = true;
                                exp.CurrentScore += treasureHit.ScoreReward;
                                exp.CurrentEnemiesKilled++;
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = treasureHit.Position;
                            Debug.WriteLine(_enemyPositionExplosion);
                            shot.IsDead = true;
                        }
                        if (hitBoss != null)
                        {
                            hitBoss.Health -= 1;
                            if (hitBoss.Health <= 0)
                            {
                                _spawnBossCompass = true;
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                hitBoss.IsDead = true;
                                bossKill = true;
                                exp.CurrentScore += hitBoss.ScoreReward;
                                for (var i = 0; i < _rng.Next(100, 150); i++)
                                    money.MoneyRoid(hitBoss.Position + new Vector2(_rng.Next(-100, 100)));
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = hitBoss.Position;
                            Debug.WriteLine(_enemyPositionExplosion);
                            shot.IsDead = true;
                        }
                        if (hitasteroid != null)
                        {
                            var xDiffPlayer = Math.Abs(hitasteroid.Position.X - Player.Position.X);
                            var yDiffPlayer = Math.Abs(hitasteroid.Position.Y - Player.Position.Y);
                            if (yDiffPlayer < 1300 && xDiffPlayer < 1300)
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                            _asteroid.miniStroid(hitasteroid.Position);
                            _asteroid.miniStroid(hitasteroid.Position);
                            _asteroid.miniStroid(hitasteroid.Position);
                            _asteroid._nrofAsteroids.Remove(hitasteroid);
                            exp.CurrentScore += hitasteroid.ScoreReward;
                            Debug.WriteLine(exp.CurrentScore);
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
                                for (var i = 0; i < _rng.Next(1, 4); i++)
                                    money.MoneyRoid(bomb.Position + new Vector2(_rng.Next(-100, 100)));
                                //MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                bomb.IsDead = true;
                                exp.CurrentScore += bomb.ScoreReward;
                                exp.CurrentEnemiesKilled++;
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = bomb.Position;
                            shot.IsDead = true;
                        }
                    }
                    foreach (var shot in enemyShots)
                    {
                        var hitasteroid = _asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));

                        if (hitasteroid != null)
                        {
                            _asteroid.miniStroid(hitasteroid.Position);
                            _asteroid.miniStroid(hitasteroid.Position);
                            _asteroid.miniStroid(hitasteroid.Position);
                            _asteroid._nrofAsteroids.Remove(hitasteroid);

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

                    foreach (var s in bossShots)
                        s.Update(gameTime);

                    var shotHit = enemyShots.FirstOrDefault(e => e.CollidesWith(Player));
                    if (shotHit != null)
                    {
                        if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                //PlayerDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Health -= 1;
                                _shieldTime = 500;
                            }
                            else
                            {
                                //ShieldDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Shield--;
                                _shieldTime = 500;
                            }

                            _playerInvincibilityTimer = 10;
                        }
                        shotHit.IsDead = true;
                    }
                    var bossShot = bossShots.FirstOrDefault(be => be.CollidesWith(Player));
                    if (bossShot != null)
                    {
                        if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                //PlayerDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Health -= 2;
                                _shieldTime = 500;
                            }
                            else
                            {
                                //ShieldDamage.Play(0.5f, 0.0f, 0.0f);
                                if (Player.Shield > 4)
                                    Player.Shield -= 5;
                                else
                                    Player.Shield = 0;

                                _shieldTime = 500;
                            }

                            _playerInvincibilityTimer = 10;
                        }
                        bossShot.IsDead = true;
                    }


                    var moneyHit = money.Moneyroids.FirstOrDefault(m => m.CollidesWith(Player));
                    if (moneyHit != null)
                    {
                        moneyHit.IsDead = true;
                        exp.CurrentExp += 50;
                    }


                    if (Player.Health <= 0)
                    {
                        Player.Position = new Vector2(0, 0);
                        Player.Health = Player.MaxHealth;
                        Player.Shield = Player.MaxShield;
                        //deathSound.Play();
                        MediaPlayer.Stop();
                        gameState = GameState.GameOver;
                    }


                    if (Player.Shield < 5 && _shieldTime <= 0)
                    {
                        Player.Shield++;
                        _shieldTime = 40;
                        //if (Player.Shield == 10)
                        //    ShieldUp.Play(0.5f, 0.0f, 0.0f);
                        //else
                        //    ShieldRegenerating.Play(0.5f, 0.0f, 0.0f);
                    }
                    if (_shieldTime >= 0)
                        _shieldTime--;
                    if (_playerInvincibilityTimer >= 0)
                        _playerInvincibilityTimer--;

                    #endregion

                    shots.RemoveAll(s => s.IsDead);
                    treasureShips.RemoveAll(treasure => treasure.IsDead);
                    bossShots.RemoveAll(bs => bs.IsDead);
                    enemyShots.RemoveAll(shot => shot.IsDead);
                    _enemies.RemoveAll(enemy => enemy.IsDead);
                    _asteroid._MiniStroids.RemoveAll(n => n.IsDead);
                    _asteroid._nrofAsteroids.RemoveAll(j => j.IsDead);
                    money.Moneyroids.RemoveAll(money => money.IsDead);
                    bosses.RemoveAll(b => b.IsDead);

                    Player.Update(gameTime);
                    _ui.Update(gameTime);

                    previousKbState = state;

                    boost.Update(gameTime);
                    _camera.Update(gameTime, Player);
                    money.Update(gameTime, this);
                    _gameOverScreen.Update(gameTime);

                    if (_reloadTime >= 0)
                        if (FasterLaser)
                            _reloadTime -= 1.6f;
                        else
                            _reloadTime--;
                    if (_playerShieldCooldown >= 0)
                        _playerShieldCooldown--;
                    if (_playerShieldTimer >= 0)
                        _playerShieldTimer--;
                    if (soundEffectTimer > 0)
                        soundEffectTimer--;

                    #endregion playing

                    break;
                case GameState.Paused:

                    #region Paused

                    if (state.IsKeyDown(Keys.P) && previousKbState.IsKeyUp(Keys.P) 
                        || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && previousGamePadState.IsButtonUp(Buttons.Start))
                        gameState = GameState.Playing;
                    previousKbState = Keyboard.GetState();
                    previousGamePadState = GamePad.GetState(PlayerIndex.One);

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
                        gameState = GameState.Playing;
                        _shoptimer = 10;
                    }
                    else if (_shoptimer > 0)
                    {
                        _shoptimer--;
                    }
                    Player.Speed = new Vector2(0, 0);

                    ShopItem.Update(gameTime);
                    shop.Update(gameTime);

                    #endregion Shopping

                    break;
                case GameState.GameOver:
                    _gameOverScreen.Update(gameTime);
                    _asteroid._nrofAsteroids.Clear();
                    _asteroid._MiniStroids.Clear();
                    _enemies.Clear();
                    bosses.Clear();

                    _wantedEnemies = 5;
                    treasureShips.Clear();
                    bossShots.Clear();
                    shots.Clear();
                    break;
                case GameState.Winscreen:
                    _winScreen.Update(gameTime);
                    _asteroid._nrofAsteroids.Clear();
                    _asteroid._MiniStroids.Clear();
                    _enemies.Clear();
                    bosses.Clear();
                    _wantedEnemies = 5;
                    treasureShips.Clear();
                    bossShots.Clear();
                    break;
            }

            base.Update(gameTime);
        }

        private void RemoveDeadGameObjects()
        {
            return;
        }

        private void MovePlayer()
        {
            return;
        }

        private void HandleCollisionDetection()
        {
            return;
        }

        private void HandleControllerInput()
        {
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            Player.Rotation = GetRotationFromVector(gamePadState.ThumbSticks.Right);


            if (gamePadState.Buttons.Back == ButtonState.Pressed)
                Exit();

            if (gamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
                Player.Accelerate();

            if (gamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
                Player.Decelerate();

            if (gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
                Player.StrafeLeft();

            if (gamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
                Player.StrafeRight();

            if (gamePadState.IsButtonDown(Buttons.Start) && previousGamePadState.IsButtonUp(Buttons.Start))
                gameState = GameState.Paused;

            if (gamePadState.IsButtonDown(Buttons.RightTrigger))
            {
                if (_reloadTime <= 0)
                {
                    if (MultiShot)
                    {
                        //laserEffect.Play(0.2f, 0.0f, 0.0f);
                        var s = Player.multiShot();
                        if (s != null)
                            shots.Add(s);
                        var s2 = Player.multiShot();
                        if (s2 != null)
                            shots.Add(s2);
                        _reloadTime = 10;
                    }
                    else
                    {
                        //laserEffect.Play(0.2f, 0.0f, 0.0f);
                        var s = Player.Shoot();
                        if (s != null)
                            shots.Add(s);
                        _reloadTime = 10;
                    }
                }
            }

            if (gamePadState.IsButtonDown(Buttons.X) && _playerShieldCooldown <= 0)
            {
                _playerShieldTimer = 60;
                _playerShieldCooldown = 300;
            }

            previousGamePadState = GamePad.GetState(PlayerIndex.One);

            float GetRotationFromVector(Vector2 vector) => (float)Math.Atan2(-vector.Y, vector.X);
        }

        private void HandleKeyboardInput()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var mouseLocation = new Vector2(mouseState.X, mouseState.Y);
            var direction = mouseLocation - Globals.ScreenCenter;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.W))
                Player.Accelerate();

            if (keyboardState.IsKeyDown(Keys.S))
                Player.Decelerate();

            if (keyboardState.IsKeyDown(Keys.A))
                Player.StrafeLeft();

            if (keyboardState.IsKeyDown(Keys.D))
                Player.StrafeRight();

            if (keyboardState.IsKeyDown(Keys.P) && previousKbState.IsKeyUp(Keys.P))
                gameState = GameState.Paused;
            
            Player.Rotation = GetRotationFromVector(direction);

            previousKbState = Keyboard.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_reloadTime <= 0)
                {
                    if (MultiShot)
                    {
                        //laserEffect.Play(0.2f, 0.0f, 0.0f);
                        var s = Player.multiShot();
                        if (s != null)
                            shots.Add(s);
                        var s2 = Player.multiShot();
                        if (s2 != null)
                            shots.Add(s2);
                        _reloadTime = 10;
                    }
                    else
                    {
                        //laserEffect.Play(0.2f, 0.0f, 0.0f);
                        var s = Player.Shoot();
                        if (s != null)
                            shots.Add(s);
                        _reloadTime = 10;
                    }
                }
            }

            if (keyboardState.IsKeyDown(Keys.C) && _playerShieldCooldown <= 0)
            {
                _playerShieldTimer = 60;
                _playerShieldCooldown = 300;
            }

            float GetRotationFromVector(Vector2 vector) => (float)Math.Atan2(vector.Y, vector.X);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (gameState)
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


                    _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        _camera.transformn);

                    _backgroundSpriteBatch.Begin();

                    startX = Player.Position.X % _backgroundTexture.Width;
                    startY = Player.Position.Y % _backgroundTexture.Height;

                    for (var y = -startY - _backgroundTexture.Height;
                        y < Globals.ScreenHeight;
                        y += _backgroundTexture.Width)
                    for (var x = -startX - _backgroundTexture.Width;
                        x < Globals.ScreenWidth;
                        x += _backgroundTexture.Width)
                        _backgroundSpriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);

                    _backgroundSpriteBatch.End();

                    foreach (var mini in _asteroid._MiniStroids)
                    {
                        _spriteBatch.Draw(_asteroid.MinitETexture2D1, mini.Position, null, Color.White,
                            _asteroid.Rotation + mini.RotationCounter,
                            new Vector2(_asteroid.MinitETexture2D1.Width / 2f, _asteroid.MinitETexture2D1.Height / 2f), 1f,
                            SpriteEffects.None, 0f);
                        mini.RotationCounter += mini.addCounter;
                    }
                    foreach (var money in money.Moneyroids)
                        _spriteBatch.Draw(_moneyTexture, money.Position, null, Color.White,
                            money.Rotation + money.RotationCounter,
                            new Vector2(_moneyTexture.Width / 2f, _moneyTexture.Height / 2f), 0.7f, SpriteEffects.None, 0f);


                    for (var i = 0; i < _asteroid._nrofAsteroids.Count; i++)
                        switch (_asteroid._nrofAsteroids[i].chosenTexture)
                        {
                            case 1:
                                _spriteBatch.Draw(_asteroid.asterTexture2D1, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D1.Width / 2f,
                                        _asteroid.asterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 2:
                                _spriteBatch.Draw(_asteroid.asterTexture2D2, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D2.Width / 2f,
                                        _asteroid.asterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 3:
                                _spriteBatch.Draw(_asteroid.asterTexture2D3, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D3.Width / 2f,
                                        _asteroid.asterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 4:
                                _spriteBatch.Draw(_asteroid.asterTexture2D4, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D4.Width / 2f,
                                        _asteroid.asterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                        }

                    foreach (var s in shots)
                        _spriteBatch.Draw(_laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2,
                            new Vector2(_laserTexture.Width / 2f, _laserTexture.Height / 2f), 1.0f, SpriteEffects.None, 0f);

                    foreach (var s in enemyShots)
                        _spriteBatch.Draw(_enemyLaserTexture, s.Position, null, Color.White, s.Rotation,
                            new Vector2(_laserTexture.Width / 2, _laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);

                    foreach (var s in bossShots)
                        _spriteBatch.Draw(s.chosenTexture2D, s.Position, null, Color.White, s.Rotation,
                            new Vector2(bossShotTexture.Width / 2, bossShotTexture.Height / 2), 1.0f, SpriteEffects.None,
                            0f);


                    foreach (var e in _enemies)
                        _spriteBatch.Draw(_enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2,
                            new Vector2(_enemyTexture.Width / 2, _enemyTexture.Height / 2), 0.4f, SpriteEffects.None, 0f);

                    foreach (var be in bombships)
                        _spriteBatch.Draw(_bombEnemyTexture, be.Position, null, Color.White, be.Rotation + 0.01f,
                            new Vector2(_bombEnemyTexture.Width / 2, _bombEnemyTexture.Height / 2), 0.2f,
                            SpriteEffects.None, 0f);

                    foreach (var p in _powerups)
                        _spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White, p.Rotation + MathHelper.PiOver2,
                            new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 0.15f, SpriteEffects.None,
                            0f);

                    foreach (var boss in bosses)
                        _spriteBatch.Draw(_bossTexture, boss.Position, null, Color.White, EnemyRotation,
                            new Vector2(_bossTexture.Width / 2f, _bossTexture.Height / 2f), 1f, SpriteEffects.None, 0f);

                    foreach (var treasureShip in treasureShips)
                        _spriteBatch.Draw(_treasureShipTexture, treasureShip.Position, null, Color.White, EnemyRotation,
                            new Vector2(_treasureShipTexture.Width / 2f, _treasureShipTexture.Height / 2f), 1f,
                            SpriteEffects.None, 0f);

                    _spriteBatch.Draw(_spaceStation, new Vector2(0, 0), null, Color.White, SpaceStationRotation,
                        new Vector2(_spaceStation.Width / 2f, _spaceStation.Height / 2f), 2f, SpriteEffects.None, 0f);

                    if (_playerShieldTimer > 0)
                        _spriteBatch.Draw(_shield, new Vector2(Player.Position.X, Player.Position.Y), null, Color.White,
                            0f, new Vector2(_shield.Width / 2f, _shield.Height / 2f), 0.5f, SpriteEffects.None, 0f);


                    Player.Draw(_spriteBatch);

                    EnemyRotation += 0.02f;
                    SpaceStationRotation += 0.001f;

                    if (_enemyHit)
                    {
                        _spriteBatch.Draw(_enemyDamage, _enemyPositionExplosion, null, Color.White, 1f,
                            new Vector2(_enemyDamage.Width / 2f, _enemyDamage.Height / 2f), 0.5f, SpriteEffects.None, 0f);
                        _enemyHit = false;
                    }

                    if (_inRangeToBuyString.Length > 0)
                        _spriteBatch.DrawString(_ui._scoreFont, _inRangeToBuyString, new Vector2(-120, -300), Color.White);


                    _spriteBatch.End();

                    _ui.Draw(gameTime);

                    #endregion state playing

                    break;
                case GameState.Paused:
                    break;
                case GameState.Shopping:

                    #region Shopping

                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    base.Draw(gameTime);
                    _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        _camera.transformn);

                    _backgroundSpriteBatch.Begin();

                    startX = Player.Position.X % _backgroundTexture.Width;
                    startY = Player.Position.Y % _backgroundTexture.Height;

                    for (var y = -startY - _backgroundTexture.Height;
                        y < Globals.ScreenHeight;
                        y += _backgroundTexture.Width)
                    for (var x = -startX - _backgroundTexture.Width;
                        x < Globals.ScreenWidth;
                        x += _backgroundTexture.Width)
                        _backgroundSpriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);

                    _backgroundSpriteBatch.End();

                    foreach (var mini in _asteroid._MiniStroids)
                        _spriteBatch.Draw(_asteroid.MinitETexture2D1, mini.Position, Color.White);
                    for (var i = 0; i < _asteroid._nrofAsteroids.Count; i++)
                        switch (_asteroid._nrofAsteroids[i].chosenTexture)
                        {
                            case 1:
                                _spriteBatch.Draw(_asteroid.asterTexture2D1, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D1.Width / 2f,
                                        _asteroid.asterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 2:
                                _spriteBatch.Draw(_asteroid.asterTexture2D2, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D2.Width / 2f,
                                        _asteroid.asterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 3:
                                _spriteBatch.Draw(_asteroid.asterTexture2D3, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D3.Width / 2f,
                                        _asteroid.asterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 4:
                                _spriteBatch.Draw(_asteroid.asterTexture2D4, _asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.asterTexture2D4.Width / 2f,
                                        _asteroid.asterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid._nrofAsteroids[i].RotationCounter += _asteroid._nrofAsteroids[i].addCounter;
                                break;
                        }

                    foreach (var s in shots)
                        _spriteBatch.Draw(_laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2,
                            new Vector2(_laserTexture.Width / 2, _laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);

                    foreach (var s in enemyShots)
                        _spriteBatch.Draw(_enemyLaserTexture, s.Position, null, Color.White, s.Rotation,
                            new Vector2(_laserTexture.Width / 2, _laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);

                    foreach (var e in _enemies)
                        _spriteBatch.Draw(_enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2,
                            new Vector2(_enemyTexture.Width / 2, _enemyTexture.Height / 2), 0.4f, SpriteEffects.None, 0f);

                    foreach (var p in _powerups)
                        _spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White, p.Rotation + MathHelper.PiOver2,
                            new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 0.15f, SpriteEffects.None,
                            0f);

                    _spriteBatch.Draw(_spaceStation, new Vector2(0, 0), null, Color.White, SpaceStationRotation,
                        new Vector2(_spaceStation.Width / 2f, _spaceStation.Height / 2f), 2f, SpriteEffects.None, 0f);

                    Player.Draw(_spriteBatch);

                    SpaceStationRotation += 0.001f;

                    if (_enemyHit)
                    {
                        _spriteBatch.Draw(_enemyDamage, _enemyPositionExplosion, null, Color.White, 1f,
                            new Vector2(_enemyDamage.Width / 2f, _enemyDamage.Height / 2f), 0.5f, SpriteEffects.None, 0f);
                        _enemyHit = false;
                    }


                    _spriteBatch.End();

                    _ui.Draw(gameTime);
                    shop.Draw(gameTime);
                    ShopItem.Draw(gameTime);

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