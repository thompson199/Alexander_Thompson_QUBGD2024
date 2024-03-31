using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] animalPrefabs;

    // Spawn position variables
    private const float SpawnRangeX = 20;
    private const float SpawnPosY = 0;
    private const float SpawnPosZ = 25;

    // Spawn timing variables
    private const float StartDelay = 2;
    private const float SpawnInterval = 1.5f;
    
    // Start is called before the first frame update
    private void Start()
    {
        // Repeatedly call "SpawnRandomAnimal" function after start delay, and after each interval has elapsed
        InvokeRepeating(nameof(SpawnRandomAnimal), StartDelay, SpawnInterval);
    }

    // Update is called once per frame
    private void Update()
    {
        // Do nothing atm
    }

    private void SpawnRandomAnimal()
    {
        var animalIndex = Random.Range(0, animalPrefabs.Length);
        var spawnPosition = new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX), SpawnPosY, SpawnPosZ);
        
        Instantiate(animalPrefabs[animalIndex], spawnPosition, animalPrefabs[animalIndex].transform.rotation);
    }
}
