using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    public Collider mouthCollider; //The area in which biting will work
    public GameObject GrabbedItem; 
    List<GameObject> swallowedObjects = new List<GameObject>();
    DragonGrowth dragonGrowth;
    KeyCode biteControl;
    bool mouthOpen; //Is the mouth open?
    [HideInInspector] public float biteDamage;
    public GameObject objectInMouth;
    Rigidbody body;
    bool isStomachFull = false;
    bool isInLair = false;

	Vector3 regurgitateForce;

    public bool MouthOpen
    {
        get
        {
            return mouthOpen;
        }
        set
        {
            if (mouthOpen == true && value == false)
            {
                Chomp(objectInMouth);
            }
            else if (mouthOpen == false && value == true && GrabbedItem != null)
            {
                LetGo();
            }

            mouthOpen = value;
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dragonGrowth = GetComponent<DragonGrowth>();
		biteControl = GetComponent<DragonControls>().biteControl;
        body = GetComponent<Rigidbody>();
        FindMouth();
		regurgitateForce = Vector3.right * 5f; //TODO temp value
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(biteControl))
        {
            MouthOpen = true;
        }
        else
        {
            MouthOpen = false;
        }

        //Test regurgitation
        if (Input.GetKeyDown(KeyCode.P))//GetComponent<DragonControls>().regurgitateTest))
        {
            Regurgitate();
        }
    }

    private void FixedUpdate()
    {
        objectInMouth = null;
    }
    

    //Figure out what behaviour to perform out of Grab(), Swallow(), Crunch()
    void Chomp(GameObject other)
    {
        //Debug.Log("Bit " + other);
        //Is the mouth full though?
        if(GrabbedItem != null)
        {
            return;
        }

        //WAIT! Is there even a thing to chomp?
        if(objectInMouth == null)
        {
            return;
        }

        //If in lair and not empty, regurgitate
        if (isInLair)
        {
            Regurgitate();
            return;
        }

        //Crunch if enemy
        if(other.transform.tag == "Enemy")
        {
            Crunch(other);
            return;
        }
        //Is it bitable?
        else if (other.transform.tag == "Food" || other.transform.tag == "Treasure")
        {
            //Alright, small enough to swallow? If not, is it small enough to grab?
            Rigidbody otherBody = other.GetComponent<Rigidbody>();
            if(otherBody.mass < body.mass / 4)
            {
                Swallow(other);
                return;
            }
            else if (otherBody.mass < body.mass / 2)
            {
                Grab(other);
                return;
            }
        }
    }

    //Damage the bitten object
    void Crunch(GameObject other)
    {
        other.GetComponent<Health>().Damage(biteDamage);
        Debug.Log("Cruched for " + biteDamage + " damage.");
        //TODO allow eating alive if big enough
    }

    void Grab(GameObject other)
    {
        if (GrabbedItem == null)
        {
            Rigidbody otherBody = other.GetComponent<Rigidbody>();
            
            /* 
            //Glue the object to the dragon
            foreach(ContactPoint contact in hit.contacts)
            {
                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.anchor = contact.point;
                fixedJoint.connectedBody = hit.rigidbody; 
             }*/
            GrabbedItem = other;
            other.transform.SetParent(transform);
            otherBody.isKinematic = true;
            otherBody.detectCollisions = false;

        }
    }

    void LetGo()
    {
        Rigidbody otherBody = GrabbedItem.GetComponent<Rigidbody>();
        otherBody.isKinematic = false;
        otherBody.detectCollisions = true;
        otherBody.transform.SetParent(null);
    }

    void Swallow(GameObject other)
    {
        //Wait! Are you full?
        if(isStomachFull == true)
        {
            return; //TODO give feedback
        }

        if (other.GetComponent<Food>() != null)
        {
            Digest(other.GetComponent<Food>());
            Destroy(other);
        }

        if (other.transform.tag == "Treasure")
        {
            swallowedObjects.Add(other);
            other.gameObject.SetActive(false);
        }

        //TODO allow eating in one bite, and ingesting drops therefore
        /*
        GameObject otherDrop = null;
        if (other.GetComponent<Death>() != null)
        {
            otherDrop = other.GetComponent<Death>().DroppedItem;
        }

        //TODO make this logic better. not everything should go in the stomach.
        if (otherDrop == null)
        {
            swallowedObjects.Add(other);
            other.gameObject.SetActive(false);
        }
        else
        {
            swallowedObjects.Add(Instantiate(otherDrop));
            otherDrop.gameObject.SetActive(false);
        }      */  

    }

    void Digest(Food food)
    {
        dragonGrowth.Grow(food.GrowthValue);
    }

    //TODO Animate regurgitation
    void Regurgitate()
    {
        if (swallowedObjects.Count != 0)
        {
            GameObject regurgitatedItem;
            regurgitatedItem = swallowedObjects[swallowedObjects.Count - 1];
            regurgitatedItem.SetActive(true);
            //This is already an instantiated object (see Swallow() method)
            //regurgitatedItem = Instantiate(regurgitatedItem);


            regurgitatedItem.transform.position = mouthCollider.transform.position;
            Rigidbody itemRB = regurgitatedItem.GetComponent<Rigidbody>();
            float facingDirection = mouthCollider.gameObject.transform.forward.z;
            itemRB.AddForce(regurgitateForce * facingDirection);

            swallowedObjects.RemoveAt(swallowedObjects.Count - 1);
        }
    }

    void FindMouth()
    {
        mouthCollider = GameObject.Find("Mouth").GetComponent<Collider>();
    }
}
