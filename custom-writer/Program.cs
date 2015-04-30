using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_writer
{
    class Program
    {
        class CustomWriter : TextWriter
        {
            private TextWriter _w;
            public int CharsWritten { get; set; }
            public CustomWriter(TextWriter w)
            {
                _w = w;
                CharsWritten = 0;
            }
            public override Encoding Encoding { get { return Console.OutputEncoding; } }
            public override void Write(char c)
            {
                CharsWritten++;
                _w.Write(c);
            }
        }
        static void Main(string[] args)
        {
            TextWriter stdout = Console.Out;
            CustomWriter writer = new CustomWriter(stdout);

            Console.SetOut(writer);
            Console.WriteLine("Hello World!");
            Console.SetOut(stdout);
            Console.WriteLine("Chars: {0}", writer.CharsWritten);
        }
    }
}
