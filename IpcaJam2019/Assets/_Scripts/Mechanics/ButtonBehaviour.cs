using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public enum ButtonType { Normal, Duration, Hold }

    public Sprite[] ButtonSprites;
    public SpriteRenderer Sprite;
    public ButtonType Type;
    public LayerMask PlayerLayers;
    public LayerMask PlatformLayers;
    public float DoorOpenDuration;

    private Sprite buttonSprite;
    private bool clicked;
    private bool hasPlatform;
    private float time;
    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        Sprite = GetComponent<SpriteRenderer>();

        switch (Type)
        {
            case ButtonType.Normal:
                Sprite.sprite = ButtonSprites[0];
                break;
            case ButtonType.Duration:
                Sprite.sprite = ButtonSprites[2];
                break;
            case ButtonType.Hold:
                Sprite.sprite = ButtonSprites[1];
                break;
            default:
                break;
        }

        buttonSprite = Sprite.sprite;
    }

    void Update()
    {
        if (IsOnButton())
        {
            if (Type == ButtonType.Normal) clicked = true;
            if (Type == ButtonType.Duration) time = DoorOpenDuration;

            Door.Instance.SetDoor(true);
            Sprite.sprite = ButtonSprites[3];
        }
        else if (clicked) Sprite.sprite = ButtonSprites[3];
        else if (Type == ButtonType.Duration)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Sprite.sprite = buttonSprite;
                Door.Instance.SetDoor(false);
            }

        }
        else if (Type == ButtonType.Hold)
        {
            Door.Instance.SetDoor(false);
            Sprite.sprite = buttonSprite;
        }
        else
        {
            Sprite.sprite = buttonSprite;
        }
    }

    private bool IsOnButton()
    {
        return (Physics2D.OverlapArea(new Vector2(transform.position.x - collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2),
                new Vector2(transform.position.x + collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2), PlayerLayers)
                ||
                Physics2D.OverlapArea(new Vector2(transform.position.x - collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2),
                new Vector2(transform.position.x + collider.bounds.size.x / 2, transform.position.y - collider.bounds.size.y / 2), PlatformLayers));
    }


}
