using System;
using System.Buffers.Text;
using System.Text;
using ExploringSpansAndIOPipelines.Core.Models;

namespace ExploringSpansAndIOPipelines.Core.Parsers
{
    public static class LineParserImproved
    {
        public static Videogame Parse(ReadOnlySpan<byte> bytes)
        {
            if (!Utf8Parser.TryParse(bytes, out Guid id, out var idConsumed)) throw new ArgumentException(nameof(bytes));
            bytes = bytes.Slice(idConsumed + 1);
            
            var namePosition = bytes.IndexOf((byte)'|');
            var name = Encoding.UTF8.GetString(bytes.Slice(0, namePosition));
            bytes = bytes.Slice(namePosition + 1);
            
            if (!Utf8Parser.TryParse(bytes, out int genre, out var genreConsumed)) throw new ArgumentException(nameof(bytes));
            bytes = bytes.Slice(genreConsumed + 1);

            var releaseDatePosition = bytes.IndexOf((byte) '|');
            var releaseDate = ParseDateTime(bytes.Slice(0, releaseDatePosition));
            bytes = bytes.Slice(releaseDatePosition + 1);

            if (!Utf8Parser.TryParse(bytes, out int rating, out var ratingConsumed)) throw new ArgumentException(nameof(bytes));
            bytes = bytes.Slice(ratingConsumed + 1);
            
            if (!Utf8Parser.TryParse(bytes, out bool hasMultiplayer, out _)) throw new ArgumentException(nameof(bytes));
            
            return new Videogame
            {
                Id = id,
                Name = name,
                Genre = (Genres) genre,
                ReleaseDate = releaseDate,
                Rating = rating,
                HasMultiplayer = hasMultiplayer
            };
        }

        // Borrowed from here:
        // https://github.com/dotnet/runtime/blob/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.Private.CoreLib/src/System/Buffers/Text/Utf8Parser/Utf8Parser.Date.O.cs
        // TODO: Add proper validation
        private static DateTime ParseDateTime(ReadOnlySpan<byte> source)
        {
            var digit1 = source[0] - 48u; // '0'
            var digit2 = source[1] - 48u; // '0'
            var digit3 = source[2] - 48u; // '0'
            var digit4 = source[3] - 48u; // '0'

            var year = 1000 * digit1 + 100 * digit2 + 10 * digit3 + digit4;
            
            var digit5 = source[5] - 48u; // '0'
            var digit6 = source[6] - 48u; // '0'

            var month = 10 * digit5 + digit6;
            
            var digit8 = source[8] - 48u; // '0'
            var digit9 = source[9] - 48u; // '0'
            
            var day = 10 * digit8 + digit9;
            
            return new DateTime((int) year, (int) month, (int) day);
        }
    }
}