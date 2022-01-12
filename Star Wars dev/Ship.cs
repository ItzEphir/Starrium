using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Star_Wars_dev
{
    public class Ship : Entity
    {
        public float Maxfuel
        {
            get { return fuel; }
            protected set { fuel = value; }
        }

        protected Sprite sprite;
        protected float fuel;

        int health;

        public Ship(string name, int health, float fuel, float maxfuel, Vector2f coords, Texture texture, float scale = 1) : base(name, coords)
        {
            this.sprite = new Sprite(texture);
            this.sprite.Position = this.coords;
            this.sprite.Scale = new Vector2f(scale, scale);
            this.fuel = fuel;
            this.Maxfuel = maxfuel;
            this.health = health;

        }

        public Ship(string name, float x, float y, Texture texture, float scale = 1) : base(name, x, y)
        {
            this.sprite = new Sprite(texture);
            this.sprite.Position = this.coords;
            this.sprite.Scale = new Vector2f(scale, scale);
        }

        public Ship() : base("player", 0, 0)
        {
            sprite = new Sprite(new Texture("files/img/ship.png"));
            sprite.Position = this.coords;
            sprite.Scale = new Vector2f(0.5f, 0.5f);
        }

        public override void Draw(RenderWindow rw)
        {
            rw.Draw(this.sprite);
        }

        public void Move(Planet planet)
        {
            base.Move(planet.coords);
        }

        public override void Update()
        {

        }
    }
}
