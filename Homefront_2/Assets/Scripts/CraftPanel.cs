using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanel : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    void Start()
    {
        GameObject.Find("HealPoison Button").GetComponent<Button>().onClick.AddListener(() => inventory.BuyItem("healPoison", "apple", 2)); // 2
        
        GameObject.Find("AxeLevel1 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("axe", 1, price: 5, "wood"));
        GameObject.Find("PickaxeLevel1 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("pickaxe", 1, price: 5, "wood"));
        GameObject.Find("SwordLevel1 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("sword", 1, price: 5, "wood"));

        GameObject.Find("AxeLevel2 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("axe", 2, price: 10, "stone"));
        GameObject.Find("PickaxeLevel2 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("pickaxe", 2, price: 10, "stone"));
        GameObject.Find("SwordLevel2 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("sword", 2, price: 10, "stone"));

        GameObject.Find("AxeLevel3 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("axe", 3, price: 1, "iron"));
        GameObject.Find("PickaxeLevel3 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("pickaxe", 3, price: 2, "iron"));
        GameObject.Find("SwordLevel3 Button").GetComponent<Button>().onClick.AddListener(() => inventory.UpgradeItem("sword", 3, price: 2, "iron"));
    }
}
