using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameWin : MonoBehaviour
{

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowGameWinMenu()
    {
        gameObject.SetActive(true);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
