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
        private Texture2D _shopWindowTexture;
        private Texture2D _itemBackground;
        private Texture2D _hoverRectangleTexture;

        //Fonts
        private SpriteFont _itemDescFont;
        private SpriteFont _shopHeaderFont;
        private SpriteFont _shopMoneyFont;
        
        //Rectangles
        private Rectangle _shopWindowRectangle;
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
            _shopWindowTexture = Game.Content.Load<Texture2D>("panel");
            _itemBackground = Game.Content.Load<Texture2D>("blue_button10");
            _hoverRectangleTexture = Game.Content.Load<Texture2D>("glassPanel_projection");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            
            _spriteBatch.Begin();
            _spriteBatch.End();
        }
    }
}
