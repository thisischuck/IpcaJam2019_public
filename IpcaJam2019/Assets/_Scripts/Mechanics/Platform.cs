using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Skill
{
    private readonly float duration = 5;
    public GameObject platform;

    public override void Activate(PlayerMechanics player)
    {
        AudioManager.Instance.Play("Mechanic");

        GameObject copy =  Instantiate(platform, player.transform.position, Quaternion.identity);
        Destroy(copy, duration);
    }
}
