using UnityEngine;
using System.Collections;

public class NetworkMenuManagerScript : MonoBehaviour {

    private string _playerName;
    private string _serverIp;
    private int _connectionPort;

    [SerializeField]
    private PlayerItemScript[] _playersItemScr;
    public PlayerItemScript[] PlayersItemScr
    {
        get { return _playersItemScr; }
        set { _playersItemScr = value; }
    }

    void Awake()
    {
        var playerName = PlayersSingleton.Instance.MyName;
        if (playerName != null && playerName.Length > 0)
            _playerName = playerName;
        else
            _playerName = PlayerPrefs.GetString("PlayerName");
        _serverIp = PlayerPrefs.GetString("ServerIp");
        _connectionPort = int.Parse(PlayerPrefs.GetString("ServerPort"));
    }

    void Start()
    {
        if (!Network.isServer)
            Network.Connect(_serverIp, _connectionPort);
    }

    #region RPC Server Side
    /* ------------------------------------------------------------------------------------------
     *                                       Server Side
     * ------------------------------------------------------------------------------------------*/

    void OnPlayerConnected(NetworkPlayer player)
    {
        networkView.RPC("AskPlayerName", player);
    }

    [RPC]
    void ResponsePlayerInformation(string playerName, NetworkPlayer player)
    {
        bool isNewPlayer = false;
        PlayersSingleton.PlayerInformation pI = PlayersSingleton.Instance.GetOrCreatePlayerInformation(playerName, player, ref isNewPlayer);

        if (isNewPlayer) // First Connection
        {
            RefreshPlayersName();
        }
        else // Reconnection
        {

        }
    }
    #endregion

    public void RefreshPlayersName()
    {
        foreach (var plItmScr in PlayersItemScr)
        {
            var pI = PlayersSingleton.Instance.GetPlayerInformation(plItmScr.PlayerNb);
            if (pI == null)
                networkView.RPC("RefreshName", RPCMode.All, plItmScr.PlayerNb, plItmScr.DefaultText);
            else
                networkView.RPC("RefreshName", RPCMode.All, plItmScr.PlayerNb, pI.PlayerName);
        }
    }

    #region RPC Client Side
    /* ------------------------------------------------------------------------------------------
     *                                      RPC Client Side
     * ------------------------------------------------------------------------------------------*/

    [RPC]
    void AskPlayerName()
    {
        networkView.RPC("ResponsePlayerInformation", RPCMode.Server, _playerName, Network.player);
    }

    [RPC]
    void RefreshName(int numItem, string playerName)
    {
        if (numItem != -1)
            PlayersItemScr[numItem].NameText.text = playerName;
    }

    #endregion
}
