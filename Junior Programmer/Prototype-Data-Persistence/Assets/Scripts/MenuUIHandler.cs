using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        ColorPicker.SelectColor(DataPersistence.Instance.TeamColor);
    }

    public void NewColorSelected(Color color)
    {
        DataPersistence.Instance.TeamColor = color;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveColorClicked()
    {
        DataPersistence.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        DataPersistence.Instance.LoadColor();
        ColorPicker.SelectColor(DataPersistence.Instance.TeamColor);
    }

    public void Exit()
    {
        DataPersistence.Instance.SaveColor();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();        
#endif
    }
}
