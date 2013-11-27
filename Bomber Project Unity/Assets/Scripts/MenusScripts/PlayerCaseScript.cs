/* --------------------------Header-------------------------------------
 * File : PlayerCaseScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:58:30
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:58:30
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class PlayerCaseScript : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private PlayerItemScript _player;
    public PlayerItemScript Player
    {
        get { return _player; }
        set { _player = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject _isIA;
    public GameObject IsIA
    {
        get { return _isIA; }
        set { _isIA = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject _isLocked;
    public GameObject IsLocked
    {
        get { return _isLocked; }
        set { _isLocked = value; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject _iaButton;
    public GameObject IAButton
    {
        get { return _iaButton; }
        set { _iaButton = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject _lockButton;
    public GameObject LockButton
    {
        get { return _lockButton; }
        set { _lockButton = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetIA()
    {
        IsIA.SetActive(true);
        IsLocked.SetActive(false);
        _lockButton.SetActive(true);
        Player.IsIA = true;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void SetLocked()
    {
        IsIA.SetActive(false);
        IsLocked.SetActive(true);
        _iaButton.SetActive(true);
        Player.IsLocked = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetNothing()
    {
        IsIA.SetActive(false);
        IsLocked.SetActive(false);
        _iaButton.SetActive(true);
        _lockButton.SetActive(true);
        Player.IsLocked = false;
        Player.IsIA = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnMouseDown()
    {
        SetNothing();
    }

}
