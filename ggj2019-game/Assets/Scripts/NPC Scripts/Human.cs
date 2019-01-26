using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    //private Health health;
    public GameObject dragon;
    public ObjectPooler arrowPool;
    private float dragonDistance;
    [SerializeField] private float speed = 0.07f;

	private float timeToMove = 0f;

	private Vector3 moveDirection;

    private float timeToReload = 0f;
	private void Awake()
	{
        arrowPool = FindObjectOfType<ObjectPooler>();

		timeToMove = Random.Range(1f, 3.5f);

		float direction = Random.Range(0f, 1f) < 0.5 ? -1 : 1;
		moveDirection = Vector3.forward * direction;
	}

	private void Update()
	{

		timeToMove -= Time.deltaTime;
		if(timeToMove <= 0f)
			TryMove();
        timeToReload -= Time.deltaTime;
        //get the hypotenuse between the dragon and the human
        dragonDistance = (float)System.Math.Sqrt(  System.Math.Pow(dragon.transform.position.x - this.transform.position.x, 2) + System.Math.Pow(dragon.transform.position.y - this.transform.position.y, 2));
        //if youre close enough, the human will stop and shoot arrows at you
        if (dragonDistance <= 15)
        {
            //face the dragon
            if (dragon.transform.position.x - this.transform.position.x < 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 270);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 90);
            }

            if (timeToReload <= 0)
            {
                timeToReload = 5f;
                Arrow arrow = arrowPool.GetPooledObject().GetComponent<Arrow>();
                arrow.Shoot(dragon.transform, this);
            }
        }
        else
        {
            transform.Translate(moveDirection * speed);
        }
    }

	private void TryMove()
	{
		float stopChance = Random.Range(0f, 1f);
		if(stopChance < 0.4f)
		{
			//Humans stop and stare into the distance, right?
			timeToMove = 5f;
			moveDirection = Vector3.zero;
			return;
		}

		timeToMove = Random.Range(1f, 3.5f);

        float dirMultiplier = Random.Range(0f, 1f) < 0.5 ? -1 : 1;
        if (dirMultiplier == -1)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 270);
        }


        moveDirection = Vector3.forward;
		//transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * dirMultiplier, transform.localScale.z);
	}

    //private void Shoot(float moveTowardsX, float moveTowardsY, Human human)
    //{
    //    Arrow newArrow = Instantiate(arrow).GetComponent<Arrow>();        
    //}
}
