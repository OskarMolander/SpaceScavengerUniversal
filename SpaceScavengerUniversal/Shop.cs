using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceScavengerUniversal;
using System.Diagnostics;

using static SpaceScavengerUniversal.ShopTile;

namespace Space_Scavenger
{

    public class Shop : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private SpriteBatch _spriteBatch;
        private readonly List<ShopTile> _shopTiles;
        private (string text, int price)[] _shopTileTexts;

        private MouseState _previousMState;
        private string _itemDescription;


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
        private SpriteFont _scoreFont;
        
        //Rectangles
        private Rectangle _mainWindow;

        

        public Shop(Game game) : base(game)
        {
            _shopTiles = new List<ShopTile>();
            

            _myGame = (SpaceScavenger) game;
            for (int i = 0; i < 6; i++)
            {
                _shopTiles.Add(new ShopTile(_myGame, 1250 ,300 + (i*80)));

            }


            _shopTileTexts = new[]
            {
                ("Health+",       500),
                ("Shield+",       500),
                ("Boost+",        500),
                ("Speed+",        500),
                ("Faster reload+",500),
                ("Multishot",     1000)

            };

            _healthRank1 = true;
            _shieldRank1 = true;
            _boostRank1 = true;
            _reloadTimeRank1 = true;
            _movementSpeedRank1 = true;
            _auraTimerRank1 = true;

            _itemDescription = "";


        }

        protected override void LoadContent()
        {
            //Spritebatch
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
           
            //Fonts
            _shopHeaderFont = Game.Content.Load<SpriteFont>("ShopHeadLine");
            _scoreFont = Game.Content.Load<SpriteFont>("ScoreFont");
            _itemDescFont = Game.Content.Load<SpriteFont>("ItemDescFont");
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
                _previousMState = Mouse.GetState(); 
            }
            base.Update(gameTime);
        }

        

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mainWindowTexture,_mainWindow, Color.White);
            _spriteBatch.DrawString(_shopHeaderFont, _myGame.Exp.CurrentExp + "$",new Vector2(1245, 240), Color.Green );
            _spriteBatch.DrawString(_itemDescFont, _itemDescription, new Vector2(_mainWindow.Left * 1.03f , _mainWindow.Bottom * 0.90f), new Color(205, 0,183));
            _spriteBatch.End();
            foreach (var shoptile in _shopTiles)
            {
                shoptile.Draw(gameTime, _shopTileTexts[_shopTiles.IndexOf(shoptile)].text, _shopTileTexts[_shopTiles.IndexOf(shoptile)].price + "$");
            }
        }

        //Health++
        private void IsHealthButtonPressed()
        {
            _itemDescription = "";
            if (Hover(_shopTiles[0].Rectangle))
            {
                if (_healthRank1)
                {
                    _itemDescription = "Increase max-health to 100.";
                }
                else if (_healthRank2)
                {
                    _itemDescription = "Increase max-health to 200.";
                }
                else if(_healthRank3)
                {
                    _itemDescription = "HP fully upgraded!";
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && _previousMState.LeftButton != ButtonState.Pressed)
                {
                    if (_myGame.Exp.CurrentExp >= _shopTileTexts[0].price)
                    {
                        IncreaseHealth(); 
                    }
                }
            }
        }

        private void IncreaseHealth()
        {
            if (_healthRank1)
            {
                HealthToRank2();
                _myGame.Exp.CurrentExp -= _shopTileTexts[0].price;

                _shopTileTexts[0].text = "Health++";
                _shopTileTexts[0].price = 1000;
            }

            else if (_healthRank2)
            {
                HealthToRank3();
                _myGame.Exp.CurrentExp -= _shopTileTexts[0].price;
                _shopTileTexts[0].text = "Health (Max)";
                _shopTileTexts[0].price = 0;
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
            //_itemDescription = "";
            if (Hover(_shopTiles[1].Rectangle))
            {
                if (_shieldRank1)
                {
                    _itemDescription = "Increase shield to 100.";
                }
                else if (_shieldRank2)
                {
                    _itemDescription = "Increase shield to 200.";
                }
                else
                {
                    _itemDescription = "Shield fully upgraded!";
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    if (_myGame.Exp.CurrentExp >= _shopTileTexts[1].price)
                    {
                        IncreaseShield(); 
                    } 
                }
            } 
        }

        private void IncreaseShield()
        {
            if (_shieldRank1)
            {
                ShieldToRank2();
                _myGame.Exp.CurrentExp -= _shopTileTexts[1].price;
                _shopTileTexts[1].text = "Shield++";
                _shopTileTexts[1].price = 1000;
            }

            else if (_shieldRank2)
            {
                ShieldToRank3();
                _myGame.Exp.CurrentExp -= _shopTileTexts[1].price;
                _shopTileTexts[1].text = "Shield (Max)";
                _shopTileTexts[1].price = 0;
            }
        }

        private void ShieldToRank2()
        {
            const int newMaxShield = 10;
                ), 
                null, 
                Color.White, 
                0f, 
                Vector2.Zero, 
                new Vector2(
                    0.6f * Globals.ScaleX, 
                    0.6f * Globals.ScaleY
                ), 
                SpriteEffects.None, 
                0f
            );
            _spriteBatch.DrawString(_shopHeadlineFont, "SHOP", new Vector2(1130 * Globals.ScaleX, 160 * Globals.ScaleY), Color.White);
            _spriteBatch.DrawString(_shopMoneyFont, "$" + _myGame.exp.CurrentExp, new Vector2(1300, 625), Color.Green);
            _spriteBatch.DrawString(_itemDescFont, "" + _myGame.ShopItem.ItemDescriptionString, new Vector2(1130, 530), Color.Black);
            _spriteBatch.DrawString(_itemDescFont, "Cost: " + _myGame.ShopItem.ItemCost + "$", new Vector2(1130, 630), Color.Black);

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
            //_itemDescription = "";
            if (Hover(_shopTiles[2].Rectangle))
            {
                if (_boostRank1)
                {
                    _itemDescription = "Increase number of boosts to 2.";
                }
                else if (_boostRank2)
                {
                    _itemDescription = "Increase number of boosts to 3.";
                }
                else
                {
                    _itemDescription = "Boost fully upgraded!";
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    if (_myGame.Exp.CurrentExp >= _shopTileTexts[2].price)
                    {
                        IncreaseNrOfBoosts();
                    }
                }
            } 
        }

        private void IncreaseNrOfBoosts()
        {
            if (_boostRank1)
            {
                BoostToRank2();
                _myGame.Exp.CurrentExp -= _shopTileTexts[2].price;
                _shopTileTexts[2].text = "Boost++";
                _shopTileTexts[2].price = 1000;
            }

            else if (_boostRank2)
            {
                BoostToRank3();
                _myGame.Exp.CurrentExp -= _shopTileTexts[2].price;
                _shopTileTexts[2].text = "Boost (Max)";
                _shopTileTexts[2].price = 0;
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
                if (_movementSpeedRank1)
                {
                    _itemDescription = "Increase Speed.";
                }
                else if (_movementSpeedRank2)
                {
                    _itemDescription = "Increase Speed."; ;
                }
                else
                {
                    _itemDescription = "Speed fully upgraded!";
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    if (_myGame.Exp.CurrentExp >= _shopTileTexts[3].price)
                    {
                        IncreaseMovementSpeed();
                    }
                    
                }
            }
        }

        private void IncreaseMovementSpeed()
        {
            if (_movementSpeedRank1)
            {
                MovementSpeedToRank2();
                _myGame.Exp.CurrentExp -= _shopTileTexts[3].price;
                _shopTileTexts[3].text = "Speed++";
                _shopTileTexts[3].price = 1000;
            }

            else if (_movementSpeedRank2)
            {
                MovementSpeedToRank3();
                _myGame.Exp.CurrentExp -= _shopTileTexts[3].price;
                _shopTileTexts[3].text = "Speed (Max)";
                _shopTileTexts[3].price = 0;
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
            _myGame.Player.SpeedMultiplier = 0.35f;
            _movementSpeedRank2 = false;
            _movementSpeedRank3 = true;
        }


        //BlasterReload--
        private void IsBlasterButtonPressed()
        {
            //_itemDescription = "";
            if (Hover(_shopTiles[4].Rectangle))
            {
                if (_reloadTimeRank1)
                {
                    _itemDescription = "Reload faster.";
                }
                else if (_reloadTimeRank2)
                {
                    _itemDescription = "Reload faster.";
                }
                else
                {
                    _itemDescription = "Faster reload fully upgraded!";
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {

                    if (_myGame.Exp.CurrentExp >= _shopTileTexts[4].price)
                    {
                        DecreaseBlasterReloadTime();
                    }
                }
            }
        }

        private void DecreaseBlasterReloadTime()
        {
            if (_reloadTimeRank1)
            {
                ReloadToRank2();
                _myGame.Exp.CurrentExp -= _shopTileTexts[4].price;
                _shopTileTexts[4].text = "Faster reload++";
                _shopTileTexts[4].price = 1000;
            }

            else if (_reloadTimeRank2)
            {
                ReloadToRank3();
                _myGame.Exp.CurrentExp -= _shopTileTexts[4].price;
                _shopTileTexts[4].text = "Faster reload (Max)";
                _shopTileTexts[4].price = 0;
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
            //_itemDescription = "";
            if (Hover(_shopTiles[5].Rectangle))
            {
                if (!_myGame.multiShot)
                {
                    _itemDescription = "Unlock Multishot.";
                }
                else if(_myGame.multiShot)
                {
                    _itemDescription = "Multishot unlocked!";
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
                {
                    if (_myGame.Exp.CurrentExp >= _shopTileTexts[5].price)
                    {
                        EnableMultiShot();
                        
                    }
                    
                }
            }
        }

        private void EnableMultiShot()
        {
            _myGame.multiShot = true;
            _myGame.Exp.CurrentExp -= _shopTileTexts[5].price;
            _shopTileTexts[5].text = "Multishot Activated!";
            _shopTileTexts[5].price = 0;
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
