using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 startingPosition;
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
        GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(MoveToPos());
        GetComponent<SplatParticleSystem>().CreateBothSplats(transform.position);
    }

    public void Die()
    {
        //GetComponent<Animator>().Play("Respawn");
        AudioManager.Instance.Play("Death");
        MoveStartingPosition();
    }

    IEnumerator MoveToPos()
    {
        yield return new WaitForSeconds(0.6f);
        transform.position = new Vector3(startingPosition.x, startingPosition.y, startingPosition.z);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<PlayerMovement>().Respawn();
        dead = false;
    }
}
