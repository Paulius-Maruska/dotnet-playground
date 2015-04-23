using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using core;

namespace async_methods
{
    public class WithTasks
    {
        private Logging _log;
        private int _delay;
        private TaskFactory _taskFactory;

        public WithTasks(Logging parent, int delay)
            : this(parent, delay, Task.Factory)
        { }

        public WithTasks(Logging parent, int delay, TaskFactory taskFactory)
        {
            _log = parent.Sub("WithTasks");
            _delay = delay;
            _taskFactory = taskFactory;
        }

        public void Method1(int x)
        {
            Logging sub = _log.Sub("Method1");
            sub.Info("Called with x:{0}", x);
            Thread.Sleep(_delay);
            sub.Info("Done with x:{0}", x);
        }

        public Task Method1Async(int x)
        {
            Logging sub = _log.Sub("Method1Async");
            sub.Info("Called with x:{0}", x);
            if (!_taskFactory.CancellationToken.IsCancellationRequested)
            {
                Task t = _taskFactory.StartNew(() =>
                {
                    Logging s = sub.Sub("~Lambda");
                    s.Info("Called with x:{0}", x);
                    Method1(x);
                    s.Info("Done with x:{0}", x);
                });
                sub.Info("Done with x:{0}, r:Task", x);
                return t;
            }
            else
            {
                sub.Warning("Ignored with x:{0} - Cancellation requested!", x);
                return null;
            }
        }

        public int Method2(int x)
        {
            Logging sub = _log.Sub("Method2");
            sub.Info("Called with x:{0}", x);
            Thread.Sleep(_delay);
            int r = x * x;
            sub.Info("Done with x:{0}, r:{1}", x, r);
            return r;
        }

        public Task<int> Method2Async(int x)
        {
            Logging sub = _log.Sub("Method2Async");
            sub.Info("Called with x:{0}", x);
            if (!_taskFactory.CancellationToken.IsCancellationRequested)
            {
                Task<int> t = _taskFactory.StartNew<int>(() =>
                {
                    Logging s = sub.Sub("~Lambda");
                    s.Info("Called with x:{0}", x);
                    int r = Method2(x);
                    s.Info("Done with x:{0}, r:{1}", x, r);
                    return r;
                });
                sub.Info("Done with x:{0}, r:Task<int>", x);
                return t;
            }
            else
            {
                sub.Warning("Ignored with x:{0} - Cancellation requested!", x);
                return null;
            }
        }
    }
}
