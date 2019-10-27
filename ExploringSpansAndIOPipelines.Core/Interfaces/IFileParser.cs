using System.Collections.Generic;
using System.Threading.Tasks;
using ExploringSpansAndIOPipelines.Core.Models;

namespace ExploringSpansAndIOPipelines.Core.Interfaces
{
    public interface IFileParser
    {
        Task<List<Videogame>> Parse(string file);
    }
}