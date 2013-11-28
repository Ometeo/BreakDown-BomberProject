/* --------------------------Header-------------------------------------
 * File : ArenaTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 18:01:39
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:01:40
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class ArenaTextScript : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private string[] _arena;
    public string[] Arena
    {
        get { return _arena; }
        set { _arena = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private int _currentValue = 0;
    public int CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        this.gameObject.GetComponent<TextMesh>().text = Arena[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Increment()
    {
        if (CurrentValue == Arena.Length - 1)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue++;
        }
        this.gameObject.GetComponent<TextMesh>().text = Arena[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Decrement()
    {
        if (CurrentValue == 0)
        {
            CurrentValue = Arena.Length - 1;
        }
        else
        {
            CurrentValue--;
        }
        this.gameObject.GetComponent<TextMesh>().text = Arena[CurrentValue];
    }
}
