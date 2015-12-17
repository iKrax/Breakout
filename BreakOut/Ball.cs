using System;

public struct position
{
    public int x, y;

    public position(int set_x, int  set_y)
    {
        x = set_x;
        y = set_y;
    }
}

namespace BreakOut
{
    //Methods for the bat, such as moving.
    public class Ball
    {
        const string ballChar = "■";
        public static position currentBallLocation;
        public static direction currentBallDirection = direction.Up_Right;


        public static void initialize()
        {
            currentBallLocation = new position(Console.WindowWidth / 2, Console.WindowHeight - 4);
            drawBall();
        }

        private static void drawBall()
        {
            Console.SetCursorPosition(currentBallLocation.x, currentBallLocation.y);
            Console.Write(ballChar);
        }

        private static void destroyBall()
        {
            Console.SetCursorPosition(currentBallLocation.x, currentBallLocation.y);
            Console.Write(" ");
        }

        public static void shiftBall()
        {
            destroyBall();
            switch (currentBallDirection)
            {
                case direction.Up:
                    if (currentBallLocation.y > 1)
                    {
                        currentBallLocation.y--;
                    }
                    else
                    {
                        currentBallDirection = direction.Down;
                        goto case direction.Down;
                    }
                    break;

                case direction.Down:
                    if ((currentBallLocation.y < Console.WindowHeight - 4) && ((Bat.currentBatLocation - currentBallLocation.x <= 0) && (Bat.currentBatLocation - currentBallLocation.x >= -8)))
                    {
                        currentBallLocation.y++;
                    }
                    else
                    {
                        currentBallDirection = direction.Up;
                        goto case direction.Up;
                    }
                    break;

                case direction.Up_Left:
                    if (currentBallLocation.y > 1 && currentBallLocation.x > 1)
                    {
                        currentBallLocation.y--;
                        currentBallLocation.x--;
                    }
                    else if (!(currentBallLocation.y > 1))
                    {
                        currentBallDirection = direction.Down_Left;
                        goto case direction.Down_Left;
                    }
                    else if (!(currentBallLocation.x > 1))
                    {
                        currentBallDirection = direction.Up_Right;
                        goto case direction.Up_Right;
                    }
                    break;

                case direction.Up_Right:
                    if (currentBallLocation.y > 1 && currentBallLocation.x < Console.WindowWidth - 2)
                    {
                        currentBallLocation.y--;
                        currentBallLocation.x++;
                    }
                    else if (!(currentBallLocation.y > 1))
                    {
                        currentBallDirection = direction.Down_Right;
                        goto case direction.Down_Right;
                    }
                    else if (!(currentBallLocation.x < Console.WindowWidth - 2))
                    {
                        currentBallDirection = direction.Up_Left;
                        goto case direction.Up_Left;
                    }
                    break;

                case direction.Down_Left:
                    if (currentBallLocation.y < Console.WindowHeight - 4 && currentBallLocation.x > 1)
                    {
                        currentBallLocation.y++;
                        currentBallLocation.x--;
                    }
                    else if (!(currentBallLocation.y < Console.WindowHeight - 4) && ((Bat.currentBatLocation - currentBallLocation.x <= 0) && (Bat.currentBatLocation - currentBallLocation.x >= -8)))
                    {
                        currentBallDirection = direction.Up_Left;
                        goto case direction.Up_Left;
                    }
                    else if (!(currentBallLocation.x > 1))
                    {
                        currentBallDirection = direction.Down_Right;
                        goto case direction.Down_Right;
                    }
                    break;

                case direction.Down_Right: //y++x++
                    if (currentBallLocation.y < Console.WindowHeight - 4 && currentBallLocation.x < Console.WindowWidth - 2)
                    {
                        currentBallLocation.y++;
                        currentBallLocation.x++;
                    }
                    else if (!(currentBallLocation.y < Console.WindowHeight - 4) && ((Bat.currentBatLocation - currentBallLocation.x <= 0) && (Bat.currentBatLocation - currentBallLocation.x >= -8)))
                    {
                        currentBallDirection = direction.Up_Right;
                        goto case direction.Up_Right;
                    }
                    else if (!(currentBallLocation.x < Console.WindowWidth - 2))
                    {
                        currentBallDirection = direction.Down_Left;
                        goto case direction.Down_Left;
                    }
                    break;

            }
            drawBall();
        }
    }
}
