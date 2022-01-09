using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Star_Wars_dev
{
    public class Game
    {
        public Sprite background;
        public RenderWindow rw;
        public string screen;
        public static Font font;

        List<Planet> planets;
        List<Button> buttons;

        Ship player;
        Planet chosenPlanet;
        Planet inplanete;
        Clock clock;
        Time time;

        int counter = 0;
        float count = 1.0f;

        public Game(RenderWindow window)
        {
            background = new Sprite(new Texture("files/img/небо.jpg"));
            float mulriplierx = Program.resolution.X / background.Texture.Size.X;
            float mulripliery = Program.resolution.Y / background.Texture.Size.Y;

            planets = new List<Planet>();
            buttons = new List<Button>();
            player = new Ship("player", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2), new Texture("files/img/ship.png"), 0.5f);

            background.Scale = new Vector2f(mulriplierx, mulripliery);

            font = new Font("files/font/ArialRegular.ttf");

            rw = window;
            screen = "0";
            clock = new Clock();
        }

        public void Next(RenderWindow rw)
        {
            time = clock.ElapsedTime;

            if (screen == "0") menu(rw, 3);
            else if (screen == "game") game(rw);
            else if (screen == "inplanetan") inplanetan(rw);
            else if (screen == "inplanet") inplanet(rw);
            else if (screen == "new game") newgame(rw);
            else if (screen == "statistic") statistic(rw);


            planets.Clear();
            clock.Restart().AsMilliseconds();
        }

        public void Draw()
        {
            rw.Draw(background);
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.F12:
                    (sender as RenderWindow)?.Close();
                    break;
            }
        }

        void menu(RenderWindow rw, int currentButtons)
        {
            if (buttons.Count != currentButtons)
            {
                buttons = currentButtons.buildButtons(
                    new List<Vector2f>() { new Vector2f(50, Program.resolution.Y / 2), new Vector2f(Program.resolution.X - 50, Program.resolution.Y / 2), new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2) },
                    new List<Vector2f>() { new Vector2f(0, 0), new Vector2f(0, 0), new Vector2f(0, 0) },
                    new List<Color>() { new Color(255, 255, 255, 100), new Color(255, 255, 255, 100), new Color(255, 255, 255, 100) },
                    new List<Color>() { new Color(0, 0, 0, 0), new Color(0, 0, 0, 0), new Color(0, 0, 0, 0) },
                    new List<Color>() { new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255) },
                    new List<Color>() { new Color(100, 100, 100, 100), new Color(100, 100, 100, 100), new Color(100, 100, 100, 100) },
                    new List<Vector2f>() { new Vector2f(0, 0), new Vector2f(0, 0), new Vector2f(0, 0) },
                    new List<string>() { "Начать игру", "Продолжить", "Статистика" },
                    new List<uint>() { 50, 50, 50 },
                    new List<int>() { 120, 120, 120 },
                    new List<uint>() { 0, 2, 1 },
                    new List<bool>() { false, false, false },
                    new List<bool>() { true, true, true },
                    new List<bool>() { true, true, true}
                    );
            }

           

            rw.PrintText("Starrium", new Vector2f(Program.resolution.X / 2, 10), 100, Color.White, font, 1);

            if (rw.button(buttons[0]))
            {
                screen = "new game";
                return;
            }

            if (rw.button(buttons[1]))
            {
                screen = "game";
                return;
            }

            if (rw.button(buttons[2]))
            {
                screen = "statistic";
                return;
            }
        }

        void game(RenderWindow rw)
        {
            int valueplanets = 2;

            Vector2f mouse = new Vector2f(Mouse.GetPosition(rw).X, Mouse.GetPosition(rw).Y);

            if (planets.Count < valueplanets)
            {
                planets.Add(new Planet("Оля", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2), 25, new Texture("files/img/planet02.png"), 0.25f));
                planets.Add(new Planet("test", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 50, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 50), 25, new Texture("files/img/planet02.png"), 0.25f));
            }
            
            foreach(Planet planet in planets)
            {
                planet.Draw(rw);
            }

            player.Draw(rw);

            chosenPlanet = GetClosestPlanet(mouse.X, mouse.Y);

            if(chosenPlanet != null)
            {
                rw.PrintText(chosenPlanet.name, new Vector2f(chosenPlanet.x, chosenPlanet.y - chosenPlanet.radius - 25), 25, Color.White, font, 1);
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    inplanete = chosenPlanet;
                    screen = "inplanetan";
                    return;
                }
            }

            checkKeys(rw);
        }

        void inplanetan(RenderWindow rw)
        {
            inplanete.Draw(rw);
            
            if(counter == 10)
            {
                count += 0.1f;
                count.normalize();
                counter = 0;
            }
            else
            {
                counter++;
            }

            if(count == 7.1f)
            {
                screen = "inplanet";
                return;
            }

            inplanete.changeSize(count);
        }

        void inplanet(RenderWindow rw)
        {
            inplanete.Draw(rw);
        }

        void checkKeys(RenderWindow rw)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {

            }
        }

        void newgame(RenderWindow rw)
        {

        }

        void statistic(RenderWindow rw)
        {

        }

        Planet GetClosestPlanet(float X, float Y) // получаем ближайшую координату к точке
        {
            Planet res = null;

            foreach (Planet planet in planets)
            {
                if (new Vector2f(planet.x, planet.y).Distance(new Vector2f(X, Y)) < planet.radius)
                {
                    res = planet;
                }
            }

            return res;
        }
    }
}
