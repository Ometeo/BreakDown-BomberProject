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
        if (Bomb.transform.localScale == Vector3.zero)
        {
            Destroy(this.gameObject);
            
        }
    }

    IEnumerator BombCountDown()
    {
        yield return new WaitForSeconds(TimeToLive);
        _destroyBombAnimation.Play();
        StartCoroutine(BombExplosion());
    }

    IEnumerator BombExplosion()
    {
        Vector3 localTransformPosition = Bomb.transform.position;
        Instantiate(Explosion, Bomb.transform.position, Bomb.transform.localRotation);
        yield return new WaitForSeconds(0.10f);
        for (int i = 1; i <= Distance; i++)
        {
            Instantiate(Explosion, new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z), Bomb.transform.localRotation);
            Instantiate(Explosion, new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z), Bomb.transform.localRotation);
            Instantiate(Explosion, new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z + i), Bomb.transform.localRotation);
            Instantiate(Explosion, new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z - i), Bomb.transform.localRotation);
            yield return new WaitForSeconds(0.10f);
        }
        DestroyBomb();
    }

}
