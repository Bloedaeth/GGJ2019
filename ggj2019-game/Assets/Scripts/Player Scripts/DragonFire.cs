using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    public GameObject firePrefab;
    public AudioClip fireSound;

    public GameObject jaw;

    private AudioSource source;
    private KeyCode fireKey;

    [SerializeField] private Animator anim;

    public float fireDamage;
    public float damageRate;
    private float damageTimer;

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
            anim.SetInteger("Mouth", 1);

            source.loop = true;   
            source.PlayOneShot(fireSound);
        }
        if (Input.GetKeyUp(fireKey))
        {
            anim.SetInteger("Mouth", -1);

            firePrefab.GetComponent<ParticleSystem>().Stop();
            source.loop = false;
            source.Stop();
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        damageTimer = Time.time;
        if (other.transform.tag == "Enemy" && damageTimer < damageRate)
        {
            damageTimer = 0;
            other.GetComponent<Health>().Damage(fireDamage);
        }
    }
}
