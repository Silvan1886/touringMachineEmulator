
START_STATE = "q1";
ACCEPTING_STATE = "q2";
record Transition(string State, char ReadSymbol, string NextState, char WriteSymbol, bool Direction);
