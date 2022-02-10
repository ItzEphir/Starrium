using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Engine;

namespace Star_Wars_dev
{
    public abstract class Entity : IDrawable
    {
        public string name;
        public Vector2f coords;
        public float x, y;

        public Entity(string name, Vector2f coords)
        {
            this.name = name;
            this.coords = coords;
            this.x = this.coords.X;
            this.y = this.coords.Y;
        }

        public Entity(string name, float x, float y)
        {
            this.name = name;
            this.coords = new Vector2f(x, y);
            this.x = x;
            this.y = y;
        }

        public abstract void Draw(RenderWindow rw);

        public abstract void Update(RenderWindow rw);

        public abstract void Attack(Entity enemy);

        public void Death()
        {
            this.coords = new Vector2f(this.x, this.y);
        }

        public void Move(Vector2f coords)
        {
            if (coords == null) throw new ArgumentNullException("Movement to nothing");
            else this.coords = coords;
        }
    }
}
