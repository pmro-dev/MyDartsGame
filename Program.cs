// See https://aka.ms/new-console-template for more information
using MyDartsGame;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        List<Player> players = new List<Player>() {
            new Player(name: "Josh", country: "USA"),
            new Player(name: "William", country: "UK")
        };

        Game dartGame = new Game(501, players);

        dartGame.StartNewRound();
        do
        {
            dartGame.DoubleBonusOrDoublePointsInSerie = false;
            dartGame.ThrowRandomPointsWithRandomRingFor(players[0]);
            if (dartGame.IsActualThrowGivingWin(players[0])) {
                dartGame.FinishSerieFor(players[0]);
                break;
            }
            dartGame.DoubleBonusOrDoublePointsInSerie = false;
            dartGame.ThrowRandomPointsWithRandomRingFor(players[0]);
            if (dartGame.IsActualThrowGivingWin(players[0])){
                dartGame.FinishSerieFor(players[0]);
                break;
            }
            dartGame.DoubleBonusOrDoublePointsInSerie = false;
            dartGame.ThrowRandomPointsWithRandomRingFor(players[0]);
            if (dartGame.IsActualThrowGivingWin(players[0])) {
                dartGame.FinishSerieFor(players[0]);
                break;
            }
            dartGame.FinishSerieFor(players[0]);

            dartGame.DoubleBonusOrDoublePointsInSerie = false;
            dartGame.ThrowRandomPointsWithRandomRingFor(players[1]);
            if (dartGame.IsActualThrowGivingWin(players[1])) {
                dartGame.FinishSerieFor(players[1]);
                break;
            }
            dartGame.DoubleBonusOrDoublePointsInSerie = false;
            dartGame.ThrowRandomPointsWithRandomRingFor(players[1]);
            if (dartGame.IsActualThrowGivingWin(players[1])) {
                dartGame.FinishSerieFor(players[1]);
                break;
            }
            dartGame.DoubleBonusOrDoublePointsInSerie = false;
            dartGame.ThrowRandomPointsWithRandomRingFor(players[1]);
            if (dartGame.IsActualThrowGivingWin(players[1])) {
                dartGame.FinishSerieFor(players[1]);
                break;
            }
            dartGame.FinishSerieFor(players[1]);

            //if (dartGame.IsWinner(players[1])) {
            //    break;
            //}

        } while (true);
        //} while (dartGame.IsWinner(players[0]) is false || dartGame.IsWinner(players[1]) is false);


        foreach (var player in dartGame.Players)
        {
            Console.WriteLine($"Name: {player.Name}, Actual Round Score: {player.RoundScore}");
            Console.WriteLine();
            int i = 1;
            int y = 1;
            foreach (var serie in player.SeriesArchive)
            {
                Console.WriteLine($"Serie {i}");
                i++;
                foreach (var throwScore in serie)
                {
                    Console.Write($"Throw {y} -> ");
                    Console.Write($" {throwScore} ");
                    y++;
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}