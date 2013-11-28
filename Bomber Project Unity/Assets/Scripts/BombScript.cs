/*
 *   File : BombScript.cs
 *   Description : This script handles the bomb, their time to live and the instantiation of explosions.
 *   Version : 1.0.0
 *   Created by : Jonathan Bihet
 *   Created Date : 17/10/2013
 *   Modification Date : 18/10/2013
 *   Modified by : Jonathan Bihet 
 */

using UnityEngine;
using System.Collections;

/// <summary>
/// This class contains code to handle bomb destuction, and explosion instantiate.
/// </summary>
public class BombScript : MonoBehaviour
{
    /// <summary>
    /// Prefab for the explosion.
    /// </summary>
    public GameObject Explosion;

    /// <summary>
    /// Time before bomb destruction and explosion instantiate.
    /// </summary>
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
    private float _defaultTimeToLive;
    public float DefaultTimeToLive
    {
        get { return _defaultTimeToLive; }
        set { _defaultTimeToLive = value; }
    }

    /// <summary>
    /// Distance of explosion in each directions.
    /// </summary>
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

    public enum ExplosionDirections
    {
        Vertical, Horizontal, DiagonaleGauche, DiagonaleDroite
    }

    [SerializeField]
    private ExplosionDirections[] _explDirection;
    public ExplosionDirections[] ExplDirection
    {
        get { return _explDirection; }
        set { _explDirection = value; }
    }

    private Transform _playerTransform;
    public Transform PlayerTransform
    {
        get { return _playerTransform; }
        set { _playerTransform = value; }
    }

    private Animation _destroyBombAnimation;
    private bool _rightOk = true;
    private bool _leftOk = true;
    private bool _forwardOk = true;
    private bool _backOk = true;
    private bool _diagLeftFwdOk = true;
    private bool _diagRightFwdOk = true;
    private bool _diagLeftBackOk = true;
    private bool _diagRightBackOk = true;

    private int nbTriggerEnter = 0;

    private bool doInit = true;

    /// <summary>
    /// Cache Collider
    /// </summary>
    private Collider _collider;

    private ArrayList _explDirArrList;


    void Awake()
    {
        _explDirArrList = new ArrayList();
        foreach (var dir in ExplDirection)
            _explDirArrList.Add(dir);
    }
    
    /// <summary>
    /// When the bomb is instantiate, get the animation, and start the countdown.
    /// </summary>
    void Start()
    {
        _collider = this.collider;
        _destroyBombAnimation = this.gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        if (doInit)
        {
            Init();
            doInit = false;
        }
    }

    void Init()
    {
        _rightOk = true;
        _leftOk = true;
        _forwardOk = true;
        _backOk = true;
        _diagLeftFwdOk = true;
        _diagRightFwdOk = true;
        _diagLeftBackOk = true;
        _diagRightBackOk = true;
        _collider.isTrigger = true;
        TimeToLive = DefaultTimeToLive;
        this.transform.localScale = Vector3.one;
        StartCoroutine(BombCountDown());
    }

    void OnTriggerEnter()
    {
        nbTriggerEnter++;
    }

    /// <summary>
    /// When the bomb is drop, it's allow passage until the player leave
    /// </summary>
    void OnTriggerExit()
    {
        nbTriggerEnter--;
        if (nbTriggerEnter <= 0)
        {
            nbTriggerEnter = 0;
            _collider.isTrigger = false;
        }

    }

    /// <summary>
    /// Destroy the object.
    /// </summary>
    void DestroyBomb()
    {
        this.gameObject.SetActive(false);
        doInit = true;
    }

    /// <summary>
    /// This method launch the animation of bomb reduction, and launch de explosion Method.
    /// </summary>
    void Destruction()
    {
        _destroyBombAnimation.Play();
        StartCoroutine(BombExplosion());
    }

    /// <summary>
    /// Handle the BombCountDown, when the end is reached, launch the destruction method.
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator BombCountDown()
    {
        yield return new WaitForSeconds(TimeToLive);
        Destruction();
    }

    

    /// <summary>
    /// Instantiate the explosion, and handles collisions.
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator BombExplosion()
    {
        Vector3 localTransformPosition = this.transform.position;
        InstantiateBombExplosion(localTransformPosition);
        yield return new WaitForSeconds(0.10f);
        for (int i = 1; i <= Distance; i++)
        {
            if (_explDirArrList.Contains(ExplosionDirections.Horizontal))
            {
                if (_rightOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z),ref _rightOk);
                }
                if (_leftOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z),ref _leftOk);
                }
            }

            if (_explDirArrList.Contains(ExplosionDirections.Vertical))
            {
                if (_forwardOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z + i), ref _forwardOk);
                }
                if (_backOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x, localTransformPosition.y, localTransformPosition.z - i), ref _backOk);
                }
            }

            if (_explDirArrList.Contains(ExplosionDirections.DiagonaleDroite))
            {
                if (_diagRightFwdOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z + i), ref _diagRightFwdOk);   
                }
                if (_diagLeftBackOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z - i), ref _diagLeftBackOk);
                }
            }

            if (_explDirArrList.Contains(ExplosionDirections.DiagonaleGauche))
            {
                if (_diagLeftFwdOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x - i, localTransformPosition.y, localTransformPosition.z + i), ref _diagLeftFwdOk);
                }
                if (_diagRightBackOk)
                {
                    InstantiateBombExplosions(new Vector3(localTransformPosition.x + i, localTransformPosition.y, localTransformPosition.z - i), ref _diagRightBackOk);
                }
            }

            yield return new WaitForSeconds(0.10f);
        }
        DestroyBomb();
    }

    /// <summary>
    /// Instantiate an explosion.
    /// </summary>
    /// <param name="origin">Position of the explosion.</param>
    /// <param name="direction">the direction where the explosion is located.</param>
    void InstantiateBombExplosions(Vector3 origin, ref bool direction)
    {
        Instantiate(Explosion, origin, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(origin, 0.40f);
        if (hitColliders.Length > 0)
        {
            direction = false;
            if (hitColliders[0].transform.CompareTag("Destructible"))
            {
                hitColliders[0].gameObject.GetComponent<DestructibleBlocScript>().NbHP--;
            }
            else if (hitColliders[0].transform.CompareTag("Bomb"))
            {
                print("Hit Bomb : [" + hitColliders[0].gameObject + "]");
                hitColliders[0].gameObject.GetComponent<BombScript>().StopCoroutine("BombCountDown");
                hitColliders[0].gameObject.GetComponent<BombScript>().Destruction();
            }
            else if (hitColliders[0].transform.CompareTag("Player"))
            {
                print("Hit Player : [" + hitColliders[0].gameObject + "]");
                HitPlayer(hitColliders[0].transform.parent);
            }
        }
    }

    /// <summary>
    /// Instantiate the explosion under the bomb
    /// </summary>
    /// <param name="origin"></param>
    void InstantiateBombExplosion(Vector3 origin)
    {
        Instantiate(Explosion, origin, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(origin, 0.40f);
        if (hitColliders.Length > 0)
        {
            if (hitColliders[0].transform.CompareTag("Player"))
            {
                print("Hit Player : [" + hitColliders[0].gameObject + "]");
                HitPlayer(hitColliders[0].transform.parent);
            }
        }
    }

    public static bool IsTileEmpty(Vector3 origin, LayerMask mask)
    {
        Collider[] hitColliders = Physics.OverlapSphere(origin, 0.40f, mask);
        if (hitColliders.Length > 0)
        {
            return false;
        }
        return true;
    }

    void HitPlayer(Transform player)
    {
        player.GetComponentInChildren<ChampionsStatsScript>().LifePoint--;
    }
}
