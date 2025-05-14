using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code from https://discussions.unity.com/t/magnus-effect-for-tennis-ball-physics-simulation/890415/3 
// Provided by user arkano22

[RequireComponent(typeof(Rigidbody))]
public class MagnusEffect : MonoBehaviour
{
    private float radius = 0.11f;  // Ball radius in meters
    private float airDensity = 1.225f;  // Air density (kg/m^3)

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var direction = Vector3.Cross(rb.angularVelocity, rb.linearVelocity);
        var magnitude = 4 / 3f * Mathf.PI * airDensity * Mathf.Pow(radius, 3);
        rb.AddForce(magnitude * direction);
    }
}
