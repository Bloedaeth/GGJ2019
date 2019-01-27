using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceBackToGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (transform.position.y < 0)
        {
            transform.position.Set(transform.position.x, 5, transform.position.z);
        }   
    }
}
