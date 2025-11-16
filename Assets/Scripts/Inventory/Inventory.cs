using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 20;
    [SerializeField] private List<InventorySlot> slots;
    [SerializeField] private List<Item> allItems;
    
    // События для обновления UI
    public System.Action<Inventory> OnInventoryChanged;
    
    public void InitializeSlots(List<InventorySlot> savedSlots = null)
    {
        if (slots != null)
        {
            foreach (var inventorySlot in savedSlots)
            {
                if (inventorySlot.item != null)
                    inventorySlot.item = allItems.FirstOrDefault(item => item.itemName == inventorySlot.item.itemName);
            }
            slots = savedSlots;
        }
        else
        {
            slots = new List<InventorySlot>();
            for (int i = 0; i < inventorySize; i++)
            {
                slots.Add(new InventorySlot());
            }
        }
    }
    
    private void Update()
    {
        //Для тестов
        if (Input.GetKeyDown(KeyCode.Alpha1) && allItems.Count > 0)
        {
            AddItem(allItems[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && allItems.Count > 1)
        {
            AddItem(allItems[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && allItems.Count > 2)
        {
            AddItem(allItems[2]);
        }
    }
    
    // Основной метод добавления предмета
    public bool AddItem(Item item, int quantity = 1)
    {
        if (item == null || quantity <= 0) return false;
        
        // Пытаемся добавить в существующие стаки
        if (item.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == item && slot.quantity < item.maxStackSize)
                {
                    int spaceLeft = item.maxStackSize - slot.quantity;
                    int addAmount = Mathf.Min(spaceLeft, quantity);
                    slot.quantity += addAmount;
                    quantity -= addAmount;
                    
                    OnInventoryChanged?.Invoke(this);
                    
                    if (quantity <= 0) return true;
                }
            }
        }
        
        // Добавляем в пустые слоты
        while (quantity > 0)
        {
            var emptySlot = GetFirstEmptySlot();
            if (emptySlot == null) return false; // Инвентарь полон
            
            int addAmount = item.isStackable ? Mathf.Min(item.maxStackSize, quantity) : 1;
            emptySlot.item = item;
            emptySlot.quantity = addAmount;
            quantity -= addAmount;
            
            OnInventoryChanged?.Invoke(this);
        }
        
        return true;
    }
    
    // Удаление предмета из слота
    public bool RemoveItem(Item item, int quantity = 1)
    {
        var index = slots.FindIndex(slot => slot.item == item);
        return RemoveItem(index, quantity);
    }
    
    // Удаление предмета из слота
    public bool RemoveItem(int slotIndex, int quantity = 1)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count) return false;
        
        var slot = slots[slotIndex];
        if (slot.IsEmpty) return false;
        
        if (quantity >= slot.quantity)
        {
            slot.Clear();
        }
        else
        {
            slot.quantity -= quantity;
        }
        
        OnInventoryChanged?.Invoke(this);
        return true;
    }
    
    // Получение первого пустого слота
    private InventorySlot GetFirstEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty) return slot;
        }
        return null;
    }
    
    // Получение всех слотов
    public List<InventorySlot> GetSlots()
    {
        return slots;
    }
}