using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entity;
    [Tooltip("Maximum amount of entities in game at any given time")]
    public int maxEntities;
    [Tooltip("Maximum range from the player entites will spawn")]
    public float maxRange;
    public GameObject spawnPoint;
    
    private float dist;

    [Space]
    public GameObject player;

    public int curEntities;
    public List<GameObject> entities = new List<GameObject>();
    private bool side;

    void Update()
    {
        curEntities = entities.Count;    

        if (curEntities < maxEntities){
            Spawn();
        }

        // Debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RemoveEntity(0);
        }
    }

    private void Spawn()
    {
        dist = spawnPoint.transform.position.x + Random.Range(0, maxRange);

        if (side)
        {
            GameObject o = Instantiate(entity, new Vector3(player.transform.position.x + dist, 0, 0), Quaternion.identity);
            entities.Add(o);
            side = !side;
        }
        else
        {
            GameObject o = Instantiate(entity, new Vector3(player.transform.position.x - dist, 0, 0), Quaternion.identity);
            entities.Add(o);
            side = !side;
        }
    }

    private void RemoveEntity(int entityIndex) 
    {
        entities.Remove(entities[entityIndex]);
        Destroy(entities[entityIndex]);
    }
}