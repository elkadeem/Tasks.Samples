using System.Globalization;

namespace Tasks.Sample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            try
            {

            

            var task1 = Task.Run(() => {
                Task.Delay(5000).Wait();
                throw new InvalidOperationException();
            });

            var task2 = Task.Run(() => {
                Task.Delay(20000).Wait();
                Console.WriteLine("Task 2 completed");
            });

            var task3 = Task.Run(() => {
                Task.Delay(30000).Wait();
                Console.WriteLine("Task 2 completed");
            });

            //Task.WaitAll(task1, task2, task3);
            await Task.WhenAll(task1, task2, task3);

            }
            catch (AggregateException ex)
            {
                Console.WriteLine("exception handler");
            }

            // Pass by value
            //int x = 10;
            //DoubleNumber(x);
            //Console.WriteLine(x);

            // Pass by ref
            //CustomData customData = new CustomData() { 
            //     CreationTime = 10L,
            //     Name = 1,
            //     ThreadNum = 2
            //};

            //DoubleNumber(customData);
            //Console.WriteLine(customData.ThreadNum);

            //Sample1();

            //Sample2();


            //Sample3();

            //Sample4();

            //Sample5();

            //FixIssueInSample5();

            UseAsyncStateSample5();

            //TheadCultureSample();

            //ContinueWithSample();

            //ContinueWithSampleChain();

            //InnerTaskSample();

            //InnerTaskWithAttachToParent();
        }

        // Pass by value
        private static void DoubleNumber(int x)
        {
            x = x * 2;
        }

        // Pass by value
        private static void DoubleNumber(CustomData customData)
        {
            customData = new CustomData();
            customData.ThreadNum = 2 * 10;
        }

        private static void InnerTaskWithAttachToParent()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task beginning.");
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    int taskNo = ctr;
                    Task.Factory.StartNew((x) =>
                    {
                        Thread.SpinWait(5000000);
                        Console.WriteLine("Attached child #{0} completed.",
                                          x);
                    },
                                          taskNo, TaskCreationOptions.AttachedToParent);
                }
            });

            parent.Wait();
            Console.WriteLine("Parent task completed.");
        }


        private static void InnerTaskSample()
        {
            var outer = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task beginning.");

                var child = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(5000000);
                    Console.WriteLine("Detached task completed.");
                });
            });

            outer.Wait();
            Console.WriteLine("Outer task completed.");
        }

        private static void ContinueWithSampleChain()
        {
            var displayData = Task.Factory.StartNew(() =>
            {
                Random rnd = new Random();
                int[] values = new int[100];
                for (int ctr = 0; ctr <= values.GetUpperBound(0); ctr++)
                    values[ctr] = rnd.Next();

                return values;
            }).
                                    ContinueWith((x) =>
                                    {
                                        int n = x.Result.Length;
                                        long sum = 0;
                                        double mean;

                                        for (int ctr = 0; ctr <= x.Result.GetUpperBound(0); ctr++)
                                            sum += x.Result[ctr];

                                        mean = sum / (double)n;
                                        return Tuple.Create(n, sum, mean);
                                    }).
                                    ContinueWith((x) =>
                                    {
                                        return String.Format("N={0:N0}, Total = {1:N0}, Mean = {2:N2}",
                                                             x.Result.Item1, x.Result.Item2,
                                                             x.Result.Item3);
                                    });
            Console.WriteLine(displayData.Result);
        }

        private static void ContinueWithSample()
        {
            var getData = Task.Factory.StartNew(() =>
            {
                Random rnd = new Random();
                int[] values = new int[100];
                for (int ctr = 0; ctr <= values.GetUpperBound(0); ctr++)
                    values[ctr] = rnd.Next();

                return values;
            });
            var processData = getData.ContinueWith((x) =>
            {
                int n = x.Result.Length;
                long sum = 0;
                double mean;

                for (int ctr = 0; ctr <= x.Result.GetUpperBound(0); ctr++)
                    sum += x.Result[ctr];

                mean = sum / (double)n;
                return Tuple.Create(n, sum, mean);
            });
            var displayData = processData.ContinueWith((x) =>
            {
                return String.Format("N={0:N0}, Total = {1:N0}, Mean = {2:N2}",
                                     x.Result.Item1, x.Result.Item2,
                                     x.Result.Item3);
            });
            Console.WriteLine(displayData.Result);
        }

        private static void TheadCultureSample()
        {
            decimal[] values = { 163025412.32m, 18905365.59m };
            string formatString = "C2";
            Func<String> formatDelegate = () =>
            {
                string output = String.Format("Formatting using the {0} culture on thread {1}.\n",
                                                                                CultureInfo.CurrentCulture.Name,
                                                                                Thread.CurrentThread.ManagedThreadId);
                foreach (var value in values)
                    output += String.Format("{0}   ", value.ToString(formatString));

                output += Environment.NewLine;
                return output;
            };

            Console.WriteLine("The example is running on thread {0}",
                              Thread.CurrentThread.ManagedThreadId);
            // Make the current culture different from the system culture.
            Console.WriteLine("The current culture is {0}",
                              CultureInfo.CurrentCulture.Name);
            if (CultureInfo.CurrentCulture.Name == "fr-FR")
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            else
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            Console.WriteLine("Changed the current culture to {0}.\n",
                              CultureInfo.CurrentCulture.Name);

            // Execute the delegate synchronously.
            Console.WriteLine("Executing the delegate synchronously:");
            Console.WriteLine(formatDelegate());

            // Call an async delegate to format the values using one format string.
            Console.WriteLine("Executing a task asynchronously:");
            var t1 = Task.Run(formatDelegate);
            Console.WriteLine(t1.Result);

            Console.WriteLine("Executing a task synchronously:");
            var t2 = new Task<String>(formatDelegate);
            t2.RunSynchronously();
            Console.WriteLine(t2.Result);
        }

        private static void UseAsyncStateSample5()
        {
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null) return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                },
                new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(taskArray);
            foreach (var task in taskArray)
            {
                var data = task.AsyncState as CustomData;
                if (data != null)
                    Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.",
                                      data.Name, data.CreationTime, data.ThreadNum);
            }
        }


        private static void FixIssueInSample5()
        {
            // Create the task object by using an Action(Of Object) to pass in custom data
            // to the Task constructor. This is useful when you need to capture outer variables
            // from within a loop.
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null)
                        return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task #{0} created at {1} on thread #{2}.",
                                     data.Name, data.CreationTime, data.ThreadNum);
                },
                                                      new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(taskArray);
        }

        private static void Sample5()
        {
            // Create the task object by using an Action(Of Object) to pass in the loop
            // counter. This produces an unexpected result.
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    //int x = (int)obj;
                    var data = new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks };
                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task #{0} created at {1} on thread #{2}.",
                                      data.Name, data.CreationTime, data.ThreadNum);
                },
                                                     i);
            }
            Task.WaitAll(taskArray);
        }

        private static void Sample4()
        {
            Task<Double>[] taskArray = { Task<Double>.Factory.StartNew(() => DoComputation(1.0)),
                                     Task<Double>.Factory.StartNew(() => DoComputation(100.0)),
                                     Task<Double>.Factory.StartNew(() => DoComputation(1000.0)) };

            var results = new Double[taskArray.Length];
            Double sum = 0;

            for (int i = 0; i < taskArray.Length; i++)
            {
                results[i] = taskArray[i].Result;
                Console.Write("{0:N1} {1}", results[i],
                                  i == taskArray.Length - 1 ? "= " : "+ ");
                sum += results[i];
            }
            Console.WriteLine("{0:N1}", sum);
        }

        private static Double DoComputation(Double start)
        {
            Double sum = 0;
            for (var value = start; value <= start + 10; value += .1)
                sum += value;

            return sum;
        }

        private static void Sample3()
        {
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null) return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                },
                new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(taskArray);
            foreach (var task in taskArray)
            {
                var data = task.AsyncState as CustomData;
                if (data != null)
                    Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.",
                                      data.Name, data.CreationTime, data.ThreadNum);
            }
        }

        private static void Sample1()
        {
            Thread.CurrentThread.Name = "Main";

            // Create a task and supply a user delegate by using a lambda expression.
            Task taskA = new Task(() => Console.WriteLine("Hello from taskA."));
            // Start the task.
            taskA.Start();
            
            // Output a message from the calling thread.
            Console.WriteLine("Hello from thread '{0}'.",
                              Thread.CurrentThread.Name);
            taskA.Wait();
        }

        private static void Sample2()
        {
            Thread.CurrentThread.Name = "Main";

            // Define and run the task.
            Task taskA = Task.Run(() => Console.WriteLine("Hello from taskA."));

            // Output a message from the calling thread.
            Console.WriteLine("Hello from thread '{0}'.",
                                Thread.CurrentThread.Name);
            taskA.Wait();
        }
    }

    class CustomData
    {
        public long CreationTime;
        public int Name;
        public int ThreadNum;
    }
}
