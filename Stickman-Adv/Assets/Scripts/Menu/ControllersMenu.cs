using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllersMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject ControllersUI;
    public Button FirstActiveButton;

    void OnEnable()
    {
        SetFirstSelectedButton();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            LoadPauseMenu();
        }        
    }

    private void SetFirstSelectedButton()
    {
        FirstActiveButton.Select();
        FirstActiveButton.OnSelect(null);
    }


    public void LoadPauseMenu()
    {
        PauseMenuUI.SetActive(true);
        ControllersUI.SetActive(false);
    }


}
