using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            enemy.WakeUp();
        }*/
    }
}
