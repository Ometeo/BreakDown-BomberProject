/* --------------------------Header-------------------------------------
 * File : QualityTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 26/11/2013 08:20:01
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:20:01
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

public class QualityTextScript : MonoBehaviour
{

    private static string[] _values = {"Fastest", "Fast", "Simple", "Good", "Beautiful", "Fantastic"};

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
        CurrentValue = QualitySettings.GetQualityLevel();

        this.gameObject.GetComponent<TextMesh>().text = _values[CurrentValue];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Increment()
    {
        if (CurrentValue == _values.Length - 1)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue++;
        }
        this.gameObject.GetComponent<TextMesh>().text = _values[CurrentValue];
    }

    public void Decrement()
    {
        if (CurrentValue == 0)
        {
            CurrentValue = _values.Length - 1;
        }
        else
        {
            CurrentValue--;
        }
        this.gameObject.GetComponent<TextMesh>().text = _values[CurrentValue];
    }
}
