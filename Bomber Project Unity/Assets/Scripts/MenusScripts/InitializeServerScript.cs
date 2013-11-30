/* --------------------------Header-------------------------------------
 * File : InitializeServerScript.cs
 * Description : Scrpt that initialize the server.
 * Version : 1.0.0
 * Created Date : 28/11/2013 20:55:57
 * Created by : Jonathan Bihet
 * Modification Date : 28/11/2013 20:55:57
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// InitializeServerScript Class = a button than inherit from GUIItemScript.
/// </summary>
public class InitializeServerScript : GUIItemScript
{

    /// <summary>
    /// the IP of the server (current computer).
    /// </summary>
    [SerializeField]
    private TextMesh _serverIP;
    public TextMesh ServerIP
    {
        get { return _serverIP; }
        set { _serverIP = value; }
    }

    /// <summary>
    /// The port choosen bu the user.
    /// </summary>
    [SerializeField]
    private GUITextField _serverPort;
    public GUITextField ServerPort
    {
        get { return _serverPort; }
        set { _serverPort = value; }
    }


    /// <summary>
    /// Initialize the button.
    /// </summary>
    void Start()
    {
        InitializeGUI();
    }
    /// <summary>
    /// on click = connect and change scene to lobby.
    /// </summary>
    public override void OnMouseUp()
    {
        string ip = ServerIP.text;
        int port = int.Parse(ServerPort.GetText());

        //??
        Network.Connect(ip, port);
        Application.LoadLevel("Play");
        //??
    }
}
