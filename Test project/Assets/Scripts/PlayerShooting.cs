using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerShooting : MonoBehaviour
{
    private InventoryManager inventoryManager;

    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, из которой будет вылетать пуля
    public float bulletSpeed = 20f; // Скорость пули
    public float fireRate = 0.5f; // Скорость стрельбы
    public float maxDistance = 10f; // Максимальная дистанция стрельбы

    public int numberOfBullets = 10; // Кол-во пуль

    public Text bulletText; // Отображает кол-во пуль

    private void Start()
    {
        
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void Shoot()
    {
        if (inventoryManager.items.Any(obj => obj.itemType == ItemType.Ammo))
        {
            // Найти ближайшего врага
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy == null)
            {
                return; // Если врагов нет, не стреляем
            }

            // Направление к ближайшему врагу
            Vector2 direction = (nearestEnemy.transform.position - firePoint.position).normalized;

            // Создаем пулю
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;

            // Уничтожаем пулю через некоторое время, чтобы избежать утечек памяти
            Destroy(bullet, 2f);

            numberOfBullets--;

            inventoryManager.RemоveItem(inventoryManager.items.FirstOrDefault(obj => obj.itemType == ItemType.Ammo));
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
