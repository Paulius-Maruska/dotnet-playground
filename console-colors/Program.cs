using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_colors
{
    class Program
    {
        static void Print(string[] left, string[] right, int rightAt)
        {
            if (left.Length != right.Length)
                throw new ArgumentException(String.Format("Argument lengths do not match ({0} != {1})", left.Length, right.Length));

            for (int i = 0; i < left.Length; i++)
            {
                Console.Write(left[i]);
                Console.CursorLeft = rightAt;
                Console.WriteLine(right[i]);
            }
            Console.WriteLine();
        }

        static string[] GetStandardColorTable()
        {
            string sep = "".PadRight(6 + 7 * 8, '-');
            string text = string.Concat("Standard Colors ( \\x1b[<fg>;<bg>m )\n", sep, "\n");
            int[] flags = new int[] { 0, 1 };
            text += "fg\\bg|";
            for (int bgColor = 0; bgColor < 8; bgColor++)
            {
                int bg = 40 + bgColor;
                text += string.Format("{0,6} ", bg);
            }
            text += string.Format("\n{0}\n", sep);
            for (int fgColor = 0; fgColor < 8; fgColor++)
            {
                int fg = 30 + fgColor;
                text += string.Format("{0,5}|", fg);
                for (int bgColor = 0; bgColor < 8; bgColor++)
                {
                    int bg = 40 + bgColor;
                    string code = string.Format("{0};{1}", fg, bg);
                    text += string.Format("\x1b[{0}m{0,6}\x1b[0m ", code);
                }
                text += "\n";
            }
            text += sep;
            return text.Split('\n');
        }

        static string[] GetStandardColorTableWithBrightFlag()
        {
            string sep = "".PadRight(6 + 7 * 8, '-');
            string text = string.Concat("Standard Colors with Bright flag ( \\x1b[<fg>;<bg>;1m )\n", sep, "\n");
            int[] flags = new int[] { 0, 1 };
            text += "fg\\bg|";
            for (int bgColor = 0; bgColor < 8; bgColor++)
            {
                int bg = 40 + bgColor;
                text += string.Format("{0,6} ", bg);
            }
            text += string.Format("\n{0}\n", sep);
            for (int fgColor = 0; fgColor < 8; fgColor++)
            {
                int fg = 30 + fgColor;
                text += string.Format("{0,5}|", fg);
                for (int bgColor = 0; bgColor < 8; bgColor++)
                {
                    int bg = 40 + bgColor;
                    string code = string.Format("{0};{1}", fg, bg);
                    text += string.Format("\x1b[{0};1m{0,6}\x1b[0m ", code);
                }
                text += "\n";
            }
            text += sep;
            return text.Split('\n');
        }

        static string[] Get256ColorTableForeground()
        {
            string sep = "".PadRight(4 + 3 * 16, '-');
            string text = string.Concat("Extended Foreground Colors 256 ( \\x1b[38;5;<fg>m )\n", sep, "\n");
            text += "h\\l|";
            for (byte low = 0; low < 16; low++)
            {
                text += string.Format("{0:X2} ", low);
            }
            text += string.Format("\n{0}\n", sep);
            for (byte high = 0; high < 16; high++)
            {
                text += string.Format(" {0:X2}|", high << 4);
                for (byte low = 0; low < 16; low++)
                {
                    int color = high << 4 | low;
                    string code = string.Format("38;5;{0}", color);
                    text += string.Format("\x1b[{0}m{1:X2}\x1b[0m ", code, color);
                }
                text += "\n";
            }
            text += sep;
            return text.Split('\n');
        }

        static string[] Get256ColorTableBackground()
        {
            string sep = "".PadRight(4 + 3 * 16, '-');
            string text = string.Concat("Extended Background Colors 256 ( \\x1b[48;5;<fg>m )\n", sep, "\n");
            text += "h\\l|";
            for (byte low = 0; low < 16; low++)
            {
                text += string.Format("{0:X2} ", low);
            }
            text += string.Format("\n{0}\n", sep);
            for (byte high = 0; high < 16; high++)
            {
                text += string.Format(" {0:X2}|", high << 4);
                for (byte low = 0; low < 16; low++)
                {
                    int color = high << 4 | low;
                    string code = string.Format("48;5;{0}", color);
                    text += string.Format("\x1b[{0}m{1:X2}\x1b[0m ", code, color);
                }
                text += "\n";
            }
            text += sep;
            return text.Split('\n');
        }

        static void Main(string[] args)
        {
            int middle = Console.WindowWidth / 2 - 1;
            Print(GetStandardColorTable(), GetStandardColorTableWithBrightFlag(), middle);
            Print(Get256ColorTableForeground(), Get256ColorTableBackground(), middle);
        }
    }
}
