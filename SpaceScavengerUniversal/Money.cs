using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class Money : GameObject
    {
        public List<Asteroid> Moneyroids = new List<Asteroid>();
        private readonly Random _rnd = new Random();


        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger) game;
            foreach (Asteroid m in Moneyroids)
            {
                var direction = MyGame.Player.Position - m.Position;
                direction.Normalize();
                m.Position += direction * 8f;
            }
        }

        public void MoneyRoid(Vector2 aspos)
        {
            Moneyroids.Add(new Asteroid
            {
                Timer = 1000,
                hpAsteroid = 1,
                value = 50,
                addCounter = _rnd.Next(-677, 677) / 10000f,
                Position = new Vector2(aspos.X + _rnd.Next(-20, 20), aspos.Y + _rnd.Next(-20, 20)),
                Speed = new Vector2((float) Math.Cos(_rnd.Next(-7, 7)), (float) Math.Sin(_rnd.Next(-7, 7))),
                Radius = 20
            });
        }
    }
}