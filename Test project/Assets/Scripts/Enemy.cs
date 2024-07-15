using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player; // Ссылка на игрока
    public float speed = 2f; // Скорость движения противника
    public int damage = 10; // Урон, который наносит противник

    public int maxHealth = 100; // Максимальное здоровье врага
    private int currentHealth; // Текущее здоровье врага

    public float detectionRange = 5f; // Дистанция, на которой враг начинает двигаться к игроку

    private Rigidbody2D rb;

    public GameObject dropItemPrefab; // Префаб предмета, который будет выпадать
    public GameObject[] dropItemPrefabs;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");
        }

        currentHealth = maxHealth; // Устанавливаем текущее здоровье равным максимальному
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Движение противника к игроку
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, столкнулся ли противник с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            // Получаем ссылку на скрипт, управляющий игроком
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)

                playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем здоровье на величину урона

        HealthBar healthBar = gameObject.GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die(); // Если здоровье меньше или равно 0, враг умирает
        }
    }

    void Die()
    {        
        Destroy(gameObject); // Уничтожаем объект врага
        DropItem();
    }

    void DropItem()
    {
        if (dropItemPrefabs != null)
        {
            GameObject dropItem = dropItemPrefabs[Random.Range(0, dropItemPrefabs.Length)];
            Instantiate(dropItem, transform.position, Quaternion.identity); // Создаем предмет на месте смерти врага
        }
    }
}