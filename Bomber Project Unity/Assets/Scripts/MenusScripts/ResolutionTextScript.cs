/* --------------------------Header-------------------------------------
 * File : ResolutionTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 26/11/2013 08:19:56
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:02:11
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class ResolutionTextScript : MonoBehaviour
{

    private string[] _values;

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
        _values = new string[Screen.resolutions.Length];
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            _values[i] = Screen.resolutions[i].width.ToString() + " x " + Screen.resolutions[i].height.ToString();
        }

        this.gameObject.GetComponent<TextMesh>().text = _values[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void Increment()
    {
        if (_currentValue == _values.Length - 1)
        {
            _currentValue = 0;
        }
        else
        {
            _currentValue++;
        }
        this.gameObject.GetComponent<TextMesh>().text = _values[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Decrement()
    {
        if (_currentValue == 0)
        {
            _currentValue = _values.Length - 1;
        }
        else
        {
            _currentValue--;
        }
        this.gameObject.GetComponent<TextMesh>().text = _values[CurrentValue];
    }
}
