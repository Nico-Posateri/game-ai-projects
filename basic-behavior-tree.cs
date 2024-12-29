// A "zoomed-in" look at the theoretical BT of an NPC.
// Some of the functions used in this Behavior Tree do not exist as a result.
// This is simply meant to showcase the basic elements of a Behavior Tree, courtesy of Alan Zucconi.

// Node class: A class representing all nodes of the Behavior Tree
public abstract class Node
{
    // The execute method for a specific node of the tree, if returned True
    public abstract bool Execute ();
}

// Task class: Extension of the Node class, executes the desired Task if possible
public class Task : Node
{
    public bool Execute ()
    {
        // The code needed to perform the desired task of your agent, returns True or False
    }
}

// Sequence class: Extension of the Node class, looks for the first Node which fails
public class Sequence : Node
{
    // List of all the Nodes
    public List<Node> Nodes;

    // Returns True or False based on the conditions which makes the sequence succeed or fail
    public bool Execute ()
    {
        // Loops through and executes all children Nodes until one fails
        foreach (Node child in Nodes)
            if (! child.Execute())
                return false;
        // If all Nodes succeed...
        return true;
    }
}

// Selector class: Extension of the Node class, looks for the first Node which succeeds
public class Selector : Node
{
    // List of all the Nodes
    public List<Node> Nodes;

    // Returns True or False based on the conditions which makes the sequence succeed or fail
    public bool Execute ()
    {
        // Loops through and executes all children Nodes until one succeeds
        foreach (Node child in Nodes)
            if (child.Execute())
                return true;
        // If all Nodes fail...
        return false;
    }
}
