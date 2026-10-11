using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DartFlight : MonoBehaviour
{

    private Rigidbody rb;
    private XRGrabInteractable grab;
   
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

    void FixedUpdate()
    {
        if (grab.isSelected){
            return;
        }

        if (rb.isKinematic) { 
            return;
        }

        if (rb.linearVelocity.magnitude < 3f) { 
            return;
        }

        //rotate the point depending on the flying
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, rb.linearVelocity);

        rb.MoveRotation(rotation);
        
    }
}
