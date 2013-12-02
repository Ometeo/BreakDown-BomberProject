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

    private ResolutionTextScript _rts;
    private QualityTextScript _qts;
    private GameModeTextScript _gmts;
    private ArenaTextScript _ats;
    private AvatarTextScript _avTxScr;
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        InitializeGUI();
        _rts = AssociatedField.GetComponent<ResolutionTextScript>();
        _qts = AssociatedField.GetComponent<QualityTextScript>();
        _gmts = AssociatedField.GetComponent<GameModeTextScript>();
        _ats = AssociatedField.GetComponent<ArenaTextScript>();
        _avTxScr = AssociatedField.GetComponent<AvatarTextScript>();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseUp()
    {
        if (_rts != null)
        {
            if (Increment)
                _rts.Increment();
            else
                _rts.Decrement();
        }
        else if (_qts != null)
        {
            if (Increment)
                _qts.Increment();
            else
                _qts.Decrement();
        }
        else
        {
            if (networkView != null && Network.isClient)
                networkView.RPC("SendClick", RPCMode.Server);
        }
    }

    [RPC]
    void SendClick(NetworkMessageInfo info)
    {
        if (GameOptionSingleton.Instance.HostPlayer == info.sender)
        {
            Click(info.sender);
            networkView.RPC("ResponseClick", RPCMode.Others);
        }
    }

    [RPC]
    void ResponseClick()
    {
        Click(Network.player);
    }

    void Click(NetworkPlayer player)
    {
        if (_gmts != null)
        {
            if (Increment)
                _gmts.Increment();
            else
                _gmts.Decrement();
            if (Network.isServer)
                GameOptionSingleton.Instance.NumMode = _gmts.CurrentValue;
        }
        else if (_ats != null)
        {
            if (Increment)
                _ats.Increment();
            else
                _ats.Decrement();
            if (Network.isServer)
                GameOptionSingleton.Instance.NumScene = _ats.CurrentValue;
        }
        else if (_avTxScr != null)
        {
            if (Increment)
                _avTxScr.Increment();
            else
                _avTxScr.Decrement();
            if (Network.isServer)
                PlayersSingleton.Instance.SetPlayerChamp(player, _avTxScr.CurrentValue);
        }
    }
}
