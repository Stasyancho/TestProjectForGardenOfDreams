using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [JsonIgnore]
    public Sprite icon;
    [JsonIgnore]
    public int maxStackSize = 1;
    [JsonIgnore]
    public bool isStackable => maxStackSize > 1;
}