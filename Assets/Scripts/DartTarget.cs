using UnityEngine;
using TMPro;

public class DartTarget : MonoBehaviour
{
    public TextMeshProUGUI textTotalScore;
    public TextMeshProUGUI textLastThrow;

    private int totalScore = 0;

    public float boardRadius = 0.5f;
    public float angleOffset = 0f;
    public float radiusInnerBull = 0.05f; // red bullseye
    public float radiusOuterBull = 0.15f; // green bullseye

    // Darts target points
    private int[] sections = { 20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5 };

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Dart"))
        {
            collision.gameObject.tag = "Untagged";

            Rigidbody rbDart = collision.gameObject.GetComponent<Rigidbody>();
            if (rbDart != null && rbDart.isKinematic == true)
            {
                return;
            }
            if (rbDart != null)
            {
                rbDart.isKinematic = true;
            }

            // take the impact point of the dart
            Vector3 impactPoint = collision.contacts[0].point;

            int points = ComputeDartsScore(impactPoint);
            totalScore += points;

            if (textLastThrow != null) textLastThrow.text = "Last Throw: " + points;
            if (textTotalScore != null) textTotalScore.text = "Total Score: " + totalScore;
        }
    }

    int ComputeDartsScore(Vector3 impactPoint)
    {
        Vector3 localPoint = transform.InverseTransformPoint(impactPoint);

        float x = - localPoint.x;
        float y = localPoint.z;

        float distance = new Vector2(x, y).magnitude;
        float normalizedDist = distance / boardRadius;

        if (normalizedDist > 1.0f) return 0;

        // Bullseye 
        if (normalizedDist <= radiusInnerBull) return 50; // Red inner bullseye
        if (normalizedDist <= radiusOuterBull) return 25;  // Green outer bullseye

        // impact angle
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        float dartAngle = 90f - angle + angleOffset;
        if (dartAngle < 0) dartAngle += 360f;

        float shiftedAngle = dartAngle + 9f;
        if (shiftedAngle >= 360f) shiftedAngle -= 360f;

        int sectorIndex = Mathf.FloorToInt(shiftedAngle / 18f);
        int baseScore = sections[sectorIndex];

        if (normalizedDist >= 0.55f && normalizedDist <= 0.62f) return baseScore * 3;
        if (normalizedDist >= 0.95f && normalizedDist <= 1.0f) return baseScore * 2;
            return baseScore;
    }
}