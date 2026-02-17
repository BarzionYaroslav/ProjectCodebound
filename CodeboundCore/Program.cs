// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int r = 0;
        bool reverse = false;
        int change_speed = 2;
        Console.Clear();
        while (true)
        {
            if (reverse)
                r += change_speed;
            else
                r -= change_speed;
            if (r >= 255 || r <= 0)
                reverse = !reverse;
            r = Math.Clamp(r, 0, 255);
            string text = $"\e[38;2;{r};255;0mHello, World!\n\e[38;2;{r};0;255mFine day, isn't it?\e[0m";
            Console.Write(text);
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(5);
        }
    }
}