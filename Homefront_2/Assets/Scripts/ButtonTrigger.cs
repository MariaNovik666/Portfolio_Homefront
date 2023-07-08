using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Animator button_animator;
    //public GameObject frame;

    public GameObject[] otherFrames;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button_animator.SetTrigger("isTriggered");
            //frame.SetActive(true);

            foreach(GameObject frame in otherFrames)
            {
                //frame.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button_animator.SetTrigger("isTriggered");
            
        }
    }
}
