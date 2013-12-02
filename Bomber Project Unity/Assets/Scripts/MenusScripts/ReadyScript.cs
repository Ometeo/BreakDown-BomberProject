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

	/// <summary>
	/// Initialize the GUI on start.
	/// </summary>
	void Start ()
    {
        InitializeGUI();
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
            LoadGame(GameOptionSingleton.Instance.NumScene);
            networkView.RPC("ResponsePlayerIsReady", RPCMode.Others, GameOptionSingleton.Instance.NumScene);
        }
    }

    [RPC]
    void ResponsePlayerIsReady(int numScene)
    {
        LoadGame(numScene);
    }

    void LoadGame(int numScene)
    {
        string arenaName;

        switch (numScene)
        {
            case 1:
                arenaName = "TinyScene";
                break;
            case 2:
                arenaName = "EdgyScene";
                break;
            case 3:
                arenaName = "CrunchyScene";
                break;
            default:
                arenaName = "ClassyTempScene";
                break;
        }
        GameOptionSingleton.Instance.SceneMngScript.LoadLevel(arenaName);
    }
}
