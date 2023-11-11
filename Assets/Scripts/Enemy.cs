using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float stoppingDistance = 1.5f;
    public Transform player;
    public BaseStatsContainer enemyStat;

    private Rigidbody2D rb;
    private HealthComponent healthComponent;
    private bool canMove = true;

    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found on enemy!");
        }

        healthComponent.OnDeath += HandleDeath;
    }

    void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * enemyStat.Speed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            rb.velocity = Vector2.zero;
        }
    }


    void HandleDeath(GameObject dead)
    {
  
        Destroy(dead);

    }


}
