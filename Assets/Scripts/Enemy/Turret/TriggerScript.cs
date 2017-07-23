/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    #region Variables 
    [HideInInspector]
    public bool playerInRange;
    public Animator turretAnimator;
    #endregion
    private void Awake()
    {
        if (turretAnimator == null)
        {
            Debug.LogError("Please Attach the turret Animator to the Turret Model Script");
        }
    }
    #region Unity Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            turretAnimator.SetBool("Alert", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            turretAnimator.SetBool("Alert", false);
        }
    }

    #endregion

    #region UserDefined

    #endregion

}
