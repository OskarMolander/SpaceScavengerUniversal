using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    class MovingMenu : GameObject
    {

        public void Update(GameTime gametime)
        {
            Speed = new Vector2(1, -1);
            Position += 2f * Speed;
        }
    }
}
