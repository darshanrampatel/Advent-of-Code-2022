namespace Advent_of_Code_2022
{
    internal class Day11 : ISolver
    {
        public string Title => "Monkey in the Middle";

        public string PartOne(string input) => Solve(input, worryLevelDivisor: true, rounds: 20).ToString();
        public string PartTwo(string input) => Solve(input, worryLevelDivisor: false, rounds: 10000).ToString();

        private static ulong Solve(string input, bool worryLevelDivisor, int rounds)
        {
            var lines = input.Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var monkeys = new List<Monkey>();
            var monkeyInspections = new Dictionary<int, ulong>();
            for (int i = 0; i < lines.Length; i += 6)
            {
                var monkeyLine = lines[i];
                var startingItemsLine = lines[i + 1];
                var operationLine = lines[i + 2];
                var testLine = lines[i + 3];
                var ifTrueLine = lines[i + 4];
                var ifFalseLine = lines[i + 5];

                var monkeyId = int.Parse(monkeyLine.Replace("Monkey ", "").Replace(":", ""));
                var startingItems = startingItemsLine.Replace("Starting items: ", "").Split(", ").Select(ulong.Parse).ToList();
                var operation = operationLine.Replace("Operation: new = ", "");
                var test = ulong.Parse(testLine.Replace("Test: ", "").Replace("divisible by ", ""));
                var ifTrue = int.Parse(ifTrueLine.Replace("If true: throw to monkey ", ""));
                var ifFalse = int.Parse(ifFalseLine.Replace("If false: throw to monkey ", ""));

                monkeys.Add(new Monkey
                {
                    Number = monkeyId,
                    StartingItems = startingItems,
                    Operation = operation,
                    Test = test,
                    TrueMonkey = ifTrue,
                    FalseMonkey = ifFalse
                });
            }

            ulong lowestCommonDivisor = 1;
            foreach (var monkey in monkeys)
            {
                lowestCommonDivisor *= monkey.Test;
            }

            for (int i = 1; i <= rounds; i++)
            {
                foreach (var monkey in monkeys.OrderBy(m => m.Number))
                {
                    for (int j = 0; j < monkey.StartingItems.Count; j++)
                    {
                        ulong item = monkey.StartingItems[j];
                        if (!monkeyInspections.ContainsKey(monkey.Number))
                        {
                            monkeyInspections.Add(monkey.Number, 0);
                        }
                        monkeyInspections[monkey.Number]++;
                        // The order of operations doesn't matter when worryLevelDivisor = true
                        var worryLevel = PerformOperation(item, monkey.Operation);
                        var (bored, testResult) = PerformTest(worryLevelDivisor, worryLevel, monkey.Test, lowestCommonDivisor);
                        monkey.StartingItems.RemoveAt(j);
                        j--; // Since we have just removed an item
                        var monkeyToThrowTo = monkeys.Find(m => m.Number == (testResult ? monkey.TrueMonkey : monkey.FalseMonkey));
                        monkeyToThrowTo?.StartingItems.Add(bored);
                    }
                }
            }

            var twoMostActiveMonkeys = monkeyInspections.OrderByDescending(m => m.Value).Take(2).ToList();
            return (twoMostActiveMonkeys[0].Value * twoMostActiveMonkeys[1].Value);
        }

        private static ulong PerformOperation(ulong old, string? operation)
        {
            ulong n = 0;
            if (!string.IsNullOrWhiteSpace(operation))
            {
                if (operation.StartsWith("old * "))
                {
                    var multiplier = operation.Replace("old * ", "");
                    if (multiplier == "old")
                    {
                        n = old * old;
                    }
                    else if (ulong.TryParse(multiplier, out ulong m))
                    {
                        n = old * m;
                    }
                }
                else if (operation.StartsWith("old + "))
                {
                    var add = operation.Replace("old + ", "");
                    if (add == "old")
                    {
                        n = old + old;
                    }
                    else if (ulong.TryParse(add, out ulong m))
                    {
                        n = old + m;
                    }
                }
            }
            return n;
        }

        private static (ulong, bool) PerformTest(bool worryLevelDivisor, ulong worryLevel, ulong divisor, ulong lowestCommonDivisor)
        {
            if (worryLevelDivisor)
            {
                var bored = (ulong)Math.Floor(worryLevel / 3.0);
                return (bored, bored % divisor == 0);
            }
            else
            {
                var bored = worryLevel % lowestCommonDivisor;                
                return (bored, bored % divisor == 0);
            }

        }

        public class Monkey
        {
            public int Number { get; set; }
            public List<ulong> StartingItems { get; set; } = new();

            public string? Operation { get; set; }
            public ulong Test { get; set; }
            public int TrueMonkey { get; set; }
            public int FalseMonkey { get; set; }
        }
    }
}
