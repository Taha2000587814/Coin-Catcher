using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoveSwipe : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private Vector3 startRocketPosition, endRocketPosition;
    [SerializeField] private float HorizontalX;
    [SerializeField] private float flightDuration = 0.1f;
    private float flyTime;

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPosition = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if ((endTouchPosition.x < startTouchPosition.x) && transform.position.x > -HorizontalX)
                StartCoroutine(Fly("left"));

            if ((endTouchPosition.x > startTouchPosition.x) && transform.position.x < HorizontalX)
                StartCoroutine(Fly("right"));
        }
    }

    private IEnumerator Fly(string whereToFly)
    {
        switch (whereToFly)
        {
            case "left":
                flyTime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3
                    (startRocketPosition.x - HorizontalX, transform.position.y, transform.position.z);

                while (flyTime < flightDuration)
                {
                    flyTime += Time.deltaTime;
                    transform.position = Vector2.Lerp
                        (startRocketPosition, endRocketPosition, flyTime / flightDuration);
                    yield return null;
                }
                break;

            case "right":
                flyTime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3
                    (startRocketPosition.x + HorizontalX, transform.position.y, transform.position.z);

                while (flyTime < flightDuration)
                {
                    flyTime += Time.deltaTime;
                    transform.position = Vector2.Lerp
                        (startRocketPosition, endRocketPosition, flyTime / flightDuration);
                    yield return null;
                }
                break;
        }

    }
}
