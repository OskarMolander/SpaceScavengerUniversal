using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
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
        private readonly int X;



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
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, string text, string cost)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(Hover(_rectangle) ? _rectangleHoverTexture : _rectangleTexture, _rectangle, Color.White );
            _spriteBatch.DrawString(_font, text, new Vector2(_rectangle.Center.X - 150,_rectangle.Center.Y - 10), new Color(205,0,183));
            _spriteBatch.DrawString(_font, cost, new Vector2(_rectangle.Center.X + 115, _rectangle.Center.Y - 10), new Color(205, 0, 183));
            _spriteBatch.End();
        }

        public Rectangle Rectangle => _rectangle;

        public static bool Hover(Rectangle rectangle)
        {
            return new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1).Intersects(rectangle);
        }
    }
}
