using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    class Globals
    {
        public static int ScreenWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int ScreenHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public static Vector2 ScreenCenter => new Vector2(ScreenWidth / 2f, ScreenHeight / 2f);
    }
}
