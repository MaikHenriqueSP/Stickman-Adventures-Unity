using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCoverScript : MonoBehaviour
{
    private Animator animator;
    public float SceneDuration;
    private bool dispose;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("Fade_Out");        
    }
    
    void Update()
    {
        if (dispose) 
        {
            return;
        }

        SceneDuration -= Time.deltaTime;
        if (SceneDuration <= 0)
        {
            dispose = true;
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
