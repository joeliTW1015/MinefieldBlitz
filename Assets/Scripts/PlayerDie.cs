using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public static int hP = 3;
    Animator animator;
    
    public GameManager gameManager;
    private void Start() {
        animator = GetComponent<Animator>();
        hP = 3;
        gameManager.UpdateHP();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            hP -= 1;
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
            gameManager.UpdateHP();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            hP -= 1;
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
            gameManager.UpdateHP();
        }
    }

    public void DeadCheck()
    {
        animator.SetBool("Hit", false);

        if(hP <= 0)
        {
            gameManager.LoseGame();
        }
    }
}

