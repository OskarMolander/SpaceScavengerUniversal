using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class BossCompass : GameObject
    {
        public int type;
        public int chosenTexture;


        public BossCompass bossCompassSpawn()
        {

            return new BossCompass()
            {
                Position = new Vector2(0, 0),
                Rotation = Rotation,
            };
        }


        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger)game;

                    var targetrotation = (float)Math.Atan2(MyGame.Player.Position.X - MyGame.bosses[0].Position.X,
                        MyGame.Player.Position.Y - MyGame.bosses[0].Position.Y);

                    if (targetrotation < 360)
                        Rotation += 360;
                    else if (targetrotation > 360)
                        Rotation -= 360;

            Rotation = -targetrotation;
        }


    }
}
