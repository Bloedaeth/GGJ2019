using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private float height;
    // Start is called before the first frame update
    public void Shoot(Transform dragonPosition, Human human)
    {
        //GameObject arrow = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        transform.position = new Vector3(human.transform.position.x, human.transform.position.y, human.transform.position.z);
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, Vector3.forward   ) * Quaternion.LookRotation((dragonPosition.position - transform.position).normalized);
        var direction = (dragonPosition.position - transform.position).normalized;
        if (dragonPosition.position.y <=7)
        {
            float extraUpNeeded = System.Math.Abs(dragonPosition.position.x - transform.position.x);
            direction.y += 0.01f * extraUpNeeded;
        }

        transform.up = direction;
        rb = GetComponent<Rigidbody>();

        gameObject.SetActive(true);

        rb.AddRelativeForce(0, 20, 0, ForceMode.Impulse);
        
    }
    private void Start()
    {
        height = GetComponent<Collider>().bounds.size.y / 2f;
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward, -25 * Time.deltaTime);
    }
}
