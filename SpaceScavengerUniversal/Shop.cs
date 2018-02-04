using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceScavengerUniversal;

using static SpaceScavengerUniversal.ShopTile;

namespace Space_Scavenger
{

    public class Shop : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private SpriteBatch _spriteBatch;
        private readonly List<ShopTile> _shopTiles;
        private readonly string[] _shopTileTexts;
        private string[] _shopTileCosts;
        private ShopTile _shopTile;

        private MouseState _previousMState;
        private int _itemCost;

        //Textures
        private Texture2D _mainWindowTexture;

        //bools
        private bool _healthRank1;
        private bool _healthRank2;
        private bool _healthRank3;

        private bool _shieldRank1;
        private bool _shieldRank2;
        private bool _shieldRank3;

        private bool _boostRank1;
        private bool _boostRank2;
        private bool _boostRank3;

        private bool _movementSpeedRank1;
        private bool _movementSpeedRank2;
        private bool _movementSpeedRank3;


        private bool _reloadTimeRank1;
        private bool _reloadTimeRank2;
        private bool _reloadTimeRank3;

        private bool _auraTimerRank1;
        private bool _auraTimerRank2;
        private bool _auraTimerRank3;



        //Fonts
        private SpriteFont _itemDescFont;
        private SpriteFont _shopHeaderFont;
        private SpriteFont _shopMoneyFont;
        
        //Rectangles
        private Rectangle _mainWindow;


        private string _nextHealthRankString;
        private string _nextShieldRankString;
        private string _nextBoostRankString;

        public Shop(Game game) : base(game)
        {
            _shopTiles = new List<ShopTile>();
            

            _myGame = (SpaceScavenger) game;
            for (int i = 0; i < 7; i++)
            {
                _shopTiles.Add(new ShopTile(_myGame, 1250 ,300 + (i*80)));

            }

            _shopTileCosts = new string[_shopTiles.Count];
            _shopTileCosts[0] = "300";
            _shopTileCosts[1] = "300";
            _shopTileCosts[2] = "300";
            _shopTileCosts[3] = "300";
            _shopTileCosts[4] = "300";
            _shopTileCosts[5] = "300";
            _shopTileCosts[6] = "300";

            _shopTileTexts = new string[_shopTiles.Count];
            _shopTileTexts[0] = "Health+";
            _shopTileTexts[1] = "Shield+";
            _shopTileTexts[2] = "Boost+";
            _shopTileTexts[3] = "Movement Speed+";
            _shopTileTexts[4] = "Reload Time-";
            _shopTileTexts[5] = "Multishot";
            _shopTileTexts[6] = "Invincibility Aura+";

            

            _healthRank1 = true;
            _shieldRank1 = true;
            _boostRank1 = true;
            _reloadTimeRank1 = true;
            _movementSpeedRank1 = true;
            _auraTimerRank1 = true;

        }

        protected override void LoadContent()
        {
            //Spritebatch
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
           
            //Fonts
            _shopHeaderFont = Game.Content.Load<SpriteFont>("ShopHeadLine");
            //_shopMoneyFont = Game.Content.Load<SpriteFont>("ScoreFont");
            //_itemDescFont = Game.Content.Load<SpriteFont>("ItemDescFont");
            foreach (var shopTile in _shopTiles)
            {
                shopTile.LoadContent();
            }

            //Textures
            _mainWindowTexture = Game.Content.Load<Texture2D>("panel");

            //Rectangles
            _mainWindow = new Rectangle(1220, 220, (int)(_mainWindowTexture.Width*0.75), (int)(_mainWindowTexture.Height * 0.75));


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (_myGame.gamestate == GameState.Shopping)
            {
                IsHealthButtonPressed();
                IsShieldButtonPressed();
                IsBoostButtonPressed();
                IsBlasterButtonPressed();
                IsMovementButtonPressed();
                IsMultishotButtonPressed();
                //IsAuraButtonPressed();
                _previousMState = Mouse.GetState(); 
            }
            base.Update(gameTime);
        }

        

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mainWindowTexture,_mainWindow, Color.White);
            _spriteBatch.DrawString(_shopHeaderFont, _myGame.Exp.CurrentExp + "$",new Vector2(1245, 240), Color.Green );
            _spriteBatch.End();
            foreach (var shoptile in _shopTiles)
            {
                shoptile.Draw(gameTime, _shopTileTexts[_shopTiles.IndexOf(shoptile)]);
            }
        }

        //Health++
        private void IsHealthButtonPressed()
        {
            if (Hover(_shopTiles[0].Rectangle))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && _previousMState.LeftButton != ButtonState.Pressed)
                {
                    IncreaseHealth();
                }
            } 
        }

        private void IncreaseHealth()
        {
            if (_healthRank1)
            {
                HealthToRank2();
                _shopTileTexts[0] = "Health++";
            }

            else if (_healthRank2)
            {
                HealthToRank3();
                _shopTileTexts[0] = "Health (Max)";
            }   


        }

        private void HealthToRank2()
        {
            var newMaxHealth = 10;

            if (_myGame.Player.Health == _myGame.Player.MaxHealth)
            {
                _myGame.Player.MaxHealth = newMaxHealth;
                _myGame.Player.Health = _myGame.Player.MaxHealth;
            }
            else
            {
                _myGame.Player.MaxHealth = newMaxHealth;
            }

            _healthRank1 = false;
            _healthRank2 = true;
        }

        private void HealthToRank3()
        {
            const int newMaxHealth = 20;

            if (_myGame.Player.Health == _myGame.Player.MaxHealth)
            {
                _myGame.Player.MaxHealth = newMaxHealth;
                _myGame.Player.Health = _myGame.Player.MaxHealth;
            }
            else
            {
                _myGame.Player.MaxHealth = newMaxHealth;
            }
            _healthRank2 = false;
            _healthRank3 = true;
        }

        //Shield++
        private void IsShieldButtonPressed()
        {
            if (Hover(_shopTiles[1].Rectangle))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    IncreaseShield();
                }
            } 
        }

        private void IncreaseShield()
        {
            if (_shieldRank1)
            {
                ShieldToRank2();
                _shopTileTexts[1] = "Shield++";
            }

            else if (_shieldRank2)
            {
                ShieldToRank3();
                _shopTileTexts[1] = "Shield (Max)";
            }
        }

        private void ShieldToRank2()
        {
            const int newMaxShield = 10;

            if (_myGame.Player.Shield == _myGame.Player.MaxShield)
            {
                _myGame.Player.MaxShield = newMaxShield;
                _myGame.Player.Shield = _myGame.Player.MaxShield;
            }
            else
            {
                _myGame.Player.MaxShield = newMaxShield;
            }

            _shieldRank1 = false;
            _shieldRank2 = true;
        }

        private void ShieldToRank3()
        {
            const int newMaxShield = 20;

            if (_myGame.Player.Shield == _myGame.Player.MaxShield)
            {
                _myGame.Player.MaxShield = newMaxShield;
                _myGame.Player.Shield = _myGame.Player.MaxShield;
            }
            else
            {
                _myGame.Player.MaxShield = newMaxShield;
            }
            _shieldRank2 = false;
            _shieldRank3 = true;
        }


        //Boost++
        private void IsBoostButtonPressed()
        {
            if (Hover(_shopTiles[2].Rectangle))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    IncreaseNrOfBoosts();
                }
            } 
        }

        private void IncreaseNrOfBoosts()
        {
            if (_boostRank1)
            {
                BoostToRank2();
                _shopTileTexts[2] = "Boost++";
            }

            else if (_boostRank2)
            {
                BoostToRank3();
                _shopTileTexts[2] = "Boost (Max)";
            }
        }

        private void BoostToRank2()
        {
            _myGame.boost.MaxNrOfBoosts = 2;
            _myGame.boost.NrOfBoosts = _myGame.boost.MaxNrOfBoosts;
            _boostRank1 = false;
            _boostRank2 = true;
        }

        private void BoostToRank3()
        {
            _myGame.boost.MaxNrOfBoosts = 3;
            _myGame.boost.NrOfBoosts = _myGame.boost.MaxNrOfBoosts;
            _boostRank2 = false;
            _boostRank3 = true;
        }

        //MovementSpeed++

        private void IsMovementButtonPressed()
        {
            if (Hover(_shopTiles[3].Rectangle))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    IncreaseMovementSpeed();
                }
            }
        }

        private void IncreaseMovementSpeed()
        {
            if (_movementSpeedRank1)
            {
                MovementSpeedToRank2();
                _shopTileTexts[3] = "Movement Speed++";
            }

            else if (_movementSpeedRank2)
            {
                MovementSpeedToRank3();
                _shopTileTexts[3] = "Movement Speed (Max)";
            }
        }

        private void MovementSpeedToRank2()
        {
            _myGame.Player.SpeedMultiplier = 0.30f;
            _movementSpeedRank1 = false;
            _movementSpeedRank2 = true;
        }

        private void MovementSpeedToRank3()
        {
            _myGame.Player.SpeedMultiplier = 0.40f;
            _movementSpeedRank2 = false;
            _movementSpeedRank3 = true;
        }


        //BlasterReload--
        private void IsBlasterButtonPressed()
        {
            if (Hover(_shopTiles[4].Rectangle))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    DecreaseBlasterReloadTime();
                }
            }
        }

        private void DecreaseBlasterReloadTime()
        {
            if (_reloadTimeRank1)
            {
                ReloadToRank2();
                _shopTileTexts[4] = "Reload Time--";
            }

            else if (_reloadTimeRank2)
            {
                ReloadToRank3();
                _shopTileTexts[4] = "Reload Time (Max)";
            }
        }

        private void ReloadToRank2()
        {
            _myGame.Player.NewReloadTime = 30;
            _reloadTimeRank1= false;
            _reloadTimeRank2 = true;
        }

        private void ReloadToRank3()
        {
            _myGame.Player.NewReloadTime = 20;
            _reloadTimeRank2 = false;
            _reloadTimeRank3 = true;
        }

        //Multishot
        private void IsMultishotButtonPressed()
        {
            if (Hover(_shopTiles[5].Rectangle))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    EnableMultiShot();
                }
            }
        }

        private void EnableMultiShot()
        {
            _myGame.multiShot = true;
            _shopTileTexts[5] = "Multishot Activated";
        }

        //Invincibility Aura
        //private void IsAuraButtonPressed()
        //{
        //    if (Hover(_shopTiles[6].Rectangle))
        //    {
        //        if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
        //            _previousMState.LeftButton != ButtonState.Pressed)
        //        {
        //            DecreaseAuraTimer();
        //        }
        //    }
        //}

        //private void DecreaseAuraTimer()
        //{
        //    if (_auraTimerRank1)
        //    {
        //        AuraTimerToRank2();
        //    }

        //    else if (_auraTimerRank2)
        //    {
        //        AuraTimerToRank3();
        //    }
        //}

        //private void AuraTimerToRank2()
        //{
        //    _myGame.NewPlayerInvincibilityTimer = 20;
        //    _auraTimerRank1= false;
        //    _auraTimerRank2 = true;
        //}

        //private void AuraTimerToRank3()
        //{
        //    _myGame.NewPlayerInvincibilityTimer = 10;
        //    _auraTimerRank2 = false;
        //    _auraTimerRank3 = true;
        //}

    }
}
