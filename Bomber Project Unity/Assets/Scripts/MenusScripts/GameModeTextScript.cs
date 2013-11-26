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
