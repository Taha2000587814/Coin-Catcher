using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float DestroyTime = 2.5f;
    public float DisableDistance = -3.5f;

    private void Start()
    {
        Invoke("RemoveCoin", DestroyTime);
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= DisableDistance)
        {
            // GetComponent<Collider2D>().enabled = false;
            // Debug.Log("Disable!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BottomSide")
        {
            GetComponent<Collider2D>().enabled = false;
            // Debug.Log("AAA");
        }
    }

    private void RemoveCoin()
    {
        // Debug.Log("RemoveCoin!");
        // Destroy(gameObject);
    }
}
