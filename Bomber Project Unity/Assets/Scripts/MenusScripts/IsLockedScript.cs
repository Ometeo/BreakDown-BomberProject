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


    public override void OnMouseUp()
    {
        networkView.RPC("SendSetLock", RPCMode.Server);
    }

    [RPC]
    void SendSetLock(NetworkMessageInfo info)
    {
        if (GameOptionSingleton.Instance.HostPlayer == info.sender)
        {
            SetLock();
            networkView.RPC("ResponseSetLock", RPCMode.Others);
        }
    }

    [RPC]
    void ResponseSetLock()
    {
        SetLock();
    }

    void SetLock()
    {
        Case.GetComponent<PlayerCaseScript>().SetLocked();
        this.gameObject.SetActive(false);
    }
}
