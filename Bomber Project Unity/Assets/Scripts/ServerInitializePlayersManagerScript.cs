using UnityEngine;
using System.Collections;
using System;

public class ServerInitializePlayersManagerScript : MonoBehaviour {

    [SerializeField]
    private Transform _playerPrefab;
    public Transform PlayerPrefab
    {
        get { return _playerPrefab; }
        set { _playerPrefab = value; }
    }

    void Awake()
    {
        ChampDbScript = GetComponent<ChampionsDatabaseScript>();
    }

    private static ChampionsDatabaseScript _champDbScript;
    public static ChampionsDatabaseScript ChampDbScript
    {
        get { return _champDbScript; }
        set { _champDbScript = value; }
    }

    // Action to do when the server has initialized
    void OnServerInitialized()
    {
        
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        SpawnPlayer(player);
    }

    void SpawnPlayer(NetworkPlayer player)
    {
        string tempPlayerString = player.ToString();
        int playerNumber = Convert.ToInt32(tempPlayerString);

        // Instantiate the player
        Transform newPlayerTransform = (Transform)Network.Instantiate(PlayerPrefab, transform.position, transform.rotation, playerNumber);
        
        
        
        // Set the player's champion
        var randChamp = UnityEngine.Random.Range(0, ChampDbScript.ChampionList.Length);
        newPlayerTransform.GetComponent<InitializePlayersChampionScript>().ChampID = randChamp;

        NetworkView theNetworkView = newPlayerTransform.networkView;
        theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
        theNetworkView.RPC("SetChamp", RPCMode.Others, randChamp);
    }
}
