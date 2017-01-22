using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Torpedo : MonoBehaviour {

    [SerializeField]
    private float _speed;

    [SerializeField]
    public Vector2 Direction;

    [SerializeField]
    public Submarine Owner;

    [SerializeField]
    private float _timeToExplode;
    

    void Start()
    {
        StartCoroutine(ExplodeAfter());
    }

    // Update is called once per frame
    void Update () {
        transform.position += (Vector3)Direction * Time.deltaTime * _speed;
        GetComponent<SpriteRenderer>().flipX = Direction.x < 0;
    }

    IEnumerator ExplodeAfter()
    {
        yield return new WaitForSeconds(_timeToExplode);
        Owner.SpawnSonar(transform.position);
        Destroy(this.gameObject);
    }
}
