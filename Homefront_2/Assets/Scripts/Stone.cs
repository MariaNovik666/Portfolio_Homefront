using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IResource
{
    private int hp = 4;
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
                playerInvontory.AddItem("stone", 1);
            }
        }
    }
}
