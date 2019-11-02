using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;
using ExploringSpansAndIOPipelines.Core.Interfaces;
using ExploringSpansAndIOPipelines.Core.Models;

namespace ExploringSpansAndIOPipelines.Core.Parsers
{
    public class FileParserSpansAndPipelines : IFileParser
    {
        private const int LengthLimit = 256;
        
        public async Task<List<Videogame>> Parse(string file)
        {
            var result = new List<Videogame>();

            using (var stream = File.OpenRead(file))
            {
                var reader = PipeReader.Create(stream);
                while (true)
                {
                    var read = await reader.ReadAsync();
                    var buffer = read.Buffer;
                    while (TryReadLine(ref buffer, out var sequence))
                    {
                        var videogame = ProcessSequence(sequence);
                        result.Add(videogame);
                    }
                    
                    reader.AdvanceTo(buffer.Start, buffer.End);
                    if (read.IsCompleted)
                    {
                        break;
                    }
                }
            }

            return result;
        }
        
        private static bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
        {
            var position = buffer.PositionOf((byte)'\n');
            if (position == null)
            {
                line = default;
                return false;
            }
            
            line = buffer.Slice(0, position.Value);
            buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
            
            return true;
        }
        
        private static Videogame ProcessSequence(ReadOnlySequence<byte> sequence)
        {
            if (sequence.IsSingleSegment)
            {
                return Parse(sequence.FirstSpan);
            }

            var length = (int) sequence.Length;
            if (length > LengthLimit)
            {
                throw new ArgumentException($"Line has a length exceeding the limit: {length}");
            }
            
            Span<byte> span = stackalloc byte[length];
            sequence.CopyTo(span);

            return Parse(span);
        }

        private static Videogame Parse(ReadOnlySpan<byte> bytes)
        {
            Span<char> chars = stackalloc char[bytes.Length];
            Encoding.UTF8.GetChars(bytes, chars);
                
            return LineParserSpans.Parse(chars);
        }
    }
}