using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace Star_Wars_dev
{
    public class Planet : Entity
    {
        public int Radius;
        public float Scale;

        CircleShape shape;
        Sprite sprite;

        public Planet(string name, float x, float y, int radius, Texture texture, float scale = 1) : base(name, x, y)
        {
            this.coords = new Vector2f(this.x, this.y);
            this.Radius = radius;
            this.shape = new CircleShape(this.Radius);
            this.shape.SetPointCount(180);
            this.shape.FillColor = Color.Transparent;
            this.shape.OutlineThickness = 1f;
            this.shape.OutlineColor = Color.Red;
            this.shape.Position = this.coords - new Vector2f(this.Radius, this.Radius) * this.Scale;
            this.sprite = new Sprite(texture);
            this.sprite.Scale = new Vector2f(scale, scale);
            this.sprite.Position = this.coords - new Vector2f(this.Radius, this.Radius) * this.Scale;
            this.Scale = scale;
        }

        public Planet(string name,Vector2f coords, int radius, Texture texture, float scale = 1) : base(name, coords)
        {
            this.Scale = scale;
            this.Radius = radius;
            this.shape = new CircleShape(this.Radius);
            this.shape.SetPointCount(180);
            this.shape.FillColor = Color.Transparent;
            this.shape.OutlineThickness = 1f;
            this.shape.OutlineColor = Color.Red;
            this.shape.Position = this.coords - new Vector2f(this.Radius, this.Radius) * this.Scale;
            this.sprite = new Sprite(texture);
            this.sprite.Scale = new Vector2f(scale, scale);
            this.sprite.Position = this.coords - new Vector2f(this.Radius, this.Radius) * this.Scale;
        }

        public override void Update() {}

        public override void Draw(RenderWindow rw)
        {
            rw.Draw(this.sprite);
            // rw.Draw(this.shape);
        }

        public void changeSize(float newscale)
        {
            this.shape.Scale = new Vector2f(newscale, newscale);
            this.shape.Position = this.coords - new Vector2f(this.Radius * newscale, this.Radius * newscale);
            this.sprite.Scale = new Vector2f(newscale * this.Scale, newscale * this.Scale);
            this.sprite.Position = this.coords - new Vector2f(this.Radius * newscale, this.Radius * newscale);
        }

        public bool Check(Ship entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(this.name);
            }

            if (entity.coords == this.coords)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Center()
        {
            this.coords = Program.resolution / 2 - new Vector2f(this.Radius, this.Radius) * this.Scale;
        }
    }
}
