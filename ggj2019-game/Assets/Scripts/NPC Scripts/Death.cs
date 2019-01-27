using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject[] DroppedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        foreach (GameObject drop in DroppedItem)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
