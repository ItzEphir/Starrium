using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Star_Wars_dev
{
    public class StarSystem : Entity
    {
        public uint ID { get; protected set; }

        public List<Planet> planets;

        private StarSystem(string name, Vector2f coords) : base(name, coords)
        {

        }

        private StarSystem(string name, float x, float y) : base(name, x, y)
        {

        }

        public StarSystem(string name, Vector2f coords, List<Planet> planets) : this(name, coords)
        {
            this.planets = planets;
        }

        public StarSystem(string name, float x, float y, List<Planet> planets) : this(name, x, y)
        {
            this.planets = planets;
        }

        public override void Draw(RenderWindow rw)
        {

        }

        public override void Update(RenderWindow rw)
        {

        }

        public override void Attack(Entity enemy)
        {
            throw new NotImplementedException();
        }

        public void AddPlanet(Planet planet)
        {
            this.planets.Add(planet);
        }
    }
}
