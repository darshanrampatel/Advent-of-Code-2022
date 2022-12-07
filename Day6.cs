namespace Advent_of_Code_2022
{
    internal class Day6 : ISolver
    {
        public string Title => "Tuning Trouble";

        public string PartOne(string input) => GetStartOfMessageMarker(input, 4);

        public string PartTwo(string input) => GetStartOfMessageMarker(input, 14);

        private static string GetStartOfMessageMarker(string input, int lengthOfSequence)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var last = i + lengthOfSequence;
                if (last > input.Length)
                {
                    last = input.Length;
                }

                if (input[i..last].Distinct().Count() == lengthOfSequence)
                {
                    return last.ToString();
                }
            }
            return "unknown";
        }
    }
}
