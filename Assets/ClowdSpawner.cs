using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float spawnRate = 2f;
    public float xSpread = 3f; 
    public float fixedYSpawn = 2f; 

    private float _nextSpawnX = 15f; 

    void Start()
    {
        InvokeRepeating(nameof(SpawnCloud), 0f, spawnRate);
    }

    private void SpawnCloud()
    {
        Vector3 spawnPos = new Vector3(
            _nextSpawnX + Random.Range(-xSpread, xSpread),
            fixedYSpawn,
            0
        );

        Instantiate(cloudPrefab, spawnPos, Quaternion.identity);
        
        
        _nextSpawnX += Random.Range(3f, 6f); 
    }
}