using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    [SerializeField]
    private AudioClip _audioClip;


    // Use this for initialization
    void Start () {
        //DontDestroyOnLoad(this.gameObject);

        //Check if instance already exists
        if (instance == null)
        {
            instance = this;
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}

}
