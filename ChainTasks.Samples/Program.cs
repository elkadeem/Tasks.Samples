namespace ChainTasks.Samples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // await Sample1();

            //await Sample2();

            //await PassDateToContinuation();

            await PassDateToContinuationHandleFault();
        }

        private static async Task PassDateToContinuationHandleFault()
        {
            await Task.Run(
                        () =>
                        {
                            DateTime date = DateTime.Now;
                            return date.Hour > 17
                               ? "evening"
                               : date.Hour > 12
                                   ? "afternoon"
                                   : "morning";
                        })
                        .ContinueWith(
                            antecedent =>
                            {
                                if (antecedent.Status == TaskStatus.RanToCompletion)
                                {
                                    Console.WriteLine($"Good {antecedent.Result}!");
                                    Console.WriteLine($"And how are you this fine {antecedent.Result}?");
                                }
                                else if (antecedent.Status == TaskStatus.Faulted)
                                {
                                    Console.WriteLine(antecedent.Exception!.GetBaseException().Message);
                                }
                            });
        }

        private static async Task PassDateToContinuation()
        {
            var task = Task.Run(
                       () =>
                       {
                           DateTime date = DateTime.Now;
                           return date.Hour > 17
                               ? "evening"
                               : date.Hour > 12
                                   ? "afternoon"
                                   : "morning";
                       });

            await task.ContinueWith(
                antecedent =>
                {
                    Console.WriteLine($"Good {antecedent.Result}!");
                    Console.WriteLine($"And how are you this fine {antecedent.Result}?");
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private static async Task Sample2()
        {
            var tasks = new List<Task<int>>();
            for (int ctr = 1; ctr <= 10; ctr++)
            {
                int baseValue = ctr;
                tasks.Add(Task.Factory.StartNew(b => (int)b! * (int)b, baseValue));
            }

            var results = await Task.WhenAll(tasks);

            int sum = 0;
            for (int ctr = 0; ctr <= results.Length - 1; ctr++)
            {
                var result = results[ctr];
                Console.Write($"{result} {((ctr == results.Length - 1) ? "=" : "+")} ");
                sum += result;
            }

            Console.WriteLine(sum);
        }

        private static async Task Sample1()
        {
            // Declare, assign, and start the antecedent task.
            Task<DayOfWeek> taskA = Task.Run(() => DateTime.Today.DayOfWeek);

            // Execute the continuation when the antecedent finishes.
            await taskA.ContinueWith(antecedent => Console.WriteLine($"Today is {antecedent.Result}."));
        }
    }
}
