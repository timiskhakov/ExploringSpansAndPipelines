using System.IO;
using System.Threading.Tasks;
using ExploringSpansAndIOPipelines.Core.Parsers;
using ExploringSpansAndIOPipelines.Core.Tests.Comparers;
using ExploringSpansAndIOPipelines.Core.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExploringSpansAndIOPipelines.Core.Tests.FileParserTests
{
    [TestClass]
    public class WhenParsingValidFile
    {
        private string _file;
        
        [TestInitialize]
        public void Setup()
        {
            var current = Directory.GetCurrentDirectory();
            _file = Path.Combine(current, "Assets", "TestData.psv");
        }
        
        [TestMethod]
        public async Task UsingLineParser_ShouldReturnVideogames()
        {
            // Arrange
            var lineParser = new LineParser();
            var fileParser = new FileParser(lineParser);

            // Act
            var videogames = await fileParser.ParseFile(_file);

            // Assert
            CollectionAssert.AreEqual(TestData.Videogames, videogames, new RecursiveComparer());
        }
        
        [TestMethod]
        public async Task UsingLineParserSpans_ShouldReturnVideogames()
        {
            // Arrange
            var lineParserSpans = new LineParserSpans();
            var fileParser = new FileParser(lineParserSpans);

            // Act
            var videogames = await fileParser.ParseFile(_file);

            // Assert
            CollectionAssert.AreEqual(TestData.Videogames, videogames, new RecursiveComparer());
        }
    }
}