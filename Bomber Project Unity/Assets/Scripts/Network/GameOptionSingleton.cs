using UnityEngine;
using System.Collections;

public class GameOptionSingleton
{
    private NetworkPlayer _hostPlayer;
    public NetworkPlayer HostPlayer
    {
        get { return _hostPlayer; }
        set { _hostPlayer = value; }
    }

    private SceneManager _sceneMngScript;
    public SceneManager SceneMngScript
    {
        get { return _sceneMngScript; }
        set { _sceneMngScript = value; }
    }

    private NetworkMenuManagerScript _nwMenuMngScript;
    public NetworkMenuManagerScript NwMenuMngScript
    {
        get { return _nwMenuMngScript; }
        set { _nwMenuMngScript = value; }
    }

    private bool _gameStarted;
    public bool GameStarted
    {
        get { return _gameStarted; }
        set { _gameStarted = value; }
    }

    private int _numScene;
    public int NumScene
    {
        get { return _numScene; }
        set { _numScene = value; }
    }

    private int _numMode;
    public int NumMode
    {
        get { return _numMode; }
        set { _numMode = value; }
    }

    private static GameOptionSingleton _Instance;
    public static GameOptionSingleton Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new GameOptionSingleton();
            return _Instance;
        }
    }

    private GameOptionSingleton()
    {
        GameStarted = false;
        NumScene = 0;
        NumMode = 0;
    }

    public void RefreshPlayersName()
    {
        if (NwMenuMngScript != null)
        {
            foreach (var plItmScr in NwMenuMngScript.PlayersItemScr)
            {
                var pI = PlayersSingleton.Instance.GetPlayerInformation(plItmScr.PlayerNb);
                if (pI == null)
                    NwMenuMngScript.networkView.RPC("RefreshName", RPCMode.All, plItmScr.PlayerNb, plItmScr.DefaultText);
                else
                    NwMenuMngScript.networkView.RPC("RefreshName", RPCMode.All, plItmScr.PlayerNb, pI.PlayerName);
            }
        }
    }

    public void SetHostRights()
    {
        if (PlayersSingleton.Instance.Players.Count > 0)
            HostPlayer = ((PlayersSingleton.PlayerInformation)PlayersSingleton.Instance.Players[0]).NwPlayer;
    }
}
