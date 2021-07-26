using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public TMP_Text speakerName;
    public TMP_Text dialogContent;

    public Animator dialogAnimator;


    private Queue<Sentence> sentences;
    public void InitDialog(Dialogue dialog)
    {
        sentences = new Queue<Sentence>(dialog.sentences);
        dialogAnimator.SetTrigger("Open_Dialog");
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
        speakerName.text = sentence.SpeakerName;
        dialogContent.text = "";

        foreach (char character in sentence.phrase.ToCharArray())
        {
            dialogContent.text += character;
            yield return null;
        }
    }

    private void FinishDialog()
    {
        dialogAnimator.SetTrigger("Close_Dialog");
    }


}
