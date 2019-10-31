using ExploringSpansAndIOPipelines.Core.Models;
using ExploringSpansAndIOPipelines.Core.Parsers;
using ExploringSpansAndIOPipelines.Core.Tests.Comparers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExploringSpansAndIOPipelines.Core.Tests.FileParserSpansAndPipesTests
{
    [TestClass]
    public class WhenParsingValidFileWithLongLine
    {
        private const string TempPsvName = "temp.psv";

        private string _file;
        private Videogame[] _expected;

        [TestInitialize]
        public async Task Setup()
        {
            var current = Directory.GetCurrentDirectory();
            var longLineFile = Path.Combine(current, "Assets", "really-long-line.txt");
            var longLine = await File.ReadAllTextAsync(longLineFile);

            _file = Path.Combine(current, TempPsvName);
            _expected = CreateExpected(longLine);

            await CreateFile(_file, _expected);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DeleteFile(_file);
        }

        [TestMethod]
        public async Task ShouldReturnVideogames()
        {
            // Arrange
            var fileParserSpansAndPipes = new FileParserSpansAndPipelines();

            // Act
            var videogames = await fileParserSpansAndPipes.Parse(_file);

            // Assert
            CollectionAssert.AreEqual(_expected, videogames, new RecursiveComparer());
        }

        private Videogame[] CreateExpected(string longLine) => new[]
        {
            new Videogame
            {
                Id = Guid.Parse("385562C2-00E0-4B45-8A99-0B54E0BC9299"),
                Name = longLine,
                Genre = Genres.Action,
                ReleaseDate = new DateTime(2010, 1, 1),
                Rating = 50,
                HasMultiplayer = false
            },
            new Videogame
            {
                Id = Guid.Parse("385562C2-00E0-4B45-8A99-0B54E0BC9299"),
                Name = "Another test name",
                Genre = Genres.Adventure,
                ReleaseDate = new DateTime(2015, 10, 10),
                Rating = 40,
                HasMultiplayer = true
            },
        };

        private async Task CreateFile(string file, IEnumerable<Videogame> videogames)
        {
            var content = new StringBuilder();
            foreach (var videogame in videogames)
            {
                content.Append($"{videogame}\n");
            }

            await File.WriteAllTextAsync(file, content.ToString());
        }

        private void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}
