using System.Collections.Generic;
using System.Threading.Tasks;
using ExploringSpansAndPipelines.Models;

namespace ExploringSpansAndPipelines.Interfaces
{
    public interface IFileParser
    {
        Task<List<Videogame>> Parse(string file);
    }
}