using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 20;
    [SerializeField] private List<InventorySlot> slots;
    
    // События для обновления UI
    public System.Action<Inventory> OnInventoryChanged;
    
    private void Awake()
    {
        InitializeSlots();
    }
    
    private void InitializeSlots()
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot());
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
    public void RemoveItem(int slotIndex, int quantity = 1)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count) return;
        
        var slot = slots[slotIndex];
        if (slot.IsEmpty) return;
        
        if (quantity >= slot.quantity)
        {
            slot.Clear();
        }
        else
        {
            slot.quantity -= quantity;
        }
        
        OnInventoryChanged?.Invoke(this);
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