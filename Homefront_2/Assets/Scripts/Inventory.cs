using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item axe = new DefaultAxe();
    public Item pickaxe = new DefaultPickaxe();
    public Item sword = new DefaultSword();

    private Item currentItem;
    [SerializeField]
    private Image currentItemImage;

    [SerializeField]
    private Text woodCountObj;
    private int woodCount = 0;
    public int WoodCount
    {
        get { return woodCount; }
        set
        {
            woodCount = value;
            woodCountObj.text = woodCount.ToString();
        }
    }

    [SerializeField]
    private Text appleCountObj;
    private int appleCount = 0;
    public int AppleCount
    {
        get { return appleCount; }
        set
        {
            appleCount = value;
            appleCountObj.text = appleCount.ToString();
        }
    }
    [SerializeField]
    private Text stoneCountObj;
    private int stoneCount = 0;
    public int StoneCount
    {
        get { return stoneCount; }
        set
        {
            stoneCount = value;
            stoneCountObj.text = stoneCount.ToString();
        }
    }

    [SerializeField]
    private Text ironCountObj;
    private int ironCount = 0;
    public int IronCount
    {
        get { return ironCount; }
        set
        {
            ironCount = value;
            ironCountObj.text = ironCount.ToString();
        }
    }

    [SerializeField]
    private Text spiderWebCountObj;
    private int spiderWebCount = 0;
    public int SpiderWebCount
    {
        get { return spiderWebCount; }
        set
        {
            spiderWebCount = value;
            spiderWebCountObj.text = spiderWebCount.ToString();
        }
    }


    private bool hasPlot;

    public delegate void VoidHandler();

    [SerializeField]
    private Text healPointsCountObj;
    private int healPoisontsCount = 0;
    public int HealPoisontsCount
    {
        get { return healPoisontsCount; }
        set
        {
            healPoisontsCount = value;
            healPointsCountObj.text = healPoisontsCount.ToString();
        }
    }

    public event VoidHandler HealPlayer;

    [SerializeField]
    private GameObject CraftTable;
    [SerializeField]
    private GameObject Plot;


    public void Start()
    {
        axe.Next = pickaxe;
        pickaxe.Next = sword;
        sword.Next = axe;

        axe.Prev = sword;
        pickaxe.Prev = axe;
        sword.Prev = pickaxe;

        currentItem = axe;
        SetSprite();
    }

    public void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            switchItem();
        }
        else if (Input.GetKeyDown("2"))
        {
            if (HealPoisontsCount > 0)
            {
                HealPlayer?.Invoke();
                --HealPoisontsCount;
            }
        }
    }
    public void AddItem(string itemType, int count)
    {
        switch (itemType)
        {
            case "wood":
                WoodCount += count;
                break;
            case "apple":
                AppleCount += count;
                break;
            case "stone":
                StoneCount += count;
                break;
            case "iron":
                IronCount += count;
                break;
            case "spiderWeb":
                SpiderWebCount += count;
                break;
        }
    }
    public int GetDamage(string target)
    {
        switch (target)
        {
            case "wood":
                return currentItem.WoodDamage;
            case "stone":
                return currentItem.StoneDamage;
            case "enemy":
                return currentItem.EnemyDamage;
        }
        return 0;
    }

    public void BuyItem(string itemName, string resourceType, int price)
    {
        switch (resourceType)
        {
            case "apple":
                if (AppleCount >= price)
                {
                    AppleCount -= price;
                    addItem(itemName);
                }
                break;
        }
    }
    private void addItem(string itemName)
    {
        switch (itemName)
        {
            case "healPoison":
                HealPoisontsCount += 1;
                break;
        }
    }

    [SerializeField]
    private int plotWoodPrice;
    [SerializeField]
    private int plotStonePrice;
    [SerializeField]
    private int plotIronPrice;
    [SerializeField]
    private int plotSpiderWebPrice;

    public void CraftPlot()
    {
        if (WoodCount >= plotWoodPrice &&
            StoneCount >= plotStonePrice &&
            IronCount >= plotIronPrice &&
            SpiderWebCount >= plotSpiderWebPrice)
        {
            WoodCount -= plotWoodPrice;
            StoneCount -= plotStonePrice;
            IronCount -= plotIronPrice;
            SpiderWebCount -= plotSpiderWebPrice;

            hasPlot = true;
            CraftTable.SetActive(false);
            Plot.SetActive(true);
        }
    }

    public bool HasPlot()
    {
        return hasPlot;
    }

    public void Load(Progress progress)
    {
        WoodCount = progress.Wood;
        StoneCount = progress.Stone;
        IronCount = progress.Iron;
        AppleCount = progress.Apple;
        SpiderWebCount = progress.SpiderWeb;
        HealPoisontsCount = progress.HealPoisonts;

        axe = getAxe(progress.AxeType);
        pickaxe = getPickaxe(progress.PickaxeType);
        sword = getSword(progress.SwordType);
    }

    

    public void UpgradeItem(string itemName, int level, int price, string resourceType)
    {
        switch (resourceType)
        {
            case "wood":
                if (WoodCount >= price)
                {
                    WoodCount -= price;
                    Upgrade(itemName, level);
                }
                break;
            case "stone":
                if (StoneCount >= price)
                {
                    StoneCount -= price;
                    Upgrade(itemName, level);
                }
                break;
            case "iron":
                if (IronCount >= price)
                {
                    IronCount -= price;
                    Upgrade(itemName, level);
                }
                break;
        }
        SetSprite();
    }

    private void Upgrade(string itemName, int level)
    {
        switch (itemName)
        {
            case "axe":
                axe = axe.Upgrade(level);
                axe.Prev.Next = axe;
                axe.Next.Prev = axe;
                currentItem = axe;
                break;
            case "pickaxe":
                pickaxe = pickaxe.Upgrade(level);
                pickaxe.Prev.Next = pickaxe;
                pickaxe.Next.Prev = pickaxe;
                currentItem = pickaxe;
                break;
            case "sword":
                sword = sword.Upgrade(level);
                sword.Prev.Next = sword;
                sword.Next.Prev = sword;
                currentItem = sword;
                break;
        }
    }

    private Item getAxe(string type)
    {
        switch (type)
        {
            case "DefaultAxe":
                return new DefaultAxe();
            case "WoodAxe":
                return new WoodAxe();
            case "StoneAxe":
                return new StoneAxe();
            case "IronAxe":
                return new IronAxe();
        }
        return null;
    }
    private Item getPickaxe(string type)
    {
        switch (type)
        {
            case "DefaultPickaxe":
                return new DefaultPickaxe();
            case "WoodPickaxe":
                return new WoodPickaxe();
            case "StonePickaxe":
                return new StonePickaxe();
            case "IronPickaxe":
                return new IronPickaxe();
        }
        return null;
    }
    private Item getSword(string type)
    {
        switch (type)
        {
            case "DefaultSword":
                return new DefaultSword();
            case "WoodSword":
                return new WoodSword();
            case "StoneSword":
                return new StoneSword();
            case "IronSword":
                return new IronSword();
        }
        return null;
    }

    private void switchItem()
    {
        currentItem = currentItem.Next;
        SetSprite();
    }
    private void SetSprite()
    {
        currentItemImage.sprite = currentItem.GetSprite();
    }
}



public abstract class Item
{
    public Item Next;
    public Item Prev;

    public Sprite Sprite;

    public int WoodDamage;
    public int StoneDamage;
    public int EnemyDamage;

    protected int currentLevel;
    public abstract Item Upgrade(int level);
    public abstract Sprite GetSprite();
}

public class DefaultAxe : Item
{
    public DefaultAxe()
    {
        currentLevel = 0;

        WoodDamage = 1;
        StoneDamage = 0;
        EnemyDamage = 1;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.Load<Sprite>("hand");
        return Sprite;
    }

    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new WoodAxe();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class WoodAxe : Item
{
    public WoodAxe()
    {
        currentLevel = 1;

        WoodDamage = 4;
        StoneDamage = 0;
        EnemyDamage = 1;
    }
    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[7];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new StoneAxe();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class StoneAxe : Item
{
    public StoneAxe()
    {
        currentLevel = 2;

        WoodDamage = 10;
        StoneDamage = 0;
        EnemyDamage = 2;
    }
    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[8];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if(level > currentLevel)
        {
            var nextLevelItem = new IronAxe();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class IronAxe : Item
{
    public IronAxe()
    {
        currentLevel = 3;

        WoodDamage = 100;
        StoneDamage = 0;
        EnemyDamage = 4;
    }
    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[6];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        return this;
    }
}

public class DefaultPickaxe : Item
{
    public DefaultPickaxe()
    {
        currentLevel = 0;

        WoodDamage = 1;
        StoneDamage = 0;
        EnemyDamage = 1;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.Load<Sprite>("Hand");
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new WoodPickaxe();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class WoodPickaxe : Item
{
    public WoodPickaxe()
    {
        currentLevel = 1;

        WoodDamage = 0;
        StoneDamage = 4;
        EnemyDamage = 1;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[1];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new StonePickaxe();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class StonePickaxe : Item
{
    public StonePickaxe()
    {
        currentLevel = 2;

        WoodDamage = 1;
        StoneDamage = 10;
        EnemyDamage = 2;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[2];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new IronPickaxe();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class IronPickaxe : Item
{
    public IronPickaxe()
    {
        currentLevel = 3;

        WoodDamage = 1;
        StoneDamage = 100;
        EnemyDamage = 4;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[0];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        return this;
    }
}

public class DefaultSword : Item
{
    public DefaultSword()
    {
        currentLevel = 0;

        WoodDamage = 1;
        StoneDamage = 0;
        EnemyDamage = 1;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.Load<Sprite>("Hand");
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new WoodSword();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class WoodSword : Item
{
    public WoodSword()
    {
        currentLevel = 1;

        WoodDamage = 1;
        StoneDamage = 1;
        EnemyDamage = 4;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[4];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new StoneSword();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class StoneSword : Item
{
    public StoneSword()
    {
        currentLevel = 2;

        WoodDamage = 1;
        StoneDamage = 1;
        EnemyDamage = 6;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[5];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        if (level > currentLevel)
        {
            var nextLevelItem = new IronSword();
            nextLevelItem.Next = Next;
            nextLevelItem.Prev = Prev;
            return nextLevelItem;
        }
        return this;
    }
}
public class IronSword: Item
{
    public IronSword()
    {
        currentLevel = 3;

        WoodDamage = 1;
        StoneDamage = 1;
        EnemyDamage = 8;
    }

    public override Sprite GetSprite()
    {
        if (Sprite is null)
            Sprite = Resources.LoadAll<Sprite>("tools")[3];
        return Sprite;
    }
    public override Item Upgrade(int level)
    {
        return this;
    }
}