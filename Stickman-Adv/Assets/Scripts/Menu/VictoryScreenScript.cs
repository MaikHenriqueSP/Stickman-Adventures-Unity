using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenScript : MonoBehaviour
{
    public Button FirstActiveButton;

    public void LoadGameCoverScene()
    {
        SceneManager.LoadScene("GameCover");
    }

    void OnEnable()
    {
        SetFirstSelectedButton();
    }

    private void SetFirstSelectedButton()
    {
        FirstActiveButton.Select();
        FirstActiveButton.OnSelect(null);
    }
}
