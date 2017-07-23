/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Turret : EnemySuperClass
{

    #region Variables 
    [Header("Unity Stuff")]
    public GameObject bullet;
    public Transform shootPosition;

    [Header("Turret Attributes")]
    [Range(1, 5)]
    public float shootingDelay = 1;
    [Range(0, 10)]
    public float speedOfBullet = 0;
    float shootTimer = 0;

    TriggerScript trigger;
    #endregion

    #region Unity Functions

    void Start()
    {
        health = 100f;
        trigger = GetComponentInChildren<TriggerScript>();
    }

    void Update()
    {
        if (trigger.playerInRange && !GameManager.Instance.isDead)
        {
            if (shootTimer > shootingDelay)
            {
                GameObject tempBullet = Instantiate(bullet, shootPosition.position, transform.rotation);
                tempBullet.GetComponent<EnemyBullet>().BulletSpeed(speedOfBullet);
                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }
    }

    #endregion

    #region UserDefined
    //public void UpdateHealth(int h)
    //{
    //    health -= h;
    //    currentHealth.fillAmount = health / 100;
    //    if (health <= 0)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //}
    #endregion

}
