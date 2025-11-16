using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private List<Item> itemsForDrop = new List<Item>();
    [SerializeField] private GameObject itemPrefab;
    
    private Transform target;
    private Rigidbody2D rb;
    private float reloadTime = 1f;
    private float timeAfterAttack = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<UnitHealth>().OnDeath.AddListener(DropResourses);
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            var dir = target.position - transform.position;
            if (dir.magnitude > 2f)
            {
                dir.Normalize();
                rb.MovePosition(rb.position + new Vector2(dir.x, dir.y) * (moveSpeed * Time.fixedDeltaTime));
            }
            else
            {
                if (timeAfterAttack > reloadTime)
                {
                    target.gameObject.GetComponent<UnitHealth>().TakeDamage(10);
                    timeAfterAttack = 0;
                }
            }
        }
        if (timeAfterAttack <= reloadTime)
            timeAfterAttack += Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            target = other.transform;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            target = null;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void DropResourses()
    {
        var go = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        go.GetComponent<DroppedItem>().Initialize(itemsForDrop[Random.Range(0, itemsForDrop.Count)], Random.Range(3, 6));
    }
}
