using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
	//private Health health;

	[SerializeField] private float speed = 0.1f;

	private float timeToMove = 0f;

	private Vector3 moveDirection;

	private void Awake()
	{
		timeToMove = Random.Range(1f, 3.5f);

		float direction = Random.Range(0f, 1f) < 0.5 ? -1 : 1;
		moveDirection = Vector3.right * direction;
	}

	private void Update()
	{
		Debug.Log(timeToMove);
		timeToMove -= Time.deltaTime;
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
		moveDirection = Vector3.right * dirMultiplier;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y *dirMultiplier, transform.localScale.z);
	}
}
