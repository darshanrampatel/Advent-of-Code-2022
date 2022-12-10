using System.Text;

namespace Advent_of_Code_2022
{
    internal class Day10 : ISolver
    {
        public string Title => "Cathode-Ray Tube";

        public string PartOne(string input)
        {
            var (signalStrength, _) = Solve(input);
            return signalStrength.ToString();
        }
        public string PartTwo(string input)
        {
            var (_, registerHistory) = Solve(input);

            // Draw CRT
            var wide = 40;
            var high = 6;
            var sb = new StringBuilder();
            var padding = new string(' ', $"{nameof(PartTwo)}: ".Length);
            for (int y = 0; y < high; y++)
            {
                for (int x = 0; x < wide; x++)
                {
                    var c = (y * wide) + (x + 1);
                    var registerValue = GetXRegister(registerHistory, c - 1);
                    var sprite = new List<int>() { registerValue - 1, registerValue, registerValue + 1 };
                    var pixel = sprite.Contains(x) ? "#" : ".";
                    sb.Append(pixel);
                }
                sb.AppendLine();
                sb.Append(padding); // Add padding at the beginning of the line
            }
            return sb.ToString();
        }

        static int GetXRegister(Dictionary<int, int> registerHistory, int upToCycle = 240) => registerHistory.Where(r => r.Key <= upToCycle).Sum(r => r.Value);

        private static (int, Dictionary<int, int>) Solve(string input)
        {
            var signalStrength = 0;
            var registerHistory = new Dictionary<int, int>()
            {
                [0] = 1 // Starting value
            };

            var cyclesToCount = new List<int>() { 20, 60, 100, 140, 180, 220 };

            void AddSignalStrength(int cycle)
            {
                if (cyclesToCount.Contains(cycle))
                {
                    var s = cycle * GetXRegister(registerHistory);
                    signalStrength += s;
                }
            }

            var cycle = 0;
            var instructions = input.Split("\n", StringSplitOptions.TrimEntries);
            foreach (var instruction in instructions)
            {
                cycle++;
                if (instruction == "noop")
                {
                    // Do nothing
                    AddSignalStrength(cycle);
                }
                else if (instruction.StartsWith("addx"))
                {
                    var v = int.Parse(instruction.Split(" ", StringSplitOptions.TrimEntries)[1]);
                    // Wait one cycle
                    AddSignalStrength(cycle);
                    cycle++;
                    AddSignalStrength(cycle);
                    registerHistory[cycle] = v;
                }
            }
            return (signalStrength, registerHistory);
        }
    }
}
