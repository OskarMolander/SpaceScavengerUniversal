using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Scavenger;

namespace SpaceScavengerUniversal
{
    class ShopTile : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private SpriteBatch _spriteBatch;

        private Texture2D _rectangleTexture;
        private Texture2D _rectangleTextures;
        private Texture2D _rectangleTextureOrig;
        private Texture2D _rectangleHoverTexture;
        private Rectangle _rectangle;
        private SpriteFont _font;
        private readonly int Y;
        private int X;

        


        public ShopTile(Game game, int x, int y) : base(game)
        {
            this.Y = y;
            this.X = x;
            _myGame = (SpaceScavenger)game;
        }

        public void LoadContent()
        {
            //Spritebatch
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //Fonts
            _font = Game.Content.Load<SpriteFont>("ItemDescFont");
            //Textures
            _rectangleTexture = Game.Content.Load<Texture2D>("itemrectangle");
            //_rectangleTextureOrig = Game.Content.Load<Texture2D>("itemrectangle");
            _rectangleHoverTexture = Game.Content.Load<Texture2D>("itemrectangle-hover");
            //Rectangle
            _rectangle = new Rectangle(X, Y, (int)(_rectangleTexture.Width * 0.7), (int)(_rectangleTexture.Height * 0.8));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Hover(_rectangle))
            {
                _rectangleTexture = Game.Content.Load<Texture2D>("itemrectangle-hover");
            }
            else
                _rectangleTexture = Game.Content.Load<Texture2D>("itemrectangle");
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_rectangleTexture,_rectangle,Color.White);
            _spriteBatch.DrawString(_font, "Item", new Vector2(_rectangle.X / 2f, _rectangle.Y / 2f), Color.Red);
            _spriteBatch.End();
        }

        public bool Hover(Rectangle rectangle)
        {
            if (new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1).Intersects(rectangle))
            {
                return true;
            }

            return false;
            
        }
    }
}
