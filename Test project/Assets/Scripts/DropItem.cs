using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private InventoryManager inventoryManager;

    //public Sprite ammoSprite;
    //public int slotIndex;

    private GameObject playerObj;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        //inventoryPanel = FindObjectOfType<InventoryManager>();

       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ����� ����� �������� ������ ��� ���������� �������� � ��������� ������ ��� ������ ��������

            //PlayerShooting playerShooting = playerObj.GetComponent<PlayerShooting>();          
            

            //InventoryManager inventoryManager = inventoryPanel.GetComponent<InventoryManager>();

            Item item = GetComponent<Item>();

            Debug.Log(item.itemName);

            inventoryManager.AddItem(item);

            Destroy(gameObject); // ���������� ������� ��� ��������������� � �������

            
        }
    }
}
