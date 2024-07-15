using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float dirX, dirY;
    public float speed;
    public Joystick joystick;
    private Rigidbody2D payerRigidbody;
    
    void Start()
    {
        payerRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        dirX = joystick.Horizontal * speed;
        dirY = joystick.Vertical * speed;        
    }

    void FixedUpdate()
    {
        payerRigidbody.velocity = new Vector2(dirX, dirY);
    }

   
}
