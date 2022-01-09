using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Star_Wars_dev
{
    public interface IDrawable
    {
        public void Draw(RenderWindow rw);
    }
}
