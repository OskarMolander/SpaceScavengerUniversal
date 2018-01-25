using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Camera
    {
        public Matrix transformn;
        private Viewport view;
        private Vector2 centre;


        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTIme, Player player)
        {
            centre = new Vector2(player.Position.X  - Globals.ScreenWidth / 2, player.Position.Y- Globals.ScreenHeight / 2);
            transformn = Matrix.CreateScale(new Vector3(1, 1, 0)) * 
                                                    Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
