using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Skill
{
    public GameObject trampoline;
    private readonly float duration = 10;

    public override void Activate(PlayerMechanics player)
    {
        AudioManager.Instance.Play("Mechanic");

        GameObject copy = Instantiate(trampoline, player.transform.position, Quaternion.identity);
        Destroy(copy, duration);
    }
}
