using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinJar : MonoBehaviour
{
    public int CoinsCount;
    public int CoinsNumber;
    public float DestroyTime = 0.4f;

    private bool completed;
    private Animation anim;

    private void Start()
    {
        // anim = GetComponent<Animation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (completed) return;

        if (collision.CompareTag("coin"))
        {
            AudioManager.instance.Play("Collect");
            CoinsManager.Instance.AddValue(1);
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.tag = "Untagged";

            CoinsNumber++;
            GameManager.Instance.IncreaseDifficulty();
            GameManager.Instance.AddCoin();
            if (CoinsNumber >= CoinsCount)
            {
                Debug.Log("move");
                completed = true;

                GameManager.Instance.ChangeJar();
                GetComponent<Animator>().SetTrigger("Move");
                Destroy(gameObject, DestroyTime);
            }
        }
        else if (collision.CompareTag("Bomb"))
        {
            Debug.Log("Lose!");
            AudioManager.instance.Play("Bomb");
            GameManager.Instance.LevelLose();

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    public void ChangeJarEvent()
    {
        // GameManager.Instance.ChangeJar();
    }
}
