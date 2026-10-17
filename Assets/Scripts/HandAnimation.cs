using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    public InputActionReference gripAction;
    public InputActionReference triggerAction;

    public float speed = 5f;

    private Animator animator;
    private float currentGrip = 0f;
    private float currentTrigger = 0f;

    private Key testGripKey;
    private Key testTriggerKey;

    void Start()
    {
        animator = GetComponent<Animator>();

        string mapName = gripAction.action.actionMap.name;

        if (mapName.Contains("Left"))
        {
            testGripKey = Key.C;
            testTriggerKey = Key.V;
        }
        else
        {
            testGripKey = Key.B;
            testTriggerKey = Key.N;
        }
    }

    void OnEnable()
    {
        gripAction.action.Enable();
        triggerAction.action.Enable();
    }

    void Update()
    {
        float targetGrip = gripAction.action.ReadValue<float>();
        float targetTrigger = triggerAction.action.ReadValue<float>();

        if (Keyboard.current != null)
        {
            if (Keyboard.current[testGripKey].isPressed)
            {
                targetGrip = 1f;
            }

            if (Keyboard.current[testTriggerKey].isPressed)
            {
                targetTrigger = 1f;
            }
        }

        currentGrip = Mathf.MoveTowards(currentGrip, targetGrip, speed * Time.deltaTime);
        currentTrigger = Mathf.MoveTowards(currentTrigger, targetTrigger, speed * Time.deltaTime);

        animator.SetFloat("Grip", currentGrip);
        animator.SetFloat("Trigger", currentTrigger);
    }
}