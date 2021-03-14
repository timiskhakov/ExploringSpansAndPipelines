using System;
using System.Buffers.Text;
using System.Text;
using ExploringSpansAndPipelines.Models;

namespace ExploringSpansAndPipelines.Parsers
{
    public static class LineParserImproved
    {
        public static Videogame Parse(ReadOnlySpan<byte> bytes)
        {
            return new Videogame
            {
                Id = ParseGuid(ref bytes),
                Name = ParseString(ref bytes),
                Genre = (Genres) ParseInt(ref bytes),
                ReleaseDate = ParseDateTime(ref bytes),
                Rating = ParseInt(ref bytes),
                HasMultiplayer = ParseBool(ref bytes)
            };
        }

        private static Guid ParseGuid(ref ReadOnlySpan<byte> bytes)
        {
            if (!Utf8Parser.TryParse(bytes, out Guid value, out var consumed)) throw new ArgumentException(nameof(bytes));
            var advance = consumed + 1;
            if (bytes.Length >= advance) bytes = bytes.Slice(advance);
            return value;
        }

        private static string ParseString(ref ReadOnlySpan<byte> bytes)
        {
            var position = bytes.IndexOf((byte) '|');
            var value = Encoding.UTF8.GetString(bytes.Slice(0, position));
            var advance = position + 1;
            if (bytes.Length >= advance) bytes = bytes.Slice(advance);
            return value;
        }

        private static int ParseInt(ref ReadOnlySpan<byte> bytes)
        {
            if (!Utf8Parser.TryParse(bytes, out int value, out var consumed)) throw new ArgumentException(nameof(bytes));
            var advance = consumed + 1;
            if (bytes.Length >= advance) bytes = bytes.Slice(advance);
            return value;
        }

        private static DateTime ParseDateTime(ref ReadOnlySpan<byte> bytes)
        {
            if (!TryParseExactDateTime(bytes, out var dateTime, out var consumed)) throw new ArgumentException(nameof(bytes));
            var advance = consumed + 1;
            if (bytes.Length >= advance) bytes = bytes.Slice(advance);
            return dateTime;
        }

        private static bool ParseBool(ref ReadOnlySpan<byte> bytes)
        {
            if (!Utf8Parser.TryParse(bytes, out bool value, out var consumed)) throw new ArgumentException(nameof(bytes));
            var advance = consumed + 1;
            if (bytes.Length >= advance) bytes = bytes.Slice(advance);
            return value;
        }

        // Borrowed from here:
        // https://github.com/dotnet/runtime/blob/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.Private.CoreLib/src/System/Buffers/Text/Utf8Parser/Utf8Parser.Date.O.cs
        private static bool TryParseExactDateTime(in ReadOnlySpan<byte> bytes, out DateTime value, out int consumed)
        {
            value = default;
            consumed = 0;
            
            if (bytes.Length < 10) return false;

            var digit1 = bytes[0] - 48u; // 48u == '0'
            var digit2 = bytes[1] - 48u;
            var digit3 = bytes[2] - 48u;
            var digit4 = bytes[3] - 48u;
            if (digit1 > 9 || digit2 > 9 || digit3 > 9 || digit4 > 9) return false;

            var year = 1000 * digit1 + 100 * digit2 + 10 * digit3 + digit4;
            
            if (bytes[4] != (byte)'-') return false;

            var digit5 = bytes[5] - 48u;
            var digit6 = bytes[6] - 48u;
            if (digit5 > 9 || digit6 > 9) return false;

            var month = 10 * digit5 + digit6;
            
            if (bytes[7] != (byte)'-') return false;
            
            var digit8 = bytes[8] - 48u;
            var digit9 = bytes[9] - 48u;
            
            var day = 10 * digit8 + digit9;
            if (digit8 > 9 || digit9 > 9) return false;
            
            value = new DateTime((int) year, (int) month, (int) day, 0, 0, 0, DateTimeKind.Utc);
            consumed = 10;

            return true;
        }
    }
}