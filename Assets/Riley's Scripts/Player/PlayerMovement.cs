using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Require dependencies for the object
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{



    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    private float horizontal;
    private float vertical;

    private Vector2 moveDirection;

    private bool canDash = true;
    private bool performedDash = false;

    private Vector2 dashForce;

    [SerializeField]
    private Data_PlayerMovement Data;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
  
    }


    void Update() {
        if(GameManager.Instance.playerIsDead) {
            rb.velocity = new Vector2(0,0);
            return;
        }
        
        if(horizontal < 0) 
        {
            sprite.flipX = true;
        } else if(horizontal > 0)
        {
            sprite.flipX = false;
        }

        if(horizontal != 0 || vertical != 0) 
        {
            animator.SetBool("isMoving", true);
        } else 
        {
            animator.SetBool("isMoving", false);
        }

        animator.SetInteger("xVelocity", (int)horizontal);
        animator.SetInteger("yVelocity", (int)vertical);
    }



    void FixedUpdate () 
    {
        if(GameManager.Instance.playerIsDead) {
            return;
        }
        
        HandleMovement();
        HandleDash();
    }

    public void GetMovementValue(InputAction.CallbackContext context) 
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void GetDashInput(InputAction.CallbackContext context) 
    {
        if (context.performed && canDash) 
        {
            performedDash = true;
        }
    }

    private void HandleMovement() 
    {
        float targetSpeedX = horizontal * Data.moveSpeed;
        float targetSpeedY = vertical * Data.moveSpeed;

        #region Calculate AccelRate
        float accelRateX;
        float accelRateY;

        // Gets an acceleration value based on if we are accelerating or trying to decelerate.
        accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        #endregion

        // Calculate difference between current velocity and desired velocity
        float speedDifX = targetSpeedX - rb.velocity.x;
        float speedDifY = targetSpeedY - rb.velocity.y;

        // Calculate force along x-axis and y-axis to apply to the player
        float movementX = speedDifX * accelRateX;
        float movementY = speedDifY * accelRateY;

        rb.AddForce(new Vector2(movementX, movementY), ForceMode2D.Force);
    }

    private void HandleDash() 
    {
        // When you perform a dash, add the correct force to dashForce according to the player direction.
        if (performedDash) 
        {
            Debug.Log("dashing");
            canDash = false;
            performedDash = false;

            // Use the player's current velocity direction as the dash force direction
            Vector2 dashDirection = rb.velocity.normalized;
            dashForce = dashDirection * Data.dashPower;

            StartCoroutine("DashCooldown");
        }

        // Add the dash force to the player
        rb.AddForce(dashForce, ForceMode2D.Force);
    }

    private IEnumerator DashCooldown() 
    {
        yield return new WaitForSeconds(Data.dashLength);
        dashForce = Vector2.zero;
        yield return new WaitForSeconds(Data.dashCooldown);
        canDash = true;
    }

   





}