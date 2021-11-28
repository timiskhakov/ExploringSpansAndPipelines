using System.IO;
using System.Threading.Tasks;
using ExploringSpansAndPipelines.Parsers;
using ExploringSpansAndPipelines.Tests.Data;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExploringSpansAndPipelines.Tests.FileParserTests
{
    [TestClass]
    public class WhenParsingValidFile
    {
        private string _file = null!;
        private CompareLogic _compareLogic = null!;
        
        [TestInitialize]
        public void Setup()
        {
            var current = Directory.GetCurrentDirectory();
            _file = Path.Combine(current, "Assets", "TestData.psv");
            _compareLogic = new CompareLogic();
        }
        
        [TestMethod]
        public async Task UsingLineParser_ShouldReturnVideogames()
        {
            var fileParser = new FileParser(new LineParser());

            var videogames = await fileParser.Parse(_file);

            var result = _compareLogic.Compare(TestData.Videogames, videogames.ToArray());
            Assert.IsTrue(result.AreEqual);
        }
        
        [TestMethod]
        public async Task UsingLineParserSpans_ShouldReturnVideogames()
        {
            var fileParser = new FileParser(new LineParserSpans());

            var videogames = await fileParser.Parse(_file);

            var result = _compareLogic.Compare(TestData.Videogames, videogames.ToArray());
            Assert.IsTrue(result.AreEqual);
        }
    }
}