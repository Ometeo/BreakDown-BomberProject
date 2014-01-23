using UnityEngine;
using System.Collections;

public class DatabaseManagerScript : ScriptableObject {

    [SerializeField]
    private Transform[] _champions;
    public Transform[] Champions
    {
        get { return _champions; }
        set { _champions = value; }
    }

    [SerializeField]
    private string[] _arenas;
    public string[] Arenas
    {
        get { return _arenas; }
        set { _arenas = value; }
    }

    [SerializeField]
    private string[] _gameModes;
    public string[] GameModes
    {
        get { return _gameModes; }
        set { _gameModes = value; }
    }
}
