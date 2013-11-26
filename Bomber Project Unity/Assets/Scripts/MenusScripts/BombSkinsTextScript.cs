using UnityEngine;
using System.Collections;

public class BombSkinsTextScript : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private string[] _bombSkins;
    public string[] BombSkins
    {
        get { return _bombSkins; }
        set { _bombSkins = value; }
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
        this.gameObject.GetComponent<TextMesh>().text = BombSkins[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Increment()
    {
        if (CurrentValue == BombSkins.Length - 1)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue++;
        }
        this.gameObject.GetComponent<TextMesh>().text = BombSkins[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Decrement()
    {
        if (CurrentValue == 0)
        {
            CurrentValue = BombSkins.Length - 1;
        }
        else
        {
            CurrentValue--;
        }
        this.gameObject.GetComponent<TextMesh>().text = BombSkins[CurrentValue];
    }
}
