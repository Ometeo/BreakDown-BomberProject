using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class ChampionMovementAuthorativeScript : MonoBehaviour {

    private Rigidbody _rigidBody;

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

    private Vector3 _lastPosition;

    [SerializeField]
    private float _minimumMovementToUpdatePos; // Default 0.05f
    public float MinimumMovementToUpdatePos
    {
        get { return _minimumMovementToUpdatePos; }
        set { _minimumMovementToUpdatePos = value; }
    }

    // Disable this script if it's not on the server
    void Awake()
    {
        _rigidBody = rigidbody;
        if (Network.isClient)
        {
            enabled = false;
            if (_rigidBody != null)
                Destroy(_rigidBody);
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

            // If the direction change, send the information to the server
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
            if (_rigidBody != null)
                rigidbody.velocity = (WalkSpeed * _serverCurrentDirection * Time.deltaTime);
            if (Vector3.Distance(transform.position, _lastPosition) > MinimumMovementToUpdatePos)
            {
                _lastPosition = transform.position;
                networkView.RPC("SetPosition", RPCMode.Others, transform.position);
            }
        }
    }

    [RPC]
    void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    [RPC]
    void SendMovementDirection(Vector3 direction)
    {
        _serverCurrentDirection = direction;
    }


    /* Save if we decide to use NetworkStream on movement
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
    //*/
}
