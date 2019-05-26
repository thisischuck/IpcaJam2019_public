using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelsController
{
    public static List<int> levels;
    public static int numLevels;
    public static int currentLevel = 0;

    public static float volume = 100f;

    // Start is called before the first frame update

    public static void Start()
    {
        volume = 50f;
        levels = new List<int>();
        for (int i = 0; i < numLevels; i++)
        {
            levels.Add(1);
        }
        levels[0] = 0;
    }
}
