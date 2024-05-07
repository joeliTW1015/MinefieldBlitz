using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject explosion;
    private void OnDestroy() {
        if(this.gameObject.scene.isLoaded)
            Instantiate(explosion,transform.position, transform.rotation);
    }
}
