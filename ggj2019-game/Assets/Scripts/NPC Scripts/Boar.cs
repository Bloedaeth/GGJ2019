using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boar : MonoBehaviour
{
	GameObject dragon;
	//private Health health;

		//Temporary variable for distance away the dragon is
	private float dragonDistance;

	[SerializeField] private float speed = 0.05f;

	private float timeToMove = 0f;

	private Vector3 moveDirection;

    private void Start()
    {
        dragon = FindObjectOfType<DragonControls>().gameObject;
    }

    private void Awake()
	{
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
	}

	private void Update()
	{
		timeToMove -= Time.deltaTime;
		dragonDistance = System.Math.Abs(dragon.transform.position.x - this.transform.position.x);
		if ( (dragonDistance <= 10) && (dragon.transform.position.y < 4))
		{
            speed = 0.09f;
            //rotate to look at the dragon
            if (dragon.transform.position.x - this.transform.position.x < 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 270);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 90);
            }

            //moveDirection.x = (dragon.transform.position.x - this.transform.position.x)/ System.Math.Abs(dragon.transform.position.x - this.transform.position.x);
            moveDirection = Vector3.forward;
            timeToMove = 2.0f;
		}
        else
        {
            speed = 0.05f;
        }

		if(timeToMove <= 0f)
			TryMove();

		transform.Translate(moveDirection * speed);
	}

	private void TryMove()
	{
		float stopChance = Random.Range(0f, 1f);
		if(stopChance < 0.7f)
		{
			timeToMove = 2f;
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
		//transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y *dirMultiplier, transform.localScale.z);
	}
}
