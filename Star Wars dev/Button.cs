using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Star_Wars_dev
{
    public class Button
    {
        Vector2f coords;
        Vector2f textCoords;
        Vector2f size;
        Color color;
        Color activeColor;
        Color colorTitle;
        Color activeColorTitle;
        Vector2f hitBox;
        Font font = Game.font;
        Text text;
        Text letter;
        string message;
        uint fontSize;
        int alreadyPressed;
        uint center;
        bool aim;
        bool check;
        bool buttonPressed;
        float textLength;
        float letterLength;

        public Button(Vector2f coords, Vector2f size,
            Color color, Color activeColor, Color colorTitle, Color activeColorTitle,
            Vector2f hitBox, string message = "",
            uint fontSize = 20, int alreadyPressed = 120,
            uint center = 0, bool aim = false, bool check = true, bool automaticalOptimisationSize = true)
        {
            this.center = center;
            this.message = message;

            this.textCoords = coords;

            if (!automaticalOptimisationSize) this.fontSize = fontSize;
            else this.fontSize = (uint)(Program.resolution.Y / (1080 / fontSize));

            this.text = new Text(this.message, this.font, this.fontSize);
            this.letter = new Text("A", this.font, this.fontSize);
            this.letterLength = letter.GetLocalBounds().Width;
            this.textLength = this.text.GetLocalBounds().Width;

            if (hitBox != new Vector2f(0, 0)) this.hitBox = hitBox;
            else this.hitBox = new Vector2f(-this.letterLength, -fontSize / 4);

            if (size != new Vector2f(0, 0)) this.size = size;
            else 
            {
                this.size = new Vector2f(this.textLength, this.fontSize * 2); 
            }

            switch (this.center)
            {
                case 0:
                    this.coords = coords;
                    break;
                case 1:
                    this.coords = new Vector2f(coords.X - this.size.X / 2, coords.Y);
                    break;
                case 2:
                    this.coords = new Vector2f(coords.X - this.size.X - 2 * hitBox.X, coords.Y);
                    break;
            }
            this.coords.X += this.hitBox.X;
            this.size.X -= 2 * this.hitBox.X;

            this.color = color;
            this.activeColor = activeColor;
            this.colorTitle = colorTitle;
            this.activeColorTitle = activeColorTitle;
            this.alreadyPressed = alreadyPressed;
            this.aim = aim;
            this.check = check;
            this.buttonPressed = false;
        }

        public Button(string message, Vector2f coords, Vector2f size,
            Color color, Color activeColor, Color colorTitle, Color activeColorTitle,
            Vector2f hitBox,
            uint fontSize = 20, int alreadyPressed = 120,
            uint center = 0, bool aim = false, bool check = true, bool automaticalOptimisationSize = true)
        {
            this.center = center;
            this.message = message;

            this.textCoords = coords;

            if (!automaticalOptimisationSize) this.fontSize = fontSize;
            else this.fontSize = (uint)(Program.resolution.Y / (1080 / fontSize));

            this.text = new Text(this.message, this.font, this.fontSize);
            this.letter = new Text("A", this.font, this.fontSize);
            this.letterLength = letter.GetLocalBounds().Width;
            this.textLength = this.text.GetLocalBounds().Width;

            if (hitBox != new Vector2f(0, 0)) this.hitBox = hitBox;
            else this.hitBox = new Vector2f(-this.letterLength, -fontSize / 4);

            if (size != new Vector2f(0, 0)) this.size = size;
            else
            {
                this.size = new Vector2f(this.textLength, this.fontSize * 2);
            }

            switch (this.center)
            {
                case 0:
                    this.coords = coords;
                    break;
                case 1:
                    this.coords = new Vector2f(coords.X - this.size.X / 2, coords.Y);
                    break;
                case 2:
                    this.coords = new Vector2f(coords.X - this.size.X - 2 * hitBox.X, coords.Y);
                    break;
            }
            this.coords.X += this.hitBox.X;
            this.size.X -= 2 * this.hitBox.X;

            this.color = color;
            this.activeColor = activeColor;
            this.colorTitle = colorTitle;
            this.activeColorTitle = activeColorTitle;
            this.alreadyPressed = alreadyPressed;
            this.aim = aim;
            this.check = check;
            this.buttonPressed = false;
        }

        public bool Draw(RenderWindow rw)
        {
            bool click = Mouse.IsButtonPressed(Mouse.Button.Left);
            Vector2i mouseCoords = Mouse.GetPosition(rw);

            if (this.color.A != 0) this.Rectangle(rw);
            rw.PrintText(this.message, this.textCoords, this.fontSize, this.colorTitle, this.font, this.center);
            if (this.alreadyPressed <= 0)
            {
                if (this.coords.X < mouseCoords.X && mouseCoords.X < this.coords.X + this.size.X && this.coords.Y < mouseCoords.Y && mouseCoords.Y < this.coords.Y + this.size.Y)
                {
                    if (!this.aim)
                    {
                        if (this.activeColor.A != 0) this.Rectangle(rw);
                        if (this.activeColorTitle.A != 0) rw.PrintText(this.message, this.textCoords, this.fontSize, this.activeColorTitle, this.font, this.center);
                        else rw.PrintText(this.message, this.textCoords, this.fontSize, this.colorTitle, this.font, this.center);

                        if (this.check)
                        {
                            if (click)
                            {
                                if (!this.buttonPressed)
                                {
                                    this.buttonPressed = true;
                                    return false;
                                }
                            }
                            else
                            {
                                if (this.buttonPressed)
                                {
                                    this.buttonPressed = false;
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (click)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (this.check)
                        {
                            this.buttonPressed = !this.buttonPressed;
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    this.buttonPressed = false;
                    return false;
                }
            }
            else
            {
                this.alreadyPressed -= 1;
                return false;
            }
            return false;
        }

        public void Rectangle(RenderWindow rw)
        {
            RectangleShape rect = new RectangleShape();
            rect.Position = this.coords;
            rect.Size = this.size;
            rect.FillColor = this.color;
            rw.Draw(rect);
        }
    }
}
