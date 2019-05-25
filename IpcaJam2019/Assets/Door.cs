using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = true;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isOpen)
        {
            Debug.Log("Level Over");
        }
    }
}
