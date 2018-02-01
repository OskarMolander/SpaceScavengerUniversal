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
        private List<ShopTile> _shopTiles;
        private string[] _shopTileTexts; 
        private ShopTile _shopTile;

        private MouseState _previousMState;

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

        private bool _reloadTimeRank1;
        private bool _reloadTimeRank2;
        private bool _reloadTimeRank3;

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
            _shopTileTexts = new string[_shopTiles.Count];
            _shopTileTexts[0] = "Health";
            _shopTileTexts[1] = "Shield";
            _shopTileTexts[2] = "Boost";
            _shopTileTexts[3] = "Movement";
            _shopTileTexts[4] = "Reload Time";
            _shopTileTexts[5] = "Multishot";
            _shopTileTexts[6] = "Aura";

            _healthRank1 = true;
            _shieldRank1 = true;
            _boostRank1 = true;
            _reloadTimeRank1 = true;
            _nextHealthRankString = "Rank 2";
            _nextShieldRankString = "Rank 2";
            _nextBoostRankString = "Rank 2";

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
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _previousMState.LeftButton != ButtonState.Pressed)
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
            }

            else if (_healthRank2)
            {
                HealthToRank3();
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
            }

            else if (_shieldRank2)
            {
                ShieldToRank3();
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
            }

            else if (_boostRank2)
            {
                BoostToRank3();
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
            }

            else if (_reloadTimeRank2)
            {
                ReloadToRank3();
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

    }
}
