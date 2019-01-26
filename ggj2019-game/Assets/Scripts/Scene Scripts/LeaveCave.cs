using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveCave : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //TODO change scene to overworld
            Debug.Log("Loading zone hit");
        }
    }
}
