using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAI : MonoBehaviour
{
    public GameObject player, alienBullet;
    Animator animator;
    AudioSource audioSource;
    Vector3 playerVector;
    float  lookAngle;
    float shootCD;
    int hP;
    public float bulletSpeed;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        hP = 1;
        timer = 0;
        shootCD = Random.Range(1,3);
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() 
    {
        playerVector = transform.position - player.transform.position;
        lookAngle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0, 0, lookAngle);

        if(timer < shootCD)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Shoot();
            timer = 0;
            shootCD = Random.Range(1,3);
        }
    }

    void Shoot()
    {
        audioSource.Play();
        GameObject newBullet = Instantiate(alienBullet, transform.position,transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(transform.up *  - bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
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
    }
}
