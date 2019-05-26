using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Singleton<Door>
{
    public bool isOpen;

    public Sprite DoorOpen;
    public Sprite DoorClosed;

    private SpriteRenderer Sprite;
    bool nextlvl = false;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        SetDoor(isOpen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isOpen)
        {
            Debug.Log("Entrou");

            MapScreenEffects.Instance.EndEffect();
            if (!nextlvl)
            {
                LevelsController.currentLevel++;
                nextlvl = true;
            }

            collision.GetComponent<PlayerMovement>().UnsetNormalMovement();
            collision.GetComponent<PlayerMovement>().UnsetRopeMovement();
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.GetComponent<Rigidbody2D>().velocity.y);
            StartCoroutine(HolUpAMinute());


        }
    }

    IEnumerator HolUpAMinute()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2 + LevelsController.currentLevel);
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
