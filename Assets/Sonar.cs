using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Sonar : MonoBehaviour {

    [SerializeField]
    private float _destroyTime;

    [SerializeField]
    private float _finalScale;

    [SerializeField]
    private float _initialScale;

    [SerializeField]
    private int _ownerId;

    public int OwnerId
    {
        get
        {
            return _ownerId;
        }
        set
        {
            _ownerId = value;
        }
    }
    
    // Use this for initialization
	void Start () {
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        float i, scale;
        for (i = 0; i < _destroyTime; i+= Time.deltaTime)
        {
            scale = Mathf.Lerp(_initialScale, _finalScale, i/_destroyTime);
            transform.localScale = new Vector2(scale, scale);
            yield return null;
        }
        scale = Mathf.Lerp(_initialScale, _finalScale, i);
        transform.localScale = new Vector2(scale, scale);
        Destroy(this.gameObject);
    }
}
