/*
 *   File : DestructibleBlocScript.cs
 *   Description : This script handles the behaviour of destructible bloc.
 *   Version : 1.0.0
 *   Created by : Jonathan Bihet
 *   Created Date : 17/10/2013
 *   Modification Date : 18/10/2013
 *   Modified by : Jonathan Bihet 
 */

using UnityEngine;
using System.Collections;

/// <summary>
/// This class handles the DestructibleBloc.
/// </summary>
public class DestructibleBlocScript : MonoBehaviour
{
    /// <summary>
    /// Avoid multiple send of the destroy order
    /// </summary>
    private bool _isDestroyOrderSent = false;

    /// <summary>
    /// The Health point of the bloc.
    /// </summary>
    [SerializeField]
    private int _nbHP;
    public int NbHP
    {
        get
        {
            return _nbHP;
        }
        set
        {
            _nbHP = value;
        }
    }

    /// <summary>
    /// The destroy animation.
    /// </summary>
    private Animation _destroyAnimation;


    void Awake()
    {
        if (Network.isClient)
            enabled = false;
    }

    /// <summary>
    /// At the start, get the animation.
    /// </summary>
    void Start()
    {
        _destroyAnimation = this.gameObject.GetComponent<Animation>();
    }

    /// <summary>
    /// Check if the NbHP of the bloc is greater than 0.
    /// </summary>
    void Update()
    {
        if (NbHP <= 0)
        {
            _destroyAnimation.Play();
            DestroyBloc();

            // Send the destroy order to clients
            if (Network.isServer && !_isDestroyOrderSent)
            {
                networkView.RPC("DestroyMe", RPCMode.Others);
                _isDestroyOrderSent = true;
            }
        }
    }

    /// <summary>
    /// Check if the scale if equal to zero (state at the end of the animation), then destroy the bloc.
    /// </summary>
    void DestroyBloc()
    {
        if(this.gameObject.transform.localScale == Vector3.zero)
        {
            Destroy(this.gameObject);
        }
            
    }

    [RPC]
    void DestroyMe()
    {
        NbHP = 0;
        enabled = true;
    }
}
