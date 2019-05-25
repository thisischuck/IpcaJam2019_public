using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Skill
{
    public GameObject Vine;

    private int index;

    private readonly int vineCount = 5;
    private readonly float duration = 5;

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
        if(canStart)
        {
            if(firstVines)
            {
                Vector2 position = startPosition + new Vector2(0, 2);

                upVines[0] = Instantiate(Vine, position, Quaternion.identity);
                upVines[0].GetComponent<SpriteRenderer>().flipY = true;
                Destroy(upVines[0], duration);

                position = startPosition - new Vector2(0, 2);

                downVines[0] = Instantiate(Vine, position, Quaternion.identity);
                Destroy(downVines[0], duration);

                index = 0;
                firstVines = false;
            }
            else if(index < vineCount - 1)
            {
                if (upVines[index].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Rope")
                    || downVines[index].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Rope"))
                {
                    index += 1;

                    Vector2 position = upVines[index - 1].transform.position + (Vector3.up * Vine.GetComponent<SpriteRenderer>().sprite.rect.height);
                    upVines[index] = Instantiate(Vine, position, Quaternion.identity);
                    upVines[index].GetComponent<SpriteRenderer>().flipY = true;
                    Destroy(upVines[index], duration);

                    position = downVines[index - 1].transform.position - (Vector3.up * Vine.GetComponent<SpriteRenderer>().sprite.rect.height);
                    downVines[index] = Instantiate(Vine, position, Quaternion.identity);
                    Destroy(downVines[index], duration);
                }
            }
            else Destroy(this);
        }
    }
}
