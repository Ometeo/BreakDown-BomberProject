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

    /// <summary>
    /// Activate a new Player and place it at position
    /// </summary>
    /// <param name="position">Futur player position</param>
    [RPC]
    void ActivatePlayer(int callerPlayerNum, int instiantePlayerNum, Vector3 position, int champNumber)
    {
        Transform newPlayer = PlayersPrefab[instiantePlayerNum];
        newPlayer.parent = null;
        newPlayer.position = position;
        newPlayer.gameObject.SetActive(true);
        
        // Set the player champion
        newPlayer.GetComponent<InitializePlayersChampionScript>().ChampID = champNumber;
        if (callerPlayerNum == instiantePlayerNum)
            newPlayer.GetComponent<PlayerInputManagerScript>().SetPlayer();
    }
}
