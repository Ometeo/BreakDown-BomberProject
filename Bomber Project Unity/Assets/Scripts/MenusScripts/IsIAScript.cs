/* --------------------------Header-------------------------------------
 * File : IsIAScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:59:05
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:59:05
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class IsIAScript : GUIItemScript
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
        networkView.RPC("SendSetIA", RPCMode.Server);
    }

    #region RPC Server Side
    /* ------------------------------------------------------------------------------------------
     *                                       Server Side
     * ------------------------------------------------------------------------------------------*/

    [RPC]
    void SendSetIA(NetworkMessageInfo info)
    {
        if (GameOptionSingleton.Instance.HostPlayer == info.sender)
        {
            Case.GetComponent<PlayerCaseScript>().SetIA();
            PlayersSingleton.Instance.AddBot(_playerItemScr.PlayerNb);
            _playerItemScr.NetwkMenuMngScr.RefreshPlayersName();
            
            this.gameObject.SetActive(false);
            networkView.RPC("ResponseSetIA", RPCMode.Others);
        }
    }
    #endregion

    #region RPC Client Side
    /* ------------------------------------------------------------------------------------------
     *                                      RPC Client Side
     * ------------------------------------------------------------------------------------------*/

    [RPC]
    void ResponseSetIA()
    {
        Case.GetComponent<PlayerCaseScript>().SetIA();
        this.gameObject.SetActive(false);
    }
    #endregion
}
