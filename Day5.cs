namespace Advent_of_Code_2022
{
    internal class Day5 : ISolver
    {
        public string Title => "Supply Stacks";

        public string PartOne(string input)
        {
            var (state, moves) = GetInitialState(input);            
            return MoveCrates(state, moves, moveTogether: false);
        }

        public string PartTwo(string input)
        {
            var (state, moves) = GetInitialState(input);            
            return MoveCrates(state, moves, moveTogether: true);
        }

        private (Dictionary<int, List<char>> state, string[] moves) GetInitialState(string input)
        {
            var sections = input.Split($"{Environment.NewLine}{Environment.NewLine}");
            var crates = sections[0].Split('\n');
            var state = new Dictionary<int, List<char>>();
            foreach (var crateLine in crates)
            {
                for (int i = 0; i < crateLine.Length; i++)
                {
                    char c = crateLine[i];
                    if (char.IsLetter(c))
                    {
                        int n = (i / 4) + 1;
                        if (!state.ContainsKey(n))
                        {
                            state[n] = new List<char>();
                        }
                        state[n].Add(c);
                    }
                }
            }

            var moves = sections[1].Split('\n');
            return (state, moves);
        }

        private static string MoveCrates(Dictionary<int, List<char>> state, string[] moves, bool moveTogether)
        {            

            foreach (var move in moves)
            {
                var numberOfCrates = int.Parse(move.Split("move ")[1].Split(" ")[0]);
                var from = int.Parse(move.Split("from ")[1].Split(" ")[0]);
                var to = int.Parse(move.Split("to ")[1].Split(" ")[0]);

                void moveCrates(int numberOfCratesToMoveAtOnce)
                {
                    var cratesToMove = state[from].GetRange(0, numberOfCratesToMoveAtOnce);
                    state[from].RemoveRange(0, numberOfCratesToMoveAtOnce);
                    state[to].InsertRange(0, cratesToMove);
                }

                if (moveTogether)
                {
                    moveCrates(numberOfCrates);
                }
                else
                {
                    for (int i = 0; i < numberOfCrates; i++)
                    {
                        moveCrates(1);
                    }
                }
            }
            return string.Join("", state.OrderBy(s => s.Key).Select(s => s.Value[0]));
        }        
    }
}
