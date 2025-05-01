using static Program;

public class TMParser
{
	public static TouringMachineDefinition Parse(string input)
	{
		var transitions = new List<Transition>();
		var lines = input.Split("111");
		var inputString = lines[1].Trim();
		var transitionStrings = lines[0].Split("11", StringSplitOptions.RemoveEmptyEntries);
		foreach (var transitionString in transitionStrings)
		{
			var parts = transitionString.Split("1", StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 5)
			{
				var state = parts[0].Trim();
				var readSymbol = parts[1].Trim()[0];
				var nextState = parts[2].Trim();
				var writeSymbol = parts[3].Trim()[0];
				var direction = parts[4].Trim() == "1";
				transitions.Add(new Transition(state, readSymbol, nextState, writeSymbol, direction));
			}
		}

		return new TouringMachineDefinition(inputString, transitions.ToArray());
	}
}