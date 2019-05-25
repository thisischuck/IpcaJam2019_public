using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{ 
    private bool inSafeZone;
    private Skill activeSkill;

    void Start()
    {
        inSafeZone = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activeSkill != null && !inSafeZone)
        {
            activeSkill.Activate(this);
            Die();
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

    public void Die()
    {
        GetComponent<PlayerDeath>().Die();
    }
}
