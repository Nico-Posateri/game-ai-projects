// A "zoomed-in" look at the theoretical pathfinding of an NPC.
// Some of the functions used in this pathfinding algorithm do not exist as a result.
// This is simply meant to showcase the basic elements of pathfinding, courtesy of Alan Zucconi.

// Node class: Lists the connections between Nodes, which an agent can use for navigation //////////////////////////////////////////////////////////////

public class Node
{
    public Vector3 Position;
    // ...
    // public List<Node> Neighbors; // Used only for BFS, so it has been replaced below for Dijkstra's Algorithm
    public List<(Node, float)> Neighbors; // Adds the cost of moving to a Node, represented by a float
}

// A graph of Nodes and connections, where connected Nodes are "Neighbors"

// Adds the Nodes A through E
Node A = new Node();
Node B = new Node();
Node C = new Node();
Node D = new Node();
Node E = new Node();

// Adds the neighbors B and C to Node A
A.Neighbors.Add(B);
A.Neighbors.Add(C);
// Adds the neighbors C and D to Node B
B.Neighbors.Add(C);
B.Neighbors.Add(D);
// Adds the neighbors B, D, and E to Node C
C.Neighbors.Add(B);
C.Neighbors.Add(D);
C.Neighbors.Add(E);
// Adds the neighbor E to Node D
D.Neighbors.Add(E);
// Adds the neighbor D to Node E
E.Neighbors.Add(D);

// These Nodes and their connections would look something like this graph:
//
//            ->(B)-------->(D)        Following the Nodes in columns from left to right, we see:
//          /   | ^      ^  | ^        A connects to B and C
//        (A)   | |    /    | |        B connects to C, C connects to B
//          \   v |  /      v |        B also connects to D, C also connects to D and E
//            ->(C)-------->(E)        D connects to E, E connects to D

// Data Structures /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// List: Random Access
List<Node> list = new List<Node>(); // Stores elements, in this case, Neighbors
list.Add(N); // Adds elements to the list

// HashSet: Quickly tests if an element is present
HashSet<Node> hashset = new HashSet<Node>(); // Stores elements
hashset.Add(N); // Adds elements to the hashset, like visited Nodes
bool contains = hashSet.Contains(N); // Quickly tells whether a specific Node is in the hashset

// Queue: First in, first out
Queue<Node> queue = new Queue<Node>();
queue.Enqueue(N); // Adds an element to the queue...
Node M = queue.Dequeue(); // ...which is then dequeued

// Stack: Last in, first out
Stack<Node> stack = new Stack<Node>();
queue.Push(N); // Within the queue, the last element is selected and...
Node M = queue.Pop(); // ...dequeued

// Dictionary: Associates a key with a value
Dictionary<Node, float> dictionary = new Dictionary<Node>();
dictionary[N] = 10;

// Reachability Algorithm //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/* Replaced with BFS
// Checks for all Nodes which are connected, or reachable, starting with...
bool Reachable (Node start, Node goal) // A starting Node and a goal Node
{
    Queue<Node> frontier = new Queue<Node>(); // Queues Nodes...
    HashSet<Node> visited = new HashSet<Node>(); // ...and adds encountered Nodes to the hashset

    frontier.Enqueue(start); // Enqueue and...
    visited.Add(start); // ...add the starting Node to the algorithm

    while (frontier.Any()) // While the frontier is not empty...
    {
        Node current = frontier.Dequeue(); // Dequeue the current Node for processing below
        if (current == goal) // Exits early, as...
            return true; // ...the goal Node is reachable! If not...

        foreach (Node next in current.Neighbors) // ...begin looping through available Neighbors
        {
            if (! visited.Contains(next)) // Asks, "has this Node been visited already?" If not...
            {
                frontier.Enqueue(next); // ...it is queued into the frontier...
                visited.Add(next); // ...and added to the list of visited Nodes
            }
        }
    }
    return false; // Returns false if the goal Node is deemed unreachable after scanning all Nodes
}
*/

// Breadth-First Search (BFS) //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/* Replaced with Dijkstra's Algorithm
// Checks for all Nodes which are connected, or reachable, starting with...
Dictionary<Node, Node> VectorField_BFS (Node start, Node goal) // A starting Node and a goal Node
{
    Queue<Node> frontier = new Queue<Node>(); // Queues Nodes...
    HashSet<Node> visited = new HashSet<Node>(); // ...and adds encountered Nodes to the hashset
    Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>(); // Tells which Node the agent came from to get to the current Node

    frontier.Enqueue(start); // Enqueue and...
    visited.Add(start); // ...add the starting Node to the algorithm
    cameFrom[start] = null; // Marks the origin Node

    while (frontier.Any()) // While the frontier is not empty...
    {
        Node current = frontier.Dequeue(); // Dequeue the current Node for processing below
        if (current == goal) // If the current Node is the goal Node, exit early
            break;
    
        foreach (Node next in current.Neighbors) // ...begin looping through available Neighbors
        {
            if (! cameFrom.ContainsKey(next)) // Is the next Node one which the agent came from? If not...
            {
                frontier.Enqueue(next); // ...it is queued into the frontier...
                cameFrom[next] = current; // ...and marked as the new current
            }
        }
    }
    return from; // Returns a dictionary of Nodes
}
*/

// Dijkstra's Algorithm ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/* Replaced with A*
// Checks for all Nodes which are connected, or reachable, starting with...
Dictionary<Node, Node> VectorField_Dijkstra (Node start, Node goal) // A starting Node and a goal Node
{
    PriorityQueue<Node> frontier = new PriorityQueue<Node>(); // Queues Nodes, then dequeues the Node with the lowest "cost"...
    HashSet<Node> visited = new HashSet<Node>(); // ...and adds encountered Nodes to the hashset
    Dictionary<Node, float> costSoFar = new Dictionary<Node, float>(); // Associates each Node with a "cost", a float

    frontier.Enqueue(start, 0); // Enqueue and...
    cameFrom[start] = null; // Marks the origin Node
    costSoFar[start] = 0; // The cost to move starts at zero

    while (frontier.Any()) // While the frontier is not empty...
    {
        Node current = frontier.Dequeue(); // Dequeue the current Node for processing below
        if (current == goal) // If the current Node is the goal Node, exit early
            break;
    
        foreach ((Node next, float cost) in current.Neighbors) // ...begin looping through available Neighboring Nodes and their costs
        {
            float newCost = costSoFar[current] + cost; // New cost is the sum of the cost so far plus the cost of the next Node
            // "Have we not been here before OR have we been here, but the new cost is less than the cost so far?"
            if (! costSoFar.ContainsKey(next) || newCost < costSoFar[next])
            {
                frontier.Enqueue(next, newCost); // ...it is queued into the frontier...
                cameFrom[next] = current; // ...and marked as the new current
                costSoFar[next] = newCost; // Accrue cost
            }
        }
    }
    return cameFrom; // Returns a dictionary of Nodes representing the vector field
}
*/

// A* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Euclidean Distance - a Heuristic implemented for A*
float Heuristic (Node start, Node goal)
{
    return Vector3.Distance(start.Position, goal.Position);
}

// Checks for all Nodes which are connected, or reachable, starting with...
Dictionary<Node, Node> VectorField_AStar (Node start, Node goal) // A starting Node and a goal Node
{
    PriorityQueue<Node> frontier = new PriorityQueue<Node>(); // Queues Nodes, then dequeues the Node with the lowest "cost"...
    HashSet<Node> visited = new HashSet<Node>(); // ...and adds encountered Nodes to the hashset
    Dictionary<Node, float> costSoFar = new Dictionary<Node, float>(); // Associates each Node with a "cost", a float

    frontier.Enqueue(start, 0); // Enqueue and...
    cameFrom[start] = null; // Marks the origin Node
    costSoFar[start] = 0; // The cost to move starts at zero

    while (frontier.Any()) // While the frontier is not empty...
    {
        Node current = frontier.Dequeue(); // Dequeue the current Node for processing below
        if (current == goal) // If the current Node is the goal Node, exit early
            break;
    
        foreach ((Node next, float cost) in current.Neighbors) // ...begin looping through available Neighboring Nodes and their costs
        {
            float newCost = costSoFar[current] + cost; // New cost is the sum of the cost so far plus the cost of the next Node
            // "Have we not been here before OR have we been here, but the new cost is less than the cost so far?"
            if (! costSoFar.ContainsKey(next) || newCost < costSoFar[next])
            {
                frontier.Enqueue(next, newCost + Heuristic(next, goal)); // ...it is queued into the frontier...
                cameFrom[next] = current; // ...and marked as the new current
                costSoFar[next] = newCost; // Accrue cost
            }
        }
    }
    return cameFrom; // Returns a dictionary of Nodes representing the vector field
}

// Pathfinding /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Reconstructs the path so that the agent may trace it, using the tree built from BFS
List<Node> FindPath (Node start, Node goal, Dictionary<Node, Node> from)
{
    if (! from.ContainsKey(goal) // If no path is found, i.e. the goal is not within the dictionary...
        return null; // ...returns null, as the goal can't be reached

    List<Node> path = new List<Node>(); // Begin the list of Nodes within the path being built...
    Node current = goal; // ...starting from the goal Node

    // Starts from the goal Node and traces the dictionary back to the start to find the optimal path
    while (current != start) // While the currently observed Node is not the starting Node...
    {
        path.Add(current); // ...add this Node to the path
        current = from[current];
    }

    path.Add(start);
    path.Reverse(); // Reverses the array, since it was built from goal to start

    return path; // Returns the constructed and reversed path, now a complete path from start to goal
}

// Final Path Construction /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public List<Node> BFS (Node start, Node goal)
{
    Dictionary<Node, Node> from = VectorField_BFS (start, goal); // Calculate the vector field, store the information in a dictionary...
    List<Node> path = FindPath (start, goal, from); // ...and use the dictionary to construct a path in reverse, then reverses the path

    return path; // Returns the complete constructed path
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
