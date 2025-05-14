using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ballPhysics : MonoBehaviour
{
    public int launchSpeed;
    public int launchAngle;
    public float sideSpin;
    public float backSpin;
    private Rigidbody rb;
    private Vector3 _originalPos;
    private bool _isLaunched;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20f;
        _originalPos = rb.transform.position;
        _isLaunched = false;
    }

    void Launch()
{
    // Convert angle to radians
    float angleRad = (launchAngle + 90) * Mathf.Deg2Rad;

    // Compute velocity components
    float xVelocity = launchSpeed * Mathf.Sin(angleRad);
    float yVelocity = launchSpeed * Mathf.Cos(angleRad);

    // Set the velocity
    rb.linearVelocity = new Vector3(xVelocity, yVelocity, 0);

    // Reset _isLaunched to false after 0.2 sec
    StartCoroutine(StopTorqueAfterDelay(0.2f)); 
}

    void ResetBall()
    {
        _isLaunched = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.transform.position = _originalPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Launch();
            _isLaunched = true;
        } 
        
        if (Input.GetKeyDown(KeyCode.Space)){
            ResetBall();
        }
    }

    void FixedUpdate()
{
    if (_isLaunched)
    {
        rb.AddTorque(new Vector3(0, sideSpin, backSpin + 0.25f));
    }
}

private IEnumerator StopTorqueAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    _isLaunched = false; // Stop applying torque after a delay
}
}
