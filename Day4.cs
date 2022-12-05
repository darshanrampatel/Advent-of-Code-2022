namespace Advent_of_Code_2022
{
    internal class Day4 : ISolver
    {
        public string Title => "Camp Cleanup";

        public string PartOne(string input)
        {
            var lines = input.Split('\n');
            var fullyContainingPairs = 0;
            foreach (var pair in lines)
            {
                var splits = pair.Split(",");
                var firstRange = GetRanges(splits[0]);
                var secondRange = GetRanges(splits[1]);
                if ((firstRange.Start.Value <= secondRange.Start.Value && firstRange.End.Value >= secondRange.End.Value) ||
                    (secondRange.Start.Value <= firstRange.Start.Value && secondRange.End.Value >= firstRange.End.Value))
                {
                    fullyContainingPairs++;
                }
            }
            return fullyContainingPairs.ToString();
        }

        private static Range GetRanges(string range)
        {
            var splits = range.Split("-");
            return int.Parse(splits[0])..int.Parse(splits[1]);
        }

        public string PartTwo(string input)
        {
            var lines = input.Split('\n');
            var overlappingPairs = 0;
            foreach (var pair in lines)
            {
                var splits = pair.Split(",");
                var firstRange = GetRanges(splits[0]);
                var secondRange = GetRanges(splits[1]);
                var firstRangeValues = Enumerable.Range(firstRange.Start.Value, firstRange.End.Value - firstRange.Start.Value + 1);
                var secondRangeValues = Enumerable.Range(secondRange.Start.Value, secondRange.End.Value - secondRange.Start.Value + 1);
                if (firstRangeValues.Intersect(secondRangeValues).Any() || secondRangeValues.Intersect(firstRangeValues).Any())
                {
                    overlappingPairs++;
                }            
            }
            return overlappingPairs.ToString();
        }
    }
}
