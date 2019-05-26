using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelObject : ScriptableObject
{
    public string levelName;
    public bool locked = true;
    public int num;
}
