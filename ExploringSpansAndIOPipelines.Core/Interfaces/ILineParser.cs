using ExploringSpansAndIOPipelines.Core.Models;

namespace ExploringSpansAndIOPipelines.Core.Interfaces
{
    public interface ILineParser
    {
        Videogame Parse(string line);
    }
}