using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Advent_of_Code_2022
{
    internal class Day9 : ISolver
    {
        public string Title => "Rope Bridge";

        public string PartOne(string input) => Solve(input, 2);
        public string PartTwo(string input) => Solve(input, 10);

        private static string Solve(string input, int knots)
        {
            var visited = new List<Point>();
            var moves = input.Split("\n");

            var rope = new Point[knots];           

            foreach (var move in moves)
            {
                var split = move.Split(' ');
                var direction = split[0].Trim();
                var steps = int.Parse(split[1].Trim());
                for (int i = 0; i < steps; i++)
                {
                    // Move the head
                    switch (direction)
                    {
                        case "R":
                            rope[0].X++;
                            break;
                        case "L":
                            rope[0].X--;
                            break;
                        case "U":
                            rope[0].Y--;
                            break;
                        case "D":
                            rope[0].Y++;
                            break;
                    }
                    // Move all the knots
                    for (int j = 1; j < rope.Length; j++)
                    {
                        // Work out how far away the next knot is
                        var x = rope[j - 1].X - rope[j].X;
                        var y = rope[j - 1].Y - rope[j].Y;
                        // Only move if we are at least 1 square away
                        if (Math.Abs(x) > 1 || Math.Abs(y) > 1)
                        {
                            // We only want to move 1 square at a time
                            // Math.Sign(...) returns 0 if the value is 0
                            rope[j].X += Math.Sign(x);
                            rope[j].Y += Math.Sign(y);
                        }
                        visited.Add(rope[^1]); // Add the tail
                    }
                }
            }
            return visited.Distinct().Count().ToString();
        }        
    }
}
