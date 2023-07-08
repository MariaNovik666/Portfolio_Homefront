using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressController : MonoBehaviour
{
    public static Progress GameProgress;

    [SerializeField]
    private GameObject ContinueButton;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (File.Exists("progress.json"))
            {
                ContinueButton.SetActive(true);
            }
        }
        else
        {
            if (GameProgress is not null)
            {
                var playerInvontory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<Inventory>();
                playerInvontory.Load(GameProgress);
            }
        }
    }

    public void SaveGame()
    {
        var playerInvontory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<Inventory>();

        var progress = new Progress
        {
            Wood = playerInvontory.WoodCount,
            Stone = playerInvontory.StoneCount,
            Iron = playerInvontory.IronCount,
            Apple = playerInvontory.AppleCount,
            SpiderWeb = playerInvontory.SpiderWebCount,
            HealPoisonts = playerInvontory.HealPoisontsCount,

            AxeType = playerInvontory.axe.GetType().FullName,
            PickaxeType = playerInvontory.pickaxe.GetType().FullName,
            SwordType = playerInvontory.sword.GetType().FullName,
        };

        using (StreamWriter sw = new StreamWriter("progress.json"))
        {
            var json = JsonUtility.ToJson(progress);
            sw.WriteLine(json);
        }
    }

    public void Load()
    {
        using (StreamReader sr = new StreamReader("progress.json"))
        {
            string progressJson = sr.ReadToEnd();
            GameProgress = JsonUtility.FromJson<Progress>(progressJson);
        }
    }
}

[Serializable]
public class Progress
{
    public int Wood;
    public int Stone;
    public int Iron;
    public int Apple;
    public int SpiderWeb;
    public int HealPoisonts;

    public string AxeType;
    public string PickaxeType;
    public string SwordType;
}