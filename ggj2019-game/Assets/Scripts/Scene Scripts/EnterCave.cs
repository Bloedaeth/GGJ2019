using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //TODO change scene to cave if button is pressed
            //Also probably give a prompt for the button
            Debug.Log("Loading zone hit");
        }
    }
}
