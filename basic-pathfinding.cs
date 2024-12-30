// A "zoomed-in" look at the theoretical pathfinding of an NPC.
// Some of the functions used in this pathfinding algorithm do not exist as a result.
// This is simply meant to showcase the basic elements of pathfinding, courtesy of Alan Zucconi.

// Node class: Lists the connections between Nodes, which an agent can use for navigation /////////

public class Node
{
  public Vector3 Position;
  // ...
  public List<Node> Neighbors;
}

// A graph of Nodes and connections, where connected Nodes are "Neighbors" ////////////////////////

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

// Data Structures ////////////////////////////////////////////////////////////////////////////////

// ...

///////////////////////////////////////////////////////////////////////////////////////////////////
