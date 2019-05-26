using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector2 startingPosition;
    bool dead = false;

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
        if (collision.gameObject.tag == "Obstacle" && !dead)
        {
            Die();
        }
    }

    public void MoveStartingPosition()
    {
        GetComponent<PlayerMovement>().SetDeath();
        dead = true;
        Camera.main.GetComponent<CameraController>().ApplyChromatic();
        Camera.main.GetComponent<CameraController>().ShakeCamera();
        StartCoroutine(MoveToPos());
        GetComponent<SplatParticleSystem>().CreateBothSplats(transform.position);
    }

    public void Die()
    {
        AudioManager.Instance.Play("Death");
        MoveStartingPosition();
    }

    IEnumerator MoveToPos()
    {
        yield return new WaitForSeconds(0.6f);
        GetComponent<PlayerMovement>().UnsetDeath();
        dead = false;
        GetComponent<Rigidbody2D>().MovePosition(startingPosition);
    }
}
