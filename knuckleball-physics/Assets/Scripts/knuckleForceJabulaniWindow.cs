using System;
using UnityEngine;

public class KnuckleForceJabulaniWindow : MonoBehaviour
{
    private Rigidbody rb;
    public float forceAmplitude = 4f;     // Max force strength
    private float wavelength = 20f;        // Meters per sine cycle
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

            if (speed > 2){  // No knuckle when speed is near 0

                // Normalized, inverse curve from Goff et al. (2014)
                float forceMultiplier = 1f / (1f + (float)Math.Pow(speed - 18f, 2f));

                // Track distance moved
                float distanceMoved = Vector3.Distance(rb.position, lastPosition);
                totalDistance += distanceMoved;
                lastPosition = rb.position;

                // Calculate sinusoidal force and apply blend
                float zForce = Mathf.Sin(2 * Mathf.PI / wavelength * totalDistance) * forceAmplitude * forceMultiplier;

                // Apply the force
                rb.AddForce(new Vector3(0f, 0f, zForce));
            }
        }
    }
}
