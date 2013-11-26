/* --------------------------Header-------------------------------------
 * File : FullScreenTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 26/11/2013 08:18:46
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:18:46
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

public class FullScreenTextScript : GUIItemScript
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Transform _associatedTick;
    public Transform AssociatedTick
    {
        get { return _associatedTick; }
        set { _associatedTick = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        if (Screen.fullScreen)
            AssociatedTick.GetComponent<CaseAndTickScript>().Checked = true;
        InitializeGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseDown()
    {
        AssociatedTick.GetComponent<CaseAndTickScript>().ClickOnCaseAction();
    }
}
