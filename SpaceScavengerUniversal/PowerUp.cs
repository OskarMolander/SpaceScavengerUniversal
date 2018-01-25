using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class PowerUp : GameObject
    {

        private readonly Random _rnd = new Random();


        public PowerUp PowerUpSpawn(Game game)
        {
            MyGame = (SpaceScavenger) game;

            int spawnside = _rnd.Next(1, 5);
            switch (spawnside)
            {
                case 1:

                    return new PowerUp
                    {
                        //vänster
                        Radius = 30,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X - MyGame.Window.ClientBounds.X -
                            _rnd.Next(1000, Globals.ScreenWidth * 3),
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600))
                    };
                case 2:
                    //höger
                    return new PowerUp()
                    {
                        Radius = 30,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600))
                    };
                case 3:
                    //upp
                    return new PowerUp()
                    {
                        Radius = 30,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 0))
                    };
                case 4:
                    //ner
                    return new PowerUp()
                    {
                        Radius = 30,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Y + _rnd.Next(1200, 2400))
                    };
            }

            return null;


        }

    }
}
