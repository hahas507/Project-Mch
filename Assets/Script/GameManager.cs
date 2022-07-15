using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Rock;
    [SerializeField] float rockSpawnRange;
    [SerializeField] float rockSpawnRate;

    void Start()
    {        
        

        for(int i = 0; i < rockSpawnRate; i++)
        {
            float randomX = Random.Range(-rockSpawnRange, rockSpawnRange);
            float randomY = Random.Range(-rockSpawnRange, rockSpawnRange);
            float randomZ = Random.Range(-rockSpawnRange, rockSpawnRange);
            Vector3 RandomSpawn = new Vector3(randomX, randomY, randomZ);   
            GameObject clone = Instantiate(Rock, RandomSpawn, Quaternion.identity);
        }           
    }
}
