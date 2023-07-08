using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHp;
    private int hp;
    private int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            hpText.text = $"{HP}/{maxHp}";
            if (hp <= 0)
            {
                var playerInvontory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<Inventory>();
                if (Random.Range(0, 100) <= 50)
                    playerInvontory.AddItem("spiderWeb", 1);
                Destroy(gameObject);
            }
        }
    }
    [SerializeField]
    private TMP_Text hpText;

    [SerializeField]
    public int damage;
    [SerializeField]
    private float speed;
    private GameObject target;

    private bool active = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        HP = maxHp;
    }
    private void FixedUpdate()
    {
        if (active)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
        }
        hpText.transform.position = transform.position + Vector3.up * 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target.GetComponent<PlayerController>().Hit(this);
        }
    }
    public void WakeUp()
    {
        active = true;
    }

    public void Hit(int damage)
    {
        HP -= damage;
    }
}
