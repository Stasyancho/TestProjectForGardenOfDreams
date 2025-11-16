using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения
    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start()
    {
        // Получаем ссылку на компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // Input обрабатываем в Update (это чаще точнее для ввода)
        movement.x = Input.GetAxisRaw("Horizontal"); // Получаем ввод по горизонтали (A/D, LeftArrow/RightArrow)
        movement.y = Input.GetAxisRaw("Vertical");   // Получаем ввод по вертикали (W/S, UpArrow/DownArrow)
            
        // Нормализуем вектор, чтобы диагональное движение не было быстрее
        movement = movement.normalized;
    }
    
    void FixedUpdate()
    {
        // Движение применяем в FixedUpdate, т.к. оно связано с физикой
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
