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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Die();
        }
    }

    public void MoveStartingPosition()
    {
        StartCoroutine(MoveToPos());
        GetComponent<SplatParticleSystem>().CreateBothSplats(transform.position);
    }

    public void Die()
    {
        MoveStartingPosition();
        //AudioManager.Instance.Play();
    }

    IEnumerator MoveToPos()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody2D>().MovePosition(startingPosition);
    }
}
