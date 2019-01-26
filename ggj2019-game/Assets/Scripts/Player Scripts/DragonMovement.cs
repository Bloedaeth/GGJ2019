using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragonMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float flyForce = 10.0f;

    private bool canFly = true;

    public float flightPower = 100;
    public float increaseRate = 0.5f;
    public float decreaseRate = 1.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetKeyPress();
    }

    private void GetKeyPress() {
        
        float v = Input.GetAxis("Horizontal");
        float h = Input.GetAxis("Vertical");

        if (!canFly)
        {
            h *= 0;
        }
        // fly only when w pressed
        if (Input.GetKey(KeyCode.W))
            Fly();

        transform.position += new Vector3(v * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void Fly() {
        rb.AddForce(Vector3.up * flyForce, ForceMode.Force);

        // ToDo
        // add limit to fly ability

    }
}
