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

    [SerializeField]
    private Transform[] _availableChampions;
    public Transform[] AvailableChampions
    {
        get { return _availableChampions; }
        set { _availableChampions = value; }
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
        Transform champion = (Transform)Instantiate(AvailableChampions[UnityEngine.Random.Range(0, AvailableChampions.Length)], transform.position, transform.rotation);
        champion.parent = newPlayerTransform;

        InitializePlayersChampionScript initPlayChampScript = newPlayerTransform.GetComponent<InitializePlayersChampionScript>();
        initPlayChampScript.OrderInitializeChampion(champion.GetComponent<ChampionsStatsScript>().SkinColor);

        NetworkView theNetworkView = newPlayerTransform.networkView;
        theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
    }
}
