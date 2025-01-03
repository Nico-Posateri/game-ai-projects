// A "zoomed-in" look at the theoretical behavior steering and emergent behaviors of an NPC.
// Some of the elements referenced in these algorithms do not exist as a result.
// This is simply meant to showcase the basic elements of emergent behavior and behavior steering, courtesy of Alan Zucconi.

/* BEHAVIOR STEERING *//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Seeking

public Rigidbody Rigidbody; // The rigid body object we will move
public Transform Target; // The target we want the rigid body to go to

public float Speed; // The target speed of the object
public float MaxForce; // How much force can be applied to the object

void FixedUpdate () // In Unity, FixedUpdate allows continuous alteration of physics objects
{
    Vector3 P = transform.position; // P: Current position
    Vector3 T = Target.position; // T: Target position

    Vector3 U_hat = (T - P).normalized; // Ū: Velocity direction (the P and T are simply flipped between Seek and Flee)
    Vector3 U = U_hat * Speed; // U: Desired velocity

    // Version 2
    Vector3 V = Rigidbody.velocity; // Extracts the current velocity from the rigid body
    Vector3 dV = U - V; // Calculate delta V
    Vector3 F = Vector3.ClampMagnitude(dV, MaxForce); // Calculate F
    Rigidbody.AddForce(F); // Add the force to the rigid body, should multiply by delta time in Unity
}

// Seek could be enhanced by asking the agent to begin decelerating once it enters a radius around its target, rather than continual "orbiting"

// Fleeing

public Rigidbody Rigidbody; // The rigid body object we will move
public Transform Target; // The target we want the rigid body to go to

public float Speed; // The target speed of the object
public float MaxForce; // How much force can be applied to the object

void FixedUpdate () // In Unity, FixedUpdate allows continuous alteration of physics objects
{
    Vector3 P = transform.position; // P: Current position
    Vector3 T = Target.position; // T: Target position

    Vector3 U_hat = (P - T).normalized; // Ū: Velocity direction (the P and T are simply flipped between Seek and Flee)
    Vector3 U = U_hat * Speed; // U: Desired velocity

    // Version 2
    Vector3 V = Rigidbody.velocity; // Extracts the current velocity from the rigid body
    Vector3 dV = U - V; // Calculate delta V
    Vector3 F = Vector3.ClampMagnitude(dV, MaxForce); // Calculate F
    Rigidbody.AddForce(F); // Add the force to the rigid body, should multiply by delta time in Unity
}

// Seek and Flee are bases upon which an agent could be built to evade or pursue

/* EMERGENT BEHAVIORS */////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Flocking

// Particle Life

// Cellular Automata

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
