using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        private enum Move { ROCK, PAPER, SCISSORS }
        private enum Result { LOSE, WIN, TIE }

        private static Random rand = new Random();
        private static Dictionary<Move, Dictionary<Move, Result>> resultMappings;

        static void Main(string[] args)
        {
            resultMappings = new Dictionary<Move, Dictionary<Move, Result>>()
            {
                { Move.ROCK, new Dictionary<Move, Result>()
                {
                    { Move.ROCK, Result.TIE },
                    { Move.PAPER, Result.LOSE },
                    { Move.SCISSORS, Result.WIN }
                }
                },
                { Move.PAPER, new Dictionary<Move, Result>()
                {
                    { Move.ROCK, Result.WIN },
                    { Move.PAPER, Result.TIE },
                    { Move.SCISSORS, Result.LOSE }
                }
                },
                { Move.SCISSORS, new Dictionary<Move, Result>()
                {
                    { Move.ROCK, Result.LOSE },
                    { Move.PAPER, Result.WIN },
                    { Move.SCISSORS, Result.TIE }
                }
                }
            };

            PlayGame(Score.Zero);
        }

        private static void PlayGame(Score score)
        {
            Console.WriteLine("How many rounds would you like to play?");
            int rounds = GetNumber("[1-10]: ", num => { return num >= 1 && num <= 10; });
            Console.WriteLine();

            // play rounds
            for (int i = 0; i < rounds; i++)
            {
                Console.WriteLine($"(Round {i + 1}) Make your move!");
                Move playerMove = (Move)(GetNumber("[1 = Rock, 2 = Paper, 3 = Scissors]: ", num => { return num >= 1 && num <= 3; }) - 1);
                Move computerMove = (Move)rand.Next(3);
                ParseRound(ref score, playerMove, computerMove);
                Console.WriteLine();
            }

            // display results
            Console.WriteLine($"{ ((score.ties > score.player && score.ties > score.computer) || score.player == score.computer ? "Nobody is" : (score.player > score.computer ? "You are" : "The computer is")) } the overall winner!");
            Console.WriteLine($"{"Player:",10} {score.player}");
            Console.WriteLine($"{"Computer:",10} {score.computer}");
            Console.WriteLine($"{"Ties:",10} {score.ties}");
            Console.WriteLine();

            // play again?
            Console.WriteLine("Would you like to play again?");
            int again = GetNumber("[1 = Exit, 2 = Play Again]: ", num => { return num >= 1 && num <= 2; });
            if (again == 2)
            {
                Console.WriteLine();
                Console.WriteLine();
                PlayGame(Score.Zero);
            }
        }

        private static void ParseRound(ref Score score, Move playerMove, Move computerMove)
        {
            Console.WriteLine($"Player chose {playerMove.ToString()}, computer chose {computerMove.ToString()}");
            switch (resultMappings[playerMove][computerMove])
            {
                case Result.WIN:
                    Console.WriteLine("Player wins!");
                    score.player++;
                    break;
                case Result.LOSE:
                    Console.WriteLine("Computer wins!");
                    score.computer++;
                    break;
                case Result.TIE:
                    Console.WriteLine("It's a tie!");
                    score.ties++;
                    break;
            }
            Console.WriteLine();
        }

        private static int GetNumber(string prompt, Func<int, bool> predicate)
        {
            int result;

            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result) || !predicate(result))
            {
                Console.WriteLine("You entered an invalid number!");
                Console.Write(prompt);
            }

            return result;
        }
    }

    struct Score
    {
        public static Score Zero { get { return new Score(0, 0, 0); } }

        public int player;
        public int computer;
        public int ties;

        public Score(int player, int computer, int ties)
        {
            this.player = player;
            this.computer = computer;
            this.ties = ties;
        }
    }
}
