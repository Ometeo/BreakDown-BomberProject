using UnityEngine;
using System.Collections;

public class PlayerPoolManagerScript : MonoBehaviour {

    [SerializeField]
    private Transform[] _playersPrefab;
    public Transform[] PlayersPrefab
    {
        get { return _playersPrefab; }
        set { _playersPrefab = value; }
    }

    void Awake()
    {
        foreach (var playerPrefab in PlayersPrefab)
            playerPrefab.gameObject.SetActive(false);
    }

    /// <summary>
    /// Activate a new Player and place it at position
    /// </summary>
    /// <param name="position">Futur player position</param>
    [RPC]
    void ResponseActivatePlayer(NetworkViewID viewID, NetworkPlayer player, int instiantePlayerNum, Vector3 position, int champNumber)
    {
        ActivatePlayer(viewID, player, instiantePlayerNum, position, champNumber);
    }

    public void ActivatePlayer(NetworkViewID viewID, NetworkPlayer player, int instiantePlayerNum, Vector3 position, int champNumber)
    {
        Transform newPlayer = PlayersPrefab[instiantePlayerNum];
        newPlayer.parent = null;
        newPlayer.position = position;
        newPlayer.gameObject.SetActive(true);

        // Set the player champion
        InitializePlayersChampionScript initScript = newPlayer.GetComponent<InitializePlayersChampionScript>();
        initScript.ChampID = champNumber;
        initScript.NwViewID = viewID;

        if (player == Network.player)
        {
            newPlayer.GetComponent<PlayerInputManagerScript>().SetPlayer();
        }

        initScript.SetChampion();
    }
}
