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
        public float Maxfuel { get; protected set; }
        public int Health { get; protected set; }
        public float Fuel { get; protected set; }
        
        protected int Damage { get; set; }

        protected Sprite sprite;

        public Ship(string name, int health, float fuel, float maxfuel, Vector2f coords, Texture texture, float scale = 1) : base(name, coords)
        {
            this.sprite = new Sprite(texture);
            this.sprite.Position = this.coords;
            this.sprite.Scale = new Vector2f(scale, scale);
            this.Fuel = fuel;
            this.Maxfuel = maxfuel;
            this.Health = health;

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
            this.sprite.Position = this.coords;
            rw.Draw(this.sprite);
        }

        public void Move(Planet planet)
        {
            base.Move(planet.coords);
            this.sprite.Position = planet.coords;
        }

        public override void Update(RenderWindow rw)
        {
            if(this.Fuel < 0)
            {

            }
        }

        public override void Attack(Entity enemy)
        {

        }

        public new void Death()
        {
            this.Health = 0;
        }

        public void countCoords(Ship main, int index)
        {
            this.coords = main.coords;
        }
    }
}
