using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Transform JarPlace;
    public GameObject JarPrefab;
    // public TextMeshProUGUI JarText;

    public float JarDuration = 1.5f;
    // public int LevelJars;
    // public int EmptyJars;

    public int coinsCollected;
    public int coinsThreshold = 10;
    public float fallSpeedMultiplier = 1.1f;

    public int totalCoinsCollected;
    public TextMeshProUGUI coinCounterText;

    public int difficultyLevel = 0;
    public int maxDifficultyLevel = 3;

    private void Start()
    {
        // EmptyJars = LevelJars;
        // JarText.text = EmptyJars + " /" + LevelJars;
        coinsCollected = 0;
        totalCoinsCollected = 0;
        UpdateCoinCounter();
    }

    public void ChangeJar()
    {
        Debug.Log("Change Jar!");

        // EmptyJars--;
        // JarText.text = EmptyJars + " /" + LevelJars;
        // if (EmptyJars <= 0)
        //     LevelWin();
        // else
        Invoke("SpawnJar", JarDuration);
    }

    private void SpawnJar()
    {
        GameObject newJar = Instantiate(JarPrefab, JarPlace.position, Quaternion.identity);
        StartCoroutine(MoveJarSmoothly(newJar.transform, JarPlace.position));
    }

    private IEnumerator MoveJarSmoothly(Transform jarTransform, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = jarTransform.position;

        while (elapsedTime < JarDuration)
        {
            jarTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / JarDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        jarTransform.position = targetPosition; // Ensure it reaches the exact position
    }

    private void LevelWin()
    {
        Debug.Log("LevelWin!");
        UIManager.Instance.WinPanel.SetActive(true);
        LevelEnd();
    }

    public void LevelLose()
    {
        Debug.Log("LevelLose!");
        AdsManager.Instance.ShowInterstitialAd();
        UIManager.Instance.LosePanel.SetActive(true);
        LevelEnd();
    }

    private void LevelEnd()
    {
        // Disable All Tubes
        GameObject tubeContainer = GameObject.Find("Tubes");
        foreach (Transform tube in tubeContainer.transform) tube.GetComponent<Tube>().DisableTube();

        // Remove Level Coins
        GameObject[] coins = GameObject.FindGameObjectsWithTag("coin");
        foreach (GameObject _coin in coins) Destroy(_coin.gameObject);
    }

    public void NewJarSpawn()
    {
        // Debug.Log("NewJarSpawn!");
        StartCoroutine(NewJarSpawnEnum());
    }

    private IEnumerator NewJarSpawnEnum()
    {
        yield return new WaitForSeconds(JarDuration);
        GameObject newJar = Instantiate(JarPrefab, JarPlace.position, Quaternion.identity);
        StartCoroutine(MoveJarSmoothly(newJar.transform, JarPlace.position));
    }

    public void IncreaseDifficulty()
    {
        if (difficultyLevel < maxDifficultyLevel)
        {
            coinsCollected++;
            if (coinsCollected >= coinsThreshold)
            {
                coinsCollected = 0;
                fallSpeedMultiplier *= 1.1f;
                AdjustFallSpeed();
                difficultyLevel++;
            }
        }
    }

    private void AdjustFallSpeed()
    {
        GameObject tubeContainer = GameObject.Find("Tubes");
        foreach (Transform tube in tubeContainer.transform)
        {
            tube.GetComponent<Tube>().AdjustFallSpeed(fallSpeedMultiplier);
        }
    }

    public void AddCoin()
    {
        totalCoinsCollected++;
        UpdateCoinCounter();
    }

    private void UpdateCoinCounter()
    {
        coinCounterText.text = "Coins: " + totalCoinsCollected;
    }
}
