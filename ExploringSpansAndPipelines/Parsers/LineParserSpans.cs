using System;
using System.Globalization;
using ExploringSpansAndPipelines.Benchmarks.Interfaces;
using ExploringSpansAndPipelines.Benchmarks.Models;

namespace ExploringSpansAndPipelines.Benchmarks.Parsers
{
    public class LineParserSpans : ILineParser
    {
        public Videogame Parse(string line)
        {
            var span = line.AsSpan();

            return Parse(span);
        }

        public static Videogame Parse(ReadOnlySpan<char> span)
        {
            var scanned = -1;
            var position = 0;
            
            var id = ParseChunk(ref span, ref scanned, ref position);
            var name = ParseChunk(ref span, ref scanned, ref position);
            var genre = ParseChunk(ref span, ref scanned, ref position);
            var releaseDate = ParseChunk(ref span, ref scanned, ref position);
            var rating = ParseChunk(ref span, ref scanned, ref position);
            var hasMultiplayer = ParseChunk(ref span, ref scanned, ref position);
            
            return new Videogame
            {
                Id = Guid.Parse(id),
                Name = name.ToString(),
                Genre = (Genres)int.Parse(genre),
                ReleaseDate = DateTime.ParseExact(releaseDate, "yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo),
                Rating = int.Parse(rating),
                HasMultiplayer = bool.Parse(hasMultiplayer) 
            };
        }

        private static ReadOnlySpan<char> ParseChunk(ref ReadOnlySpan<char> span, ref int scanned, ref int position)
        {
            scanned += position + 1;
            
            position = span.Slice(scanned, span.Length - scanned).IndexOf('|');
            if (position < 0)
            {
                position = span.Slice(scanned, span.Length - scanned).Length;
            }
            
            return span.Slice(scanned, position);
        }
    }
}