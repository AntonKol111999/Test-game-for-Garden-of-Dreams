using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerShooting : MonoBehaviour
{
    private InventoryManager inventoryManager;

    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // �����, �� ������� ����� �������� ����
    public float bulletSpeed = 20f; // �������� ����
    public float fireRate = 0.5f; // �������� ��������
    public float maxDistance = 10f; // ������������ ��������� ��������

    public int numberOfBullets = 10; // ���-�� ����

    public Text bulletText; // ���������� ���-�� ����

    private void Start()
    {
        
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void Shoot()
    {
        if (inventoryManager.items.Any(obj => obj.itemType == ItemType.Ammo))
        {
            // ����� ���������� �����
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy == null)
            {
                return; // ���� ������ ���, �� ��������
            }

            // ����������� � ���������� �����
            Vector2 direction = (nearestEnemy.transform.position - firePoint.position).normalized;

            // ������� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;

            // ���������� ���� ����� ��������� �����, ����� �������� ������ ������
            Destroy(bullet, 2f);

            numberOfBullets--;

            inventoryManager.Rem�veItem(inventoryManager.items.FirstOrDefault(obj => obj.itemType == ItemType.Ammo));
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(firePoint.position, enemy.transform.position);
            if (distance < minDistance && distance <= maxDistance)
            {
                nearestEnemy = enemy;
                minDistance = distance;
            }
        }

        return nearestEnemy;
    }

   
}
