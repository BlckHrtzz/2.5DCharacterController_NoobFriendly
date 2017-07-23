/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    #region Variables 
    public float bulletSpeed = 5.0f;
    #endregion

    #region Unity Functions

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed, Space.Self);
        if (gameObject != null)
        {
            Destroy(gameObject, 3f);
        }
        
    }

    #endregion

    #region UserDefined

    #endregion

}
