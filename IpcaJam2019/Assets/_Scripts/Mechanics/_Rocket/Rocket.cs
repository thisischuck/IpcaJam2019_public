using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Skill
{
    public RocketProjectile RocketProjectile;

    public override void Activate(PlayerMechanics player)
    {
        RocketProjectile copy = Instantiate(RocketProjectile, player.transform.position, Quaternion.identity, SkillParent);

        Vector2 direction;

        if(Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.A))
            {
                direction = new Vector2(-1, 1);
                copy.transform.eulerAngles = new Vector3(0, 0, 45);

            }
            else if(Input.GetKey(KeyCode.D))
            {
                direction = new Vector2(1, 1);
                copy.transform.eulerAngles = new Vector3(0, 0, -45);
            }
            else
            {
                direction = new Vector2(0, 1);
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            direction = new Vector2(-1, 0);
            copy.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            direction = new Vector2(1, 0);
            copy.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            direction = new Vector2(0, 1);
        }

        copy.SetDirection(direction);
    }
}
