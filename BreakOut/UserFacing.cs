using System;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

public enum direction { Up, Down, Left, Right, Up_Left, Up_Right, Down_Left, Down_Right }

/*
    Breakout (CONSOLE) v0.1 by iKrax

    Based upon the game Atari Breakout, the player controls a bat which is used to deflect
    a ball into blocks to gain points..

    Future Updates
    TO BE UPDATED
*/
namespace BreakOut
{

    //User facing methods, such as UI.
    class UserFacing
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
        public static bool menuActive = false;
        enum menuOptions { play = 1, new_game, exit }
        private static readonly object ConsoleLock = "";

        static void Main(string[] args)
        {
            Maximize();
            Thread bat = new Thread(new ThreadStart(batListener));
            Thread ball = new Thread(new ThreadStart(ballPush));
            Console.CursorVisible = false;

            DrawMenuBorder();

            Bat.initialize();
            Ball.initialize();

            bat.Start();
            ball.Start();

        }

        static void DrawMenuBorder()
        {
            //Edges
            //Top
            Console.SetCursorPosition(1, 0);
            for (int i = 0; i < Console.WindowWidth - 2; i++)
            {
                Console.Write("═");
            }
            //Bottom
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            for (int i = 0; i < Console.WindowWidth - 2; i++)
            {
                Console.Write("═");
            }
            //Left
            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
            }
            //Right
            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.Write("║");
            }

            //Corners
            Console.SetCursorPosition(0, 0);
            Console.Write("╔");
            Console.SetCursorPosition(Console.WindowWidth - 1, 0);
            Console.Write("╗");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("╚");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 1);
            Console.Write("╝");
            Console.SetCursorPosition(0, 0);
        }

        static void ballPush()
        {

            while (true)
            {
                lock (ConsoleLock)
                {
                    Ball.shiftBall();
                }
                Thread.Sleep(100);
            }
        }

        static void batListener()
        {
            while (true)
            {

                ConsoleKeyInfo CKI = Console.ReadKey(true);
                if (CKI.Key == ConsoleKey.LeftArrow)
                {
                    lock (ConsoleLock)
                    {
                        Bat.shiftBat(direction.Left);
                    }
                }
                else if (CKI.Key == ConsoleKey.RightArrow)
                {
                    lock (ConsoleLock)
                    {
                        Bat.shiftBat(direction.Right);
                    }
                }
                else if (CKI.Key == ConsoleKey.Escape)
                {
                    menu();
                }

            }
        }

        static void menu()
        {
            menuActive = true;
            menuOptions currSelection = menuOptions.play;

            //Draw basic menu
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 4);
            Console.WriteLine("xxxxxxxxxxxxxxxx");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 3);
            Console.WriteLine("x     MENU     x");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 2);
            Console.WriteLine("x ____________ x");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 1);
            Console.WriteLine("x              x");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
            Console.WriteLine("x    >PLAY<    x");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
            Console.WriteLine("x  |NEW GAME|  x");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 2);
            Console.WriteLine("x    |EXIT|    x");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 3);
            Console.WriteLine("x              x");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 4);
            Console.WriteLine("xxxxxxxxxxxxxxxx");

            //Menu actions
            while (true)
            {
                ConsoleKeyInfo CKI = Console.ReadKey(true);
                if (CKI.Key == ConsoleKey.UpArrow && (int)currSelection != 1)
                {
                    currSelection--;

                    //reset selections
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
                    Console.WriteLine("x    |PLAY|    x");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
                    Console.WriteLine("x  |NEW GAME|  x");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 2);
                    Console.WriteLine("x    |EXIT|    x");

                    switch (currSelection)
                    {
                        case menuOptions.play:
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
                            Console.WriteLine("x    >PLAY<    x");
                            break;
                        case menuOptions.new_game:
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
                            Console.WriteLine("x  >NEW GAME<  x");
                            break;
                    }
                }
                else if (CKI.Key == ConsoleKey.DownArrow && (int)currSelection != 3)
                {
                    currSelection++;

                    //reset selections
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
                    Console.WriteLine("x    |PLAY|    x");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
                    Console.WriteLine("x  |NEW GAME|  x");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 2);
                    Console.WriteLine("x    |EXIT|    x");

                    switch (currSelection)
                    {
                        case menuOptions.new_game:
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
                            Console.WriteLine("x  >NEW GAME<  x");
                            break;
                        case menuOptions.exit:
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 2);
                            Console.WriteLine("x    >EXIT<    x");
                            break;
                        default:
                            break;
                    }
                }
                else if (CKI.Key == ConsoleKey.Enter && (int)currSelection == 1)
                {
                    menuActive = false;
                    break;
                }
                else if (CKI.Key == ConsoleKey.Enter && (int)currSelection == 2)
                {
                    var fileName = Assembly.GetExecutingAssembly().Location;
                    System.Diagnostics.Process.Start(fileName);

                    Environment.Exit(0);

                    break;
                }
                else if (CKI.Key == ConsoleKey.Enter && (int)currSelection == 3)
                {
                    Environment.Exit(0);
                }
            }
            //Remove menu
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 4);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 3);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 2);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 1);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 2);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 3);
            Console.WriteLine("                ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 4);
            Console.WriteLine("                ");

        }

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }
    }
}
