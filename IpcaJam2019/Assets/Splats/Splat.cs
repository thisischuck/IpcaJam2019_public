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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetRandomSize()
    {
       Vector2 size = new Vector2(Random.Range(1, 5), Random.Range(1, 5));
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
                spriteRenderer.color = backgroundTint;
                spriteRenderer.sortingOrder = 0;
                break;
            case SplatLocation.Foreground:
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                spriteRenderer.sortingOrder = 3;
                break;
        }
    }
}
