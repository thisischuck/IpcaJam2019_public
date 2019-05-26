using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public Transform SkillParent;
    public Skill skill;

    private void Start()
    {
        skill.SkillParent = SkillParent;
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = skill.Icon;
    }

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
