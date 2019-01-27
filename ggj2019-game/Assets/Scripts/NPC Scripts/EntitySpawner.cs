using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class EntitySpawner : MonoBehaviour
{
    [Tooltip("Maximum range from the player entites will spawn")]
    public float maxRange;

	private ObjectPooler entityPool;
	private float dist;

    [Space]
    public GameObject homePoint;
	
    private bool side;

	private void Awake()
	{
		entityPool = GetComponent<ObjectPooler>();
	}

    private void Spawn()
    {
        dist = Random.Range(0, maxRange);

		GameObject entity = entityPool.GetPooledObject();
		if(entity == null)
			return;

		if(side)
        {
			entity.transform.position = new Vector3(homePoint.transform.position.x + dist, 0, 0);
			entity.SetActive(true);
			side = !side;
        }
        else
        {
			entity.transform.position = new Vector3(homePoint.transform.position.x - dist, 0, 0);
			entity.SetActive(true);
			side = !side;
        }
    }
}