using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enums
{
    class Program
    {
        enum Foo : byte
        {
            A = 0x01,
            B = 0x02,
            C = 0x03,
        }

        static void IsDefined(Foo f)
        {
            if (Enum.IsDefined(typeof(Foo), f))
                Console.WriteLine("Value {0} is defined: {1}", (int)f, f);
            else
                Console.WriteLine("Value {0} is undefined", (int)f);
        }

        static void Main(string[] args)
        {
            Foo f = Foo.A;
            Console.WriteLine("f: {0} ({1})", f, (int)f);
            IsDefined(f);

            f = (Foo)17;
            Console.WriteLine("f: {0} ({1})", f, (int)f);
            IsDefined(f);
        }
    }
}
