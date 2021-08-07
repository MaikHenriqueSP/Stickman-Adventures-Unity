using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateHolder : MonoBehaviour
{
    public static int CurrentLevel {get; set;}
    public static void GoNextLevel()
    {
        CurrentLevel++;
    }

    public static void ResetLevel()
    {
        CurrentLevel = 1;
    }
}
