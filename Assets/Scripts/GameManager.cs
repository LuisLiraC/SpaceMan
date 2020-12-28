using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Menu,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.Menu;
    public static GameManager sharedInstance;
    private PlayerController playerController;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;    
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && currentGameState != GameState.InGame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (currentGameState != GameState.InGame)
        {
            SetGameState(GameState.InGame);
        }
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.Menu);
    }

    void SetGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                MenuManager.sharedInstance.ShowMainMenu();
                break;
            case GameState.InGame:
                LevelManager.sharedInstance.RemoveAllLevelBlocks();
                LevelManager.sharedInstance.GenerateInitialBlocks();
                MenuManager.sharedInstance.HideMainMenu();
                playerController.StartGame();
                break;
            case GameState.GameOver:
                MenuManager.sharedInstance.ShowMainMenu();
                break;
        }

        currentGameState = gameState;
    }
}
