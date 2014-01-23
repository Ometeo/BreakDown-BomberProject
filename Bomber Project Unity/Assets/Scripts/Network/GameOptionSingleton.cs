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

    private int _nbTeams;
    public int NbTeams
    {
        get { return _nbTeams; }
        set { _nbTeams = value; }
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
        NbTeams = 0;
    }

    public void SetHostRights()
    {
        int playerNum = 0;
        int nbPlayers = PlayersSingleton.Instance.Players.Count;
        if (nbPlayers > 0)
        {
            while (HostPlayer == Network.player && playerNum < nbPlayers)
            {
                HostPlayer = ((PlayersSingleton.PlayerInformation)PlayersSingleton.Instance.Players[playerNum]).NwPlayer;
                playerNum++;
            }
        }
    }

    public void SyncGameOptionSingleton(int numMode, int numScene, int nbTeams)
    {
        NumMode = numMode;
        NumScene = numScene;
        NbTeams = nbTeams;
    }
}
