using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Submarine : MonoBehaviour
{

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Sonar _sonarPrefab;

    [SerializeField]
    private int _id;

    public float visibility = 0.0f;

    public void SpawnSonar()
    {
        var sonar = Instantiate(_sonarPrefab, this.transform.position, Quaternion.identity);
        sonar.OwnerId = _id;
    }

    public void GoUp()
    {
        transform.position += (Vector3)Vector2.up * Time.deltaTime * _speed;
    }

    public void GoDown()
    {
        transform.position += (Vector3)Vector2.down * Time.deltaTime * _speed;
    }

    public void GoLeft()
    {
        transform.position += (Vector3)Vector2.left * Time.deltaTime * _speed;
    }

    public void GoRight()
    {
        transform.position += (Vector3)Vector2.right * Time.deltaTime * _speed;
    }

    public void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, visibility);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var sonar = collision.gameObject.GetComponent<Sonar>();
        if(sonar != null && sonar.OwnerId != this._id)
        {
            this.visibility = 1.0f;
            StartCoroutine(Fade());
        }
    }

    [SerializeField]
    private float fadePrecision;

    [SerializeField]
    private float fadeTime;

    IEnumerator Fade()
    {
        float f;
        for (f = fadeTime; f >= 0; f -= fadePrecision)
        {
            this.visibility = Mathf.Clamp(f / fadeTime, 0, 1);
            yield return new WaitForSeconds(fadePrecision);
        }
        this.visibility = Mathf.Clamp(f / fadeTime, 0, 1);
    }

}
