using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        gameObject.SetActive(true);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
