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

    [SerializeField]
    private float _numberOfExplodingSonars;

    [SerializeField]
    private float _timeBetweenExplodingSonars;


    private IEnumerator _explodeAfterCoroutine;

    void Start()
    {
        _explodeAfterCoroutine = ExplodeAfter();
        StartCoroutine(_explodeAfterCoroutine);
    }

    // Update is called once per frame
    void Update () {
        transform.position += (Vector3)Direction * Time.deltaTime * _speed;
        GetComponent<SpriteRenderer>().flipX = Direction.x < 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var submarine = collision.gameObject.GetComponent<Submarine>();
        if (submarine != null && submarine.Id != this.Owner.Id)
        {
            StopCoroutine(_explodeAfterCoroutine);
            StartCoroutine(ExplodeNow());
        }
    }

        IEnumerator ExplodeAfter()
    {
        yield return new WaitForSeconds(_timeToExplode);
        Debug.Log("Exploding after");
        Destroy(this.gameObject);
    }

    IEnumerator ExplodeNow()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _speed = 0.0f;
        for (int i = 0; i < _numberOfExplodingSonars; i++)
        {
            Owner.SpawnSonar(transform.position);
            yield return new WaitForSeconds(_timeBetweenExplodingSonars);
        }
        Debug.Log("Exploding now");
        Destroy(this.gameObject);
    }
}
