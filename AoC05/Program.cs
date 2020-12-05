using System;
using System.Linq;
using static System.Console;
using static System.IO.File;

int[] boardingPasses = ReadAllLines("input.txt").Select(line => line.Replace('L', '0').Replace('R', '1').Replace('B', '1').Replace('F', '0')).Select(b => Convert.ToInt32(b, 2)).ToArray();

/* Part 1 */

WriteLine("Part 1: " + boardingPasses.Max());

/* Part 2 */

int previous = 0;
foreach (int boardingPass in boardingPasses.OrderBy(b => b))
{
	if (previous != 0 && previous != boardingPass - 1)
		WriteLine("Part 2: " + (boardingPass - 1));
	previous = boardingPass;
}
