using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public bool useTimer = false;
    public float timer = 1.0f;


    private void Awake()
    {
        if (useTimer)
        {
            StartCoroutine(DestroyOnTimer());
        }
    }

    public IEnumerator DestroyOnTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public void TriggerDestroy()
    {
        Destroy(gameObject);
    }
}
