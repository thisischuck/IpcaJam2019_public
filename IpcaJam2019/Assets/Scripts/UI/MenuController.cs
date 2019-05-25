using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public List<LevelObject> levels;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        LevelsController.numLevels = 1;
        LevelsController.Start();
        if (slider != null)
            slider.value = LevelsController.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (levels.Count > 0)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (LevelsController.levels[i] == 0)
                {
                    levels[i].locked = false;
                }
                else if (LevelsController.levels[i] == 1)
                {
                    levels[i].locked = true;
                }
                        
            }
        }
        if (slider != null)
            LevelsController.volume = slider.value;
    }

    public void GotoLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
