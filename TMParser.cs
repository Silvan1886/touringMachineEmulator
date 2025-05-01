using System.Text;
using static Program;

public class TMParser
{
	public static TuringMachineDefinition Parse(string input)
	{
		var transitions = new List<Transition>();
		var lines = input.Split("111");
		string inputString = string.Empty;

		if (lines.Length > 1)
		{
			inputString = lines[1].Trim();
		}
		else if (lines.Length > 2)
		{
			throw new ArgumentException("Invalid input format. Expected at most two parts when seperating with '111'.");
		}

		var transitionStrings = lines[0].Split("11", StringSplitOptions.RemoveEmptyEntries);
		foreach (var transitionString in transitionStrings)
		{
			var parts = transitionString.Split("1", StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 5)
			{
				var state = "q" + parts[0].Trim().Length;
				var readSymbol = GetSymbol(parts[1].Trim());
				var nextState = "q" + parts[2].Trim().Length;
				var writeSymbol = GetSymbol(parts[3].Trim());
				var direction = parts[4].Trim() == "00";
				transitions.Add(new Transition(state, readSymbol, nextState, writeSymbol, direction));
			}
		}

		TuringMachineDefinition tmDef = new TuringMachineDefinition(inputString, transitions.ToArray());
		Console.WriteLine(GetStringRepresentation(tmDef));
		return tmDef;
	}

	private static char GetSymbol(string symbolString)
	{
		switch (symbolString.Length)
		{
			case 1:
				return '0';
			case 2:
				return '1';
			case 3:
				return Char.MinValue; // Blank symbol
			default:
				return (char)symbolString.Length;
		}
	}

	public static string GetStringRepresentation(TuringMachineDefinition tmDef)
	{
		var sb = new StringBuilder();
		sb.AppendLine("Parsed Turing Machine Definition:");
		sb.AppendLine($"Input: {tmDef.Input}");
		sb.AppendLine("Transitions:");
		foreach (
			var transition in tmDef.Transitions)
		{
			sb.AppendLine($"State: {transition.State}, ReadSymbol: {transition.ReadSymbol}, NextState: {transition.NextState}, WriteSymbol: {transition.WriteSymbol}, Direction: {(transition.Direction ? "Right" : "Left")}");
		}
		return sb.ToString();
	}
}