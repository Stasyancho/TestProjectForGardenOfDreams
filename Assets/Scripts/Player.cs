using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Item bulletObject; 
    private List<GameObject> gameObjectsInRadius = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!gameObjectsInRadius.Contains(other.gameObject) && !other.isTrigger)
            gameObjectsInRadius.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger)
            gameObjectsInRadius.Remove(other.gameObject);
    }

    public void Shot()
    {
        if (gameObjectsInRadius.Count > 0)
        {
            var target = gameObjectsInRadius.OrderBy(go => Vector3.Distance(transform.position, go.transform.position)).First();
            var unitHealth = target.GetComponent<UnitHealth>();
            if (unitHealth != null && inventory.RemoveItem(bulletObject))
                unitHealth.TakeDamage(20);
        }
    }

    public void TakeItem(Item item, int amount)
    {
        if (inventory.AddItem(item, amount))
            Debug.Log($"Taked {amount} {item.itemName}");
    }
}
