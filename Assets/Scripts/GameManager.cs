using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject WinWindow;
    public GameObject LoseWindow;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowLoseWindow()
    {
        LoseWindow.SetActive(true);
    }
    public void ShowWinWindow()
    {
        WinWindow.SetActive(true);
    }
}
