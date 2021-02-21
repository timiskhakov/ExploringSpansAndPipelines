using System.Collections.Generic;
using System.Threading.Tasks;
using ExploringSpansAndPipelines.Benchmarks.Models;

namespace ExploringSpansAndPipelines.Benchmarks.Interfaces
{
    public interface IFileParser
    {
        Task<List<Videogame>> Parse(string file);
    }
}