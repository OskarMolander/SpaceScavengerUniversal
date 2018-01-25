using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class Compass : GameObject
    {
        public int type;
        public int chosenTexture;


        public Compass compassSpawn()
        {
                    return new Compass()
                    {
                        Position = new Vector2(0, 0),
                        Rotation = Rotation,
                    };

        }


        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger)game;
                    var targetrotation = (float)Math.Atan2(MyGame.Player.Position.X - Position.X,
                        MyGame.Player.Position.Y - Position.Y);

                    if (targetrotation < 360)
                        Rotation += 360;
                    else if (targetrotation > 360)
                        Rotation -= 360;

                    Rotation = -targetrotation;
        }


    }
}
