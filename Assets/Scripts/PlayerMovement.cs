using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    [SerializeField] private JoystickController joystickController;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + joystickController.direction * (moveSpeed * Time.fixedDeltaTime));
    }
}
