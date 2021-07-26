using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public TMP_Text speakerName;
    public TMP_Text dialogContent;


    private Queue<Sentence> sentences;
    public void InitDialog(Dialogue dialog)
    {
        sentences = new Queue<Sentence>(dialog.sentences);
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
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
}
