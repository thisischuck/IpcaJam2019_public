using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    public enum SplatLocation
    {
        Foreground,
        Background
    }

    public Color backgroundTint;
    public SplatLocation splatLocation;
    private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateSplat(SplatLocation splatLocation)
    {
        this.splatLocation = splatLocation;

        SetRandomSize();
        SetLocation();
        SetRandomRotation();
        SetRandomSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetRandomSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    private void SetRandomSize()
    {
       float size = Random.Range(2f, 3f);
       transform.localScale *= size;
    }

    private void SetRandomRotation()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
    }

    private void SetLocation()
    {
        switch(splatLocation)
        {
            case SplatLocation.Background:
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
                spriteRenderer.sortingOrder = 0;
                transform.localScale *= Random.Range(2f, 2.5f);
                break;
            case SplatLocation.Foreground:
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                spriteRenderer.sortingOrder = 3;
                break;
        }
    }
}
