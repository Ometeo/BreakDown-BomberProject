using UnityEngine;
using System.Collections;

public class PlaySelectPlayerItemScript : MonoBehaviour {

    [SerializeField]
    private TextMesh _text;
    public TextMesh Text
    {
        get { return _text; }
        set { _text = value; }
    }

    [SerializeField]
    private PlayerItemScript _playerItemScr;
    public PlayerItemScript PlayerItemScr
    {
        get { return _playerItemScr; }
        set { _playerItemScr = value; }
    }

    [SerializeField]
    private int _playerNb;
    public int PlayerNb
    {
        get { return _playerNb; }
        set { _playerNb = value; }
    }

    void OnMouseUp()
    {
        if (Network.isClient)
        {
            networkView.RPC("SendSelectThis", RPCMode.Server);
        }
    }

    [RPC]
    void SendSelectThis(NetworkMessageInfo info)
    {
        if (PlayerItemScr.IsLocked || PlayerItemScr.IsIA)
            return;

        PlayersSingleton.PlayerInformation pI = PlayersSingleton.Instance.GetPlayerInformation(PlayerNb);
        if (pI != null)
            return;

        pI = PlayersSingleton.Instance.GetPlayerInformation(info.sender);
        PlayersSingleton.Instance.ChangePlayerNumber(info.sender, PlayerNb);
        PlayerItemScr.NetwkMenuMngScr.RefreshPlayersName();
    }
}
