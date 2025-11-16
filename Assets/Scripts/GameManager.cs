using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private Item[] allItems;
    
    private string path = "Assets/Resources/SavedData/Inventory.json";
    
    private void Start()
    {
        var savedInventory = LoadData();
        inventory.InitializeSlots(savedInventory);
        inventoryUI.Initialize(inventory);
    }

    public void SaveData()
    {
        string json = JsonConvert.SerializeObject(inventory.GetSlots());

        using (StreamWriter writer = new(path))
        {
            writer.Write(json);
        }
    }

    private List<InventorySlot> LoadData()
    {
        using (StreamReader reader = new(path))
        {
            var json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<List<InventorySlot>>(json);
        }
    }
}