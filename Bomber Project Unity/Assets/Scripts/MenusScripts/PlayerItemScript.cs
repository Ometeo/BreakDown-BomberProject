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
    private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
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

    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {

    }
}
