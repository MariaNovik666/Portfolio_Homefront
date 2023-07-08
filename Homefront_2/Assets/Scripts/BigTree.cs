using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTree : MonoBehaviour, IResource
{
    public string Type => "wood";
    private int hp = 1000;

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
                if (Random.Range(1, 100) <= 100)
                    playerInvontory.AddItem("apple", 4);
                playerInvontory.AddItem("wood", 30);
            }
        }
    }
}
