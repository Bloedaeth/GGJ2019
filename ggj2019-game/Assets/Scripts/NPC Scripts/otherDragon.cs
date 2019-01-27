using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherDragon : MonoBehaviour
{

    private Health health;

    private int treasure;

    private bool aggressive;

    private bool flyAway;

    public GameObject baby_egg;
    // Start is called before the first frame update
    void Start()
    {
        treasure = 0;
        aggressive = false;
        flyAway = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (treasure >= 10)
        {
            LayEgg();
        }
        if (flyAway == true)
        {
            transform.Translate(Vector3.up * 0.2f);
        }
    }


    void LayEgg()
    {
        Instantiate(baby_egg, transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        treasure -= 10;
        flyAway = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Treasure")
        {
            Destroy(other.gameObject);
            treasure++;
            Debug.Log(treasure);
        }
    }

}
