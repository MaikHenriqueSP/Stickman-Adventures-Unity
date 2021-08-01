using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] LevelsProgressUIs;
    public float SceneDuration;
    
    void Awake()
    {
        LevelsProgressUIs[LevelStateHolder.CurrentLevel].SetActive(true);
        //@TODO: Go to the next lvl
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(SceneDuration);
        LevelStateHolder.GoNextLevel();
        SceneManager.LoadScene($"Level-{LevelStateHolder.CurrentLevel}");
    }
}
