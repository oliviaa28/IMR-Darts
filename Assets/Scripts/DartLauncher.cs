using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DartLauncher : MonoBehaviour
{
    public float speed = 10f;

    private XRGrabInteractable grab;
    private Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

   
    void OnEnable()
    {
        grab.activated.AddListener(OnActivated);
        grab.selectEntered.AddListener(OnGrabbed);
        
    }

    void OnDisable()
    {
        grab.activated.RemoveListener(OnActivated);
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        grab.throwOnDetach = true;
    }

    void OnActivated(ActivateEventArgs args)
    {
        //we stopp the normal way of trowing
        grab.throwOnDetach = false;

        grab.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grab);

        rb.linearVelocity = transform.up * speed;
        rb.angularVelocity = Vector3.zero;
    }
}
