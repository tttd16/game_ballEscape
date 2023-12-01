using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckComplete : MonoBehaviour
{
    float time=1f;
    private bool counting = false;
    void Update()
    {
        if (counting)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                counting = false;
                GameManager.instance.CompleteLevel();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (counting)
            return;
        if (collision.gameObject.CompareTag("Ball"))
        {
            counting = true;
            
        }
    }
}
