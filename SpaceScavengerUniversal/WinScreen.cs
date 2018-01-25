using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Scavenger
{
    public class WinScreen : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private readonly SpaceScavenger _myGame;
        private KeyboardState keyboardState, _prevKeyboardState;
        private SpriteFont _gameOverFont;
        public Texture2D GameOverTexture2D, GameOverFilter, PressSpaceTexture2D, YouWon;
        private int TotalScore;

        public WinScreen(Game game) : base(game)
        {
            _myGame = (SpaceScavenger)Game;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _gameOverFont = Game.Content.Load<SpriteFont>("ScoreFont");
            YouWon = Game.Content.Load<Texture2D>("YouWon");
            GameOverFilter = Game.Content.Load<Texture2D>("Transparent-filter");
            PressSpaceTexture2D = Game.Content.Load<Texture2D>("PressSpace");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (_myGame.gamestate == GameState.Winscreen)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && _prevKeyboardState.IsKeyUp(Keys.Space))
                {
                    _myGame.Player.Position = Vector2.Zero;
                    _myGame.Player.Speed = Vector2.Zero;
                    _myGame.Money.Moneyroids.Clear();
                    _myGame.Exp.CurrentScore = 0;
                    _myGame.Exp.CurrentExp = 0;
                    _myGame.BossKill = false;
                    //MediaPlayer.Play(_myGame.BackgroundSong);
                    _myGame.gamestate = GameState.Menu;

                }
                _prevKeyboardState = Keyboard.GetState();
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 textSize2 = _gameOverFont.MeasureString("Enemies Defeated: " + _myGame.defeatedEnemies);
            Vector2 textMiddlePoint2 = new Vector2(textSize2.X / 2, textSize2.Y / 2);
            Vector2 textPosition2 = new Vector2((int)textMiddlePoint2.X - textSize2.X, (int)textMiddlePoint2.Y - textSize2.Y);
            Vector2 textSize = _gameOverFont.MeasureString("Your Score is: " + _myGame.Exp.CurrentScore);
            Vector2 textMiddlePoint = new Vector2(textSize.X / 2, textSize.Y / 2);
            Vector2 textPosition = new Vector2((int)textMiddlePoint.X - textSize.X, (int)textMiddlePoint.Y - textSize.Y);
            _spriteBatch.Begin();
            _spriteBatch.Draw(YouWon, new Vector2(Globals.ScreenWidth / 2f - (YouWon.Width / 2f), Globals.ScreenHeight / 2f - (YouWon.Height / 2f) - 200), Color.White);
            _spriteBatch.Draw(PressSpaceTexture2D, new Vector2(Globals.ScreenWidth / 2f - PressSpaceTexture2D.Width / 4f, Globals.ScreenHeight / 2f + 200), null, Color.White, 0.05f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_gameOverFont, "Your Score is: " + _myGame.Exp.CurrentScore, new Vector2(Globals.ScreenWidth / 2f + textPosition.X, Globals.ScreenHeight / 2f - textPosition.Y + 50), Color.SteelBlue);
            _spriteBatch.DrawString(_gameOverFont, "Enemies Defeated: " + _myGame.defeatedEnemies, new Vector2(Globals.ScreenWidth / 2f + textPosition2.X, Globals.ScreenHeight / 2f - textPosition2.Y + 100), Color.SteelBlue);
            _spriteBatch.End();



        }
    }
}