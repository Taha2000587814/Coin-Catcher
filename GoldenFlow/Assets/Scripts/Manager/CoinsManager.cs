using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;
    public float countDuration = 1;
    public TextMeshProUGUI CoinsText;
    public float currentValue = 0, targetValue = 0;
    public string Key = "Coins";
    Coroutine _C2T;

    void Start()
    {
        Instance = this;
        CoinsText.text = PlayerPrefs.GetInt(Key, 0).ToString();
        currentValue = float.Parse(CoinsText.text);
        targetValue = currentValue;
    }

    IEnumerator CountTo(float targetValue)
    {
        var rate = Mathf.Abs(targetValue - currentValue) / countDuration;
        while (currentValue != targetValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, rate * Time.deltaTime);
            int coinsNumber = (int)currentValue;
            CoinsText.text = coinsNumber.ToString();

            // if (NumbersFormater.Instance == null)
            // {
            //     Debug.Log("A");
            // }
            // else
            // {
            //     Debug.Log("B");
            //     string CoinsNumber = NumbersFormater.Instance.FormattedNumber((int)currentValue);
            //     CoinsText.text = CoinsNumber.ToString();
            // }
            yield return null;
        }
    }

    public void AddValue(float value)
    {
        Debug.Log("AddValue");
        targetValue += value;
        PlayerPrefs.SetInt(Key, (int)targetValue);
        if (_C2T != null)
            StopCoroutine(_C2T);
        _C2T = StartCoroutine(CountTo(targetValue));
    }

    public void SetTarget(float target)
    {
        targetValue = target;
        if (_C2T != null)
            StopCoroutine(_C2T);
        _C2T = StartCoroutine(CountTo(targetValue));
    }

    // public float GetCurrentBonus() { return targetValue; }

}
