using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Star_Wars_dev
{
    public class Game
    {
        public int Money { get; set; }
        public Sprite Background;
        public RenderWindow RW;
        public string Screen;
        public static Font Font;

        Music music;

        List<Building> buildings;
        List<Planet> planets;
        List<Button> buttons;
        List<IDrawable> drawables;
        List<Ship> playersShip;

        Player player;
        Planet chosenPlanet;
        Planet inplanete;
        House house1;
        House house2;
        Factory factory;
        Park park;
        Barracks barracks;
        Clock clock;
        Time time;

        int counter = 0;
        float count = 1.0f;

        bool mousePressed = false;
        bool escapePressed = false;
        bool spacePressed = false;

        public Game(RenderWindow window)
        {
            Background = new Sprite(new Texture("files/img/space.png"));
            float mulriplierx = Program.resolution.X / Background.Texture.Size.X;
            float mulripliery = Program.resolution.Y / Background.Texture.Size.Y;

            planets = new List<Planet>();
            buttons = new List<Button>();
            player = new Player("player", 100, 100f, 500f, new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2), new Texture("files/img/Vzhvzhvzhvzh.png"), 0.5f);

            drawables = new List<IDrawable>();

            Money = 1000;

            playersShip = new List<Ship>();

            buildings = new List<Building>();

            Background.Scale = new Vector2f(mulriplierx, mulripliery);

            Font = new Font("files/font/ArialRegular.ttf");

            barracks = new Barracks("Галактическая казарма", new Vector2f(200, 100), new Texture("files/img/Barracks.png"));
            factory = new Factory("Фабрика", new Vector2f(100, 100), new Texture("files/img/Factory.png"));
            house1 = new House("Город", new Vector2f(400, 100), new Texture("files/img/House.png"));
            house2 = new House("Город", new Vector2f(500, 100), new Texture("files/img/House.png"));
            park = new Park("Центр развлечений", new Vector2f(300, 100), new Texture("files/img/Park.png"));

            RW = window;
            Screen = "0";
            clock = new Clock();

            music = new Music("files/wav/music.wav");
        }

        public void Next(RenderWindow rw)
        {
            if (!(music.Status == SoundStatus.Playing)) music.Play();

            time = clock.ElapsedTime;

            switch (Screen)
            {
                case "0": menu(rw, 4); break;
                case "game": game(rw); break;
                case "inplanetan": inplanetan(rw); break;
                case "inplanet": inplanet(rw); break;
                case "continue": continuegame(rw); break;
                case "new game": newgame(rw); break;
                case "statistic": statistic(rw); break;
                case "titles": titles(rw); break;
                case "win": win(rw); break;
            }

            clock.Restart().AsMilliseconds();
        }

        public void Draw()
        {
            RW.Draw(Background);
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
                    new List<Vector2f>() { new Vector2f(50, Program.resolution.Y / 2), new Vector2f(Program.resolution.X - 50, Program.resolution.Y / 2), new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2) , new Vector2f(Program.resolution.X / 2, Program.resolution.Y - 150)},
                    new List<Vector2f>() { new Vector2f(0, 0), new Vector2f(0, 0), new Vector2f(0, 0), new Vector2f(0, 0) },
                    new List<Color>() { new Color(255, 255, 255, 100), new Color(255, 255, 255, 100), new Color(255, 255, 255, 100), new Color(255, 255, 255, 100) },
                    new List<Color>() { new Color(0, 0, 0, 0), new Color(0, 0, 0, 0), new Color(0, 0, 0, 0), new Color(0, 0, 0, 0) },
                    new List<Color>() { new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255) },
                    new List<Color>() { new Color(100, 100, 100, 100), new Color(100, 100, 100, 100), new Color(100, 100, 100, 100), new Color(100, 100, 100, 100) },
                    new List<Vector2f>() { new Vector2f(0, 0), new Vector2f(0, 0), new Vector2f(0, 0), new Vector2f(0, 0) },
                    new List<string>() { "Начать игру", "Продолжить", "Статистика", "Титры" },
                    new List<uint>() { 50, 50, 50, 50 },
                    new List<int>() { 120, 120, 120, 120 },
                    new List<uint>() { 0, 2, 1, 1 },
                    new List<bool>() { false, false, false, false },
                    new List<bool>() { true, true, true, true },
                    new List<bool>() { true, true, true, true }
                    );
            }

           

            rw.PrintText("Starrium", new Vector2f(Program.resolution.X / 2, 10), 100, Color.White, Font, 1);

            buttons[2].changeColor(new Color(50, 50, 50, 100));

            if (rw.button(buttons[0]))
            {
                Screen = "new game";
                buttons.Clear();
                return;
            }

            if (rw.button(buttons[1]))
            {
                Screen = "continue";
                buttons.Clear();
                return;
            }

            rw.button(buttons[2]);

            if (rw.button(buttons[3]))
            {
                Screen = "titles";
                buttons.Clear();
                return;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                escapePressed = true;
            }
            else
            {
                if (escapePressed)
                {
                    escapePressed = false;
                    rw.Close();
                    return;
                }
            }
        }

        void game(RenderWindow rw)
        {
            int valueplanets = 6;
            bool winner = true;

            Vector2f mouse = new Vector2f(Mouse.GetPosition(rw).X, Mouse.GetPosition(rw).Y);

            if (planets.Count < valueplanets)
            {
                planets.Add(new Planet("Sterrum", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2), 75, new Texture("files/img/planet01.png"), 1f, true, false));
                planets.Add(new Planet("Akra", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 300, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 300), 75, new Texture("files/img/planet02.png"), 1f, false, false));
                planets.Add(new Planet("Lauzer", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 500, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 500), 75, new Texture("files/img/planet03.png"), 1f, false, false));
                planets.Add(new Planet("Nasté", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * (-200), Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * (-400)), 75, new Texture("files/img/planet04.png"), 1f, false, false));
                planets.Add(new Planet("Athou", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * (-400), Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 600), 75, new Texture("files/img/planet05.png"), 1f, false, false));
                planets.Add(new Planet("Lemon King", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * (-350)), 75, new Texture("files/img/planet06.png"), 1f, false, false));
            }

            planets.ForEach(planet => drawables.Add(planet));

            drawables.Add(player);

            drawables.ForEach(drawable => drawable.Draw(rw));

            chosenPlanet = GetClosestPlanet(mouse.X, mouse.Y);

            if (chosenPlanet != null)
            {
                rw.PrintText(chosenPlanet.name, new Vector2f(chosenPlanet.x, chosenPlanet.y - chosenPlanet.Radius), 25, Color.White, Font, 1);
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    if (!mousePressed)
                    {
                        mousePressed = true;
                    }
                }
                else
                {
                    if (mousePressed)
                    {
                        if (chosenPlanet.Check(player))
                        {
                            chosenPlanet.Center();
                            inplanete = chosenPlanet;
                            Screen = "inplanetan";
                        }
                        else
                        {
                            player.Move(chosenPlanet);
                        }

                        mousePressed = false;
                    }
                }
            }

            checkKeys(rw);
            player.Update();

            
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

            if(count == 6.1f)
            {
                count = 1.0f;
                counter = 0;
                Screen = "inplanet";
                return;
            }

            inplanete.changeSize(count);
        }

        void inplanet(RenderWindow rw)
        {
            int valuepoints = 7;
            string result;

            if(inplanete.ConnectedBuildings.Count > 0)
            {
                Building[] buffer = new Building[inplanete.ConnectedBuildings.Count];
                inplanete.ConnectedBuildings.CopyTo(buffer);
                buildings = buffer.ToList();
            }

            if(buttons.Count < valuepoints)
            {
                buttons.Add(new Button("Построить завод", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 300), new Vector2f(0, 0), new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 200), new Color(50, 50, 50, 50), new Vector2f(0, 0), 25, 120, 0, false, true, true));
                buttons.Add(new Button("Построить галактическую казарму", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 200), new Vector2f(0, 0), new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 200), new Color(50, 50, 50, 50), new Vector2f(0, 0), 25, 120, 0, false, true, true));
                buttons.Add(new Button("Построить центр развлечений", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 100), new Vector2f(0, 0), new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 200), new Color(50, 50, 50, 50), new Vector2f(0, 0), 25, 120, 0, false, true, true));
                buttons.Add(new Button("Построить город", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 0), new Vector2f(0, 0), new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 200), new Color(50, 50, 50, 50), new Vector2f(0, 0), 25, 120, 0, false, true, true));
                buttons.Add(new Button("Создать новый корабль-союзник", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * (-100)), new Vector2f(0, 0), new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 200), new Color(50, 50, 50, 50), new Vector2f(0, 0), 25, 120, 0, false, true, true));
                buttons.Add(new Button("Колонизировать", new Vector2f(Program.resolution.X / 2 + Program.resolution.X / Program.resolution.Y * 150, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 300), new Vector2f(0, 0), new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255), new Color(50, 50, 50, 50), new Vector2f(0, 0), 25, 120, 2, false, true, true));
                buttons.Add(new Button("Уничтожить", new Vector2f(Program.resolution.X / 2 + Program.resolution.X / Program.resolution.Y * 150, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 200), new Vector2f(0, 0), new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 255), new Color(50, 50, 50, 50), new Vector2f(0, 0), 25, 120, 2, false, true, true));
            }

            inplanete.Draw(rw);
            drawBlue(rw);


            if (rw.button(buttons[5]))
            {
                if (!inplanete.Colonized)
                {
                    inplanete.Colonized = true;
                }
            }

            if (rw.button(buttons[6]))
            {
                inplanete.Dead = true;
                inplanete.changeTexture(new Texture("files/img/deadplanet.png"));
            }

            if (!inplanete.Colonized)
            {
                for (int i = 0; i < 5; i++)
                {
                    buttons[i].changeColor(new Color(50, 50, 50, 100));
                }
            }

            if (inplanete.Colonized)
            {
                for (int i = 0; i < 6; i++)
                {
                    buttons[i].secondChange(new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 200), new Color(50, 50, 50, 50));
                }
                buttons[5].changeColor(new Color(50, 50, 50, 100));
                buttons[6].changeColor(new Color(50, 50, 50, 100));
            }

            if (inplanete.Dead)
            {
                inplanete.Colonized = false;
                for (int i = 0; i < 7; i++)
                {
                    buttons[i].changeColor(new Color(new Color(50, 50, 50, 100)));
                }
            }

            if (buildings.Contains(factory))
            {
                buttons[0].changeColor(new Color(50, 50, 50, 100));
            }

            if (buildings.Contains(barracks))
            {
                buttons[1].changeColor(new Color(50, 50, 50, 100));
            }

            if (buildings.Contains(park))
            {
                buttons[2].changeColor(new Color(50, 50, 50, 100));
            }

            if (buildings.Contains(house2))
            {
                buttons[3].changeColor(new Color(50, 50, 50, 100));
            }

            if (!((playersShip.Count < 5) && buildings.Contains(barracks)))
            {
                buttons[4].changeColor(new Color(50, 50, 50, 100));
            }

            if (playersShip.Count < 5 && buildings.Contains(barracks))
            {
                buttons[4].secondChange(new Color(100, 100, 100, 100), new Color(0, 0, 0, 0), new Color(255, 255, 255, 200), new Color(50, 50, 50, 50));
            }

            if (rw.button(buttons[0]))
            {
                if (inplanete.Colonized)
                {
                    if (!buildings.Contains(factory))
                    {
                        buildings.Add(factory);
                    }
                }
            }

            if (rw.button(buttons[1]))
            {
                if (inplanete.Colonized)
                {
                    if (!buildings.Contains(barracks))
                    {
                        buildings.Add(barracks);
                    }
                }
            }

            if (rw.button(buttons[2]))
            {
                if (inplanete.Colonized)
                {
                    if (!buildings.Contains(park))
                    {
                        buildings.Add(park);
                    }
                }
            }

            if (rw.button(buttons[3]))
            {
                if (inplanete.Colonized)
                {
                    if (!buildings.Contains(house1))
                    {
                        buildings.Add(house1);
                    }
                    else
                    {
                        if (!buildings.Contains(house2))
                        {
                            buildings.Add(house2);
                        }
                    }
                }
            }

            if (rw.button(buttons[4]))
            {
                if (inplanete.Colonized)
                {
                    if (playersShip.Count < 5 && buildings.Contains(barracks))
                    {
                        playersShip.Add(new Ship("Scorp", 100, 100, 500, player.coords - new Vector2f(0, 0), new Texture("files/img/Vzhvzhvzhvzh.png"), 1));
                    }
                }
            }
            
            inplanete.ConnectedBuildings = buildings;

            buildings.ForEach(building => building.Draw(rw));

            // playersShip.ForEach(playerS => playerS.Draw(rw));

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                escapePressed = true;
            }
            else
            {
                if (escapePressed)
                {
                    inplanete.changeSize(1f);
                    switch (inplanete.name)
                    {
                        case "Sterrum":
                            inplanete.coords = new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2);

                            result = "";

                            foreach (Building b in buildings)
                            {
                                result += b.Name + "/";
                            }

                            Engine.upload("files/saves/planets/01.txt", result);

                            break;
                        case "Akra":
                            inplanete.coords = new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 300, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 300);

                            result = "";

                            foreach (Building b in buildings)
                            {
                                result += b.Name + "/";
                            }

                            Engine.upload("files/saves/planets/02.txt", result);

                            break;
                        case "Lauzer":
                            inplanete.coords = new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 500, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 500);

                            result = "";

                            foreach (Building b in buildings)
                            {
                                result += b.Name + "/";
                            }

                            Engine.upload("files/saves/planets/03.txt", result);

                            break;
                        case "Nasté":
                            inplanete.coords = new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * (-200), Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * (-400));

                            result = "";

                            foreach (Building b in buildings)
                            {
                                result += b.Name + "/";
                            }

                            Engine.upload("files/saves/planets/04.txt", result);

                            break;
                        case "Athou":
                            inplanete.coords = new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * (-400), Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 600);

                            result = "";

                            foreach (Building b in buildings)
                            {
                                result += b.Name + "/";
                            }

                            Engine.upload("files/saves/planets/05.txt", result);

                            break;
                        case "Lemon King":
                            inplanete.coords = new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * (-350));

                            result = "";

                            foreach (Building b in buildings)
                            {
                                result += b.Name + "/";
                            }

                            Engine.upload("files/saves/planets/06.txt", result);

                            break;
                    }

                    inplanete.normcoords();

                    buildings = new List<Building>();
                    buttons.Clear();
                    Screen = "game";
                    escapePressed = false;
                    return;
                }
            }
        }

        void drawBlue(RenderWindow rw)
        {
            RectangleShape blueover = new RectangleShape(new Vector2f(Program.resolution.X - 20, Program.resolution.Y - 20));
            blueover.FillColor = new Color(0, 50, 100, 100);
            blueover.OutlineThickness = 5f;
            blueover.OutlineColor = new Color(100, 100, 255, 100);
            blueover.Position = new Vector2f(10, 10);
            rw.Draw(blueover);
        }

        void checkKeys(RenderWindow rw)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                escapePressed = true;
            }
            else
            {
                if (escapePressed)
                {
                    string result = "";
                    string result2 = "";
                    foreach (Planet planet in planets)
                    {
                        for (int i = 0; i < planet.ConnectedBuildings.Count; i++)
                        {
                            result += planet.ConnectedBuildings[i].Name + "/";
                        }
                        result2 += planet.Colonized.ToString() + "/" + planet.Dead.ToString() + "/";
                    }
                    Engine.upload("files/saves/player.txt", $@"{player.name}/{player.Health}/{player.Fuel}/{player.Maxfuel}/{player.coords.X}/{player.coords.Y}");

                    Engine.upload("files/saves/save.txt", result2);

                    for (int i = 0; i < playersShip.Count; i++)
                    {
                        Engine.upload($@"files/saves/ships/0{i + 1}.txt", $@"{playersShip[i].name}/{playersShip[i].Health}/{playersShip[i].Fuel}/{playersShip[i].Maxfuel}/{playersShip[i].coords.X}/{playersShip[i].coords.Y}");
                    }

                    Screen = "0";
                    escapePressed = false;
                    planets.Clear();
                    return;                
                }
            }
        }

        void continuegame(RenderWindow rw)
        {
            string[] info = Engine.load("files/saves/save.txt").Split(new char[] { '/' });
            string[] playerinfo = Engine.load("files/saves/player.txt").Split(new char[] { '/' });
            string[,] planetinfo = new string[5, 6];
            string[,] shipinfo = new string[5, 6];
            bool first = true;
            for (int i = 0; i < planetinfo.GetLength(0); i++)
            {
                if (Engine.load($@"files/saves/planets/0{i + 1}.txt") != "")
                {
                    string[] buffer1 = Engine.load($@"files/saves/planets/0{i + 1}.txt").Split(new char[] { '/' });
                    for (int j = 0; j < buffer1.Length; j++)
                    {
                        Console.WriteLine(buffer1[j]);
                        planetinfo[i, j] = buffer1[j];
                    }
                }
            }

            for(int i = 0; i < shipinfo.GetLength(0); i++)
            {
                if (Engine.load($@"files/saves/ships/0{i + 1}.txt") != "")
                {
                    string[] buffer = Engine.load($@"files/saves/ships/0{i + 1}.txt").Split(new char[] { '/' });
                    for (int j = 0; j < buffer.GetLength(0); j++)
                    {
                        shipinfo[i, j] = buffer[j];
                    }
                }
            }
            
            player = new Player(playerinfo[0], Convert.ToInt32(playerinfo[1]), Convert.ToSingle(playerinfo[2]), Convert.ToSingle(playerinfo[3]), new Vector2f(Convert.ToSingle(playerinfo[4]), Convert.ToSingle(playerinfo[5])), new Texture("files/img/Vzhvzhvzhvzh.png"), 0.5f);

            planets.Add(new Planet("Sterrum", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2), 75, new Texture("files/img/planet01.png"), 1f, Convert.ToBoolean(info[0]), Convert.ToBoolean(info[1])));
            planets.Add(new Planet("Akra", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 300, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 300), 75, new Texture("files/img/planet02.png"), 1f, Convert.ToBoolean(info[2]), Convert.ToBoolean(info[3])));
            planets.Add(new Planet("Lauzer", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 500, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 500), 75, new Texture("files/img/planet03.png"), 1f, Convert.ToBoolean(info[4]), Convert.ToBoolean(info[5])));
            planets.Add(new Planet("Nasté", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * (-200), Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * (-400)), 75, new Texture("files/img/planet04.png"), 1f, Convert.ToBoolean(info[6]), Convert.ToBoolean(info[7])));
            planets.Add(new Planet("Athou", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * (-400), Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * 600), 75, new Texture("files/img/planet05.png"), 1f, Convert.ToBoolean(info[8]), Convert.ToBoolean(info[9])));
            planets.Add(new Planet("Lemon King", new Vector2f(Program.resolution.X / 2 - Program.resolution.X / Program.resolution.Y * 400, Program.resolution.Y / 2 - Program.resolution.Y / Program.resolution.X * (-350)), 75, new Texture("files/img/planet06.png"), 1f, Convert.ToBoolean(info[10]), Convert.ToBoolean(info[11])));

            for(int i = 0; i < planetinfo.GetLength(0); i++)
            {
                for(int j = 0; j < planetinfo.GetLength(1); j++)
                {
                    switch (planetinfo[i, j])
                    {
                        case "Фабрика": planets[i].ConnectedBuildings.Add(factory); break;
                        case "Центр развлечений": planets[i].ConnectedBuildings.Add(park); break;
                        case "Город":
                            if (first)
                            {
                                first = false;
                                planets[i].ConnectedBuildings.Add(house1);
                                break;
                            }
                            else
                            {
                                planets[i].ConnectedBuildings.Add(house2);
                                break;
                            }
                        case "Галактическая казарма": planets[i].ConnectedBuildings.Add(barracks); break;
                    }
                }
                first = true;
            }

            for (int i = 0; i < shipinfo.GetLength(0); i++)
            {
                if(shipinfo[i, 0] != "")
                {
                    playersShip.Add(new Ship(shipinfo[i, 0], Convert.ToInt32(shipinfo[i, 1]), Convert.ToSingle(shipinfo[i, 2]), Convert.ToSingle(shipinfo[i, 2]), new Vector2f(Convert.ToSingle(shipinfo[i, 3]), Convert.ToSingle(shipinfo[i, 4])), new Texture("files/img/ship.png")));
                }
            }

            Screen = "game";
        }

        void newgame(RenderWindow rw)
        {
            Screen = "game";
        }

        void statistic(RenderWindow rw)
        {

        }

        void titles(RenderWindow rw)
        {
            rw.PrintText("Программист: Дворянников Дмитрий", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2 - 75), 25, new Color(255, 255, 255, 200), Font, 1);
            rw.PrintText("Арт Дизайн: Баршутина Валерия", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2 + 75), 25, new Color(255, 255, 255, 200), Font, 1);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                escapePressed = true;
            }
            else
            {
                if (escapePressed)
                {
                    escapePressed = false;
                    Screen = "0";
                    return;
                }
            }
        }

        void win(RenderWindow rw)
        {
            Text text = new Text("Поздравляем!", Font, 50);
            Text text2 = new Text("Вы выиграли!", Font, 50);

            Text contunu = new Text("Нажмите пробел, чтобы продолжить", Font, 50);

            Text letter = new Text("A", Font, 50);

            float textLength = text.GetLocalBounds().Width;
            float textLength2 = text2.GetLocalBounds().Width;

            float letterLength = letter.GetLocalBounds().Width;

            RectangleShape rect = new RectangleShape(new Vector2f(textLength2 + letterLength, 400));
            rect.Position = new Vector2f(Program.resolution.X / 2 - textLength2 / 2 - letterLength / 2, Program.resolution.Y / 2 - 200);
            rect.FillColor = new Color(0, 49, 83, 200);
            rw.Draw(rect);

            rw.PrintText("Поздравляем!", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2 - 100), 50, new Color(255, 255, 255, 200), Font, 1);
            rw.PrintText("Вы выиграли!", new Vector2f(Program.resolution.X / 2, Program.resolution.Y / 2 + 100), 50, new Color(255, 255, 255, 200), Font, 1);
            rw.PrintText("Нажмите ПРОБЕЛ, чтобы продолжить", new Vector2f(Program.resolution.X / 2, Program.resolution.Y - 100), 70, new Color(255, 255, 255, 200), Font, 1);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                escapePressed = true;
            }
            else
            {
                if (escapePressed)
                {
                    escapePressed = false;
                    Screen = "0";
                    return;
                }
            }
        }

        Planet GetClosestPlanet(float X, float Y) // получаем ближайшую координату к точке
        {
            Planet res = null;

            foreach (Planet planet in planets)
            {
                if (new Vector2f(planet.x, planet.y).Distance(new Vector2f(X, Y)) < planet.Radius)
                {
                    res = planet;
                }
            }

            return res;
        }
    }
}
