using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mvnmt : MonoBehaviour
{
    void Update()
    {
        this.transform.position += new Vector3(
            Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0
        ) * 0.25f;
    }
}
