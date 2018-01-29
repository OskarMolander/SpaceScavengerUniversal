using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Player : DrawableGameComponent, IGameObject
    {
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public bool Accelerating { get; set; }
        public bool Decelerating { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public int MaxHealth { get; set; }
        public int MaxShield { get; set; }
        public int lastShot = 1;
        private Space_Scavenger.GameState gameState;
        
        private Texture2D playerTexture;
        private Texture2D healthTexture; 

        public Player(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth/ 2,Globals.ScreenHeight/2);
            Health = 5;
            Shield = 5;
            Radius = 12;
            MaxHealth = Health;
            MaxShield = Shield;
            
        }

        protected override void LoadContent()
        {
            playerTexture = Game.Content.Load<Texture2D>("playerShipNeon");
            healthTexture = Game.Content.Load<Texture2D>("powerupRed");
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, Position, null, Color.White, Rotation + MathHelper.PiOver2, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), 0.5f, SpriteEffects.None, 0f);

        }
        public void Accelerate()
        {
            Speed += new Vector2(0, (float)Math.Sin(3 * MathHelper.PiOver2)) * 0.30f;
            Accelerating = true;
        }

        public void Decelerate()
        {
            Speed -= new Vector2(0, (float)Math.Sin(3 * MathHelper.PiOver2)) * 0.30f;
            Decelerating = true;
        }

        public void StrafeLeft()
        {
            Speed += new Vector2((float)Math.Cos(2 * MathHelper.PiOver2), 0) * 0.30f;
        }

        public void StrafeRight()
        {
            Speed -= new Vector2((float)Math.Cos(2 * MathHelper.PiOver2), 0) * 0.30f;
        }


        //public void Accelerate()
        //{
        //    Speed += new Vector2(0, 3 * MathHelper.PiOver2);
        //    //Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * 0.30f;
        //    Accelerating = true;
        //}

        //public void Decelerate()
        //{
        //    Speed -= new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation)) * 0.30f;
        //    Decelerating = true;
        //}

        //public void StrafeLeft()
        //{
        //    if (Rotation + MathHelper.PiOver2 > MathHelper.PiOver2 && Rotation < 3 * MathHelper.PiOver2)
        //    {
        //        Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)).Rotate(MathHelper.PiOver2) * 0.30f;
        //    }
        //    else
        //    {
        //        Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)).Rotate(-MathHelper.PiOver2) * 0.30f;
        //    }
        //}

        //public void StrafeRight()
        //{
        //    if (Rotation + MathHelper.PiOver2 > MathHelper.PiOver2 && Rotation < 3 * MathHelper.PiOver2)
        //    {
        //        Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)).Rotate(- MathHelper.PiOver2) * 0.30f; 
        //    }
        //    else
        //    {
        //        Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)).Rotate(MathHelper.PiOver2) * 0.30f;
        //    }
        //}
        public Shot Shoot()
        {

            return new Shot()
            {
                Position = Position,
                Rotation = Rotation,
                Timer = 200,
                Speed = 20f * new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation))
            };

        }
        public Shot multiShot()
        {
            float radius = 10;

            if (lastShot == 1)
            {
                lastShot = 0;
                return new Shot()
                {
                    Position = Position + (Vector2.One*radius).Rotate(-MathHelper.PiOver2 + Rotation),
                    Rotation = Rotation,
                    Timer = 200,
                    Speed =  20f * new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation))
                };
            }
            else
            {
                lastShot = 1;
                return new Shot()
                {
                    Position = Position + (Vector2.One * radius).Rotate(MathHelper.PiOver2 + Rotation),
                    Rotation = Rotation,
                    Timer = 200,
                    Speed = 20f * new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation))
                };
            }
            
        }
    }
}
