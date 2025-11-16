using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button slotButton;
    [SerializeField] private GameObject deleteButton;
    
    private Inventory inventory;
    private int slotIndex;
    
    public void Initialize(Inventory inv, int index)
    {
        inventory = inv;
        slotIndex = index;
        slotButton.onClick.AddListener(OnSlotClicked);
        ClearSlot();
    }
    
    public void UpdateSlot(InventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            ClearSlot();
            return;
        }
        
        itemIcon.sprite = slot.item.icon;
        itemIcon.color = Color.white;
        
        // Показываем количество только если предмет стакаемый и количество > 1
        if (slot.item.isStackable && slot.quantity > 1)
        {
            quantityText.text = slot.quantity.ToString();
            quantityText.gameObject.SetActive(true);
        }
        else
        {
            quantityText.gameObject.SetActive(false);
        }
        
        deleteButton.SetActive(false);
    }
    
    private void ClearSlot()
    {
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        quantityText.gameObject.SetActive(false);
        deleteButton.SetActive(false);
    }
    
    private void OnSlotClicked()
    {
        var slots = inventory.GetSlots();
        if (slotIndex < slots.Count && !slots[slotIndex].IsEmpty)
        {
            // Показываем/скрываем кнопку удаления
            deleteButton.SetActive(!deleteButton.activeSelf);
        }
    }
    
    // Вызывается по нажатию на кнопку удаления
    public void OnDeleteButtonClicked()
    {
        inventory.RemoveItem(slotIndex);
        deleteButton.SetActive(false);
    }
}