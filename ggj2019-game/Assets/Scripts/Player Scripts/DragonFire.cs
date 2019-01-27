using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    public GameObject firePrefab;
    public AudioClip fireSound;

    private AudioSource source;
    private KeyCode fireKey;

    private void Awake()
    {
        fireKey = GetComponent<DragonControls>().breathFireControl;
        firePrefab.GetComponent<ParticleSystem>().Stop();

        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKey(fireKey))
        {
            firePrefab.GetComponent<ParticleSystem>().Play();
        }
        if (Input.GetKeyDown(fireKey))
        {
            source.pitch = 0.2f;
            source.loop = true;
            
            source.PlayOneShot(fireSound);
        }
        if (Input.GetKeyUp(fireKey))
        {
            firePrefab.GetComponent<ParticleSystem>().Stop();
            source.pitch = 1f;
            source.loop = false;
            source.Stop();
        }
    }
}
