using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private BossController boss;
    private GameOverMenu gameOverMenu;
    private bool IsGameOver;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boss = GameObject.Find("Boss-lvl-1").GetComponent<BossController>();
        gameOverMenu = GameObject.Find("Canvas").GetComponent<GameOverMenu>();
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
            gameOverMenu.StartGameOverMenu();
        }

        if (boss.IsDead())
        {
            LevelLoader.Instance.LoadNextScene();
        }
    }
}
