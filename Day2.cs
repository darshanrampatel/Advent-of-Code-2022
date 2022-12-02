using System.ComponentModel;
using System.Numerics;

namespace Advent_of_Code_2022
{
    internal class Day2 : ISolver
    {
        public string Title => "Rock Paper Scissors";

        public string PartOne(string input)
        {
            var lines = input.Split('\n');
            return lines.Sum(CalculateScorePartOne).ToString();
        }

        private static int CalculateScorePartOne(string game)
        {
            var turns = game.Split(" ", StringSplitOptions.TrimEntries);
            var opponent = turns[0];
            var you = turns[1];
            return ChosenScore(you) + OutcomeScore(opponent, you);
        }

        private static int ChosenScore(string played) => played switch
        {
            "X" => 1, // Rock
            "Y" => 2, // Paper
            "Z" => 3, // Scissors
            _ => 0,
        };

        private static int OutcomeScore(string opponent, string played) => (opponent, played) switch
        {
            ("A", "X") => 3, // Rock, Rock
            ("B", "X") => 0, // Paper, Rock
            ("C", "X") => 6, // Scissors, Rock
            ("A", "Y") => 6, // Rock, Paper
            ("B", "Y") => 3, // Paper, Paper
            ("C", "Y") => 0, // Scissors, Paper
            ("A", "Z") => 0, // Rock, Scissors
            ("B", "Z") => 6, // Paper, Scissors
            ("C", "Z") => 3, // Scissors, Scissors
            _ => 0,
        };

        public string PartTwo(string input)
        {
            var lines = input.Split('\n');
            return lines.Sum(CalculateScorePartTwo).ToString();
        }

        private static int CalculateScorePartTwo(string game)
        {
            var turns = game.Split(" ", StringSplitOptions.TrimEntries);
            var opponent = turns[0];
            var desiredOutcome = turns[1];
            var shouldPlay = (opponent, desiredOutcome) switch
            {
                ("A", "X") => "Z", // Rock, Lose => Scissors
                ("B", "X") => "X", // Paper, Lose => Rock
                ("C", "X") => "Y", // Scissors, Lose => Paper
                ("A", "Y") => "X", // Rock, Draw => Rock
                ("B", "Y") => "Y", // Paper, Draw => Paper
                ("C", "Y") => "Z", // Scissors, Draw => Scissors
                ("A", "Z") => "Y", // Rock, Win = Paper
                ("B", "Z") => "Z", // Paper, Win => Scissors
                ("C", "Z") => "X", // Scissors, Win => Rock
                _ => "",
            };           
            return ChosenScore(shouldPlay) + OutcomeScore(opponent, shouldPlay);
        }
    }
}
