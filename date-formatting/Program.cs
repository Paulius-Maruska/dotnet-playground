using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace date_formatting
{
    class Program
    {
        static void p(string nm, DateTime dt, string fmt)
        {
            Console.Out.WriteLine("nm: {0}, fmt: {1}, date: {2}", nm, fmt, dt.ToString(fmt));
        }

        static void r(string nm, DateTime dt, string fmt)
        {
            string format = String.Concat("nm: {0}, fmt: {1}, date: {2:", fmt, "}");
            Console.Out.WriteLine(format, nm, fmt, dt);
        }

        static void t(DateTime dt1, DateTime dt2, string fmt)
        {
            p("dt1", dt1, fmt);
            r("dt1", dt1, fmt);
            p("dt2", dt2, fmt);
            r("dt2", dt2, fmt);
        }

        static void Main(string[] args)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.UtcNow;

            t(dt1, dt2, "o");
            t(dt1, dt2, "s");
            t(dt1, dt2, "d");
            t(dt1, dt2, "g");
            t(dt1, dt2, "yyyy-MM-dd");
            t(dt1, dt2, "HH:mm:ss.FFFFFFK");
        }
    }
}
