using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
using static System.IO.File;

string[] requiredCredentials = new[]
{
	"byr",
	"iyr",
	"eyr",
	"hgt",
	"hcl",
	"ecl",
	"pid",
	//"cid",
};

Dictionary<string, CredentialVerifier> credentialVerifiers = new Dictionary<string, CredentialVerifier>(new KeyValuePair<string, CredentialVerifier>[]
{
	new ("byr", new(new Regex("^(\\d{4})$"), (s) => IsIntBetween(s, 1920, 2002))),
	new ("iyr", new(new Regex("^(\\d{4})$"), (s) => IsIntBetween(s, 2010, 2020))),
	new ("eyr", new(new Regex("^(\\d{4})$"), (s) => IsIntBetween(s, 2020, 2030))),
	new ("hgt", new(new Regex("^(\\d+(cm|in))$"), (s) => IsValidHeight(s))),
	new ("hcl", new(new Regex("^#([0-9a-f]{6})$"), (s) => true)),
	new ("ecl", new(new Regex("^(amb|blu|brn|gry|grn|hzl|oth)$"), (s) => true)),
	new ("pid", new(new Regex("^\\d{9}$"), (s) => true)),
	new ("cid", new(new("."), (s) => true)),
});

string[] passports = ReadAllText("input.txt").Split("\r\n\r\n");

/* Part 1 */

int validPassports = 0;
foreach (string passport in passports)
{
	if (GetCredentials(passport).Select(c => c.Type).Distinct().Intersect(requiredCredentials).Count() == requiredCredentials.Length)
		++validPassports;
}

WriteLine("Part 1: " + validPassports);

/* Part 2 */

validPassports = 0;
foreach (string passport in passports)
{
	if (GetCredentials(passport).Where(c => IsValidCredential(c)).Select(c => c.Type).Distinct().Intersect(requiredCredentials).Count() == requiredCredentials.Length)
		++validPassports;
}

WriteLine("Part 2: " + validPassports);


Credential[] GetCredentials(string passport)
{
	Regex credentialExtractor = new Regex("(\\S+):(\\S+)(\\s|$)");
	return credentialExtractor.Matches(passport).Select(m => new Credential(m.Groups[1].Value, m.Groups[2].Value)).ToArray();
}

bool IsValidCredential(Credential credential)
{
	CredentialVerifier credentialVerifier = credentialVerifiers[credential.Type];
	Match match = credentialVerifier.Regex.Match(credential.Value);
	if (!match.Success)
		return false;
	return credentialVerifier.Validator(match.Groups[1].Value);
}

bool IsIntBetween(string s, int min, int max)
{
	return int.Parse(s) >= min && int.Parse(s) <= max;
}

bool IsValidHeight(string s)
{
	if (s.EndsWith("in"))
		return IsIntBetween(s.TrimEnd("in".ToCharArray()), 59, 76);
	else
		return IsIntBetween(s.TrimEnd("cm".ToCharArray()), 150, 193);
}

record Credential(string Type, string Value);
record CredentialVerifier(Regex Regex, Func<string, bool> Validator);
