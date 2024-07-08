namespace AsyncAwait.Sample
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            //ProgramSyncVersion.PrepareBreakfast();

            //await ProgramAsyncVersion1.PrepareBreakfast();

            //await ProgramAsyncVersion2.PrepareBreakfast();

            await ProgramAsyncVersion3.PrepareBreakfast();

            await ProgramAsyncVersion4.PrepareBreakfast();
        }
    }

    // These classes are intentionally empty for the purpose of this example. They are simply marker classes for the purpose of demonstration, contain no properties, and serve no other purpose.
    internal class Bacon { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }
}
