using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework.Input;

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

        public List<Asteroid> _MiniStroids = new List<Asteroid>();
        public List<Asteroid> _nrofAsteroids = new List<Asteroid>();

        public AsteroidComponent(Game game, Player Player, GameObject gameObject) 
        {
            mygame = (SpaceScavenger)game;
            myObject = (GameObject)gameObject;
            AsteroidSpawner();
        }

        public  void Update(GameTime gameTime)
        {
            //     Debug.WriteLine(_nrofAsteroids[1].hpAsteroid);

            if (_nrofAsteroids.Count < wantedAsteroids)
                {

                    AsteroidSpawner();
                }
                for (int i = 0; _MiniStroids.Count > i; i++)
                {
                    _MiniStroids[i].Position += _MiniStroids[i].Speed;
                }
                foreach (var asteroid in _nrofAsteroids)
                {
                    var xDiffPlayer = Math.Abs(asteroid.Position.X - mygame.Player.Position.X);
                    var yDiffPlayer = Math.Abs(asteroid.Position.Y - mygame.Player.Position.Y);
                asteroid.Position += asteroid.Speed;
                    if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                    {
                        asteroid.IsDead = true;
                    }
                }
                for (int i = 0; i < _nrofAsteroids.Count; i++)
                {

                if (_nrofAsteroids[i].hpAsteroid == 0)
                    {
                        _nrofAsteroids.Remove(_nrofAsteroids[i]);
                    }
                }
            


            // Debug.WriteLine(mygame.Window.ClientBounds.Bottom);
            // TODO: Add your update logic here
        }

        public void miniStroid(Vector2 aspos)
        {
            
            _MiniStroids.Add(new Asteroid()
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

                        _nrofAsteroids.Add(new Asteroid()
                        {

                          //vänster
                            
                            hpAsteroid = 10,
                            ScoreReward = 10,
                            chosenTexture = randomTexture.Next(1,5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X  - mygame.Window.ClientBounds.X - rand.Next(1000, Globals.ScreenWidth *3), mygame.Player.Position.Y - mygame.Window.ClientBounds.Height + rand.Next(-2400, 3600)),                          
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5))),
                            Radius = 38
                        });
                    
                    break;
                case 2:
                    //höger
                        _nrofAsteroids.Add(new Asteroid()
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
                        _nrofAsteroids.Add(new Asteroid()
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
                        _nrofAsteroids.Add(new Asteroid()
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

