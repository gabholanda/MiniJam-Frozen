using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject wallPrefab;
    private GameObject spawnedWall;
    public BaseStatsContainer baseStat;
    public Vector2 moveDirection;

    [SerializeField] private Rigidbody2D rb;
    float horizontalInput;
    float verticalInput;
    public float dashTimer = 0.5f;
    private bool isDashing;
    private bool isDashOnCooldown = false;
    public float wallSpawnDistance;
    private Vector2 dashStartPosition;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            if (!animator)
            {
                return;
            }
            if (moveDirection.magnitude == 0)
            {
                animator.Play("Idle");
            }
            else
            {
                animator.Play("Walking");
            }
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
                animator.Play("Dash_Beginning");
                StartCoroutine(DashCooldown());
                StartCoroutine(DashTimer());
            }
        }
    }

    IEnumerator DashTimer()
    {
        Vector2 originalDirection = moveDirection;

        float startTime = 0.0f;

        while (startTime < dashTimer)
        {
            rb.velocity = new Vector2(originalDirection.x * baseStat.dashSpeed, originalDirection.y * baseStat.dashSpeed);
            yield return new WaitForEndOfFrame();
            startTime += Time.deltaTime;
            float currentDistance = Vector2.Distance(transform.position, dashStartPosition);
            if (currentDistance >= wallSpawnDistance)
            {
                SpawnWall();
                dashStartPosition = transform.position;
            }
        }

        animator.Play("Dash_Ending");
    }

    public void TriggerDashEnd()
    {
        isDashing = false;
        Move();
    }

    void SpawnWall()
    {
        Vector2 wallSpawnPosition = new Vector2(transform.position.x, transform.position.y) - (moveDirection.normalized * wallSpawnDistance);

        spawnedWall = Instantiate(wallPrefab, wallSpawnPosition, Quaternion.identity);
        spawnedWall.GetComponent<Animator>().Play("Ice_Spawn");
    }

    IEnumerator DashCooldown()
    {

        isDashOnCooldown = true;
        yield return new WaitForSeconds(baseStat.dashCooldown);
        isDashOnCooldown = false;

    }
}
