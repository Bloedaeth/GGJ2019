using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    public Collider MouthCollider; //The area in which biting will work
    public GameObject GrabbedItem; 
    List<GameObject> swallowedObjects;
    DragonGrowth dragonGrowth;
    KeyCode biteControl;
    bool mouthOpen; //Is the mouth open?
    public float biteDamage;
    GameObject objectInMouth;
    Rigidbody body;
    bool isStomachFull = false;

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
    }

    private void FixedUpdate()
    {
        objectInMouth = null;
    }

    private void OnTriggerStay(Collider hit)
    {
        objectInMouth = hit.gameObject;
    }

    //Figure out what behaviour to perform out of Grab(), Swallow(), Crunch()
    void Chomp(GameObject other)
    {
        //Is the mouth full though?
        if(GrabbedItem != null)
        {
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
        }

        GameObject otherDrop = null;
        if (other.GetComponent<Death>() != null)
        {
            otherDrop = other.GetComponent<Death>().DroppedItem;
        }

        if (otherDrop == null)
        {
            swallowedObjects.Add(other);
            other.gameObject.SetActive(false);
        }
        else
        {
            swallowedObjects.Add(Instantiate(otherDrop));
            otherDrop.gameObject.SetActive(false);
        }        
    }

    void Digest(Food food)
    {
        dragonGrowth.Grow(food.GrowthValue);
    }

    //TODO Animate regurgitation
    void Regurgitate()
    {
        GameObject regurgitatedItem;
        regurgitatedItem = swallowedObjects[swallowedObjects.Count - 1];

		//This is already an instantiated object (see Swallow() method)
		//regurgitatedItem = Instantiate(regurgitatedItem);

		regurgitatedItem.transform.position = MouthCollider.transform.position;
		Rigidbody itemRB = regurgitatedItem.AddComponent<Rigidbody>();
		float facingDirection = MouthCollider.gameObject.transform.forward.z;
		itemRB.AddForce(regurgitateForce * facingDirection);

        swallowedObjects.RemoveAt(swallowedObjects.Count - 1);
    }

    void FindMouth()
    {
        MouthCollider = GameObject.Find("Mouth").GetComponent<Collider>();
    }
}
