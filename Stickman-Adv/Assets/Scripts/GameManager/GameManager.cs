using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private GameOverMenu gameOverMenu;
    private bool IsGameOver;

    public

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameOverMenu = GameObject.Find("Canvas").GetComponent<GameOverMenu>();       // 
    }

    void Update()
    {
        if (IsGameOver)
        {
            return;
        }

        if (player.IsPlayerDead())
        {
            IsGameOver = true;
            Debug.Log("Player is dead");

            gameOverMenu.StartGameOverMenu();
        }

        
    }
}
