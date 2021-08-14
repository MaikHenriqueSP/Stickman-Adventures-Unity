using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCoverScript : MonoBehaviour
{
    private Animator animator;
    public float SceneDuration;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("Fade_Out");        
    }
    
    void Update()
    {
        SceneDuration -= Time.deltaTime;
        if (SceneDuration <= 0)
        {
            animator.Play("Fade_In");
            StartCoroutine(LoadMenu());
        }        
    }

    public IEnumerator LoadMenu()
    {
        float animationDuration = AnimationUtilities.GetAnimationLength(animator);
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene("Menu");
    }
}
