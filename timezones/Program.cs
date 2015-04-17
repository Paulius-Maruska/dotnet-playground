using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timezones
{
    class Program
    {
        static void PrintAll()
        {
            string header  = String.Format("{0,10} | {1,35} | {2}", "offset", "Id", "Display Name");
            string separator = String.Format("{0}-+-{1}-+-{2}", "".PadLeft(10, '-'), "".PadLeft(35, '-'), "".PadLeft(header.Length - 50, '-'));
            Console.WriteLine(separator);
            Console.WriteLine(header);
            Console.WriteLine(separator);
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
            {
                Console.WriteLine("{0,10} | {1,35} | {2}", tz.BaseUtcOffset.ToString(), tz.Id, tz.DisplayName);
            }
            Console.WriteLine(separator);
        }

        static void Convert(DateTime t, string id)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(id);
            DateTimeOffset tt = new DateTimeOffset(t);
            DateTimeOffset to = TimeZoneInfo.ConvertTime(tt, tz);
            Console.WriteLine("{0:o} ({1:o}) => {2:o}  [ {3} ]", t, tt, to, tz);
        }

        static void Main(string[] args)
        {
            PrintAll();

            Console.WriteLine("Local: {0}", TimeZoneInfo.Local);

            // Kind == Utc
            Convert(DateTime.UtcNow, "FLE Standard Time");
            Convert(DateTime.UtcNow, "Pacific Standard Time");
            // Kind == Local
            Convert(DateTime.Now, "FLE Standard Time");
            Convert(DateTime.Now, "Pacific Standard Time");
            // Kind == Unspecified
            Convert(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC-02"), "FLE Standard Time");
            Convert(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC-02"), "Pacific Standard Time");
        }
    }
}
