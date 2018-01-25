using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    class Effects : DrawableGameComponent
    {

        private Texture2D enemyDamage;
        private SpriteBatch explosionBatch;
        private Vector2 Position;

        public Effects(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
        }

        protected override void LoadContent()
        {
            explosionBatch = new SpriteBatch(Game.GraphicsDevice);
            enemyDamage = Game.Content.Load<Texture2D>("burst");
            base.LoadContent();
        }

        public void Draw(GameTime gameTime, Vector2 enemyposition)
        {
            explosionBatch.Begin();
            explosionBatch.Draw(enemyDamage, new Vector2(Position.X + enemyposition.X, Position.Y +enemyposition.Y));
            Debug.WriteLine("Draw!");
            explosionBatch.End();
        }


    }
}
