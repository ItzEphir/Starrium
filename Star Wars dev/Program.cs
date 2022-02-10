using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Star_Wars_dev
{
    class Program
    {
        static Game game;

        public static Vector2f OriginalResolution;
        public static Vector2f Resolution;

        static void Main(string[] args)
        {
            OriginalResolution = new Vector2f(1920, 1080);

            VideoMode vm = VideoMode.DesktopMode;
            // VideoMode vm = new VideoMode(1280, 720);

            RenderWindow rw = new RenderWindow(vm, "StarWars", Styles.Fullscreen);
            // RenderWindow rw = new RenderWindow(vm, "StarWars");

            Resolution = new Vector2f(rw.Size.X, rw.Size.Y);

            game = new Game(rw);

            rw.Closed += OnClose;
            rw.KeyPressed += OnKey;

            rw.SetVerticalSyncEnabled(true);

            while (rw.IsOpen)
            {
                rw.DispatchEvents();

                rw.Clear();

                game.Draw();

                game.Update(rw);

                rw.Display();
            }
        }

        static void OnClose(object sender, EventArgs e)
        {
            (sender as RenderWindow)?.Close();
        }

        static void OnKey(object sender, KeyEventArgs e)
        {
            game.KeyPressed(sender, e);
        }
    }
}
