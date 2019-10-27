using BenchmarkDotNet.Running;

namespace ExploringSpansAndIOPipelines.Benchmarks
{
    internal static class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<ParsersComparision>();
        }
    }
}