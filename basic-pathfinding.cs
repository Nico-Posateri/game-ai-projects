// A "zoomed-in" look at the theoretical pathfinding of an NPC.
// Some of the functions used in this pathfinding algorithm do not exist as a result.
// This is simply meant to showcase the basic elements of pathfinding, courtesy of Alan Zucconi.

// Node class: Lists the connections between Nodes, which an agent can use for navigation //////////////

public class Node
{
    public Vector3 Position;
    // ...
    public List<Node> Neighbors;
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

// Data Structures /////////////////////////////////////////////////////////////////////////////////////

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

// Reachability Algorithm //////////////////////////////////////////////////////////////////////////////

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

// Breadth-First Search ////////////////////////////////////////////////////////////////////////////////

Dictionary<Node, Node> VectorField_BFS (Node start, Node goal)
{
    Queue<Node> frontier = new Queue<Node>();
    HashSet<Node> visited = new HashSet<Node>();
    Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

    frontier.Enqueue(start);
    visited.Add(start);
    cameFrom[start] = null;

    while (frontier.Any())
    {
        Node current = frontier.Dequeue();
        if (current == goal)
            break;
    
        foreach (Node next in current.Neighbors)
        {
            if (! visited.Contains(next))
            {
                frontier.Enqueue(next);
                visited.Add(next);
            }
        }
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////
