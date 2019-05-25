using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollision : MonoBehaviour
{
    public enum TrapType
    {
        PlatformMover,
        TapeteRolante,
        CrushSpikes,
    }

    public TrapType type;
    [Space(10)]
    public GameObject ActualTrap;
    public Transform MoveTo;



    [Range(0.01f, 0.2f)]
    public float speed;

    public float force;

    [Range(0.5f, 2f)]
    public float seconds;

    Vector3 moveFrom;

    bool tmp, started;

    void Start()
    {
        if (type == TrapType.CrushSpikes)
            moveFrom = ActualTrap.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case TrapType.CrushSpikes:
                CrushSpikesUpdate();
                break;
            case TrapType.TapeteRolante:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (type == TrapType.CrushSpikes)
            if (col.gameObject.tag.Equals("Player"))
            {
                tmp = true;
            }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (type == TrapType.TapeteRolante)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                col.attachedRigidbody.AddForce(Vector2.right * force, ForceMode2D.Force);
            }
        }
    }

    void CrushSpikesUpdate()
    {
        if (tmp)
        {
            ActualTrap.transform.position = Vector3.Lerp(ActualTrap.transform.position, MoveTo.position, speed);
            if (!started)
                StartCoroutine("StopWait");
        }
        else
        {
            ActualTrap.transform.position = Vector3.Lerp(ActualTrap.transform.position, moveFrom, speed);
        }
    }


    IEnumerator StopWait()
    {
        started = true;
        yield return new WaitForSecondsRealtime(seconds);
        tmp = false;
        started = false;
    }
}
