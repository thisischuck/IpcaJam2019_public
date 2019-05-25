using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{ 
    private bool inSafeZone;
    private Vector2 startingPosition;
    private Skill activeSkill;

    void Start()
    {
        inSafeZone = true;
        startingPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activeSkill != null && !inSafeZone)
        {
            MoveStartingPosition();
            activeSkill.Activate(this);
        }
    }

    public void SetSkill(Skill skill)
    {
        activeSkill = skill;
    }
    public void SetSafeZone(bool v)
    {
        inSafeZone = v;
    }
    public void MoveStartingPosition()
    {
        GetComponent<Rigidbody2D>().MovePosition(startingPosition);
    }
}
