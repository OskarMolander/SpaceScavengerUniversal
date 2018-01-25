using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class Shot : GameObject
    {
        public Shot()
        {
            Radius = 16;
        }

        public void Update(GameTime gameTime)
        {
            Position += Speed;
        }
    }
}
