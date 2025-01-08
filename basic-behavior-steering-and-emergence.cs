// A "zoomed-in" look at the theoretical behavior steering and emergent behaviors of an NPC.
// Some of the elements referenced in these algorithms do not exist as a result.
// This is simply meant to showcase the basic elements of emergent behavior and behavior steering, courtesy of Alan Zucconi.

/* BEHAVIOR STEERING *//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Seeking //

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

// Fleeing //

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

// Flocking //

public class Boid : MonoBehaviour                                    // Boids v
{
    // Physics
    public Vector2 P; // Position
    public Vector2 V; // Velocity
    public Vector2 F; // Force
    public float MaxForce;

    // Flocking
    public Boid[] Boids; // The flock

    public float SeparationRadius;
    public float AlignmentRadius;
    public float CohesionRadius;

    void Update ()
    {
        // Flocking
        F = Vector2.zero;
        F += SeparationForce();
        F += AlignmentForce();
        F += CohesionForce();

        // Physics
        V += F * Time.deltaTime;
        P += V * Time.deltaTime;
    }                                                                // Boids ^

    Vector2 SeparationForce ()                                       // Boids Separation v
    {
        int count = 0;
        Vector2 force = Vector2.zero;

        // Loops through all the boids
        foreach (Boid boid in Boids)
        {
            // Avoid itself
            if (boid == this)
                continue;

            // Only boids close enough
            float distance = Vector2.Distance(P, boid.P);
            if (distance > SeparationRadius)
                continue;

            // Separation force
            Vector2 direction = (P - boid.P).normalized;
            force += direction / d;

            count ++;
        }

        // No forces?
        if (count == 0)
            return Vector2.zero;

        // Steering
        force = Vector2.ClampMagnitude(force, MaxForce);
        return Vector2.ClampMagnitude(force - V, MaxForce);
    }                                                                // Boids Separation ^

    Vector2 AlignmentForce ()                                        // Boids Alignment v
    {
        int count = 0;
        Vector2 velocity = Vector2.zero;

        // Loops through all the boids
        foreach (Boid boid in Boids)
        {
            // Avoid itself
            if (boid == this)
                continue;

            // Only boids close enough
            float distance = Vector2.Distance(P, boid.P);
            if (distance > AlignmentRadius)
                continue;

            // Average velocity
            velocity += boid.v;

            count ++;
        }

        // No forces?
        if (count == 0)
            return Vector2.zero;

        // Average velocity
        velocity /= count;

        // Steering
        velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);
        return Vector2.ClampMagnitude(velocity - V, MaxForce);
    }                                                                // Boids Alignment ^

    Vector2 CohesionForce ()                                         // Boids Cohesion v
    {
        int count = 0;
        Vector2 velocity = Vector2.zero;

        // Loops through all the boids
        foreach (Boid boid in Boids)
        {
            // Avoid itself
            if (boid == this)
                continue;

            // Only boids close enough
            float distance = Vector2.Distance(P, boid.P);
            if (distance > CohesionRadius)
                continue;

            // Average velocity
            position += boid.v;

            count ++;
        }

        // No forces?
        if (count == 0)
            return Vector2.zero;

        // Average position
        position /= count;

        // Steering
        return Seek(position);
    }                                                                // Boids Cohesion ^
}

// Particle Life //

public class Particle : MonoBehaviour
{
    public float Radius; // The radius of interactions
    public AnimationCurve[] Forces; // The force profile against i-th
    public int Type; // The type of this particle

    public LayerMask ParticleMask;
    public Rigidbody Rigidbody;

    void Update()
    {
        // Retrieves colliders around this particle by returning colliders which fall within the particle's imaginary surrounding sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, ParticleMask);
        foreach (Collider collider in colliders)
        {
            // Extracts particles from colliders
            Particle particle = collider.GetComponent<Particle>();
            if (particle == null) continue; // The i-th collider is not a particle
            if (particle == this) continue; // Avoid self-interactions

            // Add a force in the direction of the particle, based on the distance
            float distance = Vector3.Distance(transform.position, particle.transform.position);
            float force = -Force[particle.Type].Evaluate(distance / Radius); // [0. Radius] -> [0, 1]
            Vector3 direction = (particle.transform.position = transform.position).normalized;
            Rigidbody.AddForce(direction * force);
        }
    }
}

// Cellular Automata: John Conway's Game of Life //

// Integer grid
int [,] Grid; // 2D grid of integers where [0] is dead, [1] is alive

// Toroidal world: If a coordinate surpasses an edge of the grid, it loops over to the other side
int Get (int x, int y)
{
    if (x < 0) x = Grid.GetLength(0) - 1;
    if (y < 0) y = Grid.GetLength(1) - 1;

    if (x >= Grid.GetLength(0)) x = 0;
    if (y >= Grid.GetLength(1)) y = 0;

    return Grid[x,y];
}

int [,] UpdateGrid;

void Update ()
{
    for (int x = 0; x < Grid.GetLength(0); x++) // Loops through the x-dimension of the grid
    for (int y = 0; y < Grid.GetLength(1); y++) // Loops through the y-dimension of the grid
    {
        // Counts the neighbors surrounding the cell X:
        int neighbors =
            Get(x-1, y-1) + Get(x, y-1) + Get(x+1,y-1) + //     *  *  *
            Get(x-1, y)                  + Get(x+1, y) + //     *  X  *
            Get(x-1, y+1) + Get(x, y+1) + Get(x+1, y+1); //     *  *  *

        // Rules
        if (neighbors == 2) UpdatedGrid[x,y] = Grid[x,y]; // Rule 1: Survival
        else if (neighbors == 3) UpdatedGrid[x,y] = 1;    // Rule 2: Birth
        else UpdatedGrid[x,y] = 0;                        // Rule 3: Death
    }

    // Swaps the grids
    int [,] tempGrid = Grid;
    Grid = UpdatedGrid;
    UpdatedGrid = tempGrid;
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
