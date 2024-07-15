using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject[] inventorySlots;
    public Text[] quantityText;

    private bool isInventoryOpen = false;

    public List<Item> items = new List<Item>();
    Dictionary<string, int> countDictionary = new Dictionary<string, int>();

    public GameObject startItem;
    public int startItemCount = 30;

    public GameObject deleteButton;
    Sprite buttonSprite;


    void Start()
    {
        for (int i = 0; i < startItemCount; i++)
        {
            AddItem(startItem.GetComponent<Item>());
        }
        
        UpdateInventoryUI();
        inventoryPanel.SetActive(false);
        //deleteButton.SetActive(false);
    }
       
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
        //deleteButton.SetActive(isInventoryOpen);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateInventoryUI();
    }

    public void RemоveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            UpdateInventoryUI();
        }
    }

    private void UpdateInventoryUI()
    {
        CountingTheNumberOfItems(items);

        int slotIndex = 0;

        foreach (var kvp in countDictionary)
        {
            if (slotIndex < inventorySlots.Length)
            {
                // Устанавливаем иконку предмета
                inventorySlots[slotIndex].GetComponent<Image>().sprite = items.Find(item => item.itemName == kvp.Key).itemIcon;
                inventorySlots[slotIndex].GetComponent<Image>().enabled = true;

                // Устанавливаем количество предметов
                if (kvp.Value > 1)
                {
                    quantityText[slotIndex].GetComponent<Text>().enabled = true;
                    quantityText[slotIndex].text = $"{kvp.Value}";
                }
                else
                {
                    quantityText[slotIndex].GetComponent<Text>().enabled = false;
                }

                slotIndex++;
            }
        }

        // Отключаем оставшиеся слоты
        for (int i = slotIndex; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].GetComponent<Image>().enabled = false;
            quantityText[i].GetComponent<Text>().enabled = false;
        }
    }

    private void CountingTheNumberOfItems(List<Item> arrayList)
    {
        countDictionary.Clear();

        foreach (var item in arrayList)
        {
            if (countDictionary.ContainsKey(item.itemName))
            {
                countDictionary[item.itemName]++;
            }
            else
            {
                countDictionary[item.itemName] = 1;
            }
        }

        foreach (var kvp in countDictionary)
        {
            Debug.Log($"Предмет: {kvp.Key}, Кол-во: {kvp.Value}");
        }
    }

    public void OnItemButtonClick(Button button)
    {
        // Получаем спрайт, прикрепленный к Image компонента кнопки
        buttonSprite = button.GetComponent<Image>().sprite;

        // Здесь вы можете сделать что-то с этим спрайтом
        deleteButton.SetActive(true);
    }

    public void OnDeleteButton()
    {
        RemоveItem(items.FirstOrDefault(obj => obj.itemIcon == buttonSprite));
        deleteButton.SetActive(false);
    }
}
