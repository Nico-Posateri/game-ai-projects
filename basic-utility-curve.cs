// A "zoomed-in" look at the animation curve of an AI agent, a component designed for Unity, courtesy of Alan Zucconi.

public class Agent : MonoBehaviour
{
    public float Health;
    public AnimationVurve Curve; // Produces a box in the inspector called "Health" which displayes the utility curve for an agent's health

    public void Update ()
    {
        float utility = Curve.Evaluate(Health);
        Debug.Log(utility);
    }
}
