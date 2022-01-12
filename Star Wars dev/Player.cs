using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Star_Wars_dev
{
    public class Player : Ship
    {
        Vector2f inputCoors;
        Sprite inputShip;

        public Player(string name, int health, float fuel, float maxfuel, Vector2f coords, Texture texture, float scale = 1) : base(name, health, fuel, maxfuel, coords, texture, scale) 
        {
            this.inputCoors = new Vector2f(this.coords.X, this.coords.Y);
            this.inputShip = this.sprite;
        }

        public Player(string name, float x, float y, Texture texture, float scale = 1) : base(name, x, y, texture, scale) 
        {
            this.inputCoors = new Vector2f(this.coords.X, this.coords.Y);
            this.inputShip = this.sprite;
        }

        public void normCoords(Planet planet)
        {
            // this.inputCoors -= new Vector2f(planet.Radius * planet.Scale, planet.Radius * planet.Scale);
            this.inputShip.Position = this.inputCoors;
        }

        public override void Update()
        {
            this.inputCoors = this.coords;
        }

        public override void Draw(RenderWindow rw)
        {
            rw.Draw(inputShip);
        }
    }
}
