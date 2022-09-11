using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDartsGame
{
    public class Player
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public int? RoundScore { get; set; }
        public int SerieScore { get; set; }
        public List<int> ActualSerie{ get; set; }
        public List<List<int>> SeriesArchive { get; set; }

        public Player(string name, string country)
        {
            this.Name = name;
            this.Country = country;
            this.SerieScore = 0;

            ActualSerie = new();
            SeriesArchive = new();
        }
    }
}
