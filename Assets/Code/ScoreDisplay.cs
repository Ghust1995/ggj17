using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    private GameManager _gameManager;
    public Vector2 Position;
    public Text scorePrefab;

    private Text p1Score;
    private Text p2Score;

    // Use this for initialization
    void Start ()
    {
        _gameManager = FindObjectOfType<GameManager>();

        p1Score = Instantiate(scorePrefab, transform);
        p1Score.transform.localScale = Vector3.one;
        p1Score.gameObject.name = "P1";

        p2Score = Instantiate(scorePrefab, transform);
        p2Score.transform.localScale = Vector3.one;
        p2Score.gameObject.name = "P2";
    }
	
	// Update is called once per frame
	void Update ()
	{
	    p1Score.transform.localPosition = new Vector2(-Position.x, Position.y);
        p2Score.transform.localPosition = new Vector2(Position.x, Position.y);


        p1Score.text = _gameManager.Player1Score.ToString();
        p2Score.text = _gameManager.Player2Score.ToString();
    }
}
