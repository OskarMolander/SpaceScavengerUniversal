using Microsoft.Xna.Framework;
using System;

namespace Space_Scavenger
{
    public class Compass : GameObject
    {
        public int type;
        public int chosenTexture;

        public Compass CompassSpawn()
        {
            return new Compass
            {
                Position = new Vector2(0, 0),
                Rotation = Rotation,
            };
        }

        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger)game;

            var targetRotation = (float)Math.Atan2(MyGame.Player.Position.X - Position.X,
                MyGame.Player.Position.Y - Position.Y
            );

            if (targetRotation < 360)
                Rotation += 360;
            else if (targetRotation > 360)
                Rotation -= 360;

            Rotation = -targetRotation;
        }
    }
}