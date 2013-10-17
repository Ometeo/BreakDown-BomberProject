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

    private ArrayList _playerScripts = new ArrayList();

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

        Transform newPlayerTransform = (Transform)Network.Instantiate(PlayerPrefab, transform.position, transform.rotation, playerNumber);
        _playerScripts.Add(newPlayerTransform.GetComponent("cubeMoveAuthorativeScript"));

        NetworkView theNetworkView = newPlayerTransform.networkView;
        theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
    }
}
