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
            var fileParser = new FileParser(new LineParser());

            // Act
            var videogames = await fileParser.Parse(_file);

            // Assert
            CollectionAssert.AreEqual(TestData.Videogames, videogames, new RecursiveComparer());
        }
        
        [TestMethod]
        public async Task UsingLineParserSpans_ShouldReturnVideogames()
        {
            // Arrange
            var fileParser = new FileParser(new LineParserSpans());

            // Act
            var videogames = await fileParser.Parse(_file);

            // Assert
            CollectionAssert.AreEqual(TestData.Videogames, videogames, new RecursiveComparer());
        }
    }
}