using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureCounter : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    static int CountTreasure() //Call this when you enter the cave and when regurgitating
    {
        int count = 0;
        foreach(Treasure treasure in FindObjectsOfType<Treasure>())
        {
            count += treasure.value;
        }
        return count;
    }
}
