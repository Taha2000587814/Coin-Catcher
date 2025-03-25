using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tube : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject BombPrefab;
    public Transform SpawnPoint;

    public bool Shoot;
    public float FallSpeed;
    public float DestroyTime;

    // New variables for random spawn timing
    public float minFallTime = 1f;  // Minimum time before spawning
    public float maxFallTime = 5f;  // Maximum time before spawning


    // New variables for automatic movement
    public float moveSpeed = 2f;    // Speed of horizontal movement
    public float moveRange = 5f;    // Range of horizontal movement
    private float direction = 1f;   // Current movement direction

    private void Start()
    {
        Shoot = true;
    }

    private void FixedUpdate()
    {
        if (Shoot)
        {
            Shoot = false;
            StartCoroutine(SpawnEnum());
        }

        // Move the tube horizontally
        MoveTubeHorizontally();
    }

    public IEnumerator SpawnEnum()
    {
        // Wait for a random amount of time between minFallTime and maxFallTime
        float randomFallTime = Random.Range(minFallTime, maxFallTime);
        yield return new WaitForSeconds(randomFallTime);

        // Call the method to spawn the object (coin or bomb)
        SpawnItem();
    }

    public void SpawnItem()
    {
        Vector3 BoxPoint = SpawnPoint.position;
        int randomObj = Random.Range(0, 4);
        var spawnedObj = randomObj != 0 ? CoinPrefab : BombPrefab;
        GameObject Box = Instantiate(spawnedObj, BoxPoint, Quaternion.identity);
        Box.GetComponent<Rigidbody2D>().AddForce(Vector2.down * FallSpeed);
        Destroy(Box, DestroyTime);
        Shoot = true;
    }

    // New method to move the tube horizontally
    private void MoveTubeHorizontally()
    {
        // Move the tube left and right within the specified range
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // Check if the tube reached the boundaries of the range
        if (transform.position.x > moveRange || transform.position.x < -moveRange)
        {
            direction *= -1;  // Change direction
        }
    }

    public void DisableTube()
    {
        Debug.Log("DisableTube");
        StopCoroutine(SpawnEnum());
        Destroy(this);
    }

    public void AdjustFallSpeed(float multiplier)
    {
        FallSpeed *= multiplier;
    }
}
