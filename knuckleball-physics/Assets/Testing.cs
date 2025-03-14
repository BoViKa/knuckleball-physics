using UnityEngine;

public class Testing : MonoBehaviour
{
    public float radius = 0.5f;
    public float airDensity = 0.1f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var direction = Vector3.Cross(rb.angularVelocity, rb.linearVelocity);
        var magnitude = 4 / 3f * Mathf.PI * airDensity * Mathf.Pow(radius, 3);
        rb.AddForce(magnitude * direction);
    }
}
