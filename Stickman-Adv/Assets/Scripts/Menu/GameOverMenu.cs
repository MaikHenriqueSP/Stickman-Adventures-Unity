using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{

   public GameObject GameOverMenuUI;

   public GameObject firstSelectedButton;

   void Awake()
   {
       Time.timeScale = 1f;
   }

   public void StartGameOverMenu()
   {
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;       
   }


    public void LoadMenu()
    {
        //Time.timeScale = 1f;
        //GameOverMenuUI.SetActive(false);
        
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();        
    }

}
