using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceScavengerUniversal;

namespace Space_Scavenger
{
    public class Shop : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private readonly SpaceScavenger _myGame;
        private Texture2D _shopPanel;
        public Texture2D SmallPanel;
        private SpriteFont _shopHeadlineFont;
        private SpriteFont _shopMoneyFont;
        public Rectangle RectangleHover;
        public Texture2D HoverTexture;
        private KeyboardState _state;
        private GamePadState _gpState;
        private SpriteFont _itemDescFont;
        private string CloseShopString;
       
        
        

        public Shop(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
            
           
            
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _shopPanel = Game.Content.Load<Texture2D>("panel");
            SmallPanel = Game.Content.Load<Texture2D>("blue_button10");
            _shopHeadlineFont = Game.Content.Load<SpriteFont>("ShopHeadLine");
            HoverTexture = Game.Content.Load<Texture2D>("glassPanel_projection");
            _shopMoneyFont = Game.Content.Load<SpriteFont>("ScoreFont");
            _itemDescFont = Game.Content.Load<SpriteFont>("ItemDescFont");

           
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var x = HoverTexture.Width + 10;

               if ((int)gameTime.TotalGameTime.TotalMilliseconds % 20 == 0)
               {
                _state = Keyboard.GetState();
                   _gpState = GamePad.GetState(PlayerIndex.One);
                   if (_state.IsKeyDown(Keys.D) || _gpState.IsButtonDown(Buttons.LeftThumbstickRight) || _gpState.IsButtonDown(Buttons.DPadRight))
                   {

                        if(RectangleHover.X < 1120 + 2*x)
                        RectangleHover.X += x/2;

                   } 
                   else if (_state.IsKeyDown(Keys.A) || _gpState.IsButtonDown(Buttons.LeftThumbstickLeft) || _gpState.IsButtonDown(Buttons.DPadLeft))
                   {
                       if(RectangleHover.X > 1120)
                       RectangleHover.X -= x/2;
                   }

                   if (_state.IsKeyDown(Keys.S) || _gpState.IsButtonDown(Buttons.LeftThumbstickDown) || _gpState.IsButtonDown(Buttons.DPadDown))
                   {
                       if (RectangleHover.Y < 205 + 2*x)
                           RectangleHover.Y += x / 2;
                   }
                   else if (_state.IsKeyDown(Keys.W) || _gpState.IsButtonDown(Buttons.LeftThumbstickUp) || _gpState.IsButtonDown(Buttons.DPadUp))
                   {
                       if (RectangleHover.Y > 205)
                           RectangleHover.Y -= x / 2;
                   }
               }

           
           
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            
            _spriteBatch.Draw(_shopPanel, new Vector2(1100, 150), null, Color.White, 0f, Vector2.Zero, new Vector2(0.6f, 0.6f), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopHeadlineFont, "SHOP", new Vector2(1130, 160), Color.White);
            _spriteBatch.DrawString(_shopMoneyFont, "$" + _myGame.exp.CurrentExp, new Vector2(1300, 625), Color.Green);
            _spriteBatch.DrawString(_itemDescFont, "" + _myGame.ShopItem.ItemDescriptionString, new Vector2(1130, 530), Color.Black);
            _spriteBatch.DrawString(_itemDescFont, "Cost: " + _myGame.ShopItem.ItemCost + "$", new Vector2(1130, 630), Color.Black);

            

            for (int i = 0; i < 3; i++)
            {
                _spriteBatch.Draw(SmallPanel, new Vector2(1120 + (i*110), 210), null, Color.White, 0f, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);

                    for (int k = 0; k < 2; k++)
                    {
                        _spriteBatch.Draw(SmallPanel, new Vector2(1120 + (i*110), 320), null, Color.White, 0f, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);
                       for (int l = 0; l < 2; l++)
                       {
                          for (int m = 0; m < 1; m++)
                          {
                                 _spriteBatch.Draw(SmallPanel, new Vector2(1120 + (i*110), 430), null, Color.White, 0f, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);
                          }
                       }
                    }
            }
            
            _spriteBatch.Draw(HoverTexture, RectangleHover, Color.White);
            char button = App.IsXbox() || ControllerValidator.IsGamePadConnected() ? 'Y' : 'E';
            _spriteBatch.DrawString(_shopMoneyFont,$"Press {button} to close the shop", new Vector2(Globals.ScreenWidth / 2f - 300, Globals.ScreenHeight / 2f - 300), Color.White);

            _spriteBatch.End();
        }
    }
}
