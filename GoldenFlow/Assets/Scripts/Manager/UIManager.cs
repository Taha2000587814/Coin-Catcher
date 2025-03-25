using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject PausePanel;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1.0f;
    }
    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
