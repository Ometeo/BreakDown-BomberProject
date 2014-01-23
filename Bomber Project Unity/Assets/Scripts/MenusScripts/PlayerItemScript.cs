/* --------------------------Header-------------------------------------
 * File : PlayerItemScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:57:38
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:57:38
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class PlayerItemScript : MonoBehaviour
{
    [SerializeField]
    private NetworkMenuManagerScript _netwkMenuMngScr;
    public NetworkMenuManagerScript NetwkMenuMngScr
    {
        get { return _netwkMenuMngScr; }
        set { _netwkMenuMngScr = value; }
    }

    [SerializeField]
    private string defaultText;
    public string DefaultText
    {
        get { return defaultText; }
        set { defaultText = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool _isIA;
    public bool IsIA
    {
        get { return _isIA; }
        set { _isIA = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool _isLocked;
    public bool IsLocked
    {
        get { return _isLocked; }
        set { _isLocked = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private int _playerNb;
    public int PlayerNb
    {
        get { return _playerNb; }
        set { _playerNb = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private TextMesh _nameText;
    public TextMesh NameText
    {
        get { return _nameText; }
        set { _nameText = value; }
    }


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        NameText.text = DefaultText;
        GetComponentInChildren<PlaySelectPlayerItemScript>().PlayerNb = PlayerNb;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {

    }
}
