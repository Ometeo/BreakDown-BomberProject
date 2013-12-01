/* --------------------------Header-------------------------------------
 * File : CameraViewScript.cs
 * Description : Scrpt that enables to move the camera with mouse.
 * Version : 1.0.0
 * Created Date : 05/11/2013 18:08:38
 * Created by : Jonathan Bihet
 * Modification Date : 01/12/2013 19:43:55
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */


using UnityEngine;
using System.Collections;

/// <summary>
/// Class to move camera with mouse.
/// </summary>
public class CameraViewScript : MonoBehaviour
{
    /// <summary>
    /// Camera Speed = the speed the camera moves.
    /// </summary>
    [SerializeField]
    private float _cameraSpeed;
    public float CameraSpeed
    {
        get { return _cameraSpeed; }
        set { _cameraSpeed = value; }
    }

    /// <summary>
    /// The key maped to center the camera on player.
    /// </summary>
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

    /// <summary>
    /// Enable moving if one player or more is connected.
    /// </summary>
    void Start()
    {
        if (Network.connections.Length == 0)
            EnableMoving(false);
        else
            EnableMoving(true);
    }

    /// <summary>
    /// Move The camera accordingly to the mouse moves.
    /// </summary>
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

    /// <summary>
    /// Detect if the "camera" enters on the border of the arena.
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Detect if the "camera" exits the border of the arena.
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Enable movement or not.
    /// </summary>
    /// <param name="value">can move or not.</param>
    void EnableMoving(bool value)
    {
        _canMoveLeft = value;
        _canMoveRight = value;
        _canMoveFront = value;
        _canMoveBack = value;
    }

    /// <summary>
    /// disable movement on server initialisation.
    /// </summary>
    void OnServerInitialized()
    {
        EnableMoving(false);
    }

    /// <summary>
    /// Enable movement when a player is connected.
    /// </summary>
    /// <param name="player"></param>
    void OnPlayerConnected(NetworkPlayer player)
    {
        EnableMoving(true);
    }

    /// <summary>
    /// Enable movement when a player is connected.
    /// </summary>
    void OnConnectedToServer()
    {
        EnableMoving(true);
    }

    /// <summary>
    /// Method to center the camera on the player.
    /// </summary>
    /// <param name="player"></param>
    void CenterOnPlayer(Transform player)
    {
        this.transform.position = player.position;
    }

}
