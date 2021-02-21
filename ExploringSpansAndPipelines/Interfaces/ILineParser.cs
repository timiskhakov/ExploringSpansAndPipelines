using ExploringSpansAndPipelines.Benchmarks.Models;

namespace ExploringSpansAndPipelines.Benchmarks.Interfaces
{
    public interface ILineParser
    {
        Videogame Parse(string line);
    }
}