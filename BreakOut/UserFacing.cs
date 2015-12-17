using System;
using System.Threading;

public enum direction { Up, Down, Left, Right, Up_Left, Up_Right, Down_Left, Down_Right}

/*
    Breakout (CONSOLE) v0.1 by iKrax

    Based upon the game Atari Breakout, the player controls a bat which is used to deflect
    a ball into blocks to gain points.
*/
namespace BreakOut
{
    //User facing methods, such as UI.
    class UserFacing 
    {
        private static readonly object ConsoleLock = "";

        static void Main(string[] args)
        {

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
                Thread.Sleep(1);
            }
        }

        static void batListener()
        {
            while(true)
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
                
            }
        }
    }
}
