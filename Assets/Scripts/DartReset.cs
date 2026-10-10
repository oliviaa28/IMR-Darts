using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DartReset : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;
    private XRGrabInteractable grab;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb= GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (Keyboard.current == null ){
            return;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame ){
            ResetDart();
        }
    }

    public void ResetDart()
    {

        if (grab.isSelected){
            return;
        }


        transform.SetParent(null);
        rb.isKinematic = false;

        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}