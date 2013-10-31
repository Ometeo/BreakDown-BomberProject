using UnityEngine;
using System.Collections;

public class ChampionsDatabaseScript : MonoBehaviour {

    [SerializeField]
    private Transform[] _championList;
    public Transform[] ChampionList
    {
        get { return _championList; }
        set { _championList = value; }
    }
}
