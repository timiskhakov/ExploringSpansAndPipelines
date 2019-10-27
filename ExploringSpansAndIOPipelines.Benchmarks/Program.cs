using BenchmarkDotNet.Running;
using ExploringSpansAndIOPipelines.Benchmarks.Comparisions;

namespace ExploringSpansAndIOPipelines.Benchmarks
{
    internal static class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<FileParsersComparision>();
        }
    }
}