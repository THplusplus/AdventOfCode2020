using System.Linq;
using static System.Console;
using static System.IO.File;

int[] inputs = ReadAllLines("input.txt").Select(line => int.Parse(line)).ToArray();

Write("Part 1: ");
for (int i = 0; i < inputs.Length; ++i)
	for (int j = i + 1; j < inputs.Length; ++j)
	{
		if (inputs[i] + inputs[j] == 2020)
		{
			WriteLine(inputs[i] * inputs[j]);
		}
	}

Write("Part 2: ");
for (int i = 0; i < inputs.Length; ++i)
	for (int j = i + 1; j < inputs.Length; ++j)
		for (int k = j + 1; k < inputs.Length; ++k)
		{
			if (inputs[i] + inputs[j] + inputs[k] == 2020)
			{
				WriteLine(inputs[i] * inputs[j] * inputs[k]);
			}
		}
