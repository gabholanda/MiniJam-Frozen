using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BaseStatsContainer baseStat;
    Vector2 moveDirection;

    [SerializeField]    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput);
        rb.velocity = new Vector2(moveDirection.x * baseStat.Speed, moveDirection.y * baseStat.Speed);
    }
}
