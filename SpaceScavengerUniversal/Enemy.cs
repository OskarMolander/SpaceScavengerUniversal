using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Enemy : GameObject

    {
        private Texture2D enemyTexture;
        private SpaceScavenger MyGame;
        private int reloadTime;
        
        private readonly Random rnd = new Random();




        public Enemy enemySpawn(Game game)
        {
            MyGame = (SpaceScavenger)game;

            MyGame.gameState = GameState.Playing;
            int Spawnside = rnd.Next(1, 5);
            switch (Spawnside)
            {
                case 1:

                    return new Enemy
                    {
                        //vänster
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(MyGame.Player.Position.X - MyGame.Window.ClientBounds.X - rnd.Next(1000, Globals.ScreenWidth * 3), MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 3600))
                    };
                case 2:
                    //höger
                    return new Enemy()
                    {
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(MyGame.Player.Position.X + rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) + MyGame.Window.ClientBounds.X, MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 3600))
                    };
                case 3:
                    //upp
                    return new Enemy()
                    {
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            MyGame.Player.Position.X + rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 0))
                    };
                case 4:
                    //ner
                    return new Enemy()
                    {
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            MyGame.Player.Position.X + rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Y + rnd.Next(1200, 2400))
                    };
            }





            return null;
        }

        public Shot EnemyShoot()
        {
            if (reloadTime <= 0)
                return new Shot
                {
                    Timer = 200,
                    Position = Position - new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10)),
                    Rotation = Rotation,
                    Speed = 15f * new Vector2((float) Math.Cos(Rotation - MathHelper.PiOver2),
                                (float) Math.Sin(Rotation - MathHelper.PiOver2))
                };

            return null;
        }

        public void Update(GameTime gameTime, Game game)
        {
            MyGame = (SpaceScavenger) game;
            var followDistance = 1000;
            var aimDistance = 100;

            var direction = MyGame.Player.Position - Position;
            direction.Normalize();
            Speed += direction * 0.2f;

            if (Speed.LengthSquared() > 25)
                Speed = Vector2.Normalize(Speed) * 5;

            var xDiffPlayer = Math.Abs(Position.X - MyGame.Player.Position.X);
            var yDiffPlayer = Math.Abs(Position.Y - MyGame.Player.Position.Y);


            var targetrotation = (float) Math.Atan2(Position.X - MyGame.Player.Position.X,
                Position.Y - MyGame.Player.Position.Y);

            if (targetrotation < 360)
                Rotation += 360;
            else if (targetrotation > 360)
                Rotation -= 360;

            Rotation = -targetrotation;

            if (reloadTime > 0)
                reloadTime--;


            if (xDiffPlayer < followDistance && yDiffPlayer < followDistance)
            {
                if (xDiffPlayer > aimDistance || yDiffPlayer > aimDistance)
                    Position += Speed;
                else
                    Speed -= Speed;
                if (xDiffPlayer < 300 || yDiffPlayer < 300)
                    if (reloadTime <= 0)
                    {
                        var s = EnemyShoot();
                        if (s != null)
                        {
                            MyGame.enemyShots.Add(s);
                            if (MyGame.soundEffectTimer <= 0)
                            {
                                //MyGame.EnemyShootEffect.Play(0.8f, 0.0f, 0.0f);
                                MyGame.soundEffectTimer = 15;
                            }
                            reloadTime += 60;
                        }
                    }
            }

        }
    }
}