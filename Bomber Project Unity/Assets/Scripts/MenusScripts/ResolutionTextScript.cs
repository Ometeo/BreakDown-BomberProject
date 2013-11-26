/* --------------------------Header-------------------------------------
 * File : ResolutionTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 26/11/2013 08:19:56
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:19:56
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

public class ResolutionTextScript : MonoBehaviour
{

    private string[] _values;

    [SerializeField]
    private int _currentValue = 0;
    public int CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }

    // Use this for initialization
    void Start()
    {
        _values = new string[Screen.resolutions.Length];
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            _values[i] = Screen.resolutions[i].width.ToString() + " x " + Screen.resolutions[i].height.ToString();
        }

        this.gameObject.GetComponent<TextMesh>().text = _values[CurrentValue];
    }

    // Update is called once per frame
    void Update()
    {

    }

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
