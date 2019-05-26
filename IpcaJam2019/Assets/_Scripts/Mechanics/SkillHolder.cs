using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillHolder : MonoBehaviour
{
    public Transform SkillParent;
    public Skill skill;

    public Image SkillDisplay;

    private void Start()
    {
        skill.SkillParent = SkillParent;
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = skill.Icon;
        SkillDisplay.color = Color.clear;
        SkillDisplay.transform.Find("Text").GetComponent<Text>().text = " ";
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.Instance.Play("Interact");
                collision.GetComponent<PlayerMechanics>().SetSkill(skill);
                SkillDisplay.sprite = skill.Icon;
                SkillDisplay.color = Color.white;
                SkillDisplay.transform.Find("Text").GetComponent<Text>().text = skill.transform.name;

            }
        }
    }
}
