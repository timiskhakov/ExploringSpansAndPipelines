using ExploringSpansAndPipelines.Models;

namespace ExploringSpansAndPipelines.Interfaces
{
    public interface ILineParser
    {
        Videogame Parse(string line);
    }
}