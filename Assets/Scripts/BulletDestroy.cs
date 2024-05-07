using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("PlayerBullet") )
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy") )
        {
            Destroy(other.gameObject);
        }
    }
    
}
