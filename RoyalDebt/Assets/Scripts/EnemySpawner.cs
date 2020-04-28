using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<HeadEnemy> enemyPrefabs;
    public Rect spawnArea;

    private float timer = 0.0f;
    // time in between spawns
    public float waveSpawnDelta;

    //max number of enemies that can spawn in a wave
    public int maxPerWave;
    //min number of enemies that can spawn in a wave
    public int minPerWave;

    //spawns a random amount of enemies on a timer

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= waveSpawnDelta)
        {
            SpawnWave();
            timer -= waveSpawnDelta;
        }
    }

    private void SpawnWave()
    {
        //lets figure out how many spawn this wave
        int numSpawn = Random.Range(minPerWave, maxPerWave+1);
        
        for(int i = 0; i < numSpawn; i++)
        {
            // roll a spawn position
            Vector3 spawnPos = new Vector3(Random.Range(spawnArea.xMin, spawnArea.xMax), Random.Range(spawnArea.yMin, spawnArea.yMax), 0);
            // roll a prefab to spawn
            HeadEnemy spawnPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Instantiate<HeadEnemy>(spawnPrefab, transform.position+spawnPos, transform.rotation, GetComponentInParent<Camera>().transform);
            //stagger the spawns a bit
            new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }
}
