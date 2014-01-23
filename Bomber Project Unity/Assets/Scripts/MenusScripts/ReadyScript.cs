/* --------------------------Header-------------------------------------
 * File : ReadyScript.cs
 * Description : Script for the button that launch the game.
 * Version : 1.0.0
 * Created Date : 27/11/2013 18:36:27
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:36:27
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// ReadyScript Class
/// </summary>
public class ReadyScript : GUIItemScript {

    private string _arenaName;
    public string ArenaName
    {
        get { return _arenaName; }
        set { _arenaName = value; }
    }

	/// <summary>
	/// Initialize the GUI on start.
	/// </summary>
	void Start ()
    {
        InitializeGUI();

        DatabaseManagerScript databaseScript = (DatabaseManagerScript)Resources.Load("DatabaseManager", typeof(DatabaseManagerScript));
        ArenaName = databaseScript.Arenas[GameOptionSingleton.Instance.NumScene] + "Scene";
	}

    public override void OnMouseUp()
    {
        networkView.RPC("SendPlayerIsReady", RPCMode.Server);
    }

    [RPC]
    void SendPlayerIsReady(NetworkMessageInfo info)
    {
        if (PlayersSingleton.Instance.AllPlayerReady(info.sender))
        {
            LoadGame();
            networkView.RPC("ResponsePlayerIsReady", RPCMode.Others);
        }
    }

    public void StartGame()
    {
        LoadGame();
        networkView.RPC("ResponsePlayerIsReady", RPCMode.Others);
    }

    [RPC]
    void ResponsePlayerIsReady()
    {
        LoadGame();
    }

    void LoadGame()
    {
        GameOptionSingleton.Instance.SceneMngScript.LoadLevel(ArenaName);
    }
}
