using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Star_Wars_dev
{
    public class Park : Building 
    {
        public Park(string name, Vector2f coords, Texture texture) : base(name, coords)
        {
            this.model = new Sprite(texture);
            this.model.Position = this.coords;
        }

        public override void Draw(RenderWindow rw)
        {
            rw.Draw(this.model);
        }

        public override void Update(RenderWindow rw)
        {
            
        }
    }
}
