using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Star_Wars_dev
{
    public static class Engine
    {
        public static void PrintText(this RenderWindow rw, string message, Vector2f coords, uint size, Color color, Font font, uint mode = 0)
        {
            Font font_type;
            if (font != null)
            {
                font_type = new Font(font);
            }
            else
            {
                font_type = new Font("files/font/ArialRegular.ttf");
            }

            Text text = new Text(message, font_type);
            text.CharacterSize = size;
            float textLength = text.GetLocalBounds().Width;


            switch (mode)
            {
                case 0:
                    text.Position = coords;
                    break;
                case 1:
                    text.Position = new Vector2f(coords.X - textLength / 2, coords.Y);
                    break;
                case 2:
                    text.Position = new Vector2f(coords.X - textLength, coords.Y);
                    break;
            }
            text.FillColor = color;

            rw.Draw(text);
        }

        public static bool button(this RenderWindow rw, Button bt)
        {
            return bt.Draw(rw);
        }

        public static List<Button> buildButtons(this int count, List<Vector2f> coords, List<Vector2f> sizes,
            List<Color> colors, List<Color> activeColors, List<Color> colorTitles, List<Color> activeColorTitles,
            List<Vector2f> hitboxes, List<string> messages, List<uint> fontSizes, List<int> alreadyPressed,
            List<uint> centers, List<bool> aimes, List<bool> checks, List<bool> automaticallyOptimisationSizes)
        {
            List<Button> buttons = new List<Button>();

            for (int i = 0; i < count; i++)
            {
                buttons.Add(new Button(coords[i], sizes[i], colors[i], activeColors[i], colorTitles[i], activeColorTitles[i], hitboxes[i], messages[i], fontSizes[i], alreadyPressed[i], centers[i], aimes[i], checks[i], automaticallyOptimisationSizes[i]));
            }
            return buttons;
        }

        public static float Distance(this Vector2f a, Vector2f b) // найти расстояние от одного вектора до другого по теореме Пифагора
        {
            Vector2f diff = a - b;
            return MathF.Sqrt(MathF.Pow(diff.X, 2) + MathF.Pow(diff.Y, 2));
        }

        public static float Distance(this Vector2f a)
        {
            return MathF.Sqrt(MathF.Pow(a.X, 2) + MathF.Pow(a.Y, 2));
        }

        public static Vector2f Normalise(this Vector2f a) // вычисляем вектор направления
        {
            float length = a.Distance();
            Vector2f dir = a / length;
            return dir;
        }

        public static Vector2f Normalise(this Vector2f a, Vector2f b)
        {
            float length = a.Distance(b);
            Vector2f dir = (a - b) / length;
            return dir;
        }

        public static void normalize (this ref float normalizing)
        {
            normalizing += 0.01f;
            normalizing = (float)(int)(normalizing * 10);
            normalizing /= 10;
        }
    }
}
