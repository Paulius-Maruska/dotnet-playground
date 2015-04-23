using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using core;

namespace async_methods
{
    public class Program
    {
        private static Logging log = new Logging("async_methods");
        static void Main(string[] args)
        {
            log.Info("Starting...");
            CancellationTokenSource token = new CancellationTokenSource();
            TaskFactory factory = new TaskFactory(token.Token);
            WithTasks wt = new WithTasks(log, 1000, factory);
            int x = 0;
            wt.Method1(x++);
            wt.Method2(x++);
            ProcTask1(wt.Method1Async(x++), Done1(x - 1));
            ProcTask2(wt.Method2Async(x++), Done2(x - 1));
            token.Cancel();
            ProcTask1(wt.Method1Async(x++), Done1(x - 1));
            ProcTask2(wt.Method2Async(x++), Done2(x - 1));
            log.Info("Sleeping a bit, to wait for all the results");
            Thread.Sleep(2000);
            log.Info("Done...");
        }
        static void ProcTask1(Task t, Action<Task> c)
        {
            if (t != null)
                t.ContinueWith(c);
        }
        static void ProcTask2(Task<int> t, Action<Task<int>> c)
        {
            if (t != null)
                t.ContinueWith(c);
        }
        static Action<Task> Done1(int x)
        {
            return (task) =>
            {
                log.Info("Method1Async with {0}", x - 1);
            };
        }
        static Action<Task<int>> Done2(int x)
        {
            return (task) =>
            {
                log.Info("Method2Async with {0} returned {1}", x - 1, task.Result);
            };
        }
    }
}
