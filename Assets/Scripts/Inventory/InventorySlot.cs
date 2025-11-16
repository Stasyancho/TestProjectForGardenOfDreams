[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;
    
    public bool IsEmpty => item == null || quantity == 0;
    
    public void Clear()
    {
        item = null;
        quantity = 0;
    }
    
    public bool CanAddItem(Item newItem)
    {
        return IsEmpty || (item == newItem && quantity < item.maxStackSize);
    }
}