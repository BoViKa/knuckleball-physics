using UnityEngine;

public class Testing : MonoBehaviour
{
    public float launchForce = 10;
    public float spinForce = 5;
    public float radius = 0.5f;
    public float airDensity = 1.2f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0.5f, 1, 0) * launchForce);
            rb.AddTorque(new Vector3(0, 1, 0) * spinForce, ForceMode.Force);
        }

        Debug.Log(rb.angularVelocity);
    }

    void FixedUpdate()
    { 
        var direction = Vector3.Cross(rb.angularVelocity, rb.linearVelocity);
        var magnitude = 4 / 3f * Mathf.PI * airDensity * Mathf.Pow(radius, 3);
        rb.AddForce(magnitude * direction);
    } 
}
