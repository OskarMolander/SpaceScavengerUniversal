using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Camera
    {
        public Matrix TransformMatrix { get; private set; }
        private Viewport _viewport;
        private Vector2 _centreVector;


        public Camera(Viewport newViewport)
        {
            _viewport = newViewport;
        }

        public void Update(GameTime gameTIme, Player player)
        {
            _centreVector = new Vector2(player.Position.X - Globals.ScreenWidth / 2f, player.Position.Y - Globals.ScreenHeight / 2f);
            //_centreVector = new Vector2(player.Position.X - Globals.ScreenWidth / 2f, player.Position.Y  - Globals.ScreenHeight / 2f);
            TransformMatrix = Matrix.CreateScale(new Vector3(Globals.ScaleX, Globals.ScaleY, 0)) *
                                                    Matrix.CreateTranslation(new Vector3(-_centreVector.X * Globals.ScaleX, -_centreVector.Y * Globals.ScaleY, 0));
            //var translation = Matrix.CreateTranslation(new Vector3(-_centreVector.X, _centreVector.Y, 0));
            //TransformMatrix = Matrix.CreateScale(new Vector3(1, 1, 0)) * translation;
        }
    }
}
