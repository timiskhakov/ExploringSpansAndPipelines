using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ExploringSpansAndIOPipelines.Core.Models;
using ExploringSpansAndIOPipelines.Core.Parsers;
using ExploringSpansAndIOPipelines.Core.Tests.Comparers;
using ExploringSpansAndIOPipelines.Core.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExploringSpansAndIOPipelines.Core.Tests.FileParserTests
{
    [TestClass]
    public class FileParserImprovedTests
    {
        [TestMethod]
        public async Task Parse_RegularLines()
        {
            // Arrange
            var file = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "TestData.psv");
            var fileParser = new FileParserImproved();

            // Act
            var videogames = await fileParser.Parse(file);

            // Assert
            CollectionAssert.AreEqual(TestData.Videogames, videogames, new RecursiveComparer());
        }
        
        [TestMethod]
        public async Task Parse_LongLine()
        {
            // Arrange
            var current = Directory.GetCurrentDirectory();
            var longLineFile = Path.Combine(current, "Assets", "really-long-line.txt");
            var longLine = await File.ReadAllTextAsync(longLineFile);

            var file = Path.Combine(current, "temp.psv");
            var expected = CreateExpected(longLine);
            await CreateFile(file, expected);
            
            var fileParser = new FileParserImproved();

            // Act
            var videogames = await fileParser.Parse(file);
            DeleteFile(file);

            // Assert
            CollectionAssert.AreEqual(expected, videogames, new RecursiveComparer());
        }
        
        private static Videogame[] CreateExpected(string longLine)
        {
            return new[]
            {
                new Videogame
                {
                    Id = Guid.Parse("385562C2-00E0-4B45-8A99-0B54E0BC9299"),
                    Name = longLine,
                    Genre = Genres.Action,
                    ReleaseDate = new DateTime(2010, 1, 1),
                    Rating = 50,
                    HasMultiplayer = false
                }
            };
        }

        private static async Task CreateFile(string file, IEnumerable<Videogame> videogames)
        {
            var content = new StringBuilder();
            foreach (var videogame in videogames)
            {
                content.Append($"{videogame}\n");
            }

            await File.WriteAllTextAsync(file, content.ToString());
        }

        private static void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}