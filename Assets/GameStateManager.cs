using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Starting,
    Playing,
    Ending,
    Done
}


public class GameStateManager : MonoBehaviour {

    public GameState State { get; private set; }

    public void StartGame()
    {
        State = GameState.Starting;
    }

    public void EndGame()
    {
        State = GameState.Ending;
    }


	// Use this for initialization
	void Start () {
        State = GameState.Starting;
	}
	
	// Update is called once per frame
	void Update () {
		switch (State) {
            case GameState.Starting:
                break;

            case GameState.Done:
                break;
        }
	}
}
