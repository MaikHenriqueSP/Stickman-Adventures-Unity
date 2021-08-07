using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public TMP_Text SpeakerName;
    public TMP_Text DialogContent;

    public Animator DialogAnimator;

    private Queue<Sentence> sentences;

    public PlayerController PlayerController;
    public EnemyController EnemyController;

    public void InitDialog(Dialogue dialog)
    {
        sentences = new Queue<Sentence>(dialog.sentences);
        NextSentence();
        DialogAnimator.SetTrigger("Open_Dialog");
        PlayerController.Freeze();
    }
    void Update()
    {
        bool isContinuingDialogThroughKeyBoard = Input.GetKeyDown(KeyCode.Space);
        
        if (isContinuingDialogThroughKeyBoard)
        {
            NextSentence();
        }        
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            FinishDialog();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentences(sentences.Dequeue()));
    }

    private IEnumerator TypeSentences (Sentence sentence )
    {
        SpeakerName.text = sentence.SpeakerName;
        DialogContent.text = "";

        foreach (char character in sentence.phrase.ToCharArray())
        {
            DialogContent.text += character;
            yield return null;
        }
    }

    private void FinishDialog()
    {
        PlayerController.UnFreeze();
        EnemyController.UnFreeze();
        DialogAnimator.SetTrigger("Close_Dialog");
    }


}
