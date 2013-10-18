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

    /// <summary>
    /// Boolean to allow horiontal direction.
    /// </summary>
    [SerializeField]
    private bool _horizontalDirection;
    public bool HorizontalDirection
    {
        get { return _horizontalDirection; }
        set { _horizontalDirection = value; }
    }

    /// <summary>
    /// Boolean to allow vertical direction.
    /// </summary>
    [SerializeField]
    private bool _verticalDirection;
    public bool VerticalDirection
    {
        get { return _verticalDirection; }
        set { _verticalDirection = value; }
    }

    /// <summary>
    /// Boolean to allow left diagonal direction.
    /// </summary>
    [SerializeField]
    private bool _leftDiagonalDirection;
    public bool LeftDiagonalDirection
    {
        get { return _leftDiagonalDirection; }
        set { _leftDiagonalDirection = value; }
    }

    /// <summary>
    /// Boolean to right diagonal direction.
    /// </summary>
    [SerializeField]
    private bool _rightDiagonalDirection;
    public bool RightDiagonalDirection
    {
        get { return _rightDiagonalDirection; }
        set { _rightDiagonalDirection = value; }
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


    /// <summary>
    /// When the bomb is instantiate, get the animation, and start the countdown.
    /// </summary>
    void Start()
    {
        _destroyBombAnimation = this.gameObject.GetComponent<Animation>();
        StartCoroutine(BombCountDown());
    }

    /// <summary>
    /// Nothing for the moment.
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// Destroy the object.
    /// </summary>
    void DestroyBomb()
    {
        Destroy(this.gameObject);
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
        Instantiate(Explosion, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.10f);
        for (int i = 1; i <= Distance; i++)
        {
            if (HorizontalDirection)
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

            if (VerticalDirection)
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

            if (RightDiagonalDirection)
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

            if (LeftDiagonalDirection)
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
        Quaternion uselessRotation = Quaternion.identity;
        Instantiate(Explosion, origin, uselessRotation);
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
                print(hitColliders[0].gameObject);
                hitColliders[0].gameObject.GetComponent<BombScript>().StopCoroutine("BombCountDown");
                hitColliders[0].gameObject.GetComponent<BombScript>().Destruction();
            }
        }
    }

}
