// A "zoomed-in" look at the theoretical FSM of a Unity-cloned Blinky from Pac-Man.
// The functions used in this Finite State Machine do not exist as a result.

public class BlinkyFSM : MonoBehavior
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
