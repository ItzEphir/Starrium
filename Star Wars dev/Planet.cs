using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace Star_Wars_dev
{
    public class Planet : Entity
    {
        public int radius;
        CircleShape shape;
        Sprite sprite;
        float scale;

        public Planet(string name, float x, float y, int radius, Texture texture, float scale = 1) : base(name, x, y)
        {
            this.coords = new Vector2f(this.x, this.y);
            this.radius = radius;
            this.shape = new CircleShape(this.radius);
            this.shape.SetPointCount(180);
            this.shape.FillColor = Color.Transparent;
            this.shape.OutlineThickness = 1f;
            this.shape.OutlineColor = Color.Red;
            this.shape.Position = this.coords - new Vector2f(this.radius, this.radius);
            this.sprite = new Sprite(texture);
            this.sprite.Scale = new Vector2f(scale, scale);
            this.sprite.Position = this.coords - new Vector2f(this.radius, this.radius);
            this.scale = scale;
        }

        public Planet(string name,Vector2f coords, int radius, Texture texture, float scale = 1) : base(name, coords)
        {
            this.radius = radius;
            this.shape = new CircleShape(this.radius);
            this.shape.SetPointCount(180);
            this.shape.FillColor = Color.Transparent;
            this.shape.OutlineThickness = 1f;
            this.shape.OutlineColor = Color.Red;
            this.shape.Position = this.coords - new Vector2f(this.radius, this.radius);
            this.sprite = new Sprite(texture);
            this.sprite.Scale = new Vector2f(scale, scale);
            this.sprite.Position = this.coords - new Vector2f(this.radius, this.radius);
            this.scale = scale;
        }

        public override void Draw(RenderWindow rw)
        {
            rw.Draw(this.sprite);
            // rw.Draw(this.shape);
        }

        public void changeSize(float newscale)
        {
            this.shape.Scale = new Vector2f(newscale, newscale);
            this.shape.Position = this.coords;
            this.shape.Position -= new Vector2f(this.radius * newscale, this.radius * newscale);
            this.sprite.Scale = new Vector2f(newscale * this.scale, newscale * this.scale);
            this.sprite.Position = this.coords;
            this.sprite.Position -= new Vector2f(this.radius * newscale, this.radius * newscale);
        }
    }
}
