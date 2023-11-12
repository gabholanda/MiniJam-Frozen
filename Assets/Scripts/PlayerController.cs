using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BaseStatsContainer baseStat;
    Vector2 moveDirection;

    [SerializeField]    private Rigidbody2D rb;
    float horizontalInput;
    float verticalInput;
    public float dashTimer = 0.5f;
    private bool isDashing;
    private bool isDashOnCooldown;
    public float distace;
    public float wallSpawnDistance;
    public float currentWallPos;
    private Vector2 dashStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

     
    }

    // Update is called once per frame

    void Update()
    {
        Move();
        Dash();
    }

    void Move()
    {
        if (!isDashing)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            moveDirection = new Vector2(horizontalInput, verticalInput);
            rb.velocity = new Vector2(moveDirection.x * baseStat.Speed, moveDirection.y * baseStat.Speed);
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isDashOnCooldown)
            {
                isDashing = true;
                dashStartPosition = transform.position;
                StartCoroutine(DashTimer());
            }
        }
    }

    IEnumerator DashTimer()
    {
        StartCoroutine(DashCooldown());

        Vector2 originalVelocity = rb.velocity;
        Vector2 originalDirection = moveDirection;


        float startTime = Time.time;

        while (Time.time - startTime < dashTimer)
        {
            rb.velocity = new Vector2(originalDirection.x * baseStat.dashSpeed, originalDirection.y * baseStat.dashSpeed);
            float currentDistance = Vector2.Distance(transform.position, dashStartPosition);
            if (currentDistance >= wallSpawnDistance)
            {
                SpawnWall();
                dashStartPosition = transform.position; 
            }

            yield return null;
        }

        rb.velocity = originalVelocity;
        isDashing = false;
    }

    void SpawnWall()
    {
        
        Debug.Log("Wall spawned at: " + transform.position);
    }

    IEnumerator DashCooldown() {

        isDashOnCooldown = true;
        yield return new WaitForSeconds(baseStat.dashCooldown);
        isDashOnCooldown = false;

    }
}
