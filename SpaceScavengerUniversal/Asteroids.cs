using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Space_Scavenger
{
    public class Asteroid : GameObject
    {

        public int hpAsteroid;
        public float RotationCounter;
        public float addCounter;
        public int chosenTexture;
        public int value;
    }

    class AsteroidComponent : GameObject
    {
        public Random randomTexture = new Random();
        public Random rand = new Random();
        public Random randAsteroitField = new Random();
        private  List<Texture2D> meteorTexture2Ds = new List<Texture2D>();
        public  SpaceScavenger mygame;
        public GameObject myObject;

        public Texture2D asterTexture2D1;
        public Texture2D asterTexture2D2;
        public Texture2D asterTexture2D3;
        public Texture2D asterTexture2D4;
        public Texture2D MinitETexture2D1;
        public int aTimer = 10;
        public bool isDead { get; set; }
        public float Rotation { get; set; }
        public int Health { get; set; }
        public int wantedAsteroids = 100;

        public List<Asteroid> MiniAsteroids = new List<Asteroid>();
        public List<Asteroid> Asteroids = new List<Asteroid>();

        public AsteroidComponent(Game game, Player Player, GameObject gameObject) 
        {
            mygame = (SpaceScavenger)game;
            myObject = (GameObject)gameObject;
            AsteroidSpawner();
        }

        public  void Update(GameTime gameTime)
        {
            //     Debug.WriteLine(_nrofAsteroids[1].hpAsteroid);

            if (Asteroids.Count < wantedAsteroids)
            {
                AsteroidSpawner();
            }

            foreach (var miniAsteroid in MiniAsteroids)
            {
                miniAsteroid.Position += miniAsteroid.Speed;
            }

            foreach (var asteroid in Asteroids)
            {
                var xDiffPlayer = Math.Abs(asteroid.Position.X - mygame.Player.Position.X);
                var yDiffPlayer = Math.Abs(asteroid.Position.Y - mygame.Player.Position.Y);
                asteroid.Position += asteroid.Speed;
                if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                {
                    asteroid.IsDead = true;
                }
            }

            for (int i = 0; i < Asteroids.Count; i++)
            {
                if (Asteroids[i].hpAsteroid == 0)
                {
                    Asteroids.Remove(Asteroids[i]);
                }
            }
            


            // Debug.WriteLine(mygame.Window.ClientBounds.Bottom);
            // TODO: Add your update logic here
        }

        public void MiniStroid(Vector2 aspos)
        {
            
            MiniAsteroids.Add(new Asteroid()
            {
                Timer = rand.Next(100, 300),
                //vänster
                hpAsteroid = 10,
                chosenTexture = randomTexture.Next(4),
                addCounter = rand.Next(-677, 677) / 10000f,
                Position = new Vector2(aspos.X + rand.Next(-20,20), aspos.Y + rand.Next(-20, 20)),
                Speed = new Vector2((float)Math.Cos(rand.Next(-7, 7)), (float)Math.Sin(rand.Next(-7, 7))),
                Radius = 38
            });

        }
        public void AsteroidSpawner()
        {
            var xDiff = Math.Abs(mygame.Player.Position.X - 500);

       int Spawnside = rand.Next(1, 5);
       //     int Spawnside = 1;
      // Debug.WriteLine(Spawnside);
            switch (Spawnside)
            {
                case 1:

                        Asteroids.Add(new Asteroid()
                        {

                          //vänster
                            
                            hpAsteroid = 10,
                            ScoreReward = 10,
                            chosenTexture = randomTexture.Next(1,5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(
                                mygame.Player.Position.X  - mygame.Window.ClientBounds.X - rand.Next(1000, Globals.ScreenWidth *3), 
                                mygame.Player.Position.Y - mygame.Window.ClientBounds.Height + rand.Next(-2400, 3600)
                            ),                          
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5))),
                            Radius = 38
                        });
                    
                    break;
                case 2:
                    //höger
                        Asteroids.Add(new Asteroid()
                        {
                            Radius = 38,
                            hpAsteroid = 10,
                            ScoreReward = 10,
                            chosenTexture = randomTexture.Next(1,5 ),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X + rand.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) + mygame.Window.ClientBounds.X, mygame.Player.Position.Y + mygame.Window.ClientBounds.Height + rand.Next(-2400, 3600)),
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5)))
                        });
                    
                    break;
                case 3:
                    //upp
                        Asteroids.Add(new Asteroid()
                        {
                            Radius = 38,
                            hpAsteroid = 10,
                            ScoreReward = 10,
                            chosenTexture = randomTexture.Next(1,5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X + rand.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) + mygame.Window.ClientBounds.X, mygame.Player.Position.Y - mygame.Window.ClientBounds.Height + rand.Next(-2400, 0)),
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 50)))
                        });
                    
                    break;
                case 4:
                    //ner
                        Asteroids.Add(new Asteroid()
                        {
                            Radius = 38,
                            hpAsteroid = 10,
                            ScoreReward = 10,
                            chosenTexture = randomTexture.Next(1,5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X + rand.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) + mygame.Window.ClientBounds.X, mygame.Player.Position.Y + mygame.Window.ClientBounds.Y + rand.Next(1200, 2400)),
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5)))
                        });
                    
                    break;


            }

        }
    }
}

