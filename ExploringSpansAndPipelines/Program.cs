using BenchmarkDotNet.Running;
using ExploringSpansAndPipelines.Benchmarks.Comparisons;

namespace ExploringSpansAndPipelines.Benchmarks
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