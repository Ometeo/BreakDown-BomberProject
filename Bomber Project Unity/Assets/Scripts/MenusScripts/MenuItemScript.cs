/* --------------------------Header-------------------------------------
 * File : MenuItemScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 26/11/2013 08:21:14
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:21:14
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class MenuItemScript : GUIItemScript
{
    private string _itemName;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Transform _titleEffectObject;
    public Transform TitleEffectObject
    {
        get { return _titleEffectObject; }
        set { _titleEffectObject = value; }
    }


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _itemName = this.transform.name;
        InitializeGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseUp()
    {
        if (_itemName == "Quit")
            Application.Quit();
        else if (_itemName == "Title")
        {
            StartCoroutine(TitleEffect());
        }
        else
        {
            if ("MainMenu".Equals(_itemName))
            {
                PlayerPrefs.Save();
                if (Network.isClient)
                {
                    if (networkView != null)
                        networkView.RPC("SendRemovePlayer", RPCMode.Server);
                    Network.Disconnect();
                }
                else if (Network.isServer)
                {
                    Network.Disconnect();
                    GameOptionSingleton.Instance.GameStarted = false;
                }
                Application.LoadLevel(_itemName);
            }
            else if ("Play!".Equals(_itemName))
            {
                networkView.RPC("SendChangeScene", RPCMode.Server, _itemName);
            }
            else
                Application.LoadLevel(_itemName);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator TitleEffect()
    {
        TitleEffectObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        TitleEffectObject.gameObject.SetActive(false);
    }

    [RPC]
    void SendRemovePlayer(NetworkMessageInfo info)
    {
        PlayersSingleton.Instance.RemovePlayer(info.sender);
    }

    [RPC]
    void SendChangeScene(string _itemName, NetworkMessageInfo info)
    {
        var GOInstance = GameOptionSingleton.Instance;
        if (GOInstance.HostPlayer == info.sender)
        {
            
            GOInstance.GameStarted = true;
            GOInstance.SceneMngScript.LoadLevel(_itemName);
            networkView.RPC("SynchronizeGameOptions", RPCMode.Others, GOInstance.NumMode, GOInstance.NumScene, GOInstance.NbTeams);
            networkView.RPC("ResponseChangeScene", RPCMode.Others, _itemName);
        }
    }

    [RPC]
    void SynchronizeGameOptions(int numMode, int numScene, int nbTeams)
    {
        GameOptionSingleton.Instance.SyncGameOptionSingleton(numMode, numScene, nbTeams);
    }

    [RPC]
    void ResponseChangeScene(string _itemName)
    {
        GameOptionSingleton.Instance.SceneMngScript.LoadLevel(_itemName);
    }
}
