using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDartsGame
{
    public class Game
    {
        public List<Player> Players { get; set; }

        private int rndIndex;
        private int rndPoints;
        private int rndRingBonus;
        private int actualThrowScore;
        private readonly int startingScore;
        private bool isDoubleBonusOrDoublePointsInSerie = false;

        public bool DoubleBonusOrDoublePointsInSerie
        {
            get { return isDoubleBonusOrDoublePointsInSerie; }
            set { isDoubleBonusOrDoublePointsInSerie = value; }
        }

        readonly Random random = new Random();

        private readonly int[] DartsPoints = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 19, 20, 25, 50 };
        private readonly int[] BonusRing = new int[] { 1, 2, 3, 2, 3, 2, 3, 1 };
        //private readonly int[] WinScores = new int[3];

        public Game(int startingScore, List<Player> players)
        {
            this.Players = players;
            this.startingScore = startingScore;
            DoubleBonusOrDoublePointsInSerie = this.isDoubleBonusOrDoublePointsInSerie;
            foreach (var player in Players)
            {
                player.RoundScore = startingScore;
                Console.WriteLine($"Welcome {player.Name}");
            }
        }

        int throwPointsTemp;

        public bool IsActualThrowGivingWin(Player player)
        {
            throwPointsTemp = player.ActualSerie[0];

            if (isDoubleBonusOrDoublePointsInSerie is false)
            {
                if (player.ActualSerie.Count(pointsInPastThrowsInActualSerie => pointsInPastThrowsInActualSerie == throwPointsTemp) >= 2)
                {
                    isDoubleBonusOrDoublePointsInSerie = true;
                    Console.WriteLine("DOUBLE POINTS!");
                }
                else
                {
                    isDoubleBonusOrDoublePointsInSerie = false;
                }
            }

            if(player.RoundScore - player.ActualSerie.Sum() == 0 && isDoubleBonusOrDoublePointsInSerie is true)
            {
                Console.WriteLine($"WINNER! {player.Name}");
                return true;
            }

            return false;
        }

        // one dart throw in a serie
        public void ThrowRandomPointsWithRandomRingFor(Player player)
        {
            Console.Write($"\n{player.Name} Throw\n");
            rndRingBonus = 1;
            rndIndex = random.Next(0, DartsPoints.Length);
            rndPoints = DartsPoints[rndIndex];

            if (rndPoints < 0)
            {
                throw new ArgumentOutOfRangeException(rndPoints.GetType().Name, "Error cuz Random Points behavior and range.");
            }

            if (rndPoints != 0)
            {
                // If it's not a center field and ring (25 and 50)
                if (rndPoints <= 20)
                {
                    rndIndex = random.Next(0, BonusRing.Length);
                    rndRingBonus = BonusRing[rndIndex];

                    switch (rndRingBonus)
                    {
                        case 1:
                            actualThrowScore = rndPoints;
                            break;

                        case 2:
                            actualThrowScore = rndRingBonus * rndPoints;
                            isDoubleBonusOrDoublePointsInSerie = true;
                            Console.WriteLine($"{player.Name} DOUBLE BONUS!");
                            break;

                        case 3:
                            actualThrowScore = rndRingBonus * rndPoints;
                            Console.WriteLine($"{player.Name} TRIPLE BONUS!");
                            break;
                    }

                    player.SerieScore += actualThrowScore;
                }
                else
                {
                    actualThrowScore = rndPoints;
                    player.SerieScore += actualThrowScore;
                }
            }
            else
            {
                actualThrowScore = 0;
            }

            player.ActualSerie.Add(actualThrowScore);
            Console.WriteLine($"Actual Throw Points: {actualThrowScore}");
        }

        public void FinishSerieFor(Player player)
        {
            player.SeriesArchive.Add(player.ActualSerie);
            if (player.RoundScore - player.ActualSerie.Sum() > 0)
            {
                player.RoundScore -= player.ActualSerie.Sum();
            }

            if (player.RoundScore - player.ActualSerie.Sum() == 0 && isDoubleBonusOrDoublePointsInSerie is true)
            {
                player.RoundScore -= actualThrowScore;
            }

            Console.WriteLine($"{player.Name} Score: {player.RoundScore}\n");
            player.ActualSerie = new();
            player.SerieScore = 0;
        }

        public void StartNewRound()
        {
            foreach (var player in Players)
            {
                player.ActualSerie = new();
                player.SerieScore = 0;
                player.RoundScore = startingScore;
                isDoubleBonusOrDoublePointsInSerie = false;
            }
            Console.WriteLine("\nNew Round\n");

        }

        //public void FinishRound(int RoundIndex, int winnerId)
        //{
        //    if (Players[0].ActualSerie.Count != Players[1].ActualSerie.Count)
        //    {
        //            Players[1].ActualSerie.Add(0);
        //            Players[1].ActualSerie.Add(0);
        //            Players[1].ActualSerie.Add(0);
        //    }

        //    WinScores[RoundIndex] = winnerId;
        //}
    }
}
