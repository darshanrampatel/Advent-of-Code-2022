namespace Advent_of_Code_2022
{
    internal class Day3 : ISolver
    {
        public string Title => "Rucksack Reorganization";

        public string PartOne(string input)
        {
            var lines = input.Split('\n');
            var sum = 0;
            foreach (var rucksack in lines)
            {
                var halfway = rucksack.Length / 2;
                var compartment1 = rucksack[..halfway];
                var compartment2 = rucksack[halfway..];
                var sharedItems = compartment1.Intersect(compartment2);                
                sum += GetPriorities(sharedItems);
            }
            return sum.ToString();
        }

        public string PartTwo(string input)
        {
            var lines = input.Split('\n');
            var sum = 0;
            for (int i = 0; i < lines.Length; i+=3)
            {
                string? rucksack1 = lines[i];
                string? rucksack2 = lines[i + 1];
                string? rucksack3 = lines[i + 2];
                var sharedItems = rucksack1.Intersect(rucksack2).Intersect(rucksack3);
                sum += GetPriorities(sharedItems);
            }
            return sum.ToString();
        }

        private int GetPriorities(IEnumerable<char> sharedItems) => sharedItems.Where(char.IsLetter).Sum(i => char.IsLower(i) ? i - 'a' + 1 : i - 'A' + 27);                
    }
}
