

public class Program
{
	public record TouringMachineDefinition(string Input, Transition[] Transitions);
	public record Transition(string State, char ReadSymbol, string NextState, char WriteSymbol, bool Direction);

	public static void Main(string[] args)
	{
		Console.WriteLine("This is a simple Turing machine emulator.");
		Console.WriteLine("Please enter the input string (e.g., '010010001010011000101010010110001001001010011000100010001010'): ");
		string input = Console.ReadLine() ?? string.Empty;
		TouringMachineDefinition transitions = TMParser.Parse(input);
	}
}