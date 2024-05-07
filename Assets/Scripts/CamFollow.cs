using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{   
    GameObject player;
    Vector3 playerPos;
    private void Start() {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, -10f);
    }
}
