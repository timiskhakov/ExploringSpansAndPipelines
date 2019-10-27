using BenchmarkDotNet.Attributes;
using ExploringSpansAndIOPipelines.Core.Interfaces;
using ExploringSpansAndIOPipelines.Core.Parsers;

namespace ExploringSpansAndIOPipelines.Benchmarks.Comparisons
{
    [MemoryDiagnoser]
    public class LineParsersComparison
    {
        private const string Line = "38e27dea-1d7d-4279-be97-e29d53a8af89|F.E.A.R.|4|2005-10-18|90|False";
        
        private ILineParser _lineParser;
        private ILineParser _lineParserSpans;

        [GlobalSetup]
        public void Setup()
        {
            _lineParser = new LineParser();
            _lineParserSpans = new LineParserSpans();
        }
        
        [Benchmark]
        public void LineParser()
        {
            _lineParser.Parse(Line);
        }

        [Benchmark]
        public void LineParserSpans()
        {
            _lineParserSpans.Parse(Line);
        }
    }
}