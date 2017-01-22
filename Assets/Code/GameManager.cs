using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int Player1Score { get; private set; }
    public int Player2Score { get; private set; }

    public void Score(int player)
    {
        switch (player)
        {
            case 1:
                Player1Score++;
                break;
            case 2:
                Player2Score++;
                break;
        }
    }



    [SerializeField]
    private Pickup _pickupPrefab;

    [SerializeField]
    private float _nextpickupTime;

    [SerializeField]
    private float _pickupCooldownTimer;

    [SerializeField]
    private int _numberOfActivePickups;

    [SerializeField]
    private float _maxNumberOfActivePickups;

    [SerializeField]
    private float _gameWorldHeight;

    [SerializeField]
    private float _gameWorldWidth;

    [SerializeField]
    private Submarine _submarine1;

    [SerializeField]
    private Submarine _submarine2;

    [SerializeField]
    private Transform _submarineTransform1;

    [SerializeField]
    private Transform _submarineTransform2;

    [SerializeField]
    private GameStateManager _gameStateManager;

    [SerializeField]
    private float _padding;

    void SpawnPickup()
    {
        //float x = UnityEngine.Random.Range(-_gameWorldWidth / 2 + padding, _gameWorldWidth / 2 - padding);
        //float y = UnityEngine.Random.Range(-_gameWorldHeight / 2 + padding, _gameWorldHeight / 2 - padding);

        float r = UnityEngine.Random.Range(0.0f, _gameWorldHeight - _padding);
        float theta = UnityEngine.Random.Range(0.0f, 2 * Mathf.PI);

        Vector3 position = new Vector3((float)(r * Math.Cos(theta)), (float)(r * Math.Sin(theta)), 0);

        var pickup = Instantiate(_pickupPrefab, position, Quaternion.identity);
        _numberOfActivePickups += 1;
        pickup.PickupEvent += (object o, EventArgs e) => _numberOfActivePickups -= 1;

        _nextpickupTime = UnityEngine.Random.Range(3.0f, 5.0f);
    }


    public void CreatePlayers()
    {
        //player1
        _submarine1.transform.position = _submarineTransform1.position;

        //player2
        _submarine2.transform.position = _submarineTransform2.position;
    }

	// Use this for initialization
	void Start () {
        _nextpickupTime = 2;
        _pickupCooldownTimer = 0.0f;
        _numberOfActivePickups = 0;
        CreatePlayers();
    }
	
	// Update is called once per frame
	void Update () {
        //timer updates
        _pickupCooldownTimer += Time.deltaTime;


        //pickup
        if (_pickupCooldownTimer > _nextpickupTime)
        {
            if (_numberOfActivePickups < 3)
                SpawnPickup();
            _pickupCooldownTimer = 0.0f;
        }

        Debug.DrawLine(Vector3.zero, Vector3.up * (_gameWorldHeight - _padding), Color.magenta);
	}
}
