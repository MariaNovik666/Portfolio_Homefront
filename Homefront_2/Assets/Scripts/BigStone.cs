using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStone : MonoBehaviour, IResource
{
    private int hp = 1000;
    public string Type => "stone";
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;

            if (hp <= 0)
            {
                Destroy(gameObject);

                var playerInvontory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<Inventory>();
                if (Random.Range(1, 100) <= 100)
                    playerInvontory.AddItem("iron", 3);
                playerInvontory.AddItem("stone", 30);
            }
        }
    }
}
