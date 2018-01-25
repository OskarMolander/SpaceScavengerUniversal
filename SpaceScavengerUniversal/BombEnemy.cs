using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class BombEnemy : GameObject

    {

        private SpaceScavenger MyGame;
        private readonly Random rnd = new Random();
        public BombEnemy BombEnemySpawn(Game game)
        {
            MyGame = (SpaceScavenger)game;

            MyGame.gamestate = GameState.Playing;
            int Spawnside = rnd.Next(1, 5);
            switch (Spawnside)
            {
                case 1:

                    return new BombEnemy
                    {
                        //vänster
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = MyGame.EnemyRotation,
                        Position = new Vector2(MyGame.Player.Position.X - MyGame.Window.ClientBounds.X - rnd.Next(1000, Globals.ScreenWidth * 3), MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 3600))
                    };
                case 2:
                    //höger
                    return new BombEnemy()
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = MyGame.EnemyRotation,
                        Position = new Vector2(MyGame.Player.Position.X + rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) + MyGame.Window.ClientBounds.X, MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 3600))
                    };
                case 3:
                    //upp
                    return new BombEnemy()
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = MyGame.EnemyRotation,
                        Position = new Vector2(
                            MyGame.Player.Position.X + rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 0))
                    };
                case 4:
                    //ner
                    return new BombEnemy()
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = MyGame.EnemyRotation,
                        Position = new Vector2(
                            MyGame.Player.Position.X + rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Y + rnd.Next(1200, 2400))
                    };
            }
            return null;
        }

        public void Update(GameTime gameTime, Game game)
        {
            MyGame = (SpaceScavenger)game;
            var followDistance = 1500;

            var direction = MyGame.Player.Position - Position;
            direction.Normalize();
            Speed += direction * 0.2f;

            if (Speed.LengthSquared() > 25)
                Speed = Vector2.Normalize(Speed) * 5;

            var xDiffPlayer = Math.Abs(Position.X - MyGame.Player.Position.X);
            var yDiffPlayer = Math.Abs(Position.Y - MyGame.Player.Position.Y);

            if (xDiffPlayer < followDistance && yDiffPlayer < followDistance)
            {
                    Position += Speed;
            }
        }
    }
}