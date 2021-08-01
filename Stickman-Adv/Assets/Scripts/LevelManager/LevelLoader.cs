using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnimatorController;
    public float transitionDuration;    

    private static LevelLoader instance;

    public static LevelLoader Instance { get { return instance; }}

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(transitionDuration);

        transitionAnimatorController.SetTrigger("Exit");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene("LevelProgressScene");
    }
}
