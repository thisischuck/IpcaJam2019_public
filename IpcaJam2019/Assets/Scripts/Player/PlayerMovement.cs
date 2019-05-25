using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;

    //Movement variables
    private float horizonalAxis = 0;
    public float horizontalSpeed = 5;
    //Movement Rope Variables
    private float ropeHorizontalSpeed = 5;
    private float ropeVerticalSpeed = 10;


    //Jumping variables
    private float verticalAxis = 0;
    public float jumpAmount = 10;
    private float jumpForce = 0;
    public bool onGround = true;
    public LayerMask groundLayers;
    public LayerMask vinesLayers;
    public LayerMask currentLayerMask;

    //Collider
    private Collider2D collider;

    //Debug
    public Vector2 debugDir;

    private Action action;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        Physics2D.gravity = new Vector2(0, -19.8f);

        SetNormalMovement();
    }

    void Update()
    {
        action?.Invoke();

        IsPlayerOnVines();
    }

    #region Control Action
    public void SetNormalMovement()
    {
        currentLayerMask = groundLayers;
        action += HorizontalMovement;
        action += isPlayerGrounded;
        action += Jump;
    }
    public void UnsetNormalMovement()
    {
        action -= HorizontalMovement;
        action -= isPlayerGrounded;
        action -= Jump;
    }
    public void SetRopeMovement()
    {
        currentLayerMask = vinesLayers;
        action += RopeMovement;
        Physics2D.gravity = Vector2.zero;
    }
    public void UnsetRopeMovement()
    {
        action -= RopeMovement;
        rb.velocity = new Vector3(rb.velocity.x, 0);
        Physics2D.gravity = new Vector2(0, -19.8f);
    }
    #endregion

    #region Rope Movement
    private void RopeMovement()
    {
        float vert_axis = Input.GetAxis("Vertical") * ropeVerticalSpeed;
        float axis = Input.GetAxis("Horizontal") * ropeHorizontalSpeed;

        direction = new Vector2(axis, vert_axis);

        rb.velocity = direction;
    }
    private void IsPlayerOnVines()
    {
        if( Physics2D.OverlapArea(new Vector2(transform.position.x - collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2),
            new Vector2(transform.position.x + collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2), vinesLayers))
        {
            if(currentLayerMask != vinesLayers)
            {
                SetRopeMovement();
                UnsetNormalMovement();
            }
        }
        else
        {
            if(currentLayerMask == vinesLayers)
            {
                UnsetRopeMovement();
                SetNormalMovement();
            }
        }
    }
    #endregion

    #region Normal Movement
    private void HorizontalMovement()
    {
        horizonalAxis = Input.GetAxis("Horizontal") * horizontalSpeed;

        direction = new Vector2(horizonalAxis, rb.velocity.y);

        rb.velocity = direction;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            rb.velocity = Vector2.up * jumpAmount;
        }

        //TODO: Extriminar bug de premir muitas vezes W no ar

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y <= jumpAmount && !onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }
    private void isPlayerGrounded()
    {
        onGround = Physics2D.OverlapArea(new Vector2(transform.position.x - collider.bounds.size.x/2, transform.position.y - collider.bounds.size.y / 2), 
            new Vector2(transform.position.x + collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2), groundLayers);

        if (onGround == true)
        {
            jumpForce = 0;
        }
    }
    #endregion
}