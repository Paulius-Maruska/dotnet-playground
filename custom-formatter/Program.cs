using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_formatter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            User user = new User("pam", "Paulius", "Maruška", new DateTime(1983, 4, 12), "Vilnius, Lithuania");

            Console.WriteLine("User: {0}", user);
            Console.WriteLine("User: {0:FirstName} {0:LastName} ({0:Age,X8}) from {0:Location}", user);
            Console.WriteLine("User: {0:FirstName} {0:LastName} ({0:Age}) from {0:Location}", user);
            Console.WriteLine("User: {0:FirstName} {0:LastName} ({0:Birthday,d}) from {0:Location}", user);
            Console.WriteLine("User: {0:FirstName} {0:LastName} ({0:Birthday,yyyy}) from {0:Location}", user);
            Console.WriteLine("User: {0:FirstName|' '|LastName|' ('|Birthday,yyyy|') from '|Location}", user);
        }
    }
}
