

using System.Numerics;
using System.Text.RegularExpressions;

public class Program
{
	public record TuringMachineDefinition(string Input, Transition[] Transitions);
	public record Transition(string State, char ReadSymbol, string NextState, char WriteSymbol, bool Direction);

	public static void Main(string[] args)
	{
		string input;
		if (args.Length > 0)
		{
			if (!File.Exists(args[0]))
			{
				Console.WriteLine($"File {args[0]} does not exist.");
				return;
			}
			input = File.ReadAllText(args[0]);
		}
		else
		{
			Console.WriteLine("This is a simple Turing machine emulator.");
			Console.WriteLine("Please enter the input string (e.g., '010010001010011000101010010110001001001010011000100010001010'): ");
			input = Console.ReadLine() ?? string.Empty;

            // If the input is a decimal number, parse it into a binary string
            if (!Regex.IsMatch(input, "^[01]+$") && BigInteger.TryParse(input, out BigInteger result))
            {
                input = ToBinaryString(result).Substring(1);
            }
        }
		TuringMachineDefinition transitions = TMParser.Parse(input);
		TuringMachine turingMachine = new TuringMachine(transitions);

        string mode = string.Empty;

        while (!(mode.Equals("1") || mode.Equals("2")))
        {
            Console.WriteLine("Which mode do you want: ");
            Console.WriteLine("\t1. Step ");
            Console.WriteLine("\t2. Fast ");
            mode = Console.ReadLine() ?? string.Empty;
        }

        Console.WriteLine(string.Empty);
        Console.WriteLine(string.Empty);
        Console.WriteLine(string.Empty);
        Console.WriteLine("'@' is at the left of the current position");
        Console.WriteLine(string.Empty);
        Console.WriteLine(string.Empty);
        Console.WriteLine(string.Empty);

        if (mode.Equals("1"))
        {
            turingMachine.PrintState();
            Thread.Sleep(1000);

            while (!turingMachine.HasStopped)
            {
                turingMachine.Step(true);
                Thread.Sleep(1000);
            }
        }
        else if (mode.Equals("2"))
        {
            turingMachine.FastMode();
        }
    }

    private static string ToBinaryString(BigInteger number)
    {
        if (number == 0)
            return "0";

        string result = "";
        while (number > 0)
        {
            result = (number % 2) + result;
            number /= 2;
        }
        return result;
    }
}