using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Skill
{
    public GameObject Vine;

    private int index;

    private readonly int vineCount = 5;
    private readonly float duration = 10;

    private bool canStart;
    private bool firstVines;

    private Vector2 startPosition;

    private GameObject[] upVines;
    private GameObject[] downVines;

    public override void Activate(PlayerMechanics player)
    {
        Rope rope = Instantiate(this, player.transform.position, Quaternion.identity);
        rope.Begin();
    }
    public void Begin()
    {
        startPosition = transform.position;

        canStart = true;
        firstVines = true;

        upVines = new GameObject[vineCount];
        downVines = new GameObject[vineCount];
    }

    void Update()
    {
        if (canStart)
        {
            if (firstVines)
            {
                Vector2 position = startPosition + new Vector2(0, 0.8f);

                upVines[0] = Instantiate(Vine, position, Quaternion.identity, SkillParent);
                upVines[0].GetComponent<SpriteRenderer>().flipY = true;
                Destroy(upVines[0], duration);

                position = startPosition - new Vector2(0, 0.8f);

                position = startPosition - new Vector2(0, 0.8f);

                downVines[0] = Instantiate(Vine, position, Quaternion.identity, SkillParent);
                Destroy(downVines[0], duration);

                AudioManager.Instance.Play(Sounds.Mechanic);

                index = 0;
                firstVines = false;
            }
            else if (index < vineCount - 1)
            {
                if (upVines[index].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1
                    && downVines[index].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    index += 1;
                    //Vine.GetComponent<SpriteRenderer>().sprite.rect.center.y / 10
                    Vector2 position = upVines[index - 1].transform.position + new Vector3(0, 1.6f);
                    upVines[index] = Instantiate(Vine, position, Quaternion.identity, SkillParent);
                    upVines[index].GetComponent<SpriteRenderer>().flipY = true;
                    Destroy(upVines[index], duration);

                    position = downVines[index - 1].transform.position - new Vector3(0, 1.6f);
                    downVines[index] = Instantiate(Vine, position, Quaternion.identity, SkillParent);
                    Destroy(downVines[index], duration);

                    AudioManager.Instance.Play(Sounds.Mechanic);
                }
            }
            else Destroy(this);
        }
    }
}
