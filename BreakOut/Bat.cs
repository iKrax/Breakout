using System;

namespace BreakOut
{
    //Methods for the bat, such as shifting to the side.
    public class Bat
    {
        public static int currentBatLocation;

        public static void initialize()
        {
            currentBatLocation = (Console.WindowWidth / 2 - 4);
            drawBat();
        }

        private static void drawBat()
        {
            Console.SetCursorPosition(currentBatLocation, Console.WindowHeight - 3);
            Console.Write("┌───────┐");
        }

        public static void shiftBat(direction direct)
        {
            if (direct == direction.Left && currentBatLocation > 1)
            {
                Console.SetCursorPosition(currentBatLocation - 1, Console.WindowHeight - 3);
                Console.Write("┌───────┐ ");
                currentBatLocation--;
            }
            else if (direct == direction.Right && currentBatLocation < Console.WindowWidth - 10)
            {
                Console.SetCursorPosition(currentBatLocation, Console.WindowHeight - 3);
                Console.Write(" ┌───────┐");
                currentBatLocation++;
            }
        }
    }
}
