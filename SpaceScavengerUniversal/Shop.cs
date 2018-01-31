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
        private List<ShopTile> _shopTiles;

        //Textures
        private Texture2D _mainWindowTexture;


        //Fonts
        private SpriteFont _itemDescFont;
        private SpriteFont _shopHeaderFont;
        private SpriteFont _shopMoneyFont;
        
        //Rectangles
        private Rectangle _mainWindow;


        public Shop(Game game) : base(game)
        {   
            _shopTiles = new List<ShopTile>();
            
            _myGame = (SpaceScavenger) game;
            for (int i = 0; i < 7; i++)
            {
                _shopTiles.Add(new ShopTile(_myGame, 1250 ,300 + (i*80)));
            }
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
                shoptile.Draw(gameTime);
            }
        }
    }
}
