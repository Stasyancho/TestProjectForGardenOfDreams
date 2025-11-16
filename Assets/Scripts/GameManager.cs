using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private Item[] testItems;
    
    private void Start()
    {
        inventoryUI.Initialize(inventory);
        
        // Тестирование - добавление предметов
        if (testItems.Length > 0)
        {
            inventory.AddItem(testItems[0], 5); // Добавить 5 предметов
            inventory.AddItem(testItems[1], 1); // Добавить 1 предмет
        }
    }
    
    private void Update()
    {
        // Открытие/закрытие инвентаря по клавише I
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.ToggleInventory();
        }
        
        // Тест добавления предметов по клавишам
        if (Input.GetKeyDown(KeyCode.Alpha1) && testItems.Length > 0)
        {
            inventory.AddItem(testItems[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && testItems.Length > 1)
        {
            inventory.AddItem(testItems[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && testItems.Length > 2)
        {
            inventory.AddItem(testItems[2]);
        }
    }
}