using System.Linq;
using static System.Console;
using static System.IO.File;

bool[][] map = ReadAllLines("input.txt").Select(line => line.ToArray()).Select(ca => ca.Select(c => c == '#').ToArray()).ToArray();

/* Part 1 */

WriteLine($"Part 1: {CountTreesForSlope(map, new Slope(3, 1))}");

/* Part 2 */

Slope[] slopes = new[]
{
	new Slope(1, 1),
	new Slope(3, 1),
	new Slope(5, 1),
	new Slope(7, 1),
	new Slope(1, 2),
};
WriteLine($"Part 2: {slopes.Select(s => (long)CountTreesForSlope(map, s)).Aggregate((a, b) => a * b)}");

static int CountTreesForSlope(bool[][] map, Slope slope)
{
	int trees = 0;
	int x = 0;
	for (int y = 0; y < map.Length;)
	{
		if (map[y][x])
			++trees;

		x = (x + slope.X) % map[0].Length;
		y += slope.Y;
	}
	return trees;
}

record Slope(int X, int Y);
