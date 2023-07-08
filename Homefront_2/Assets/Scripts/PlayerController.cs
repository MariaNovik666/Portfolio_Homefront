using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private int maxHp;
    private int hp;
    private int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            hpText.text = hp.ToString();
        }
    }

    public float speed;

    public Animator animator;
    private Vector2 direction;
    private Rigidbody2D Rb;

    public VectorValue player_position;

    [SerializeField]
    private GameObject craft;

    [SerializeField]
    private GameEvents gameEvents;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject homeCamera;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        hpText.text = HP.ToString();
        HP = maxHp;
        inventory.HealPlayer += () => Heal();

        transform.position = player_position.initialValue;
    }

    void Update()
    {

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (craft.activeSelf)
                craft.SetActive(false);
            else
                craft.SetActive(true);
        }
    }

    void FixedUpdate()
    {

        Rb.MovePosition(Rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public void Heal()
    {
        if (HP < maxHp)
            ++HP;
    }
    public int GetDamage(string target)
    {
        Debug.Log($"Current damage to {target} is {inventory.GetDamage(target)}");
        return inventory.GetDamage(target);
    }

    public void Hit(Enemy sender)
    {
        HP -= sender.damage;
        if (HP <= 0)
        {
            gameEvents.GameOver();
        }
        sender.transform.position = new Vector3(sender.transform.position.x, sender.transform.position.y + 3);
    }

    public void GoHome()
    {
        mainCamera.SetActive(false);
        homeCamera.SetActive(true);
    }
    public void GoOutFromHome()
    {
        mainCamera.SetActive(true);
        homeCamera.SetActive(false);
    }
}
