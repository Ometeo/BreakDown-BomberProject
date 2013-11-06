using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour
{
    [SerializeField]
    private string _serverIp;
    public string ServerIp
    {
        get { return _serverIp; }
        set { _serverIp = value; }
    }

    [SerializeField]
    private int _connectionPort;
    public int ConnectionPort
    {
        get { return _connectionPort; }
        set { _connectionPort = value; }
    }

    [SerializeField]
    private string _playerName;
    public string PlayerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    

    // Use this for initialization
    void Start()
    {

    }

    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            PlayerName = GUI.TextField(new Rect(10, 10, 200, 20), PlayerName);
            ServerIp = GUI.TextField(new Rect(10, 30, 200, 20), ServerIp);
            if (GUI.Button(new Rect(10, 50, 120, 20), "Client Connect"))
            {
                Network.Connect(ServerIp, ConnectionPort);
            }
            if (GUI.Button(new Rect(10, 70, 120, 20), "Initialize Server"))
            {
                Network.InitializeSecurity();
                Network.InitializeServer(10, ConnectionPort, false);
            }
        }
        else if (Network.peerType == NetworkPeerType.Client)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Status: " + PlayerName + " as Client");
            if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
            {
                Network.Disconnect(200);
            }
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Server");
            if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
            {
                Network.Disconnect(200);
            }
        }

    }
}
