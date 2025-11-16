using UnityEngine;
using UnityEngine.UI;

public class DroppedItem : MonoBehaviour
{
    private Item _item;
    private int _amount;
    public void Initialize(Item item, int amount)
    {
        _item = item;
        _amount = amount;
        GetComponentInChildren<Image>().sprite = item.icon;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_item != null && other.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<Player>().TakeItem(_item, _amount);
            Destroy(gameObject);
        }
    }
}
