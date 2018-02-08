using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public static class Globals
    {
        public static int ScreenWidth  => (int) (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width  / ScaleX);
        public static int ScreenHeight => (int) (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / ScaleY);
        public static Vector2 ScreenCenter => new Vector2(ScreenWidth / 2f, ScreenHeight / 2f);

        public static float ScaleX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width  / 1920f;
        public static float ScaleY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 1200f;
    }
}
