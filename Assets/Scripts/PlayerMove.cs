using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject tower, chassis;
    Vector2 movement;
    Vector2 mousePos;
    public float speed = 250;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    // Update is called once per frame
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.velocity = movement * Time.deltaTime * speed;
        
        if(movement.x * movement.y == 1)
        {
            Vector3 newRotation = new Vector3(0, 0, 45f - 90f * movement.x);
            chassis.transform.eulerAngles = newRotation;  
        }
        else if(movement.x * movement.y == -1)
        {
            Vector3 newRotation = new Vector3(0, 0, -45f - 90f * movement.x);
            chassis.transform.eulerAngles = newRotation;  
        }
        else if(movement.x != 0)
        {
            Vector3 newRotation = new Vector3(0, 0, -90f * movement.x);
            chassis.transform.eulerAngles = newRotation;  
        }
        else if(movement.y != 0)
        {
            Vector3 newRotation = new Vector3(0, 0, -90f + movement.y * 90);
            chassis.transform.eulerAngles = newRotation;  
        }

        chassis.GetComponent<Animator>().SetFloat("moveSpeed", movement.SqrMagnitude());

        Vector2 lookVector = mousePos - rb.position;
        float angle = Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg - 90f;
        tower.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
