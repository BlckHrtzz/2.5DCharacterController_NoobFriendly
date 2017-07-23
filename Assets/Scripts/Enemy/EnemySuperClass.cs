/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemySuperClass : MonoBehaviour
{

    #region Variables 
    [Header("Unity Stuff")]
    public Image currentHealth;

    [Header("Enemy Attributes")]
    public float health;
    #endregion

    #region Unity Functions

    #endregion

    #region UserDefined
    public void UpdateHealth(int h)
    {
        health -= h;
        currentHealth.fillAmount = health / 100;
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

}
