

public class Program
{
    public record TuringMachineDefinition(string Input, Transition[] Transitions);
    public record Transition(string State, char ReadSymbol, string NextState, char WriteSymbol, bool Direction);

    public static void Main(string[] args)
    {
        Console.WriteLine("This is a simple Turing machine emulator.");
        Console.WriteLine("Please enter the input string (e.g., '010010001010011000101010010110001001001010011000100010001010'): ");
        string input = Console.ReadLine() ?? string.Empty;
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

        Console.WriteLine("'@' is at the left of the current position");
        Console.WriteLine(string.Empty);

        if (mode.Equals("1"))
        {
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
}