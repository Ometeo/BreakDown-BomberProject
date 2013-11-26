using UnityEngine;
using System.Collections;

public class PlayerCaseScript : MonoBehaviour {

    [SerializeField]
    private PlayerItemScript _player;
    public PlayerItemScript Player
    {
        get { return _player; }
        set { _player = value; }
    }

    [SerializeField]
    private GameObject _isIA;
    public GameObject IsIA
    {
        get { return _isIA; }
        set { _isIA = value; }
    }

    [SerializeField]
    private GameObject _isLocked;
    public GameObject IsLocked
    {
        get { return _isLocked; }
        set { _isLocked = value; }
    }

    [SerializeField]
    private GameObject _iaButton;
    public GameObject IAButton
    {
        get { return _iaButton; }
        set { _iaButton = value; }
    }

    [SerializeField]
    private GameObject _lockButton;
    public GameObject LockButton
    {
        get { return _lockButton; }
        set { _lockButton = value; }
    }

    public void SetIA()
    {
        IsIA.SetActive(true);
        IsLocked.SetActive(false);
        _lockButton.SetActive(true);
        Player.IsIA = true;
    }

    public void SetLocked()
    {
        IsIA.SetActive(false);
        IsLocked.SetActive(true);
        _iaButton.SetActive(true);
        Player.IsLocked = true;
    }

    public void SetNothing()
    {
        IsIA.SetActive(false);
        IsLocked.SetActive(false);
        _iaButton.SetActive(true);
        _lockButton.SetActive(true);
        Player.IsLocked = false;
        Player.IsIA = false;
    }

    public void OnMouseDown()
    {
        SetNothing();
    }

}
