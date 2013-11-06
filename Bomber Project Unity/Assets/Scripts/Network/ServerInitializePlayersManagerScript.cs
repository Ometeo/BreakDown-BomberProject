using UnityEngine;
using System.Collections;
using System;

public class ServerInitializePlayersManagerScript : MonoBehaviour {

    public class PlayerInformation : IEquatable<PlayerInformation>
    {
        public string PlayerName { get; set; }
        public NetworkPlayer NetworkInfo { get; set; }
        public string PlayerGuid { get; set; }
        public int PlayerNumber { get; set; }

        public PlayerInformation(string playerName, NetworkPlayer networkPlayer)
        {
            PlayerName = playerName;
            NetworkInfo = networkPlayer;
            PlayerGuid = networkPlayer.guid;

            var tempPlayerString = networkPlayer.ToString();
            PlayerNumber = Convert.ToInt32(tempPlayerString);
        }

        public bool Equals(PlayerInformation otherPlayer)
        {
            Debug.Log("[PlayerInformation] comparing : [" + PlayerName + "] x [" + otherPlayer.PlayerName + "] / {" + NetworkInfo.guid + "} x {" + otherPlayer.PlayerGuid + "}");
            if (PlayerName == otherPlayer.PlayerName && NetworkInfo.guid == otherPlayer.PlayerGuid)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            Debug.Log("In here!");
            if (obj == null)
                return false;

            if (!(obj is PlayerInformation))
                throw new InvalidCastException("The object isn't of Type PlayerInformation");
            return Equals(obj as PlayerInformation);
        }

        public override int GetHashCode()
        {
            return (PlayerName + NetworkInfo.guid).GetHashCode();
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
    private Transform _playerPrefab;
    public Transform PlayerPrefab
    {
        get { return _playerPrefab; }
        set { _playerPrefab = value; }
    }

    private Hashtable _players;
    public Hashtable Players
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
        Players = new Hashtable();
    }

    // Action to do when the server has initialized
    void OnServerInitialized()
    {
        
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        networkView.RPC("AskPlayerName", player);
        Debug.Log("---> AskPlayerName");
    }

    

    [RPC]
    void ResponsePlayerInformation(string playerName, NetworkPlayer player)
    {
        var connectionPlayerInformation = new PlayerInformation(playerName, player);
        Debug.Log("<--- ResponsePlayerInformation");

        if (Players.Contains(connectionPlayerInformation)) // Reconnection
        {
            ((Transform)Players[connectionPlayerInformation]).networkView.RPC("SetPlayer", RPCMode.All, connectionPlayerInformation.NetworkInfo);
            Debug.Log("Reconnect");
        }
        else // First Connection
        {
            Debug.Log("First Connection");
            SpawnPlayer(connectionPlayerInformation);   
        }
        
    }

    [RPC]
    void AskPlayerName()
    {
        Debug.Log("<--- AskPlayerName");
        networkView.RPC("ResponsePlayerInformation", RPCMode.Server, NetworkMngrScript.PlayerName, Network.player);
        Debug.Log("---> ResponsePlayerInformation");
    }

    void SpawnPlayer(PlayerInformation player)
    {
        int playerNumber = player.PlayerNumber;

        // Instantiate the player
        var spwnPos = SpwnScript.SpawnPoints[playerNumber - 1].position;
        spwnPos += new Vector3(0, 0.5f, 0);
        Transform newPlayerTransform = (Transform)Network.Instantiate(PlayerPrefab, spwnPos, transform.rotation, playerNumber);
        
        
        
        // Set the player's champion
        var randChamp = UnityEngine.Random.Range(0, ChampDbScript.ChampionList.Length);
        newPlayerTransform.GetComponent<InitializePlayersChampionScript>().ChampID = randChamp;

        NetworkView theNetworkView = newPlayerTransform.networkView;
        theNetworkView.RPC("SetPlayer", RPCMode.All, player.NetworkInfo);
        theNetworkView.RPC("SetChamp", RPCMode.Others, randChamp);
        Players.Add(player, newPlayerTransform);
    }
}
