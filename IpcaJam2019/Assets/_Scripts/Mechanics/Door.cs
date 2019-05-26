using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Singleton<Door>
{
    public bool isOpen;

    public Sprite DoorOpen;
    public Sprite DoorClosed;

    private SpriteRenderer Sprite;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isOpen)
        {
            Debug.Log("Entrou");

            MapScreenEffects.Instance.EndEffect();

            collision.GetComponent<PlayerMovement>().UnsetNormalMovement();
            collision.GetComponent<PlayerMovement>().UnsetRopeMovement();
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void SetDoor(bool open)
    {
        isOpen = open;
        if (open)
        {
            Sprite.sprite = DoorOpen;
        }
        else Sprite.sprite = DoorClosed;
    }
}
