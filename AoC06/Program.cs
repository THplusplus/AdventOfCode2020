using System.Linq;
using static System.Console;
using static System.IO.File;

string[] groups = ReadAllText("input.txt").Split("\r\n\r\n");
char[][] anyoneAnswers = groups.Select(g => g.ToCharArray().Except(new[] { '\r', '\n' }).ToArray()).Distinct().ToArray();
char[][] everyoneAnswers = groups
	.Select(g =>
		g.Split("\r\n")
		.Select(g => g.ToCharArray().Distinct().ToArray())
		.Aggregate(
			(g1, g2) => g1.Intersect(g2).ToArray()
		)
	)
	.ToArray();

/* Part 1 */

WriteLine("Part 1: " + anyoneAnswers.Sum(a => a.Count()));

/* Part 2 */

WriteLine("Part 2: " + everyoneAnswers.Sum(a => a.Count()));
