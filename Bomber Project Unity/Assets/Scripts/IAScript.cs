/* --------------------------Header-------------------------------------
 * File : IAScript.cs
 * Description : Script that handles IA champions
 * Version : 1.0.0
 * Created Date : 01/12/2013 15:12:35
 * Created by : Jonathan Bihet
 * Modification Date : 01/12/2013 18:27:48
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

public class IAScript : MonoBehaviour
{
    /// <summary>
    /// The directions where the bot can go.
    /// </summary>
    enum Directions { None = 0, Forward, Right, Back, Left };

    /// <summary>
    /// Is the player is Artificial.
    /// </summary>
    [SerializeField]
    private bool _isIA;
    public bool IsIA
    {
        get { return _isIA; }
        set { _isIA = value; }
    }


    private ChampionMovementAuthorativeScript _movementHandler;

    private PlayerInputManagerScript _inputScript;

    private int _direction;
    private float _delay;

    /// <summary>
    /// get the different script needed.
    /// </summary>
    void Awake()
    {
        _movementHandler = GetComponent<ChampionMovementAuthorativeScript>();
        _inputScript = GetComponent<PlayerInputManagerScript>();
    }

    /// <summary>
    /// Set the direction to 0.
    /// </summary>
    void Start()
    {
        _direction = 0;
    }

    /// <summary>
    /// If on server and is IA, move the player.
    /// </summary>
    void FixedUpdate()
    {
        if (Network.isServer && IsIA)
        {
            _delay += Time.deltaTime;
            if (_delay > 1.0f) //Every seconds
            {
                ChooseDirection(); //do we change direction?
                UseBomb(); //do we use bomb?
                _delay = 0.0f;
            }
            GoToDirection(); // move.
        }
    }

    /// <summary>
    /// Select the direction we want to go.
    /// </summary>
    void ChooseDirection()
    {
        int changeDirection = Random.Range(0, 1); 
        if (changeDirection == 0) // Avoid changing direction each second.
        {
            _direction = Random.Range(0, 5); 
        }
    }

    /// <summary>
    /// Select if we use bomb or not.
    /// </summary>
    void UseBomb()
    {
        int useBomb = Random.Range(0, 1);
        if (useBomb == 0)
            _inputScript.SendUseBomb();
    }

    /// <summary>
    /// Move in the selected direction.
    /// </summary>
    void GoToDirection()
    {
        switch (_direction)
        {
            case (int)Directions.None:
                _movementHandler.ServerCurrentDirection = new Vector3(0, 0, 0);
                break;
            case (int)Directions.Forward:
                _movementHandler.ServerCurrentDirection = new Vector3(0, 0, 1);
                break;
            case (int)Directions.Right:
                _movementHandler.ServerCurrentDirection = new Vector3(1, 0, 0);
                break;
            case (int)Directions.Back:
                _movementHandler.ServerCurrentDirection = new Vector3(0, 0, -1);
                break;
            case (int)Directions.Left:
                _movementHandler.ServerCurrentDirection = new Vector3(-1, 0, 0);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Use to change direction in case of collision.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        _direction++;
        if (_direction > 4)
        {
            _direction = 0;
            _delay = 0.0f;
        }
    }
}
