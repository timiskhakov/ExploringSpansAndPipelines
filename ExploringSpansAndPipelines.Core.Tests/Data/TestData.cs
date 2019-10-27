using System;
using System.Collections.Generic;
using ExploringSpansAndIOPipelines.Core.Models;

namespace ExploringSpansAndIOPipelines.Core.Tests.Data
{
    internal static class TestData
    {
        internal static readonly Videogame[] Videogames = 
        {
            new Videogame
            {
                Id = Guid.Parse("38e27dea-1d7d-4279-be97-e29d53a8af89"),
                Name = "F.E.A.R.",
                Genre = Genres.Shooter,
                ReleaseDate = new DateTime(2005, 10, 18),
                Rating = 90,
                HasMultiplayer = false
            },
            new Videogame
            {
                Id = Guid.Parse("6f3e9012-5d8c-43c4-b0d0-894fbff5a521"),
                Name = "Football Manager 2020",
                Genre = Genres.Sports,
                ReleaseDate = new DateTime(2019, 11, 19),
                Rating = 80,
                HasMultiplayer = true
            },
            new Videogame
            {
                Id = Guid.Parse("d79bbb41-f66a-46e9-b4d3-72295cca8324"),
                Name = "The Witcher 3: Wild Hunt",
                Genre = Genres.Rpg,
                ReleaseDate = new DateTime(2015, 05, 19),
                Rating = 95,
                HasMultiplayer = false
            }
        };
    }
}