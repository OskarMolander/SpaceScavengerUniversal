using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class StartMenu : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private readonly SpaceScavenger _myGame;
        private SpriteFont _menufont;
        private Texture2D _background;
        private Texture2D InsertCoinTexture;
        private MovingMenu movingMenu;
        private Texture2D MenuText;
        private int flashingTextTimer = 0;
        private bool flashingTextState = true;
        public float startY, startX;


        public StartMenu(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) Game;
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _menufont = Game.Content.Load<SpriteFont>("ScoreFont");
            _background = Game.Content.Load<Texture2D>("StartBackgroundNeon");
            MenuText = Game.Content.Load<Texture2D>("MenuText");
            InsertCoinTexture = Game.Content.Load<Texture2D>("InsertCoinText");
            movingMenu = new MovingMenu();
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            movingMenu.Update(gameTime);

            if(flashingTextTimer > 0)
            {
                flashingTextTimer--;
            }
            else if (flashingTextTimer <= 0)
            {
                if (flashingTextState)
                {
                    flashingTextState = false;
                }
                else
                {
                    flashingTextState = true;
                }
                flashingTextTimer = 60;
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            startX = movingMenu.Position.X % _background.Width;
            startY = movingMenu.Position.Y % _background.Height;

            for (float y = -startY - _background.Height; y < Globals.ScreenHeight; y += _background.Width)
            {
                for (float x = -startX - _background.Width; x < Globals.ScreenWidth; x += _background.Width)
                {
                    _spriteBatch.Draw(_background, new Vector2(x, y), Color.White);

                }
            }
            _spriteBatch.Draw(MenuText, new Vector2(Globals.ScreenWidth / 2 - MenuText.Width / 2, Globals.ScreenHeight / 2 - 300 - MenuText.Height / 2), Color.White);

            if (flashingTextState)
                _spriteBatch.Draw(InsertCoinTexture, new Vector2(Globals.ScreenWidth / 2 - InsertCoinTexture.Width / 2, Globals.ScreenHeight / 2 + 300 - InsertCoinTexture.Height / 2), Color.White);
            _spriteBatch.End();
        }

    }
}
