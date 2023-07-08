using UnityEngine;

public class Tree : MonoBehaviour, IResource
{
    private int hp = 4;
    public string Type => "wood";

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
                    playerInvontory.AddItem("apple", 1);
                playerInvontory.AddItem("wood", 1);
            }
        }
    }
}
