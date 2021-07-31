using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateHolder : MonoBehaviour
{
    public static int CurrentLevel {get; set;}
    public void GoNextLevel()
    {
        CurrentLevel++;
    }

    public void ResetLevel()
    {
        CurrentLevel = 0;
    }
}
