using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(NetworkView))]
public class ChampionMovementAuthorativeScript : MonoBehaviour {

    private NetworkPlayer _theOwner;
    public NetworkPlayer TheOwner
    {
        get { return _theOwner; }
        set { _theOwner = value; }
    }

    [SerializeField]
    private float _walkSpeed;
    public float WalkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    private Vector3 _lastClientDirection = Vector3.zero;
    private Vector3 _serverCurrentDirection = Vector3.zero;

    // Disable this script if it's not on the server
    void Awake()
    {
        if (Network.isClient)
        {
            enabled = false;
            Destroy(rigidbody);
        }
    }

    // This method enable the possibility to move the champion only by the player who ordered the creation
    [RPC]
    void SetPlayer(NetworkPlayer player)
    {
        TheOwner = player;
        if (player == Network.player)
            enabled = true;
    }

    void Update()
    {
        if (TheOwner != null && Network.player == TheOwner)
        {
            float xAxis = 0;
            float zAxis = 0;
            if (Input.GetKey(KeyCode.UpArrow))
                zAxis += 1;
            if (Input.GetKey(KeyCode.DownArrow))
                zAxis -= 1;
            if (Input.GetKey(KeyCode.LeftArrow))
                xAxis -= 1;
            if (Input.GetKey(KeyCode.RightArrow))
                xAxis += 1;
            Vector3 newDirection = new Vector3(xAxis, 0, zAxis).normalized;
            if (newDirection != _lastClientDirection)
            {
                _lastClientDirection = newDirection;
                if (Network.isClient)
                {
                    networkView.RPC("SendMovementDirection", RPCMode.Server, _lastClientDirection);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (Network.isServer)
        {
            Vector3 temp = (WalkSpeed * _serverCurrentDirection * Time.deltaTime);
            rigidbody.velocity = temp;
        }
    }

    [RPC]
    void SendMovementDirection(Vector3 direction)
    {
        _serverCurrentDirection = direction;
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.position;
            stream.Serialize(ref pos);
        }
        else
        {
            Vector3 posReceive = Vector3.zero;
            stream.Serialize(ref posReceive);
            transform.position = posReceive;
        }
    }
}
