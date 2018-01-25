using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class TreasureShip : GameObject
    {
        private readonly Random _rnd = new Random();
        private int _aoECaseDirection = 1;

        public void Update(GameTime gametime, Game game)
        {

            MyGame = (SpaceScavenger)game;

            var direction = MyGame.Player.Position - Position;
            direction.Normalize();

            if (ReloadTimer <= 0)
            {
                var sa1 = TreasueShootAoE(direction);
                if (sa1 != null)
                {
                    sa1.Radius = 30;
                    sa1.chosenTexture2D = MyGame.BossShotTexture2;
                    MyGame.BossShots.Add(sa1);
                }
                ReloadTimer = 5;
            }
            ReloadTimer--;
        }

        public TreasureShip SpawnTreasureShip(Game game)
        {
            MyGame = (SpaceScavenger)game;

            var spawnside = _rnd.Next(1, 5);
            switch (spawnside)
            {
                case 1:

                    return new TreasureShip()
                    {
                        //vänster
                        Timer = 7200,
                        Radius = 50,
                        Health = 30,
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
                    return new TreasureShip()
                    {
                        Timer = 7200,
                        Radius = 50,
                        Health = 30,
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
                    return new TreasureShip()
                    {
                        Timer = 7200,
                        Radius = 50,
                        Health = 30,
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
                    return new TreasureShip()
                    {
                        Timer = 7200,
                        Radius = 50,
                        Health = 30,
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


        public Shot TreasueShootAoE(Vector2 direction)
        {
            switch (_aoECaseDirection)
            {
                case 1:

                    //up
                    _aoECaseDirection = 2;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(0, 1),
                        Speed = 5f * new Vector2(0, -1)
                    };

                case 2:

                    //up right
                    _aoECaseDirection = 3;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(0.5, 0.5),
                        Speed = 4f * new Vector2(1, -1)
                    };

                case 3:
                    // Right
                    _aoECaseDirection = 4;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(1, 0),
                        Speed = 5f * new Vector2(1, 0)
                    };


                case 4:

                    //Down right
                    _aoECaseDirection = 5;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(0.5, -0.5),
                        Speed = 4f * new Vector2(1, 1)
                    };

                case 5:
                    //down
                    _aoECaseDirection = 6;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(0, -1),
                        Speed = 5f * new Vector2(0, 1)
                    };

                case 6:

                    //Down left
                    _aoECaseDirection = 7;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(-0.5, -0.5),
                        Speed = 4f * new Vector2(-1, 1)
                    };

                case 7:
                    // left
                    _aoECaseDirection = 8;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(-1, 0),
                        Speed = 5f * new Vector2(-1, 0)
                    };

                case 8:
                    //up left
                    _aoECaseDirection = 1;
                    return new Shot
                    {
                        Timer = 120,
                        Position = Position,
                        Rotation = (float)Math.Atan2(-0.5, 0.5),
                        Speed = 4f * new Vector2(-1, -1)
                    };




                default:
                    return null;
            }
        }

    }
}
