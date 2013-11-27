/* --------------------------Header-------------------------------------
 * File : BombSkinsTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 18:01:03
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:01:03
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class BombSkinsTextScript : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private string[] _bombSkins;
    public string[] BombSkins
    {
        get { return _bombSkins; }
        set { _bombSkins = value; }
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
        this.gameObject.GetComponent<TextMesh>().text = BombSkins[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Increment()
    {
        if (CurrentValue == BombSkins.Length - 1)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue++;
        }
        this.gameObject.GetComponent<TextMesh>().text = BombSkins[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Decrement()
    {
        if (CurrentValue == 0)
        {
            CurrentValue = BombSkins.Length - 1;
        }
        else
        {
            CurrentValue--;
        }
        this.gameObject.GetComponent<TextMesh>().text = BombSkins[CurrentValue];
    }
}
