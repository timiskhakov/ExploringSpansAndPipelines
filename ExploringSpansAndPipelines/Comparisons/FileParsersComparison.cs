using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using ExploringSpansAndPipelines.Interfaces;
using ExploringSpansAndPipelines.Parsers;

namespace ExploringSpansAndPipelines.Comparisons
{
    [MemoryDiagnoser]
    public class FileParsersComparison
    {
        private readonly Consumer _consumer = new();
        private string _file = null!;
        private IFileParser _fileParser = null!;
        private IFileParser _fileParserSpans = null!;
        private IFileParser _fileParserSpansAndPipes = null!;
        private IFileParser _fileParserImproved = null!;

        [GlobalSetup]
        public void Setup()
        {
            var current = Directory.GetCurrentDirectory();
            _file = Path.Combine(current, "Assets", "BenchmarkData.psv");
            
            _fileParser = new FileParser(new LineParser());
            _fileParserSpans = new FileParser(new LineParserSpans());
            _fileParserSpansAndPipes = new FileParserSpansAndPipelines();
            _fileParserImproved = new FileParserImproved();
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
        
        [Benchmark]
        public async Task FileParserSpansAndPipes()
        {
            (await _fileParserSpansAndPipes.Parse(_file)).Consume(_consumer);
        }
        
        [Benchmark]
        public async Task FileParserImproved()
        {
            (await _fileParserImproved.Parse(_file)).Consume(_consumer);
        }
    }
}