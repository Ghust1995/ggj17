using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Submarine : MonoBehaviour
{
    static int NextId = 0;

    public bool Alive { get; private set; }

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Sonar _sonarPrefab;

    [SerializeField]
    private Torpedo _torpedoPrefab;

    [SerializeField]
    public int Id { get; private set; }

    [SerializeField]
    private float _fadeSpeed;

    [SerializeField]
    public int AmmoCount = 3;

    [SerializeField]
    public int MaxAmmo;

    [SerializeField]
    private float _sonarCooldown;
    
    private float _timeSinceLastSonar;
    
    private SonarShader _sonarSpawner;

    private Vector2 _direction;

    public float visibility = 0.0f;

    public void SpawnSonar()
    {
        if (_timeSinceLastSonar < _sonarCooldown) return;
        SpawnSonar(this.transform.position);
        _timeSinceLastSonar = 0;
    }

    public void SpawnSonar(Vector3 position)
    {
        _sonarSpawner.SpawnSonar(position);
        //var sonar = Instantiate(_sonarPrefab, position, Quaternion.identity);
        //sonar.OwnerId = Id;
    }

    public bool CanMove(Vector2 diff)
    {
        Vector2 distToOrigin = -transform.position; 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, diff * 10, diff.magnitude);

        Debug.DrawLine(transform.position, transform.position + (Vector3)diff * 10, Color.black, 2, false);
        if (hit.collider == null && Vector2.Dot(distToOrigin, diff) < 0)
        {
            Debug.Log("Not supposed to move");
            return false;
        }
        return true;
    }

    public void GoUp()
    {
        Vector3 movementVector = (Vector3)Vector2.up * Time.deltaTime * _speed;

        if (CanMove(movementVector))
            transform.position += movementVector;
    }

    public void GoDown()
    {
        Vector3 movementVector = (Vector3)Vector2.down * Time.deltaTime * _speed;

        if (CanMove(movementVector))
            transform.position += (Vector3)Vector2.down * Time.deltaTime * _speed;
    }

    public void GoLeft()
    {
        Vector3 movementVector = (Vector3)Vector2.left * Time.deltaTime * _speed;

        if (CanMove(movementVector))
            transform.position += (Vector3)Vector2.left * Time.deltaTime * _speed;

        _direction = Vector2.left;
    }

    public void GoRight()
    {
        Vector3 movementVector = (Vector3)Vector2.right * Time.deltaTime * _speed;

        if (CanMove(movementVector))
            transform.position += (Vector3)Vector2.right * Time.deltaTime * _speed;

        _direction = Vector2.right;
    }

    public void SpawnTorpedo()
    {
        if (AmmoCount <= 0) return;

        AmmoCount -= 1;

        var torpedo = Instantiate(_torpedoPrefab, this.transform.position, Quaternion.identity);
        torpedo.Owner = this;
        torpedo.Direction = _direction.x > 0 ? Vector2.right : Vector2.left;
    }

    public void GetAmmo()
    {
        AmmoCount += 1;
        AmmoCount = Mathf.Min(MaxAmmo, AmmoCount);
    }

    //Called when submarine is hit by torpedo
    IEnumerator Sink()
    {
        var renderer = GetComponent<Renderer>();
        for (float time = 1; time >= 0; time -= 0.01f)
        {
            renderer.transform.position +=  new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f),
                                                        UnityEngine.Random.Range(-0.5f, 0.5f),
                                                        0);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void Start()
    {
        Id = NextId;
        NextId++;
        AmmoCount = MaxAmmo;
        _sonarSpawner = FindObjectOfType<SonarShader>();
    }
    
    public void Update()
    {
        _timeSinceLastSonar += Time.deltaTime;

        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, visibility);
        GetComponent<SpriteRenderer>().flipX = _direction.x < 0;
        this.visibility = Mathf.Clamp((visibility - _fadeSpeed*Time.deltaTime), 0, 1);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var sonar = collision.gameObject.GetComponent<Sonar>();
        if(sonar != null && sonar.OwnerId != this.Id)
        {
            this.visibility = 1.0f;
        }

        var torpedo = collision.gameObject.GetComponent<Torpedo>();
        if (torpedo != null && torpedo.Owner.Id != this.Id)
        {
            Debug.Log(string.Format("{0} lost!", Id));
            StartCoroutine("Sink");
        }

        var pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup != null)
        {
            pickup.Disappear();
            GetAmmo();
            //Debug.Log("Got pickup");
        }
    }

}
