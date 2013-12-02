using UnityEngine;
using System.Collections;
using System;

public class ServerInitializePlayersManagerScript : MonoBehaviour {

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

    private static ChampionsDatabaseScript _champDbScript;
    public static ChampionsDatabaseScript ChampDbScript
    {
        get { return _champDbScript; }
        set { _champDbScript = value; }
    }

    void Awake()
    {
        ChampDbScript = GetComponent<ChampionsDatabaseScript>();
    }

    void Start()
    {
        ActivatePlayer();
    }

    void ActivatePlayer()
    {
        foreach (PlayersSingleton.PlayerInformation playerInfo in PlayersSingleton.Instance.Players)
        {
            Transform playerTransform = PlayerPoolMngScript.PlayersPrefab[playerInfo.PlayerNumber];
            var champId = playerInfo.ChampNumber;
            var viewId = Network.AllocateViewID();
            var spwnPos = SpwnScript.SpawnPoints[playerInfo.PlayerNumber].position;
            spwnPos += new Vector3(0, 0.5f, 0);
            playerInfo.PlayerTransform = playerTransform;

            PlayerPoolMngScript.ActivatePlayer(viewId, playerInfo.NwPlayer, playerInfo.PlayerNumber, spwnPos, champId);
            PlayerPoolMngScript.networkView.RPC("ResponseActivatePlayer", RPCMode.Others, viewId, playerInfo.NwPlayer, playerInfo.PlayerNumber, spwnPos, champId);
            
        }
    }
}
