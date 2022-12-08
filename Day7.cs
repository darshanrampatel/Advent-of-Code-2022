namespace Advent_of_Code_2022
{
    internal class Day7 : ISolver
    {
        public string Title => "No Space Left On Device";

        public string PartOne(string input)
        {
            ProcessInput(input);
            return filesystem.Where(d => d.Value <= 100000).Sum(d => d.Value).ToString();
        }

        const string ROOT_DIRECTORY = "/";

        readonly Dictionary<string, int> filesystem = new();

        void AddFileSize(string directory, int fileSize)
        {
            if (!filesystem.ContainsKey(directory))
            {
                filesystem[directory] = 0;
            }
            filesystem[directory] += fileSize;
        }

        void ProcessInput(string input)
        {
            var lines = input.Split("\n");
            List<string> pwd = new();

            static string pwdPrint(List<string> pwd) => string.Join("/", pwd);

            foreach (var line in lines)
            {
                if (line.StartsWith("$ "))
                {
                    var command = line.Replace("$ ", "");
                    if (command.StartsWith("cd "))
                    {
                        var cd = command.Replace("cd ", "").Trim();
                        switch (cd)
                        {
                            case ROOT_DIRECTORY: pwd = new(); break;
                            case "..": pwd.RemoveAt(pwd.Count - 1); break; // Remove last element
                            default: pwd.Add(cd); break;
                        };
                    }
                    else if (command.StartsWith("ls"))
                    {
                        // Skip
                    }
                }
                else // Not a command
                {
                    if (line.StartsWith("dir "))
                    {
                        // Skip
                    }
                    else
                    {
                        var file = line.Split(" ");
                        int fileSize = int.Parse(file[0]);
                        var fileName = file[1].Trim();
                        foreach (var r in Enumerable.Range(0, pwd.Count + 1))
                        {
                            var take = pwd.Take(0..r).ToList(); // Get the actual directory, with the prefixed folders
                            var path = ROOT_DIRECTORY + pwdPrint(take);
                            AddFileSize(path, fileSize);
                        }
                    }
                }
            }

        }

        public string PartTwo(string input)
        {
            var totalDiskSpace = 70000000;
            var unusedSpaceRequired = 30000000;
            var rootDirectorySize = filesystem[ROOT_DIRECTORY];
            var currentUnusedSpace = totalDiskSpace - rootDirectorySize;
            var dataRequiredToDelete = unusedSpaceRequired - currentUnusedSpace;
            var candidates = filesystem.Where(d => d.Value > dataRequiredToDelete);
            return candidates.Min(d => d.Value).ToString();            
        }
    }
}
