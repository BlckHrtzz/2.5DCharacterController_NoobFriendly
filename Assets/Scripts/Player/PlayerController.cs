/*
Copyright (c) Mr BlckHrtzz
Let The Mind Dominate The Hrtzz
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;                //Provides Multi-Platform controller support. Free On GitHub by name InControl.

public class PlayerController : MonoBehaviour
{

    #region Variables 
    InputDevice gamepad;                    //To get the Input Devices attached.
    CharacterController characterController;//Reference To CharacterController Componenet.
    float animVelocityX;

    [HideInInspector]
    public Vector3 velocity;
    Vector2 input;

    [Header("UnityStuff")]
    public Animator playerAnimator;
    public LayerMask groundLayer;
    public Image currentHealth;
    public GameObject playerBullet;
    public Transform shootPoint;


    [Header("X Movement Factor")]
    public float inAirAcceleration;
    public float onGroundAcceleration;
    float targetVelocityX;
    float xSmmothFactor;
    public float moveSpeed = 5.0f;          //The speed of the Player.  
    Vector3 lastMotion;                     //To store the last motion of the player
    bool isFacingRight = true;
    Vector3 faceDirection;
    bool sideColliding;

    [Header("Y Movement Factors")]
    public float minJumpHeight = 1f;        //The Min jump height 
    public float maxJumpHeight = 3.0f;      //The Max height the player can reach in one jump.
    public float timeToJumpApex = 0.3f;     //Time required to reach the jump Apex.
    float jumpVelocity;
    float minJumpVelocity;
    [Tooltip("Make it True To Give Player The Ability Of Air Jump")]
    public bool airJumpAllowed = false;
    [Range(2, 10)]
    [Tooltip("Above Field Must Be True To Have This field Any Effect")]
    public int maxJumpAllowed = 2;          //Max Air jumps Allowed
    int jumpsLeft;                          //Number of Jumps Left
    float gravity;
    bool isJumping = false;
    bool isGrounded;
    const float skinWidth = 0.1f;

    [Header("PlayerAttributes")]
    float health;
    bool damageTaken = false;
    float damageTimer = 0;
    bool isDead;
    [Range(0, 5f)]
    public float speedOfBullet = 0;



    #endregion

    #region Unity Functions

    void Awake()
    {
        health = 100;
        gamepad = InputManager.ActiveDevice;
        characterController = GetComponent<CharacterController>();

        gravity = 2 * maxJumpHeight / (Mathf.Pow(timeToJumpApex, 2));           //Calculating the Gravity using the Eqaution of Motion
        jumpVelocity = gravity * timeToJumpApex;                                //Calculating the JumpVelocity using the Eqaution of Motion
        minJumpVelocity = Mathf.Sqrt(2 * gravity * minJumpHeight);              //Calculating MinJump Height;
    }

    void Update()
    {
        if (isDead == true)
            return;
        if(GameManager.Instance.gameWin)
        {
            Destroy(this);
            return;
        }

        isGrounded = IsGrounded();
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));      //Getting The Input
        Flip(input.x);      //Flipping The Player

        //Jumping Conditions
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpVelocity;
            playerAnimator.SetTrigger("Jump");
            jumpsLeft++;
            isJumping = true;

        }
        else if (!isGrounded && jumpsLeft < maxJumpAllowed && Input.GetButtonDown("Jump") && airJumpAllowed)
        {
            velocity.y = jumpVelocity;
            jumpsLeft++;
        }
        else if (isGrounded || characterController.collisionFlags == CollisionFlags.Above)
        {
            jumpsLeft = 0;
            velocity.y = 0;
            isJumping = false;
            playerAnimator.ResetTrigger("Jump");

        }
        //For Small Jump.
        if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject tempBullet = Instantiate(playerBullet, shootPoint.position, transform.rotation);
        }

        //Setting the Aninator Reference.
        playerAnimator.SetFloat("VelocityY", velocity.y);
        playerAnimator.SetBool("IsJumping", isJumping);

        targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref xSmmothFactor, isGrounded ? onGroundAcceleration : inAirAcceleration);
        animVelocityX = Mathf.Abs(velocity.x);                 //For the use of Animator.
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);        //Moving The Player.

        //Setting the Aninator Reference.
        playerAnimator.SetFloat("Speed", animVelocityX);
        playerAnimator.SetBool("IsGrounded", isGrounded);
        playerAnimator.SetBool("SideCollision", sideColliding);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(characterController.bounds.center, Vector3.down, Color.red);

        if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
        {
            velocity.x = 0;
            sideColliding = true;
        }
        else
            sideColliding = false;

        if (damageTaken)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer > 1)
            {
                damageTaken = false;
                damageTimer = 0;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.collider.tag)
        {
            case "Coin":
                GameManager.Instance.UpdateCoin();
                Destroy(hit.gameObject);
                break;
            case "Enemy":
                if (damageTaken == false)
                {
                    playerAnimator.SetBool("GotHit", true);
                    UpdateHealth(-hit.gameObject.GetComponent<Enemy>().damageToPlayer);
                    Debug.Log(health);
                }
                break;
        }
    }

    #endregion


    #region UserDefined
    public void Flip(float inputX)
    {
        if (inputX > 0 && !isFacingRight || inputX < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(Vector3.up, 180, Space.Self);

        }
    }

    bool IsGrounded()
    {
        Vector3 leftOrigin = characterController.bounds.center;
        Vector3 rightOrigin = characterController.bounds.center;

        leftOrigin.x -= (characterController.bounds.extents.x - skinWidth * 1.5f);
        rightOrigin.x += (characterController.bounds.extents.x - skinWidth * 1.5f);
        Debug.DrawRay(leftOrigin, Vector3.down, Color.red);
        Debug.DrawRay(rightOrigin, Vector3.down, Color.red);

        if (Physics.Raycast(leftOrigin, Vector3.down, (characterController.height / 2) + skinWidth, groundLayer))
            return true;
        if (Physics.Raycast(rightOrigin, Vector3.down, (characterController.height / 2) + skinWidth, groundLayer))
            return true;

        return false;
    }

    public void UpdateHealth(int h)
    {
        health += h;
        currentHealth.fillAmount = health / 100;
        damageTaken = true;

        if (health <= 0)
        {
            isDead = true;
            GameManager.Instance.isDead = isDead;
            playerAnimator.SetBool("IsDead", isDead);
            Destroy(this);
            Destroy(gameObject, 1.5f);
            return;
        }
    }


    #endregion

}
