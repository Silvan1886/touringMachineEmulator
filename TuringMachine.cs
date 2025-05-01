using static Program;

public class TuringMachine
{
    private readonly Transition[] _transitions;
    private readonly List<char> _bandLeft = new List<char>() { Char.MinValue };
    private readonly List<char> _bandRight = new List<char>() { Char.MinValue };
    private string _currentState = "q1";
    private int _currentPosition = 0;
    private int _stepCount = 0;
    public bool HasStopped { get; private set; }

    public TuringMachine(Transition[] transitions)
    {
        _transitions = transitions;
    }

    public void Step(bool shouldPrint)
    {
        if (HasStopped)
        {
            return;
        }

        Transition? transition = _transitions.FirstOrDefault(t => t.State.Equals(_currentState) && GetCharAtCurrentPosition().Equals(t.ReadSymbol));

        if (transition is not null)
        {
            WriteCharToCurrentPosition(transition.WriteSymbol);
            _currentState = transition.NextState;
            _currentPosition = transition.Direction ? _currentPosition + 1 : _currentPosition - 1;

            if (transition.Direction && _bandRight.Count - 1 < _currentPosition)
            {
                _bandRight.Add(Char.MinValue);
            }
            else if (!transition.Direction && _bandLeft.Count - 1 < Math.Abs(_currentPosition))
            {
                _bandLeft.Add(Char.MinValue);
            }
        }
        else
        {
            HasStopped = true;
        }

        if (shouldPrint)
        {
            PrintState();
        }

        _stepCount++;
    }

    public void LaufModus()
    {
        while (!HasStopped)
        {
            Step(false);
        }

        PrintState(true);
    }

    private void PrintState(bool printIsAccepted = false)
    {
        if (printIsAccepted)
        {
            Console.WriteLine("IsAccepted: " + _currentState.Equals("q2"));
        }

        List<char> left = new(_bandLeft);
        List<char> right = new(_bandRight);

        if (_currentPosition < 0)
        {
            left.Insert(Math.Abs(_currentPosition), '@');
        }
        else
        {
            right.Insert(_currentPosition, '@');
        }

        left.Reverse();

        Console.WriteLine("== STATE ==");
        Console.WriteLine("Current state:\t" + _currentState);
        Console.WriteLine("Band:\t" + string.Join("", left.Concat(right)));
        Console.WriteLine("Current Position:\t" + _currentPosition);
        Console.WriteLine("Step count:\t" + _stepCount);
    }

    private char GetCharAtCurrentPosition()
    {
        if (_currentPosition < 0)
        {
            return _bandLeft[Math.Abs(_currentPosition)];
        }
        return _bandRight[_currentPosition];
    }

    private void WriteCharToCurrentPosition(char symbol)
    {
        if (_currentPosition < 0)
        {
            _bandLeft[Math.Abs(_currentPosition)] = symbol;
            return;
        }
        _bandRight[_currentPosition] = symbol;
    }
}