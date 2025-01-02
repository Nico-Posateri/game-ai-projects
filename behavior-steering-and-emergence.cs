// A "zoomed-in" look at the theoretical behavior steering and emergent behaviors of an NPC.
// Some of the elements referenced in these algorithms do not exist as a result.
// This is simply meant to showcase the basic elements of emergent behavior and behavior steering, courtesy of Alan Zucconi.

// Seeking ////////////////////////////////////////////////////////////////

public Rigidbody Rigidbody;
public Transform Target;

public float Speed;
public float MaxForce;

void FixedUpdate ()
{
    Vector3 P = transform.position;
    Vector3 T = Target.position;

    Vector3 U_hat = (T - P).normalized;
    Vector3 U = U_hat * Speed;

    Vector3 V = Rigidbody.velocity;
    Vector3 dV = U - V;
    Vector3 F = Vector3.ClampMagnitude(dV, MaxForce);
    Rigidbody.AddForce(F);
}

// Fleeing ////////////////////////////////////////////////////////////////