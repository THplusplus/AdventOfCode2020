using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
using static System.IO.File;

Regex lineRegex = new Regex("^(\\d+)-(\\d+) (\\w): (\\w+)");
Input[] inputs = ReadAllLines("input.txt").Select(line => lineRegex.Match(line)).Select(match => new Input(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value[0], match.Groups[4].Value)).ToArray();

/* Part 1 */

int numValid = 0;
foreach (Input input in inputs)
{
	int count = 0;
	for (int i = 0; i < input.Password.Length; ++i)
	{
		if (input.Password[i] == input.Letter)
			++count;
	}

	if (input.Val1 <= count && count <= input.Val2)
		++numValid;
}

WriteLine("Part 1: " + numValid);

/* Part 2 */

numValid = 0;
foreach (Input input in inputs)
{
	bool match1 = input.Password[input.Val1 - 1] == input.Letter;
	bool match2 = input.Password[input.Val2 - 1] == input.Letter;

	if (match1 ^ match2)
		++numValid;
}

WriteLine("Part 2: " + numValid);


record Input(int Val1, int Val2, char Letter, string Password);