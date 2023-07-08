using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float offset;

    private bool canHitEnemy;
    private Enemy targetedEnemy;

    private bool canBreakResource;
    private IResource targetedResource;

    [SerializeField]
    private AudioClip hitSong;
    [SerializeField]
    private new AudioSource audio;

    [SerializeField]
    private PlayerController player;
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate2 = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotate2 + offset);

        if (Input.GetMouseButtonDown(0))
        {
            audio.PlayOneShot(hitSong);
            if (canHitEnemy)
            {
                targetedEnemy.Hit(player.GetDamage("enemy"));
            }
            else if (canBreakResource)
            {
                targetedResource.Hit(player.GetDamage(targetedResource.Type));
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Resource":
                canBreakResource = true;
                targetedResource = collision.gameObject.GetComponent<IResource>();
                break;
            case "Enemy":
                canHitEnemy = true;
                targetedEnemy = collision.gameObject.GetComponent<Enemy>();
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Resource":
                canBreakResource = false;
                targetedResource = null;
                break;
            case "Enemy":
                canHitEnemy = false;
                targetedEnemy = null;
                break;
        }
    }
}
