using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using Engine;

namespace Star_Wars_dev
{
    public abstract class Building : IDrawable
    {
        public string Name { get; protected set; }

        protected Vector2f coords;
        protected Sprite model;

        public Building(string name, Vector2f coords)
        {
            this.coords = coords;
            this.Name = name;
        }

        public abstract void Draw(RenderWindow rw);

        public abstract void Update(RenderWindow rw);
    }
}
