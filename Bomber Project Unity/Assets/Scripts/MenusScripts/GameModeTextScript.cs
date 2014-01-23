/* --------------------------Header-------------------------------------
 * File : GameModeTextScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:59:46
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:59:46
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GameModeTextScript : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private string[] _gameMode;
    public string[] GameMode
    {
        get { return _gameMode; }
        set { _gameMode = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private int _currentValue = 0;
    public int CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        DatabaseManagerScript databaseScript = (DatabaseManagerScript)Resources.Load("DatabaseManager", typeof(DatabaseManagerScript));
        GameMode = databaseScript.GameModes;
        this.gameObject.GetComponent<TextMesh>().text = GameMode[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Increment()
    {
        if (CurrentValue == GameMode.Length - 1)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue++;
        }
        this.gameObject.GetComponent<TextMesh>().text = GameMode[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Decrement()
    {
        if (CurrentValue == 0)
        {
            CurrentValue = GameMode.Length - 1;
        }
        else
        {
            CurrentValue--;
        }
        this.gameObject.GetComponent<TextMesh>().text = GameMode[CurrentValue];
    }
}
