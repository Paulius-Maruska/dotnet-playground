using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace unhandled_exception
{
    class Program
    {
        static void BadFunc(string foo, int bar)
        {
            Console.WriteLine("{0}: {1}", foo, bar);
            if (bar % 2 == 1)
                throw new ArgumentException();
        }

        static void Main(string[] args)
        {
            AppDomain domain = AppDomain.CurrentDomain;
            domain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

            for (int i = 0; i < 5; i++)
            {
                BadFunc(string.Format("dummy{0}", i + 1), i);
            }
        }
        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                AppDomain domain = (AppDomain)sender;
                Exception error = (Exception)args.ExceptionObject;
                Console.WriteLine("Unhandled Exception.");
                Console.WriteLine("Sender name: {0}", domain.FriendlyName);
                Console.WriteLine("Exception: {0}", error);
                Console.WriteLine("Exception.Data.Count: {0}", error.Data.Count);
                foreach (object key in error.Data.Keys)
                {
                    Console.WriteLine("Exception.Data[{0}]: {1}", key, error.Data[key]);
                }
                Console.WriteLine("Exception.TargetSite: {0}", error.TargetSite);
                ParameterInfo[] parameters = error.TargetSite.GetParameters();
                Console.WriteLine("Exception.TargetSite.Parameters.Count: {0}", parameters.Length);
                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterInfo param = parameters[i];
                    Console.WriteLine("Exception.TargetSite.Parameters[{0}].Type: {1}", i, param.ParameterType.FullName);
                    Console.WriteLine("Exception.TargetSite.Parameters[{0}].Name: {1}", i, param.Name);
                }
            }
            catch (Exception) {/* at this point we can only give up */}
        }
    }
}
