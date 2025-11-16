using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private GameObject slotPrefab;
    
    private Inventory inventory;
    private InventorySlotUI[] slotUIs;
    
    public void Initialize(Inventory inv)
    {
        inventory = inv;
        inventory.OnInventoryChanged += UpdateUI;
        
        // Создаем UI слоты
        slotUIs = new InventorySlotUI[inventory.GetSlots().Count];
        for (int i = 0; i < slotUIs.Length; i++)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotsParent);
            slotUIs[i] = slotObject.GetComponent<InventorySlotUI>();
            slotUIs[i].Initialize(inventory, i);
        }
        
        UpdateUI(inventory);
    }
    
    private void UpdateUI(Inventory inv)
    {
        var slots = inv.GetSlots();
        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].UpdateSlot(slots[i]);
        }
    }
    
    // Переключение видимости инвентаря
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}