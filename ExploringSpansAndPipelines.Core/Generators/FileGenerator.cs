using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExploringSpansAndIOPipelines.Core.Models;

namespace ExploringSpansAndIOPipelines.Core.Generators
{
    public static class FileGenerator
    {
        private static readonly Random Random = new Random();
        private static readonly char[] AllowedChars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,-:".ToCharArray();
        private static readonly Array Genres = Enum.GetValues(typeof(Genres));
        private static readonly DateTime MinReleaseDate = new DateTime(1990, 1, 1);
        private static readonly DateTime MaxReleaseDate = new DateTime(2020, 1, 1);
        
        public static async Task Generate(string file, int numberOfLines)
        {
            await File.WriteAllLinesAsync(file, CreateContent(numberOfLines), Encoding.UTF8);
        }

        private static IEnumerable<string> CreateContent(int numberOfLines)
        {
            for (var i = 0; i < numberOfLines; i++)
            {
                yield return new Videogame
                {
                    Id = Guid.NewGuid(),
                    Name = GetRandomName(),
                    Genre = GetRandomGenre(),
                    ReleaseDate = GetRandomDate(),
                    Rating = Random.Next(100),
                    HasMultiplayer = Random.Next(2) == 0
                }.ToString();
            }
        }

        private static string GetRandomName()
        {
            var words = Enumerable.Range(1, Random.Next(1, 5)).Select(_ => CreateRandomWord());

            return string.Join(' ', words);
        }

        private static string CreateRandomWord() =>
            new string(Enumerable
                .Repeat(AllowedChars, Random.Next(5, 15))
                .Select(x => x[Random.Next(x.Length)])
                .ToArray());
        
        private static Genres GetRandomGenre() => (Genres)Genres.GetValue(Random.Next(Genres.Length));

        private static DateTime GetRandomDate()
        {
            var period = MaxReleaseDate - MinReleaseDate;
            var random = new TimeSpan(0, Random.Next(0, (int)period.TotalMinutes), 0);
            
            return MinReleaseDate + random;
        }
    }
}