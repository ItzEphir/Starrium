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
        public Player(string name, int health, float fuel, float maxfuel, Vector2f coords, Texture texture, float scale = 1) : base(name, health, fuel, maxfuel, coords, texture, scale) 
        {
            
        }

        public Player(string name, float x, float y, Texture texture, float scale = 1) : base(name, x, y, texture, scale) 
        {
            
        }
        public override void Update()
        {
            
        }

        public override void Draw(RenderWindow rw)
        {
            this.sprite.Position = this.coords;
            rw.Draw(this.sprite);
        }

        public override void Attack(Entity enemy)
        {

        }
    }
}
