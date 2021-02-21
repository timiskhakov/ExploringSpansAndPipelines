using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExploringSpansAndPipelines.Benchmarks.Interfaces;
using ExploringSpansAndPipelines.Benchmarks.Models;

namespace ExploringSpansAndPipelines.Benchmarks.Parsers
{
    public class FileParser : IFileParser
    {
        private readonly ILineParser _lineParser;
        
        public FileParser(ILineParser lineParser)
        {
            _lineParser = lineParser;
        }
        
        public async Task<List<Videogame>> Parse(string file)
        {
            var videogames = new List<Videogame>();

            using (var stream = File.OpenRead(file))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var videogame = _lineParser.Parse(line);
                    videogames.Add(videogame);
                }    
            }
            
            return videogames;
        }
    }
}