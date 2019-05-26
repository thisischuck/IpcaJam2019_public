using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineJump : MonoBehaviour
{
    private readonly float jumpSpeed = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.y > (transform.position.y + 0.8f))
            {
                //Jump
                collision.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
            }
        }
    }
}
