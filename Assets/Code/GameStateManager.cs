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
    private GameState _gameState;
    private GameManager _gameManager;

    public void StartGame()
    {
        switch (_gameState)
        {
            case GameState.Menu:
            case GameState.GameOver:
                _gameState = GameState.Playing;
                var subs = FindObjectsOfType<Submarine>();
                foreach (var sub in subs)
                {
                    sub.gameObject.SetActive(true);
                    sub.AmmoCount = sub.MaxAmmo;
                    _gameManager.CreatePlayers();
                }
                break;
            case GameState.Playing:
                break;
        }
    }

    public void PlayerLose(int losingPlayer)
    {
        switch (_gameState)
        {
            case GameState.Menu:
            case GameState.GameOver:
                break;
            case GameState.Playing:
                _gameState = GameState.GameOver;
                _gameManager.Score(losingPlayer == 1 ? 2 : 1);
                break;
        }
    }
}
