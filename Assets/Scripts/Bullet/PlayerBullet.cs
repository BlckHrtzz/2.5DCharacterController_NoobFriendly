/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    #region Variables 
    PlayerController playerController;

    #endregion

    #region Unity Functions
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bulletSpeed += playerController.speedOfBullet;                  //Setting The New speed Relative to Player.
        bulletSpeed += Mathf.Abs(playerController.velocity.x);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Damage To Turret
        if (other.tag == "Turret")
        {
            other.GetComponent<Turret>().UpdateHealth(10);
            Destroy(gameObject);
        }
        else
        //Damage to enemy
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().UpdateHealth(30);
            Destroy(gameObject);
        }
    }
    #endregion

    #region UserDefined

    #endregion

}
