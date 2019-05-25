using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [HideInInspector]
    public Transform SkillParent;
    public abstract void Activate(PlayerMechanics player);
}
