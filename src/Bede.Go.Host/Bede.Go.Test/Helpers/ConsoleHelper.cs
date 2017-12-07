using Bede.Go.Test.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bede.Go.Test.Helpers
{
    public class ConsoleHelper
    {
        private const char DefaultBarChar = '\u2592';
        private const int DefaultBarSize = 50;

        public static void Print(string message, MessageTypeEnum messageType)
        {
            var tmpColor = Console.ForegroundColor;
            Console.ForegroundColor = messageType == MessageTypeEnum.Error ? ConsoleColor.Red : ConsoleColor.White;
            Console.WriteLine(message);
            Console.ForegroundColor = tmpColor;
        }

        public static void DrawProgressBar(long completed, long total)
        {
            Console.CursorVisible = false;

            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            var perc = (double)completed / total;
            var chars = (int)Math.Floor(perc * DefaultBarSize);
            var p1 = string.Empty.PadLeft(chars, DefaultBarChar);
            var p2 = string.Empty.PadLeft(DefaultBarSize - chars, DefaultBarChar);

            Print("Progress: ", MessageTypeEnum.Info);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(p1);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(p2);

            Console.ResetColor();
            Console.Write(" {0}%", (perc * 100).ToString("n2"));
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }
    }
}
