using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSonarSpawner : MonoBehaviour {


    [SerializeField]
    private SonarShader _sonarSpawner;

    [SerializeField]
    private float _sonarCooldown;

    [SerializeField]
    private float _sonarTimer;

    [SerializeField]
    private GameObject _helpObject;


    public void SpawnSonar()
    {
        float r = UnityEngine.Random.Range(0.0f, 5f - 0.2f);
        float theta = UnityEngine.Random.Range(0.0f, 2 * Mathf.PI);

        Vector3 position = new Vector3((float)(r * Math.Cos(theta)), (float)(r * Math.Sin(theta)), 0);

        _sonarSpawner.SpawnSonar(position);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _sonarTimer += Time.deltaTime;

	    if (_sonarTimer >= _sonarCooldown)
        {
            _sonarTimer = 0;
            SpawnSonar();
        }

        //Start game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("Game");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            _helpObject.active ^= true;

        }
	}
}
