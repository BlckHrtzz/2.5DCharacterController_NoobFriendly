/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{

    #region Variables 
    InputDevice gamepad;                    //To get the Input Devices attached.
    CharacterController characterController;//Reference To CharacterController Componenet.

    public Vector3 velocity;

    [Header("X Movement Factor")]
    Vector2 input;
    public float moveSpeed = 5.0f;          //The speed of the Player.  
    Vector3 lastMotion;                     //To store the last motion of the player

    [Header("Y Movement Factors")]
    public float maxJumpHeight = 3.0f;      //The Max height the player can reach in one jump.
    public float timeToJumpApex = 0.3f;     //Time required to reach the jump Apex.
    [Tooltip("Make it True To Give Player The Ability Of Air Jump")]
    public bool airJumpAllowed = false;
    [Range(2, 10)]
    [Tooltip("Above Field Must Be True To Have This field Any Effect")]
    public int maxJumpAllowed = 2;          //Max Air jumps Allowed
    int jumpsLeft;                          //Number of Jumps Left
    float gravity;
    float jumpVelocity;
    //bool isJumping = false;

    #endregion

    #region Unity Functions

    void Awake()
    {
        gamepad = InputManager.ActiveDevice;
        characterController = GetComponent<CharacterController>();

        gravity = 2 * maxJumpHeight / (Mathf.Pow(timeToJumpApex, 2));       //Calculating the Gravity using the Eqaution of Motion
        jumpVelocity = gravity * timeToJumpApex;                            //Calculating the JumpVelocity using the Eqaution of Motion
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            velocity.y = jumpVelocity;
            jumpsLeft++;
            velocity.x = lastMotion.x;                                      //Restricting The Player From Changing Dirction While in Air.
        }
        else if (!characterController.isGrounded && jumpsLeft < maxJumpAllowed && Input.GetButtonDown("Jump") && airJumpAllowed)
        {
            velocity.y = jumpVelocity;
            velocity.x = input.x * moveSpeed;                               //Allowing to Player to Change direction in Air while doing airjump.
            jumpsLeft++;
        }
        else if (characterController.isGrounded)
        {
            jumpsLeft = 0;
            velocity.y = 0;
            velocity.x = input.x * moveSpeed;

        }
        if (gamepad.Action1.WasPressed)
        {
            Debug.Log("Button Pressed");
        }
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        lastMotion = velocity;
    }

    #endregion


    #region UserDefined


    #endregion

}
