using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public Skill skill;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.Instance.Play("Interact");
                collision.GetComponent<PlayerMechanics>().SetSkill(skill);
            }
        }
    }
}
