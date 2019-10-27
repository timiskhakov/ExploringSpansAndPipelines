using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using ExploringSpansAndIOPipelines.Core.Parsers;

namespace ExploringSpansAndIOPipelines.Benchmarks.Comparisions
{
    [MemoryDiagnoser]
    public class FileParsersComparision
    {
        private string _file;
        private FileParser _fileParser;
        private FileParser _fileParserSpans;
        private Consumer _consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            var current = Directory.GetCurrentDirectory();
            _file = Path.Combine(current, "Assets", "BenchmarkData.psv");
            
            _fileParser = new FileParser(new LineParser());
            _fileParserSpans = new FileParser(new LineParserSpans());
        }
        
        [Benchmark]
        public async Task FileParser()
        {
            (await _fileParser.Parse(_file)).Consume(_consumer);
        }

        [Benchmark]
        public async Task FileParserSpans()
        {
            (await _fileParserSpans.Parse(_file)).Consume(_consumer);
        }
    }
}