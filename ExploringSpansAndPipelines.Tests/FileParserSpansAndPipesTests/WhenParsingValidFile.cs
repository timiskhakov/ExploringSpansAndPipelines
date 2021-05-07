using System.IO;
using System.Threading.Tasks;
using ExploringSpansAndPipelines.Parsers;
using ExploringSpansAndPipelines.Tests.Comparers;
using ExploringSpansAndPipelines.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExploringSpansAndPipelines.Tests.FileParserSpansAndPipesTests
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
        public async Task ShouldReturnVideogames()
        {
            // Arrange
            var fileParserSpansAndPipes = new FileParserSpansAndPipelines();

            // Act
            var videogames = await fileParserSpansAndPipes.Parse(_file);

            // Assert
            CollectionAssert.AreEqual(TestData.Videogames, videogames, new RecursiveComparer());
        }
    }
}