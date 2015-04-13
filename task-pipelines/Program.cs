using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_pipelines
{
    class Program
    {
        static void Main(string[] args)
        {
            Pipeline p = new Pipeline();
            p.Start(10);

            p.Wait(30000);

            Console.ReadKey();
        }
    }
}
