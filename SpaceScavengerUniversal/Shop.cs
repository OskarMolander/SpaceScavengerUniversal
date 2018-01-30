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

        private readonly SpaceScavenger _myGame;
        private SpriteBatch _spriteBatch;

        //Textures
        private Texture2D _mainWindowTexture;
        private Texture2D _itemRectangleTexture;
        private Texture2D _hoverRectangleTexture;

        //Fonts
        private SpriteFont _itemDescFont;
        private SpriteFont _shopHeaderFont;
        private SpriteFont _shopMoneyFont;
        
        //Rectangles
        private Rectangle _mainWindow;
        private Rectangle _itemRectangle;
        private Rectangle _hoverRectangle;

        private KeyboardState _state;
        private GamePadState _gpState;
        //private string CloseShopString;

        public Shop(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
        }

        protected override void LoadContent()
        {
            //Spritebatch
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
           
            //Fonts
            _shopHeaderFont = Game.Content.Load<SpriteFont>("ShopHeadLine");
            _shopMoneyFont = Game.Content.Load<SpriteFont>("ScoreFont");
            _itemDescFont = Game.Content.Load<SpriteFont>("ItemDescFont");
            

            //Textures
            _mainWindowTexture = Game.Content.Load<Texture2D>("panel");
            _hoverRectangleTexture = Game.Content.Load<Texture2D>("glassPanel_projection");
            _itemRectangleTexture = Game.Content.Load<Texture2D>("itemrectangle");

            //Rectangles
            _mainWindow = new Rectangle(1220, 220, (int)(_mainWindowTexture.Width*0.75), (int)(_mainWindowTexture.Height * 0.75));
            //for (int i = 0; i < 7; i++)
            //{
            //    _itemRectangle = new Rectangle(1250, 310 + i * _itemRectangleTexture.Height, (int)(_itemRectangleTexture.Width * 0.7), (int)(_itemRectangleTexture.Height * 0.8));
            //}
            //base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mainWindowTexture,_mainWindow, Color.White);
            for (var i = 0; i < 7; i++)
            {
                _spriteBatch.Draw(_itemRectangleTexture,new Rectangle(1250, 310 + i*_itemRectangleTexture.Height, (int)(_itemRectangleTexture.Width * 0.7), (int)(_itemRectangleTexture.Height*0.8)), Color.White);    
            }
            _spriteBatch.End();
        }
    }
}
