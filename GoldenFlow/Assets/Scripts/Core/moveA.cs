using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveA : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement

    void Update()
    {
        // Get input from the horizontal and vertical axes
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down arrow

        // Create a movement vector
        Vector2 movement = new Vector2(moveX, moveY);

        // Move the object using the movement vector, time, and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
