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

    //Jumping variables
    private float verticalAxis = 0;
    public float jumpAmount = 10;
    private float jumpForce = 0;
    public bool onGround = true;
    public bool specialOnGround = true;
    public LayerMask groundLayers;

    //Collider
    private Collider2D collider;

    //Debug
    public Vector2 debugDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        Physics2D.gravity = new Vector2(0, -19.8f);
    }

    // Update is called once per frame
    void Update()
    {
       


        //Movement
        HorizontalMovement();

        #region Jump

        //Verifica se o jogador está ou não no chão
        isPlayerGrounded();

        Jump();

        #endregion

        debugDir = rb.velocity;
        SpecialOnGround();

    }

    private void HorizontalMovement()
    {
        horizonalAxis = Input.GetAxis("Horizontal") * horizontalSpeed;

        direction = new Vector2(horizonalAxis, rb.velocity.y);

        rb.velocity = direction;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && (onGround || specialOnGround))
        {
            rb.velocity = Vector2.up * jumpAmount;
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y <= jumpAmount && (!onGround && !specialOnGround))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }

    private void isPlayerGrounded()
    {
        onGround = Physics2D.OverlapArea(new Vector2(transform.position.x - collider.bounds.size.x/2, transform.position.y - collider.bounds.size.y / 2), 
            new Vector2(transform.position.x + collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2), groundLayers);
    }

    private void SpecialOnGround()
    {
        Vector2 newPos = new Vector2(transform.position.x - (collider.bounds.size.x / 2 * 1.05f), transform.position.y - (collider.bounds.size.y * 0.3f));
        Vector2 newPos2 = new Vector2(transform.position.x + (collider.bounds.size.x / 2 * 1.05f), transform.position.y - (collider.bounds.size.y * 0.3f));

        Debug.DrawRay(newPos, Vector2.down * (collider.bounds.size.y/3 + 0.1f));
        Debug.DrawRay(newPos2, Vector2.down * (collider.bounds.size.y/3 + 0.1f));

        if (Physics2D.Raycast(newPos, Vector2.down, (collider.bounds.size.y / 3 + 0.1f)) || 
            Physics2D.Raycast(newPos2, Vector2.down, (collider.bounds.size.y / 3 + 0.1f)))
        {
            specialOnGround = true;
        }
        else
        {
            specialOnGround = false;
        }
    }

    
  
}