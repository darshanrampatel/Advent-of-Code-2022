namespace Advent_of_Code_2022
{
    internal class Day1 : ISolver
    {
        public string PartOne(string input)
        {
            ProcessInput(input);
            return elfs.Max(e => e.Value.TotalCalories).ToString();
        }

        public string PartTwo(string input) => elfs.OrderByDescending(e => e.Value.TotalCalories)
                                                   .Take(3)
                                                   .Sum(e => e.Value.TotalCalories)
                                                   .ToString();

        public string Title => "Calorie Counting";

        private static readonly Dictionary<int, Elf> elfs = new();

        private static void ProcessInput(string input)
        {
            var lines = input.Split('\n');
            var elfNumber = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elfNumber++;
                }
                else
                {
                    if (!elfs.ContainsKey(elfNumber))
                    {
                        elfs[elfNumber] = new Elf();
                    }
                    elfs[elfNumber].Items.Add(new Elf.Item() { Calories = int.Parse(line) });
                }
            }
        }

        class Elf
        {
            public List<Item> Items { get; set; } = new List<Item>();
            public int TotalCalories => Items.Sum(i => i.Calories);

            public class Item
            {
                public int Calories { get; set; }
            }
        }
    }
}
