using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet, shootPoint, tower;
    public float bulletSpeed, bulletCD;
    float countTime;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && countTime <= 0f)
        {
            countTime = bulletCD;
            Shoot();
        }
        else if(countTime > 0)
        {
            countTime -=Time.deltaTime;
        }
    }

    void Shoot()
    {
        audioSource.Play();
        GameObject newBullet = Instantiate(bullet, shootPoint.transform.position, tower.transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
