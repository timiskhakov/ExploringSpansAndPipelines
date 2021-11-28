using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExploringSpansAndPipelines.Interfaces;
using ExploringSpansAndPipelines.Models;

namespace ExploringSpansAndPipelines.Parsers
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

            await using var stream = File.OpenRead(file);
            using var reader = new StreamReader(stream);
            
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var videogame = _lineParser.Parse(line!);
                videogames.Add(videogame);
            }

            return videogames;
        }
    }
}