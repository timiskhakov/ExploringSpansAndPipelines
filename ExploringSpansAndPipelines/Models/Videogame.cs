using System;

namespace ExploringSpansAndPipelines.Benchmarks.Models
{
    public class Videogame
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Genres Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public bool HasMultiplayer { get; set; }

        public override string ToString()
        {
            return $"{Id}|{Name}|{(int)Genre}|{ReleaseDate:yyyy-MM-dd}|{Rating}|{HasMultiplayer}";
        }
    }
}