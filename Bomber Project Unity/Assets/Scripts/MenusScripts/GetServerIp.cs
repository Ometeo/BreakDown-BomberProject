using UnityEngine;
using System.Collections;
using System.Net;

public class GetServerIp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        IPHostEntry host;
        string localIP = "?";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily.ToString() == "InterNetwork")
            {
                localIP = ip.ToString();
            }
        }

        this.GetComponent<TextMesh>().text = localIP;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
