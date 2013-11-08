/* --------------------------Header-------------------------------------
 * File : CameraViewScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 05/11/2013 18:08:38
 * Created by : Jonathan Bihet
 * Modification Date : 06/11/2013 10:51:27
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */


using UnityEngine;
using System.Collections;

public class CameraViewScript : MonoBehaviour
{

    [SerializeField]
    private float _cameraSpeed;
    public float CameraSpeed
    {
        get { return _cameraSpeed; }
        set { _cameraSpeed = value; }
    }

    [SerializeField]
    private Transform[] _players;
    public Transform[] Players
    {
        get { return _players; }
        set { _players = value; }
    }

    [SerializeField]
    private Transform _currentPlayer;

    [SerializeField]
    private KeyCode _centerOnPlayerKey;
    public KeyCode CenterOnPlayerKey
    {
        get { return _centerOnPlayerKey; }
        set { _centerOnPlayerKey = value; }
    }

    private bool _canMoveLeft = false;
    private bool _canMoveRight = false;
    private bool _canMoveFront = false;
    private bool _canMoveBack = false;

    private float _mouseBorderDetect = 30.0f;
    private Vector3 _cameraDirection;

    void Start()
    {
        if (Network.connections.Length == 0)
            EnableMoving(false);
        else
            EnableMoving(true);
    }

    void Update()
    {
        _cameraDirection = Vector3.zero;

        if (Input.mousePosition.x >= Screen.width - _mouseBorderDetect)
        {
            if (_canMoveRight)
                _cameraDirection.x = 1.0f;
        }
        if (Input.mousePosition.x <= _mouseBorderDetect)
        {
            if (_canMoveLeft)
                _cameraDirection.x = -1.0f;
        }

        if (Input.mousePosition.y >= Screen.height - _mouseBorderDetect)
        {
            if (_canMoveFront)
                _cameraDirection.z = 1.0f;
        }
        if (Input.mousePosition.y <= _mouseBorderDetect)
        {
            if (_canMoveBack)
                _cameraDirection.z = -1.0f;
        }

        transform.Translate(_cameraDirection.normalized * CameraSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals("LeftBorder"))
        {
            _canMoveLeft = false;
        }
        if (other.transform.name.Equals("RightBorder"))
        {
            _canMoveRight = false;
        }
        if (other.transform.name.Equals("FrontBorder"))
        {
            _canMoveFront = false;
        }
        if (other.transform.name.Equals("BackBorder"))
        {
            _canMoveBack = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.name.Equals("LeftBorder"))
        {
            _canMoveLeft = true;
        }
        if (other.transform.name.Equals("RightBorder"))
        {
            _canMoveRight = true;
        }
        if (other.transform.name.Equals("FrontBorder"))
        {
            _canMoveFront = true;
        }
        if (other.transform.name.Equals("BackBorder"))
        {
            _canMoveBack = true;
        }
    }

    void EnableMoving(bool value)
    {
        _canMoveLeft = value;
        _canMoveRight = value;
        _canMoveFront = value;
        _canMoveBack = value;
    }

    void OnServerInitialized()
    {
        EnableMoving(false);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        EnableMoving(true);
    }

    void OnConnectedToServer()
    {
        EnableMoving(true);
    }
}
