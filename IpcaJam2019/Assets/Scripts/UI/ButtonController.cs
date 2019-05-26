using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button button;
    public LevelObject levelInfo;
    private GameObject txtChild;

    private TMP_Text levelTxt;

    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        txtChild = transform.Find("txtLv").gameObject;
        levelTxt = txtChild.GetComponent<TMP_Text>();

        if (!levelInfo.locked)
            levelTxt.text = levelInfo.num.ToString();
        else
            levelTxt.text = "?";
    }

    public void TaskOnClick()
    {
        if(!levelInfo.locked)
            SceneManager.LoadScene(levelInfo.levelName);
    }   

    // Update is called once per frame
    void Update()
    {
        //HighlightButton();
        if (!levelInfo.locked)
            levelTxt.text = levelInfo.num.ToString();
        else
            levelTxt.text = "?";
    }

    public void Highlight()
    {
        levelTxt.color = color;
    }

    public void DeHighlight()
    {
        levelTxt.color = Color.black;
    }
}
