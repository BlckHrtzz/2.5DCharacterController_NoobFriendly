/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : EnemySuperClass
{

    #region Variables 
    [Header("Enemy Attributes")]
    [Range(30, 50)]
    public int damageToPlayer = 30;
    public float enemyMoveSpeed;

    [Header("Unity Stuff")]
    public Animator enemyAnimator;
    int waypointIndex = 0;
    public Transform[] wayPoints;
    Transform targetWaypoint;

    float idleTimer;
    bool destReached;
    bool isFacingRight = true;
    float direction;
    

    #endregion

    #region Unity Functions

    void Start()
    {
        health = 100f;
        targetWaypoint = wayPoints[waypointIndex];
        transform.position = targetWaypoint.position;
    }

    void Update()
    {

        Flip();
        Vector2 velocity = Vector2.zero;

        if (Mathf.Abs(targetWaypoint.position.x - transform.position.x) <= 0.5f)
        {
            enemyAnimator.SetFloat("Speed", 0f);
            if (idleTimer > 2)
            {
                GetNextWayPoint();
                idleTimer = 0;
                return;
            }
            idleTimer += Time.deltaTime;
            return;
        }
        enemyAnimator.SetFloat("Speed", 1f);
        direction = targetWaypoint.position.x - transform.position.x;
        velocity = new Vector2(direction, 0).normalized;
        transform.Translate(velocity * Time.deltaTime * enemyMoveSpeed, Space.World);
    }

    #endregion

    #region UserDefined


    void GetNextWayPoint()
    {
        if (waypointIndex >= wayPoints.Length - 1)
        {
            waypointIndex = 0;
            System.Array.Reverse(wayPoints);
        }
        waypointIndex++;
        targetWaypoint = wayPoints[waypointIndex];

    }

    void Flip()
    {
        Vector3 faceDirection = transform.localScale;

        if (Mathf.Sign(direction) > 0)
        {
            faceDirection.x = 1;
            transform.localScale = faceDirection;
        }
        else
        if (Mathf.Sign(direction) < 0)
        {
            faceDirection.x = -1;
            transform.localScale = faceDirection;
        }
    }
    #endregion

}
