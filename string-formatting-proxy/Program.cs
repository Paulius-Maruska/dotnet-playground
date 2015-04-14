using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace string_formatting_proxy
{
    class Program
    {
        static string Proxy1(string msg, object[] args)
        {
            return String.Concat("Proxy1: ", String.Format(msg, args));
        }

        static string Proxy2(string msg, params object[] args)
        {
            return String.Concat("Proxy2: ", String.Format(msg, args));
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Proxy1("Will {0} work {1}", new object[] { "this", '?' }));
            Console.WriteLine(Proxy1("{0} + {0} = {1}", new object[] { 1, 2 }));
            Console.WriteLine(Proxy1("{0} / {1} = {2}", new object[] { 1, 2, 0.5 }));
            // Console.WriteLine(Proxy1("No Params?", null)); // => crash
            Console.WriteLine(Proxy1("No Params?", new object[0]));

            Console.WriteLine(Proxy2("Will {0} work {1}", "this", '?'));
            Console.WriteLine(Proxy2("{0} + {0} = {1}", 1, 2));
            Console.WriteLine(Proxy2("{0} / {1} = {2}", 1, 2, 0.5));
            Console.WriteLine(Proxy2("No Params?"));
        }
    }
}
