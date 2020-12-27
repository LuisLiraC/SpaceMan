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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

    }

    public void GameOver()
    {

    }

    public void BackToMenu()
    {

    }

    void SetGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                break;
            case GameState.InGame:
                break;
            case GameState.GameOver:
                break;
        }

        currentGameState = gameState;
    }
}
