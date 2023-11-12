using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float stoppingDistance = 1.5f;
    public Transform player;
    public BaseStatsContainer stats;
    protected float speed;
    protected Rigidbody2D rb;
    protected HealthComponent healthComponent;
    protected Animator animator;
    protected Vector2 cachedDirection;
    protected bool canMove = true;
    protected bool isInRange = false;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found on enemy!");
        }

        healthComponent.OnDeath += HandleDeath;

        speed = stats.Speed;
    }

    public virtual void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    protected void MoveTowardsPlayer()
    {
        if (!player)
        {
            Debug.LogError("Player not found!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        cachedDirection = direction;
        rb.velocity = direction * speed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            rb.velocity = Vector2.zero;
            isInRange = true;
            return;
        }
        isInRange = false;
    }


    protected void HandleDeath(GameObject dead)
    {
        Destroy(dead);
        InstantiateBullet();
    }

    protected void InstantiateBullet()
    {
        for (int i = 0; i < 3; i++)
        {

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = directionToPlayer * bulletSpeed;

        }
    }


}
