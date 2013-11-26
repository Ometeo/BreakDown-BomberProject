using UnityEngine;
using System.Collections;

public class ChampionSkinsTextScript : MonoBehaviour {
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private string[] _championSkins;
    public string[] ChampionSkins
    {
        get { return _championSkins; }
        set { _championSkins = value; }
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
        this.gameObject.GetComponent<TextMesh>().text = ChampionSkins[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Increment()
    {
        if (CurrentValue == ChampionSkins.Length - 1)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue++;
        }
        this.gameObject.GetComponent<TextMesh>().text = ChampionSkins[CurrentValue];
    }

    /// <summary>
    /// 
    /// </summary>
    public void Decrement()
    {
        if (CurrentValue == 0)
        {
            CurrentValue = ChampionSkins.Length - 1;
        }
        else
        {
            CurrentValue--;
        }
        this.gameObject.GetComponent<TextMesh>().text = ChampionSkins[CurrentValue];
    }
}
