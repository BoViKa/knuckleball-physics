using System;
using UnityEngine;

public class KnuckleForceNoise : MonoBehaviour
{
    private Rigidbody rb;
    public float forceAmplitude = 12f;  // Max force strength
    private Vector3 lastPosition;
    private float totalDistance;
    private float smoothedZForce = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = rb.position;
        totalDistance = 0f;
        smoothedZForce = 0f;
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

                // Perlin noise (air turbulence)
                float noise = Mathf.PerlinNoise(totalDistance * 0.2f, 0f) - 0.5f;
                float noise2 = Mathf.PerlinNoise(totalDistance * 0.6f + 42f, 0f) - 0.5f;
                float turbulence = noise + noise2;

                float zForce = turbulence * forceAmplitude * forceMultiplier;

                // Apply the force
                rb.AddForce(new Vector3(0f, 0f, zForce));
            }
        }
    }
}
