using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void PlayGameScene()
    {
        SceneManager.LoadScene("LevelProgressScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
