using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player; // ������ �� ������
    public float speed = 2f; // �������� �������� ����������
    public int damage = 10; // ����, ������� ������� ���������

    public int maxHealth = 100; // ������������ �������� �����
    private int currentHealth; // ������� �������� �����

    public float detectionRange = 5f; // ���������, �� ������� ���� �������� ��������� � ������

    private Rigidbody2D rb;

    public GameObject dropItemPrefab; // ������ ��������, ������� ����� ��������
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

        currentHealth = maxHealth; // ������������� ������� �������� ������ �������������
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // �������� ���������� � ������
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ���������� �� ��������� � �������
        if (collision.gameObject.CompareTag("Player"))
        {
            // �������� ������ �� ������, ����������� �������
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)

                playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ��������� �������� �� �������� �����

        HealthBar healthBar = gameObject.GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die(); // ���� �������� ������ ��� ����� 0, ���� �������
        }
    }

    void Die()
    {        
        Destroy(gameObject); // ���������� ������ �����
        DropItem();
    }

    void DropItem()
    {
        if (dropItemPrefabs != null)
        {
            GameObject dropItem = dropItemPrefabs[Random.Range(0, dropItemPrefabs.Length)];
            Instantiate(dropItem, transform.position, Quaternion.identity); // ������� ������� �� ����� ������ �����
        }
    }
}