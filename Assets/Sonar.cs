using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Sonar : MonoBehaviour {

    [SerializeField]
    private float _destroyTime;

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
        Invoke("DestroyObject" , _destroyTime);
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
