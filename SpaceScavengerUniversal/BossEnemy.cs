using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class BossEnemy : GameObject
    {
        private readonly Random _rnd = new Random();
        private int _aoECase = 1;
        private int _aoECaseDirection = 1;
        private int _reloadTimer2;

        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger) game;

            if (Speed.LengthSquared() > 25)
                Speed = Vector2.Normalize(Speed) * 5;

            Position += Speed;

            var direction = MyGame.Player.Position - Position;
            direction.Normalize();

            if (_reloadTimer2 <= 0)
            {
                var s = BossShoot(direction);
                if (s != null)
                    MyGame.EnemyShots.Add(s);
                _reloadTimer2 = 10;
            }

            if (ReloadTimer <= 0)
            {
                var sa1 = BossShootAoE(direction);
                if (sa1 != null)
                {
                    sa1.Radius = 30;
                    sa1.chosenTexture2D = MyGame.BossShotTexture;
                    MyGame.BossShots.Add(sa1);
                }
                var sa2 = BossShootAoE(direction);
                if (sa2 != null)
                {
                    sa2.Radius = 30;
                    sa2.chosenTexture2D = MyGame.BossShotTexture;
                    MyGame.BossShots.Add(sa2);
                }
                if (Health > 60)
                    ReloadTimer = 60;
                else if (Health > 30)
                    ReloadTimer = 10;
                else
                    ReloadTimer = 2;
            }

            if (ReloadTimer > 0)
                ReloadTimer--;
            if (_reloadTimer2 > 0)
                _reloadTimer2--;
        }

        public Shot BossShoot(Vector2 direction)
        {
            return new Shot
            {
                Timer = 500,
                Position = Position,
                Rotation = (float) Math.Atan2(direction.X, -direction.Y),
                Speed = 10f * direction
            };
        }

        public Shot BossShootAoE(Vector2 direction)
        {
            switch (_aoECaseDirection)
            {
                case 1:

                    #region Up/Down

                    switch (_aoECase)
                    {
                        case 1:
                            //up
                            _aoECase = 2;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(0, 1),
                                Speed = 5f * new Vector2(0, -1)
                            };


                        case 2:
                            //down
                            _aoECase = 1;
                            _aoECaseDirection = 2;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(0, -1),
                                Speed = 5f * new Vector2(0, 1)
                            };
                    }
                    return null;

                #endregion

                case 2:

                    #region Up+45/Down+45

                    switch (_aoECase)
                    {
                        case 1:
                            //right
                            _aoECase = 2;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(-0.5, -0.5),
                                Speed = 3f * new Vector2(-1, 1)
                            };

                        case 2:
                            //left
                            _aoECase = 1;
                            _aoECaseDirection = 3;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(0.5, 0.5),
                                Speed = 3f * new Vector2(1, -1)
                            };
                    }
                    return null;

                #endregion

                case 3:

                    #region Left/Right

                    switch (_aoECase)
                    {
                        case 1:
                            _aoECase = 2;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(-1, 0),
                                Speed = 5f * new Vector2(-1, 0)
                            };

                        case 2:
                            _aoECase = 1;
                            _aoECaseDirection = 4;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(1, 0),
                                Speed = 5f * new Vector2(1, 0)
                            };
                    }
                    return null;

                #endregion

                case 4:

                    #region Up-45/Down-45

                    switch (_aoECase)
                    {
                        case 1:
                            _aoECase = 2;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(0.5, -0.5),
                                Speed = 3f * new Vector2(1, 1)
                            };

                        case 2:
                            _aoECase = 1;
                            _aoECaseDirection = 1;
                            return new Shot
                            {
                                Timer = 500,
                                Position = Position,
                                Rotation = (float) Math.Atan2(-0.5, 0.5),
                                Speed = 3f * new Vector2(-1, -1)
                            };
                    }
                    return null;

                #endregion

                default:
                    return null;
            }
        }

        public BossEnemy SpawnBoss(Game game)
        {
            MyGame = (SpaceScavenger) game;

            var spawnside = _rnd.Next(1, 5);
            switch (spawnside)
            {
                case 1:

                    return new BossEnemy
                    {
                        //vänster
                        Timer = 3600,
                        Radius = 150,
                        Health = 100,
                        ExpReward = 100,
                        ScoreReward = 1000,
                        Position = new Vector2(
                            MyGame.Player.Position.X - MyGame.Window.ClientBounds.X -
                            _rnd.Next(1000, Globals.ScreenWidth * 3),
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600)),
                        Rotation = MathHelper.PiOver2,
                        Speed = new Vector2(0, 0) /*new Vector2(1,0)*/
                    };
                case 2:
                    //höger
                    return new BossEnemy
                    {
                        Timer = 3600,
                        Radius = 150,
                        Health = 100,
                        ExpReward = 100,
                        ScoreReward = 1000,
                        Rotation = -MathHelper.PiOver2,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600)),
                        Speed = new Vector2(0, 0) /*new Vector2(-1,0)*/
                    };
                case 3:
                    //upp
                    return new BossEnemy
                    {
                        Timer = 3600,
                        Radius = 150,
                        Health = 100,
                        ExpReward = 100,
                        ScoreReward = 1000,
                        Rotation = MathHelper.Pi,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 0)),
                        Speed = new Vector2(0, 0) /*new Vector2(0,1)*/
                    };
                case 4:
                    //ner
                    return new BossEnemy
                    {
                        Timer = 3600,
                        Radius = 150,
                        Health = 100,
                        ExpReward = 100,
                        ScoreReward = 1000,
                        Rotation = Rotation,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Y + _rnd.Next(1200, 2400)),
                        Speed = new Vector2(0, 0) /*new Vector2(0,-1)*/
                    };
            }

            return null;
        }
    }
}