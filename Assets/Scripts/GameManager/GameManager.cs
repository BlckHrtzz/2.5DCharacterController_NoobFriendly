/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    #region Variables
    public GameOver gameOverScript;
    public GameWin gameWinUI;
    int coin;
    public Text coinText;
    public bool isDead = false;
    public bool gameWin = false;
    #endregion

    #region Unity Functions

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coin = 0;
        coinText.text = "Coins : " + coin;
    }

    void Update()
    {
        if (isDead)
        {
            gameOverScript.ShowGameOverMenu();
        }

        if (gameWin)
        {
            gameWinUI.ShowGameWinMenu();
        }
    }

    #endregion

    #region UserDefined
    public void UpdateCoin()
    {
        coin++;
        coinText.text = "Coins : " + coin;
    }


    #endregion

}
