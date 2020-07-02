using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    public List<GameObject> gameObjectsToSpawn = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject whereToSpawn = gameObjectsToSpawn[Random.Range(0, gameObjectsToSpawn.Count)];
        Instantiate(whereToSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
