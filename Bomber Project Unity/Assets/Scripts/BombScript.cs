using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
    public GameObject Bomb;
    public GameObject Explosion;

    private Animation _destroyBombAnimation;

    [SerializeField]
    private float _timeToLive;
    public float TimeToLive
    {
        get
        {
            return _timeToLive;
        }
        set
        {
            _timeToLive = value;
        }
    }

    [SerializeField]
    private int _distance;
    public int Distance
    {
        get
        {
            return _distance;
        }
        set
        {
            _distance = value;
        }
    }

    [SerializeField]
    private bool _horizontalDirection;
    public bool HorizontalDirection
    {
        get { return _horizontalDirection; }
        set { _horizontalDirection = value; }
    }

    [SerializeField]
    private bool _verticalDirection;
    public bool VerticalDirection
    {
        get { return _verticalDirection; }
        set { _verticalDirection = value; }
    }

    [SerializeField]
    private bool _leftDiagonalDirection;
    public bool LeftDiagonalDirection
    {
        get { return _leftDiagonalDirection; }
        set { _leftDiagonalDirection = value; }
    }

    [SerializeField]
    private bool _rightDiagonalDirection;
    public bool RightDiagonalDirection
    {
        get { return _rightDiagonalDirection; }
        set { _rightDiagonalDirection = value; }
    }

    private bool _rightOk = true;
    private bool _leftOk = true;
    private bool _forwardOk = true;
    private bool _backOk = true;
    private bool _diagLeftFwdOk = true;
    private bool _diagRightFwdOk = true;
    private bool _diagLeftBackOk = true;
    private bool _diagRightBackOk = true;


    // Use this for initialization
    void Start()
    {
        _destroyBombAnimation = Bomb.GetComponent<Animation>();
        StartCoroutine(BombCountDown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyBomb()
    {
        Destroy(this.gameObject);
    }

    IEnumerator BombCountDown()
    {
        yield return new WaitForSeconds(TimeToLive);
        _destroyBombAnimation.Play();
        StartCoroutine(BombExplosion());
    }

    IEnumerator BombExplosion()
    {
        RaycastHit hit;
        Vector3 localTransformPosition = Bomb.transform.position;
        Instantiate(Explosion, Bomb.transform.position, Bomb.transform.localRotation);
        yield return new WaitForSeconds(0.10f);
        for (int i = 1; i <= Distance; i++)
        {
            if (HorizontalDirection)
            {
                if (_rightOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _rightOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }


                    }
                }
                //int j = 0;
                //while (j < hitColliders.Length)
                //{
                //    print(hitColliders[j].transform);
                //    print(hitColliders[j].transform.position);
                //    j++;
                //}

                if (_leftOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _leftOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }
                    }
                }
            }

            if (VerticalDirection)
            {
                if (_forwardOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z + i), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z + i), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _forwardOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }
                    }
                }
                if (_backOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z - i), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z - i), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _backOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }
                    }
                }
            }

            if (RightDiagonalDirection)
            {
                if (_diagRightFwdOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z + i), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z + i), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _diagRightFwdOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }
                    }
                }
                if (_diagLeftBackOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z - i), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z - i), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _diagLeftBackOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }
                    }
                }
            }

            if (LeftDiagonalDirection)
            {
                if (_diagLeftFwdOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z + i), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z + i), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _diagLeftFwdOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }
                    }
                }
                if (_diagRightBackOk)
                {
                    Instantiate(Explosion, new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z - i), Bomb.transform.localRotation);
                    Collider[] hitColliders = Physics.OverlapSphere(new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z - i), 0.45f);
                    if (hitColliders.Length > 0)
                    {
                        _diagRightBackOk = false;
                        if (hitColliders[0].transform.CompareTag("Destructible"))
                        {
                            hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.10f);
        }
        DestroyBomb();
    }

}
