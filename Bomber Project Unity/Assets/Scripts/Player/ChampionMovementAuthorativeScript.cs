using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class ChampionMovementAuthorativeScript : MonoBehaviour {

    private ChampionsStatsScript _championStats;
    public ChampionsStatsScript ChampionStats
    {
        get {
            if (_championStats != null)
                return _championStats;
            else
            {
                _championStats = GetComponentInChildren<ChampionsStatsScript>();
                return _championStats;
            }
        }
        set { _championStats = value; }
    }

    private Rigidbody _rigidBody;    
    private Vector3 _serverCurrentDirection = Vector3.zero;
    private Vector3 _lastPosition;

    private Transform _transform;

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
        _transform = this.transform;
        if (Network.isClient)
        {
            enabled = false;
            if (_rigidBody != null)
                Destroy(_rigidBody);
        }
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Network.isServer)
        {
            if (_rigidBody != null)
            {
                rigidbody.velocity = (ChampionStats.MovementSpeed * _serverCurrentDirection * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, _lastPosition) > MinimumMovementToUpdatePos)
            {
                if (_serverCurrentDirection != Vector3.zero)
                    _transform.rotation = Quaternion.LookRotation(_serverCurrentDirection);
                _lastPosition = _transform.position;
                networkView.RPC("SetTransform", RPCMode.Others, transform.position, transform.rotation);
            }
        }
    }

    [RPC]
    void SetTransform(Vector3 newPosition, Quaternion newRotation)
    {
        transform.position = newPosition;
        transform.rotation = newRotation;
    }

    [RPC]
    void SendMovementDirection(Vector3 direction)
    {
        _serverCurrentDirection = direction;
    }
}
