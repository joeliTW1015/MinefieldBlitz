using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GhostDie : MonoBehaviour
{
    GameObject player;
    Animator animator;
    int hP;
    private void Start() 
    {
        animator = GetComponentInChildren<Animator>();
        hP = 1;
        player = GameObject.Find("Player");
        GetComponent<AIDestinationSetter>().target = player.transform;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
            GetComponent<AIPath>().maxSpeed = 0f;
        }
    }

    public void DeadCheck()
    {
        animator.SetBool("Hit", false);
        hP -= 1;
        if(hP == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<AIPath>().maxSpeed = 3f;
        }
    }

    
}
