using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScreenEffects : Singleton<MapScreenEffects>
{
    public GameObject Player;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = Camera.main.WorldToScreenPoint(Player.transform.position);
    }

    public void EndEffect()
    {
        transform.position = Camera.main.WorldToScreenPoint(Player.transform.position);
        animator.StartPlayback();
        animator.speed = -1.5f;
        animator.Play("LevelIntro");
    }
}
