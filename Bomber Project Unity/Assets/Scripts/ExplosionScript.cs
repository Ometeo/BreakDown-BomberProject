/*
 *   File : ExplosionScript.cs
 *   Description : This script destroy the explosion object when the visual effect finished.
 *   Version : 1.0.0
 *   Created by : Jonathan Bihet
 *   Created Date : 17/10/2013
 *   Modification Date : 18/10/2013
 *   Modified by : Jonathan Bihet 
 */

using UnityEngine;
using System.Collections;

/// <summary>
///  This class handle the destruction of the explosion unit.
/// </summary>
public class ExplosionScript : MonoBehaviour
{

    /// <summary>
    /// The start method launch a coroutine.
    /// </summary>
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    /// <summary>
    /// Do nothing for the moment.
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Wait 0.6f before destroying the object.
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}
