/* --------------------------Header-------------------------------------
 * File : IsLockedScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:58:51
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:58:51
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class IsLockedScript : GUIItemScript
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Transform _case;
    public Transform Case
    {
        get { return _case; }
        set { _case = value; }
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
    public override void OnMouseUp()
    {
        Case.GetComponent<PlayerCaseScript>().SetLocked();
        this.gameObject.SetActive(false);
    }
}
