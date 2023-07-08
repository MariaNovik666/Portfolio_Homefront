using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemies;
    private Queue<int> enemiesQueue = new Queue<int>();
    private Queue<int> buf;

    private void Start()
    {
        for (int i = 0; i < 1000; ++i)
            enemiesQueue.Enqueue(UnityEngine.Random.Range(0, enemies.Count));

        buf = new Queue<int>(enemiesQueue);
    }

    public void SpawnEnemy()
    {
        try
        {
            int i;
            enemiesQueue.TryDequeue(out i);
            var newEnemy = Instantiate(enemies[i], transform);
            newEnemy.WakeUp();
        }
        catch (ArgumentOutOfRangeException)
        {
            enemiesQueue = new Queue<int>(buf);
        }
    }
}
