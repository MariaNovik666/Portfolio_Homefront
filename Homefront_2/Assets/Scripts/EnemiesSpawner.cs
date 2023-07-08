using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField]
    private List<EnemySpawnPoint> spawnPoints;
    [SerializeField]
    private int spawnRate = 3;
    private Queue<int> spawnQueue = new Queue<int>();
    private Queue<int> buf;

    private void Start()
    {
        for (int i = 0; i < 1000; ++i)
            spawnQueue.Enqueue(UnityEngine.Random.Range(0, spawnPoints.Count));

        buf = new Queue<int>(spawnQueue);
    }

    private void Update()
    {
        if (UnityEngine.Random.Range(1, 1000) <= spawnRate)
            try
            {
                int i;
                spawnQueue.TryDequeue(out i);
                spawnEnemie(i);
            }
            catch (ArgumentOutOfRangeException)
            {
                spawnQueue = new Queue<int>(buf);
            }
    }

    private void spawnEnemie(int index)
    {
        EnemySpawnPoint spawnPoint = spawnPoints[index];
        spawnPoint.SpawnEnemy();
    }
}
