using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public event EventHandler PickupEvent;
    public void Disappear()
    {
        StartCoroutine("Fade");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float PickupTime;

    IEnumerator Fade()
    {
        if (PickupEvent != null)
            PickupEvent(this, null);

        var renderer = GetComponent<SpriteRenderer>();
        for (float t = PickupTime; t >= 0; t -= 0.02f)
        {
            var c = renderer.material.color;
            c.a = t;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }
        Debug.Log("Pickup destroyed");
        Destroy(this.gameObject);
    }

}
