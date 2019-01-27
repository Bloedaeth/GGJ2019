using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField] Bite bite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider hit)
    {
        if(hit.transform.tag == "Food" || hit.transform.tag == "Treasure" || hit.transform.tag == "Enemy")
        bite.objectInMouth = hit.gameObject;
    }
}
