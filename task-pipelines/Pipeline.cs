using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using core;

namespace task_pipelines
{
    public class Pipeline
    {
        #region (private) fields

        private Logging _log;
        private List<Task> _completionTasks;
        private Func<object, int[]> generatorFunc;
        private Func<object, int> itemCalculatorFunc;
        private Action<Task<int[]>> calculatorFunc;
        private Action<Task<int[]>> outputFunc;

        #endregion

        #region (public) constructors

        public Pipeline()
        {
            _log = new Logging("Pipeline");
            generatorFunc = new Func<object, int[]>(numberGenerator);
            itemCalculatorFunc = new Func<object, int>(itemCalculator);
            calculatorFunc = new Action<Task<int[]>>(resultCalculator);
            outputFunc = new Action<Task<int[]>>(resultOutputter);
        }

        #endregion

        #region (public) controls

        public void Start(int count)
        {
            _completionTasks = new List<Task>();


            Task<int[]> generator = new Task<int[]>(generatorFunc, new int[2] { (int)DateTime.Now.ToFileTime(), count });
            _completionTasks.Add(generator.ContinueWith(outputFunc));
            _completionTasks.Add(generator.ContinueWith(calculatorFunc));

            generator.Start();
        }

        public bool Wait(int timeout)
        {
            Logging log = _log.Sub("Wait");
            log.Debug("Waiting for tasks...");
            bool result = Task.WaitAll(_completionTasks.ToArray(), timeout);

            if (result) { log.Info("Done."); } else { log.Warning("Done."); }

            return result;
        }

        #endregion

        #region (private) work function wrappers

        private int[] numberGenerator(object state)
        {
            int[] args = (int[])state;
            return getNumbers(args[0], args[1]);
        }

        private void resultCalculator(Task<int[]> gen)
        {
            int[] numbers = gen.Result;

            List<Task<int>> calculators = new List<Task<int>>();
            int idx = 0;
            foreach (int number in numbers)
            {
                Task<int> t = new Task<int>(itemCalculatorFunc, new int[2] { idx++, number });
                calculators.Add(t);
                t.Start();
            }
            _completionTasks.Add(Task.WhenAll<int>(calculators).ContinueWith(outputFunc));
        }

        private int itemCalculator(object state)
        {
            int[] args = (int[])state;
            return calcSquare(args[0], args[1]);
        }

        private void resultOutputter(object state)
        {
            Task<int[]> t = (Task<int[]>)state;
            outputNumbers(t.Result);
        }

        #endregion

        #region (private) work functions

        private int[] getNumbers(int seed, int count)
        {
            DateTime start = DateTime.UtcNow;

            Logging log = _log.Sub("getNumbers");
            log.Debug(String.Format("generating {1} numbers (seed = {0})...", seed, count));

            Random rand = new Random(seed);
            List<int> result = new List<int>(count);
            for (int i = 0; i < count; ++i)
            {
                result.Add(rand.Next(1, 100));
            }

            log.Debug("sleeping a bit");
            Thread.Sleep(1000);

            log.Debug(String.Format("finished generating numbers in {0} ms", (DateTime.UtcNow - start).TotalMilliseconds));
            return result.ToArray();
        }

        private int calcSquare(int workerId, int num)
        {
            DateTime start = DateTime.UtcNow;

            Logging log = _log.Sub(String.Format("calcSquare<{0}>", workerId));
            log.Debug(String.Format("calculating square for {0}...", num));

            int result = num * num;

            log.Debug("sleeping a bit");
            Thread.Sleep(1000);

            log.Debug(String.Format("finished calculating square for {1}, result is {2} in {0} ms", (DateTime.UtcNow - start).TotalMilliseconds, num, result));
            return result;
        }

        private void outputNumbers(IEnumerable<int> numbers)
        {
            DateTime start = DateTime.UtcNow;

            Logging log = _log.Sub("outputNumbers");
            log.Debug(String.Format("outputing numbers..."));

            string output = "numbers:";
            foreach (int n in numbers)
            {
                output = String.Concat(output, " ", n.ToString());
            }
            log.Info(output);

            log.Debug("sleeping a bit");
            Thread.Sleep(1000);

            log.Debug(String.Format("finished outputing numbers in {0} ms", (DateTime.UtcNow - start).TotalMilliseconds));
        }

        #endregion
    }
}
