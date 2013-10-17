using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class ChampionMovementAuthorativeScript : MonoBehaviour {

    private ChampionsStatsScript _championStats;
    public ChampionsStatsScript ChampionStats
    {
        get { return _championStats; }
        set { _championStats = value; }
    }

    private Rigidbody _rigidBody;    
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

    void Start()
    {
        ChampionStats = GetComponentInChildren<ChampionsStatsScript>();
    }

    void FixedUpdate()
    {
        if (Network.isServer)
        {
            if (_rigidBody != null)
                rigidbody.velocity = (ChampionStats.MovementSpeed * _serverCurrentDirection * Time.deltaTime);
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
}
