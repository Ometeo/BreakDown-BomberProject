using UnityEngine;
using System.Collections;
using System;

public class ServerInitializePlayersManagerScript : MonoBehaviour {

    public class PlayerInformation : IEquatable<PlayerInformation>
    {
        public string PlayerName { get; set; }
        public NetworkPlayer NetworkInfo { get; set; }
        public int PlayerNumber { get; set; }

        public PlayerInformation(string playerName, NetworkPlayer networkPlayer, int playerNumber)
        {
            PlayerName = playerName;
            NetworkInfo = networkPlayer;
            PlayerNumber = playerNumber;
        }

        public bool Equals(PlayerInformation otherPlayer)
        {
            if (PlayerName == otherPlayer.PlayerName && NetworkInfo.externalIP == otherPlayer.NetworkInfo.externalIP)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is PlayerInformation))
                throw new InvalidCastException("The object isn't of Type PlayerInformation");
            return Equals(obj as PlayerInformation);
        }

        public override int GetHashCode()
        {
            return (PlayerName + NetworkInfo.externalIP).GetHashCode();
        }
    }

    [SerializeField]
    private SpawnerScript _spwnScript;
    public SpawnerScript SpwnScript
    {
        get { return _spwnScript; }
        set { _spwnScript = value; }
    }

    [SerializeField]
    private PlayerPoolManagerScript _playerPoolMngScript;
    public PlayerPoolManagerScript PlayerPoolMngScript
    {
        get { return _playerPoolMngScript; }
        set { _playerPoolMngScript = value; }
    }

    private ArrayList _players;
    public ArrayList Players
    {
        get { return _players; }
        set { _players = value; }
    }

    private static ChampionsDatabaseScript _champDbScript;
    public static ChampionsDatabaseScript ChampDbScript
    {
        get { return _champDbScript; }
        set { _champDbScript = value; }
    }

    [SerializeField]
    private NetworkManagerScript _networkMngrScript;
    public NetworkManagerScript NetworkMngrScript
    {
        get { return _networkMngrScript; }
        set { _networkMngrScript = value; }
    }

    void Awake()
    {
        ChampDbScript = GetComponent<ChampionsDatabaseScript>();
    }

    void Start()
    {
        Players = new ArrayList();
    }

    // Action to do when the server has initialized
    void OnServerInitialized()
    {
        
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        networkView.RPC("AskPlayerName", player);
    }


    [RPC]
    void ResponsePlayerInformation(string playerName, NetworkPlayer player)
    {
        var connectionPlayerInformation = new PlayerInformation(playerName, player, Players.Count);
        var indexOfConnectedPlayer = Players.IndexOf(connectionPlayerInformation);

        if (indexOfConnectedPlayer != -1) // Reconnection
        {
            ((PlayerInformation)Players[indexOfConnectedPlayer]).NetworkInfo = player;
            foreach (PlayerInformation playerInfo in Players)
            {
                Transform playerTransform = PlayerPoolMngScript.PlayersPrefab[playerInfo.PlayerNumber];
                var champId = playerTransform.GetComponent<InitializePlayersChampionScript>().ChampID;
                var spwnPos = playerTransform.position;

                PlayerPoolMngScript.networkView.RPC("ActivatePlayer", player, connectionPlayerInformation.PlayerNumber, playerInfo.PlayerNumber, spwnPos, champId);
            }
        }
        else // First Connection
        {
            SpawnPlayer(connectionPlayerInformation);   
        }
        
    }

    [RPC]
    void AskPlayerName()
    {
        networkView.RPC("ResponsePlayerInformation", RPCMode.Server, NetworkMngrScript.PlayerName, Network.player);
    }

    void SpawnPlayer(PlayerInformation player)
    {
        int playerNumber = player.PlayerNumber;

        // Instantiate the player
        var spwnPos = SpwnScript.SpawnPoints[playerNumber].position;
        spwnPos += new Vector3(0, 0.5f, 0);

        
        var randChamp = UnityEngine.Random.Range(0, ChampDbScript.ChampionList.Length);
        PlayerPoolMngScript.networkView.RPC("ActivatePlayer", RPCMode.All, playerNumber, playerNumber, spwnPos, randChamp);
        Players.Add(player);
    }
}
