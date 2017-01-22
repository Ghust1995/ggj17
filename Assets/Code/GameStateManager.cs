using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    GameOver
}


public class GameStateManager : MonoBehaviour
{
    public GameState GameState = GameState.Playing;

    private GameManager _gameManager
    {
        get { return FindObjectOfType<GameManager>(); }
    }

    public void StartGame()
    {
        switch (GameState)
        {
            case GameState.Menu:
            case GameState.GameOver:
            case GameState.Playing:
                break;
        }
    }

    public void PlayerLose(int losingPlayer)
    {
        switch (GameState)
        {
            case GameState.Menu:
            case GameState.GameOver:
                break;
            case GameState.Playing:
                //GameState = GameState.GameOver;
                _gameManager.Score(losingPlayer == 1 ? 2 : 1);
                break;
        }
    }
}
