using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineSoundPlayer : MonoBehaviour {

    [SerializeField]
    private AudioSource _sonarClip;

    [SerializeField]
    private AudioSource _explosionClip;

    [SerializeField]
    private AudioSource _reloadClip;

    public void PlaySonar()
    {
        _sonarClip.Play();
    }

    public void PlayReload()
    {
        _reloadClip.Play();
    }

    public void PlayExplosion()
    {
        _explosionClip.Play();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
