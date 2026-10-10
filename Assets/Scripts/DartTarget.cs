using UnityEngine;
using TMPro; 

public class DartTarget : MonoBehaviour
{
    public TextMeshProUGUI textTotalScore;
    public TextMeshProUGUI textLastThrow;

    private int totalScore = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            Vector3 impactPoint = collision.contacts[0].point;
            Vector3 targetCenter = transform.position;
            float distance = Vector3.Distance(impactPoint, targetCenter);

            int points = 0;
            if (distance < 0.05f) points = 50; // Bullseye
            else if (distance < 0.15f) points = 25;
            else if (distance < 0.3f) points = 10;
            else if (distance < 0.5f) points = 5;

            totalScore += points;

            if (textLastThrow != null) textLastThrow.text = "Last Throw: " + points;
            if (textTotalScore != null) textTotalScore.text = "Total Score: " + totalScore;

            Rigidbody rbDart = collision.gameObject.GetComponent<Rigidbody>();
            if (rbDart != null)
            {
                rbDart.isKinematic = true; 
            }

            collision.transform.SetParent(transform);
        }
    }
}