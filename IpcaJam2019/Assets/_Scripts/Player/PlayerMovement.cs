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
    public bool specialOnGround = true;
    public LayerMask groundLayers;
    public LayerMask vinesLayers;
    public LayerMask platformLayers;
    public LayerMask currentLayerMask;

    //Collider
    private Collider2D collider;

    //Animator
    private Animator animator;
    private bool rotated;

    //Debug
    public Vector2 debugDir;

    private Action action;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        Physics2D.gravity = new Vector2(0, -19.8f);

        rotated = true;
        SetNormalMovement();
    }

    void Update()
    {
        action?.Invoke();

        IsPlayerOnVines();
    }

    #region Death Action
    public void SetDeath()
    {
        UnsetNormalMovement();
        UnsetRopeMovement();
        Physics2D.gravity = new Vector2(0, 0);
        rb.velocity = Vector2.zero;
    }

    public void Respawn()
    {
        GetComponent<Animator>().Play("Respawn");
        GetComponent<PlayerMovement>().UnsetDeath();
    }

    public void UnsetDeath()
    {
        Physics2D.gravity = new Vector2(0, -19.8f);
        UnsetRopeMovement();
        SetNormalMovement();
    }

    #endregion

    #region Control Action
    public void SetNormalMovement()
    {
        currentLayerMask = groundLayers;
        action += HorizontalMovement;
        action += isPlayerGrounded;
        action += SpecialOnGround;
        action += Jump;
    }
    public void UnsetNormalMovement()
    {
        action -= HorizontalMovement;
        action -= isPlayerGrounded;
        action -= SpecialOnGround;
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
        currentLayerMask = groundLayers;
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

        if (axis > 0)
        {
            animator.SetBool("Right", true);
        }
        else animator.SetBool("Right", false);
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


        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(5f, transform.localScale.y, transform.localScale.z);
            rotated = false;
        }
        else if(Input.GetAxis("Horizontal") < -0.1f)
        {
            rotated = true;
            float a = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && (onGround || specialOnGround))
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
        onGround = (Physics2D.OverlapArea(new Vector2(transform.position.x - collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2),
            new Vector2(transform.position.x + collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2), groundLayers)
            ||
            Physics2D.OverlapArea(new Vector2(transform.position.x - collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2),
            new Vector2(transform.position.x + collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2), platformLayers));
    }

    private void SpecialOnGround()
    {
        Vector2 newPos = new Vector2(transform.position.x - (collider.bounds.size.x / 2 * 1.05f), transform.position.y - (collider.bounds.size.y * 0.3f));
        Vector2 newPos2 = new Vector2(transform.position.x + (collider.bounds.size.x / 2 * 1.05f), transform.position.y - (collider.bounds.size.y * 0.3f));

        Debug.DrawRay(newPos, Vector2.down * (collider.bounds.size.y/3 + 0.1f));
        Debug.DrawRay(newPos2, Vector2.down * (collider.bounds.size.y/3 + 0.1f));


        if (Physics2D.Raycast(newPos, Vector2.down, (collider.bounds.size.y / 3 + 0.1f), groundLayers) || 
            Physics2D.Raycast(newPos2, Vector2.down, (collider.bounds.size.y / 3 + 0.1f), groundLayers))
        {
            specialOnGround = true;
        }
        else
        {
            specialOnGround = false;
        }
    }
    #endregion
}