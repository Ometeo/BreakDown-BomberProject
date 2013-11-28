/* --------------------------Header-------------------------------------
 * File : ArrowScript.cs
 * Description : 
 * Version : 1.0.1
 * Created Date : 26/11/2013 08:27:46
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:37:07
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class ArrowScript : GUIItemScript
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool _increment;
    public bool Increment
    {
        get { return _increment; }
        set { _increment = value; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Transform _associatedField;
    public Transform AssociatedField
    {
        get { return _associatedField; }
        set { _associatedField = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        InitializeGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseDown()
    {
        if (Increment)
        {
            if (AssociatedField.GetComponent<ResolutionTextScript>())
            {
                AssociatedField.GetComponent<ResolutionTextScript>().Increment();
            }
            else if (AssociatedField.GetComponent<QualityTextScript>())
            {
                AssociatedField.GetComponent<QualityTextScript>().Increment();
            }
            else if (AssociatedField.GetComponent<GameModeTextScript>())
            {
                AssociatedField.GetComponent<GameModeTextScript>().Increment();
            }
            else if (AssociatedField.GetComponent<ArenaTextScript>())
            {
                AssociatedField.GetComponent<ArenaTextScript>().Increment();
            }
            else if (AssociatedField.GetComponent<AvatarTextScript>())
            {
                AssociatedField.GetComponent<AvatarTextScript>().Increment();
            }
        }
        else
        {
            if (AssociatedField.GetComponent<ResolutionTextScript>())
            {
                AssociatedField.GetComponent<ResolutionTextScript>().Decrement();
            }
            else if (AssociatedField.GetComponent<QualityTextScript>())
            {
                AssociatedField.GetComponent<QualityTextScript>().Decrement();
            }
            else if (AssociatedField.GetComponent<GameModeTextScript>())
            {
                AssociatedField.GetComponent<GameModeTextScript>().Decrement();
            }
            else if (AssociatedField.GetComponent<ArenaTextScript>())
            {
                AssociatedField.GetComponent<ArenaTextScript>().Decrement();
            }
            else if (AssociatedField.GetComponent<AvatarTextScript>())
            {
                AssociatedField.GetComponent<AvatarTextScript>().Decrement();
            }
        }
    }

}
