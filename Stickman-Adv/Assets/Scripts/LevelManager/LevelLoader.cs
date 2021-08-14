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

    public float TimerUntilFadeIn;

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
        yield return new WaitForSeconds(TimerUntilFadeIn);

        transitionAnimatorController.SetTrigger("Exit");
        float animationDuration = AnimationUtilities.GetAnimationLength(transitionAnimatorController);

        yield return new WaitForSeconds(animationDuration);

        SceneManager.LoadScene("LevelProgressScene");
    }
/*
    private float GetAnimationLength(string animationClipName)
    {
        transitionAnimatorController.Play(animationClipName);
        AnimatorClipInfo[] animationInfo = transitionAnimatorController.GetCurrentAnimatorClipInfo(0);
        return animationInfo[0].clip.length;
    }
    */
}
