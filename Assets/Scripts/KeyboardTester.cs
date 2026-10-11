using UnityEngine;

public class KeyboardTester : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform targetPosition;
    public float throwForce = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {LaunchDart();
        }
    }

    void LaunchDart()
    {
        GameObject newDart = Instantiate(dartPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (targetPosition.position - transform.position).normalized;

        Rigidbody rb = newDart.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
        }
    }
}