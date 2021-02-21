using ExploringSpansAndIOPipelines.Core.Interfaces;
using ExploringSpansAndIOPipelines.Core.Parsers;
using ExploringSpansAndIOPipelines.Core.Tests.Comparers;
using ExploringSpansAndIOPipelines.Core.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExploringSpansAndIOPipelines.Core.Tests.LineParserTests
{
    [TestClass]
    public class WhenParsingValidLine
    {
        private ILineParser _lineParser;
        
        [TestInitialize]
        public void Setup()
        {
            _lineParser = new LineParser();
        }
        
        [DataTestMethod]
        [DataRow("38e27dea-1d7d-4279-be97-e29d53a8af89|F.E.A.R.|4|2005-10-18|90|False", 0)]
        [DataRow("6f3e9012-5d8c-43c4-b0d0-894fbff5a521|Football Manager 2020|5|2019-11-19|80|True", 1)]
        [DataRow("d79bbb41-f66a-46e9-b4d3-72295cca8324|The Witcher 3: Wild Hunt|3|2015-05-19|95|False", 2)]
        public void ShouldReturnVideogame(string line, int index)
        {
            // Arrange
            var expected = TestData.Videogames[index];
            
            // Act
            var actual = _lineParser.Parse(line);

            // Assert
            CollectionAssert.AreEqual(new[] { expected }, new[] { actual }, new RecursiveComparer());
        }
    }
}