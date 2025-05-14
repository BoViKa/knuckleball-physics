using UnityEngine;

public class KnuckleForce : MonoBehaviour
{
    private Rigidbody rb;
    private float forceAmplitude = 4f;     // Max force strength
    private float wavelength = 20f;          // Meters per sine cycle
    private Vector3 lastPosition;
    private float totalDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = rb.position;
        totalDistance = 0f;
    }

    void FixedUpdate()
    {
        if (rb.angularVelocity.magnitude < 2){
            
            float speed = rb.linearVelocity.magnitude;

            if (speed >= 20 && speed <= 25){
                // Smooth blend factor between 0 and 1
                float blend = Mathf.InverseLerp(20f, 25f, speed);
                blend = Mathf.Clamp01(blend);

                // Track distance moved
                float distanceMoved = Vector3.Distance(rb.position, lastPosition);
                totalDistance += distanceMoved;
                lastPosition = rb.position;

                // Calculate sinusoidal force and apply blend
                float zForce = Mathf.Sin(2 * Mathf.PI / wavelength * totalDistance) * forceAmplitude * blend;

                // Apply the force
                rb.AddForce(new Vector3(0f, 0f, zForce));
            }
        }
    }
}
