using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleTree : MonoBehaviour, IResource
{
    public string Type => "wood";
    private int hp = 50;

    public int HP
    {
        get => hp;
        set
        {
            hp = value;

            if (hp <= 0)
            {
                Destroy(gameObject);

                var playerInvontory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<Inventory>();
                if (Random.Range(1, 100) <= 20)
                    playerInvontory.AddItem("apple", 3);
                playerInvontory.AddItem("wood", 5);
            }
        }
    }
}