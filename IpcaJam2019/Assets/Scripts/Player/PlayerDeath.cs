using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector2 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AY");
        if (collision.gameObject.tag == "Obstacle")
        {
            Die();
        }
    }

    public void MoveStartingPosition()
    {
        GetComponent<Rigidbody2D>().MovePosition(startingPosition);
    }

    public void Die()
    {
        MoveStartingPosition();
        //AudioManager.Instance.Play();
    }
}
