// A "zoomed-in" look at the theoretical deterministic FSM of a Unity-cloned Blinky from Pac-Man.
// The functions used in this Finite State Machine do not exist as a result.
// This is simply meant to showcase the basic elements of an FSM, courtesy of Alan Zucconi.
//
// The components of a Finite State Machine and their corresponding functions or data types:
//
// States:								Transitions:
//		States				switch			Transitions				if
//		Available states	enum {}			Transition condition	bool F()
//		Current state		enum S			Transition action		void F()
//		State action		void F()

public class BlinkyFSM : MonoBehaviour
{
	// Declare available states
	public enum State
	{
		Scatter,
		Chase,
		Frightened,
		Dead
	}

	// Declare current state
	public State CurrentState = State.Scatter;

	// Call update once per frame
	void Update ()
	{
		switch (CurrentState)
		{
			// Blinky is in the: Scatter State.
			case State.Scatter:
   				GoToRandomPlace(); // State action

				// If the grace period timer ends...
    			if (Timer()) // Scatter => Chase
				{
					CurrentState = State.Chase;
     				break;
    			}

				// If Pac-Man gets a power-up...
				if (PowerUp()) // Scatter => Frightened
				{
					TurnBlue(); // Transition action
	 				CurrentState = State.Frightened;
	  				break;
 				}
	 
				break;

			// Blinky is in the: Chase State.
			case State.Chase:
   				GoToPacman(); // State action
	   
				// If the grace period timer ends...
				if (Timer()) // Chase => Scatter
				{
					CurrentState = State.Scatter;
	 				break;
 				}

				// If Pac-Man gets a power-up...
  				if (PowerUp()) // Chase => Frightened
	  			{
					TurnBlue(); // Transition action
	 				CurrentState = State.Frightened;
	  				break;
   				}
	   
				break;

			// Blinky is in the: Frightened State.
			case State.Frightened:
				FleeFromPacman(); // State action
	   
				// If Pac-Man eats Blinky...
				if (HasBeenEaten()) // Frightened => Dead
				{
					TurnDead(); // Transition action
					CurrentState = State.Dead;
	 				break;
 				}

				// If Pac-Man loses the power-up...
  				if (PowerDown()) // Frightened => Chase
	  			{
					TurnRed(); // Transition action
	 				CurrentState = State.Chase;
	  				break;
   				}
	   
				break;

			// Blinky is in the: Dead State.
			case State.Dead:
				GoToHouse(); // State action

				// If dead Blinky reaches the center house...
 				if (IsHouseReached()) // Dead => Scatter
	 			{
					TurnRed(); // Transition action
	 				CurrentState = State.Scatter;
	  				break;
  				}
	  
				break;
		}
	}
}
