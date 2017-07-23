/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    #region Variables 
    Turret turretScript;
    float speedOfBullet;

    #endregion

    #region Unity Functions
    private void Awake()
    {
        turretScript = GameObject.FindGameObjectWithTag("Turret").GetComponent<Turret>();
        bulletSpeed += turretScript.speedOfBullet;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().UpdateHealth(-10);
            Destroy(gameObject);
        }
    }

    #endregion

    #region UserDefined
    public void BulletSpeed(float _speedOfBullet)
    {
        speedOfBullet = _speedOfBullet;
    }
    #endregion

}
