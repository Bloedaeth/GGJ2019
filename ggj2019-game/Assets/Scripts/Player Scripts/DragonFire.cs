using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    public GameObject firePrefab;

    private KeyCode fireKey;

    private void Awake()
    {
        fireKey = GetComponent<DragonControls>().breathFireControl;
        firePrefab.GetComponent<ParticleSystem>().Stop();
    }

    void Update()
    {
        if(Input.GetKey(fireKey))
        {
            firePrefab.GetComponent<ParticleSystem>().Play();
        }
        if (Input.GetKeyUp(fireKey))
        {
            firePrefab.GetComponent<ParticleSystem>().Stop();
        }
    }
}
