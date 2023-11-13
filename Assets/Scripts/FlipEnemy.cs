using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemy : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.cachedDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (enemy.cachedDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
