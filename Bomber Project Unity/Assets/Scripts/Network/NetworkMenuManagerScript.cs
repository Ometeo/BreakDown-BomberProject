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
        _playerName = PlayerPrefs.GetString("PlayerName");
        _serverIp = PlayerPrefs.GetString("ServerIp");
        _connectionPort = int.Parse(PlayerPrefs.GetString("ServerPort"));
    }

    void Start()
    {
        if (!Network.isServer)
            Network.Connect(_serverIp, _connectionPort);
        else
            GameOptionSingleton.Instance.NwMenuMngScript = this;
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        networkView.RPC("AskPlayerName", player);
    }

    [RPC]
    void AskPlayerName()
    {
        networkView.RPC("ResponsePlayerInformation", RPCMode.Server, _playerName, Network.player);
    }

    [RPC]
    void ResponsePlayerInformation(string playerName, NetworkPlayer player)
    {
        bool isNewPlayer = false;
        PlayersSingleton.PlayerInformation pI = PlayersSingleton.Instance.GetPlayerInformation(playerName, player, ref isNewPlayer);

        if (isNewPlayer) // First Connection
        {
            GameOptionSingleton.Instance.RefreshPlayersName();
        }
        else // Reconnection
        {

        }
    }

    [RPC]
    void RefreshName(int numItem, string playerName)
    {
        PlayersItemScr[numItem].NameText.text = playerName;
    }
}
