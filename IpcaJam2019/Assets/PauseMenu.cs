using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public TMP_Text leveltxt;

    // Start is called before the first frame update
    void Start()
    {
        leveltxt.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
