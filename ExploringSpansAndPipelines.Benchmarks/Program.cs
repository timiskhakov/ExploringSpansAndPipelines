using BenchmarkDotNet.Running;
using ExploringSpansAndIOPipelines.Benchmarks.Comparisons;

namespace ExploringSpansAndIOPipelines.Benchmarks
{
    internal static class Program
    {
        private static void Main()
        {
            //BenchmarkRunner.Run<LineParsersComparison>();
            BenchmarkRunner.Run<FileParsersComparison>();
        }
    }
}