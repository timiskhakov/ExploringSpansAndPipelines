using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using ExploringSpansAndPipelines.Comparisons;

namespace ExploringSpansAndPipelines
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