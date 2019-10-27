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
    public class FileParserSpansAndPipes : IFileParser
    {
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
                        var videogames = ProcessSequence(sequence);
                        result.AddRange(videogames);
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
        
        private static IEnumerable<Videogame> ProcessSequence(ReadOnlySequence<byte> sequence)
        {
            if (!sequence.IsSingleSegment)
            {
                yield break;
            }

            #if DEBUG
            Span<char> span = new char[sequence.FirstSpan.Length];
            #else
            Span<char> span = stackalloc char[sequence.FirstSpan.Length];
            #endif
            
            Encoding.UTF8.GetChars(sequence.FirstSpan, span);
                
            yield return LineParserSpans.Parse(span);
        }
    }
}