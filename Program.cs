using Advent_of_Code_2022;
using System.IO;
using System.Reflection;

Console.WriteLine("""
        _       _                 _            __    ____          _        ____   ___ ____  ____  
       / \   __| |_   _____ _ __ | |_    ___  / _|  / ___|___   __| | ___  |___ \ / _ \___ \|___ \ 
      / _ \ / _` \ \ / / _ \ '_ \| __|  / _ \| |_  | |   / _ \ / _` |/ _ \   __) | | | |__) | __) |
     / ___ \ (_| |\ V /  __/ | | | |_  | (_) |  _| | |__| (_) | (_| |  __/  / __/| |_| / __/ / __/ 
    /_/   \_\__,_| \_/ \___|_| |_|\__|  \___/|_|    \____\___/ \__,_|\___| |_____|\___/_____|_____|
                                                                                                   
    """);
Console.WriteLine();

Console.WriteLine("Choose a day to run or press any other key to run all days:");
if (int.TryParse(Console.ReadLine(), out var day))
{
    RunDay(day);
}
else
{
    Console.WriteLine("Running all days...");
    for (int i = 1; i <= 25; i++)
    {
        RunDay(i);
    }
}

Console.WriteLine();

static void RunDay(int day)
{
    var run = Type.GetType($"{nameof(Advent_of_Code_2022)}.Day{day}");
    if (run != null)
    {
        var classInstance = Activator.CreateInstance(run, null);
        run?.GetInterface(nameof(ISolver))?.GetMethod(nameof(ISolver.Run))?.Invoke(classInstance, new object[] { day });
    }
}

interface ISolver
{
    public void Run(int day)
    {
        Console.WriteLine();
        Console.WriteLine($"--- Day {day:00}: {Title} ---");
        var filename = $"Day{day}.txt";
        try
        {
            var input = File.ReadAllText(filename);
            Console.WriteLine($"{nameof(PartOne)}: {PartOne(input)}");
            Console.WriteLine($"{nameof(PartTwo)}: {PartTwo(input)}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Could not find input file {filename}!");
        }

    }
    string PartOne(string input);
    string PartTwo(string input);
    string Title { get;}
}