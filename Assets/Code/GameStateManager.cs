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

    void Update()
    {
        switch (_gameState)
        {
                case GameState.Menu:
                break;
                case GameState.GameOver:
                break;
                case GameState.Playing:
                break;
        }
    }

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
                }
                break;
            case GameState.Playing:
                break;
        }
    }
}
