using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    public class ShopItem : DrawableGameComponent
    {
        private Texture2D _itemBetterWeapon;
        private Texture2D _itemPlusMaxHealth;
        private Texture2D _itemPlusMaxShield;
        private readonly SpaceScavenger _myGame;
        private int _rectangleHeight;
        private Rectangle _rectangleItemEight;
        private Rectangle _rectangleItemFive;
        private Rectangle _rectangleItemFour;
        private Rectangle _rectangleItemNine;
        private Rectangle _rectangleItemOne;
        private Rectangle _rectangleItemSeven;
        private Rectangle _rectangleItemSix;
        private Rectangle _rectangleItemThree;
        private Rectangle _rectangleItemTwo;
        private int _rectangleStartX;
        private int _rectangleStartY;
        private int _rectangleWidth;
        private SpriteFont _shopfont;


        private SpriteBatch _spriteBatch;
        private KeyboardState _state;
        private GamePadState _gpState;
        private int _x;
        private int _y;


        public ShopItem(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) Game;
        }

        public string ItemDescriptionString { get; private set; }
        public int ItemCost { get; private set; }


        protected override void LoadContent()
        {
            _rectangleWidth = _myGame._shop.SmallPanel.Width;
            _rectangleHeight = _myGame._shop.SmallPanel.Height;
            _rectangleStartX = 1120;
            _rectangleStartY = 210;
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _itemPlusMaxHealth = Game.Content.Load<Texture2D>("shopicon_health");
            _shopfont = Game.Content.Load<SpriteFont>("shopfont");
            _itemPlusMaxShield = Game.Content.Load<Texture2D>("shopicon_shield");
            _itemBetterWeapon = Game.Content.Load<Texture2D>("crossair_redOutline");

            _myGame._shop.RectangleHover = new Rectangle(_rectangleStartX, 205, _myGame._shop.HoverTexture.Width,
                _myGame._shop.HoverTexture.Height);
            _rectangleItemOne = new Rectangle(_rectangleStartX, _rectangleStartY, _rectangleWidth, _rectangleHeight);
            _rectangleItemTwo = new Rectangle(_rectangleStartX + 110, _rectangleStartY, _rectangleWidth,
                _rectangleHeight);
            _rectangleItemThree = new Rectangle(_rectangleStartX + 110 * 2, _rectangleStartY, _rectangleWidth,
                _rectangleHeight);
            _rectangleItemFour = new Rectangle(_rectangleStartX, _rectangleStartY + 110, _rectangleWidth,
                _rectangleHeight);
            _rectangleItemFive = new Rectangle(_rectangleStartX + 110, _rectangleStartY + 110, _rectangleWidth,
                _rectangleHeight);
            _rectangleItemSix = new Rectangle(_rectangleStartX + 110 * 2, _rectangleStartY + 110, _rectangleWidth,
                _rectangleHeight);
            _rectangleItemSeven = new Rectangle(_rectangleStartX, _rectangleStartY + 110 * 2, _rectangleWidth,
                _rectangleHeight);
            _rectangleItemEight = new Rectangle(_rectangleStartX + 110, _rectangleStartY + 110 * 2, _rectangleWidth,
                _rectangleHeight);
            _rectangleItemNine = new Rectangle(_rectangleStartX + 110 * 2, _rectangleStartY + 110 * 2, _rectangleWidth,
                _rectangleHeight);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _state = Keyboard.GetState();
            _gpState = GamePad.GetState(PlayerIndex.One);

            if (_myGame.gamestate == GameState.Shopping)
            {
                #region 1-3 MaxHealth++

                if (_myGame._shop.RectangleHover.Intersects(_rectangleItemOne))
                    if (_myGame.Player.MaxHealth == 5)
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "Increased Maxhealth" + "\r\n" + "(100%)";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 10;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 300;
                            }
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already bought this item";
                    }

                if (_myGame._shop.RectangleHover.Intersects(_rectangleItemTwo))
                    if (_myGame.Player.MaxHealth == 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Increased Maxhealth" + "\r\n" + "(150%)";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 15;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 600;
                            }
                    }
                    else if (_myGame.Player.MaxHealth < 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Locked!" + "\r\n" + "Unlock previous upgrade first!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already bought this item";
                    }

                if (_myGame._shop.RectangleHover.Intersects(_rectangleItemThree))
                    if (_myGame.Player.MaxHealth == 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Increased MaxHealth" + "\r\n" + "(200%)";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 20;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 1000;
                            }
                    }
                    else if (_myGame.Player.MaxHealth < 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Locked!" + "\r\n" + "Unlock previous upgrades first!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already bought this item";
                    }

                #endregion

                #region  3-6 Shield++

                if (_myGame._shop.RectangleHover.Intersects(_rectangleItemFour))
                    if (_myGame.Player.MaxShield == 5)
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "Increased MaxShield" + "\r\n" + "(100%)";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxShield = 10;
                                _myGame.Player.Shield = _myGame.Player.MaxShield;
                                _myGame.Exp.CurrentExp -= 300;
                            }
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                else if (_myGame._shop.RectangleHover.Intersects(_rectangleItemFive))
                    if (_myGame.Player.MaxShield == 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Increased MaxShield" + "\r\n" + "(150%)";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxShield = 15;
                                _myGame.Player.Shield = _myGame.Player.MaxShield;
                                _myGame.Exp.CurrentExp -= 600;
                            }
                    }
                    else if (_myGame.Player.Shield < 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Locked!" + "\r\n" + "Unlock previous upgrade first!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }

                else if (_myGame._shop.RectangleHover.Intersects(_rectangleItemSix))
                    if (_myGame.Player.MaxShield == 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Increased MaxShield" + "\r\n" + "(200%)";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxShield = 20;
                                _myGame.Player.Shield = _myGame.Player.MaxShield;
                                _myGame.Exp.CurrentExp -= 1000;
                            }
                    }
                    else if (_myGame.Player.MaxShield < 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Locked!" + "\r\n" + "Unlock previous upgrades first!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }

                #endregion

                #region 6-9 Weapons++

                if (_myGame._shop.RectangleHover.Intersects(_rectangleItemSeven))
                    if (!_myGame.fasterLaser)
                    {
                        ItemDescriptionString = "Increased Laserspeed.";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                        {
                            ItemCost = 300;
                            _myGame.fasterLaser = true;
                            _myGame.Exp.CurrentExp -= 300;
                        }
                    }
                    else
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }


                else if (_myGame._shop.RectangleHover.Intersects(_rectangleItemEight))
                    if (_myGame.fasterLaser && !_myGame.multiShot)
                    {
                        ItemDescriptionString = "DoubleShot.";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                        {
                            ItemCost = 600;
                            _myGame.multiShot = true;
                            _myGame.fasterLaser = false;
                            _myGame.Exp.CurrentExp -= 600;
                        }
                    }
                    else if (!_myGame.fasterLaser && _myGame.multiShot)
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                    else if (_myGame.fasterLaser && _myGame.multiShot)
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                    else
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Locked!" + "\r\n" + "Unlock previous upgrade first!";
                    }

                else if (_myGame._shop.RectangleHover.Intersects(_rectangleItemNine))
                    if (_myGame.multiShot && !_myGame.fasterLaser)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Doubleshot + Increased Laserspeed.";
                        if (_state.IsKeyDown(Keys.Space) || _gpState.IsButtonDown(Buttons.A))
                        {
                            ItemCost = 1000;
                            _myGame.fasterLaser = true;
                            _myGame.Exp.CurrentExp -= 1000;
                        }
                    }
                    else if (_myGame.fasterLaser && _myGame.multiShot)
                    {
                        ItemCost = 0;
                        ItemDescriptionString = ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                    else
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Locked!" + "\r\n" + "Unlock previous upgrades first!";
                    }

                #endregion
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _x = _rectangleItemOne.X + _myGame._shop.SmallPanel.Width / 2 + 10;
            _y = _rectangleItemOne.Y + _myGame._shop.SmallPanel.Height / 2;
            _spriteBatch.Begin();

            //Health
            _spriteBatch.Draw(_itemPlusMaxHealth, new Vector2(_x, _y), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "100% HP", new Vector2(_x - 20, _y + 30), Color.White);

            _spriteBatch.Draw(_itemPlusMaxHealth, new Vector2(_x + 110, _y), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "150% HP", new Vector2(_x + 90, _y + 30), Color.White);

            _spriteBatch.Draw(_itemPlusMaxHealth, new Vector2(_x + 220, _y), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "200% HP", new Vector2(_x + 200, _y + 30), Color.White);

            //Shield
            _spriteBatch.Draw(_itemPlusMaxShield, new Vector2(_x, _y + 110), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "100% HP", new Vector2(_x - 20, _y + 140), Color.White);

            _spriteBatch.Draw(_itemPlusMaxShield, new Vector2(_x + 110, _y + 110), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "150% HP", new Vector2(_x + 90, _y + 140), Color.White);

            _spriteBatch.Draw(_itemPlusMaxShield, new Vector2(_x + 220, _y + 110), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "200% HP", new Vector2(_x + 200, _y + 140), Color.White);

            //Weapon
            _spriteBatch.Draw(_itemBetterWeapon, new Vector2(_x, _y + 215), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "Speed++", new Vector2(_x - 20, _y + 252), Color.White, 0f, Vector2.Zero,
                0.8f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(_itemBetterWeapon, new Vector2(_x + 110, _y + 215), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "Multishot", new Vector2(_x + 85, _y + 252), Color.White, 0f, Vector2.Zero,
                0.8f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(_itemBetterWeapon, new Vector2(_x + 220, _y + 215), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1, 1), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopfont, "Combo", new Vector2(_x + 210, _y + 252), Color.White, 0f, Vector2.Zero,
                0.8f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
    }
}