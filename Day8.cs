using System.Linq;

namespace Advent_of_Code_2022
{
    internal class Day8 : ISolver
    {
        public string Title => "Treetop Tree House";

        public string PartOne(string input)
        {
            ProcessInput(input);
            var visibleTreeCount = 0;
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var tree = ArrayOfTrees![i, j];
                    if (i == 0 || j == 0 || i == GridSize - 1 || j == GridSize - 1) // On the outside
                    {
                        visibleTreeCount++;
                    }
                    else if (IsTreeVisible(GridSize, i, j, tree, ArrayOfTrees).Item1)
                    {
                        visibleTreeCount++;
                    }
                }
            }
            return visibleTreeCount.ToString();
        }

        int[,]? ArrayOfTrees;
        int GridSize;

        void ProcessInput(string input)
        {
            var lines = input.Split('\n');
            GridSize = lines.Length;
            ArrayOfTrees = new int[GridSize, GridSize]; // Assume we have a square input
            for (int i = 0; i < GridSize; i++)
            {
                string? line = lines[i].Trim();
                for (int j = 0; j < GridSize; j++)
                {
                    char c = line[j];
                    var tree = (int)char.GetNumericValue(c);
                    ArrayOfTrees[i, j] = tree;
                }
            }
        }

        static (bool, int) IsTreeVisible(int gridSize, int i, int j, int tree, int[,] arrayOfTrees)
        {
            var (leftVisible, leftScenic) = VisibleFromLeft(i, j, tree, arrayOfTrees);
            var (rightVisible, rightScenic) = VisibleFromRight(gridSize, i, j, tree, arrayOfTrees);
            var (topVisible, topScenic) = VisibleFromTop(i, j, tree, arrayOfTrees);
            var (bottomVisible, bottomScenic) = VisibleFromBottom(gridSize, i, j, tree, arrayOfTrees);
            return (leftVisible || rightVisible || topVisible || bottomVisible, leftScenic * rightScenic * topScenic * bottomScenic);
        }

        static (bool, int) VisibleFromLeft(int i, int j, int tree, int[,] arrayOfTrees)
        {
            var count = 0;
            for (int y = j - 1; y >= 0; y--)
            {
                count++;
                var neighbouringTree = arrayOfTrees[i, y];
                if (tree <= neighbouringTree)
                {
                    return (false, count);
                }
            }
            return (true, count);
        }

        static (bool, int) VisibleFromRight(int gridSize, int i, int j, int tree, int[,] arrayOfTrees)
        {
            var count = 0;
            for (int y = j + 1; y < gridSize; y++)
            {
                count++;
                var neighbouringTree = arrayOfTrees[i, y];
                if (tree <= neighbouringTree)
                {
                    return (false, count);
                }
            }
            return (true, count);
        }

        static (bool, int) VisibleFromTop(int i, int j, int tree, int[,] arrayOfTrees)
        {
            var count = 0;
            for (int x = i - 1; x >= 0; x--)
            {
                count++;
                var neighbouringTree = arrayOfTrees[x, j];
                if (tree <= neighbouringTree)
                {
                    return (false, count);
                }
            }
            return (true, count);
        }

        static (bool, int) VisibleFromBottom(int gridSize, int i, int j, int tree, int[,] arrayOfTrees)
        {
            var count = 0;
            for (int x = i + 1; x < gridSize; x++)
            {
                count++;
                var neighbouringTree = arrayOfTrees[x, j];
                if (tree <= neighbouringTree)
                {
                    return (false, count);
                }
            }
            return (true, count);
        }

        public string PartTwo(string input)
        {
            var maxScenic = 0;
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var tree = ArrayOfTrees![i, j];
                    if (i == 0 || j == 0 || i == GridSize - 1 || j == GridSize - 1) // On the outside
                    {
                        // We don't care about these trees
                    }
                    else
                    {
                        var (_, scenic) = IsTreeVisible(GridSize, i, j, tree, ArrayOfTrees);
                        if (scenic > maxScenic)
                        {
                            maxScenic = scenic;
                        }
                    }
                }
            }
            return maxScenic.ToString();
        }
    }
}
