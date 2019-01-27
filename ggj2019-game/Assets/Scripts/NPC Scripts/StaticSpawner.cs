using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class StaticSpawner : MonoBehaviour
{
    [Tooltip("The maximum range from the spawner that entites will spawn")]
    public float maxRange;

	private ObjectPooler entityPool;
	private Transform playerTransform;

	[SerializeField] private float maxDistanceForSpawning = 150f;
	[SerializeField] private float timeBetweenSpawns = 7f;
	private float timeUntilNextSpawn;

	private void Awake()
	{
		entityPool = GetComponent<ObjectPooler>();
		playerTransform = FindObjectOfType<DragonControls>().transform;
		timeUntilNextSpawn = timeBetweenSpawns;
	}

	private void Update()
	{
		timeUntilNextSpawn -= Time.deltaTime;

		float distToPlayer = Vector3.Distance(playerTransform.position, transform.position);
		if(timeUntilNextSpawn <= 0 && distToPlayer < maxDistanceForSpawning)
			TrySpawn();
	}

	private bool TrySpawn()
	{
		timeUntilNextSpawn = timeBetweenSpawns;

		GameObject entity = entityPool.GetPooledObject();
		if(entity == null)
			return false;

		entity.transform.position = new Vector3(transform.position.x + Random.Range(-maxRange, maxRange), 0, 0);

		Vector3 screenPoint = Camera.main.WorldToViewportPoint(entity.transform.position);
		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

		if(onScreen)
		{
			entity.SetActive(true);
			return true;
		}

		return false;
	}
}