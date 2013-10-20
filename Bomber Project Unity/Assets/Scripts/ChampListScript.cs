using UnityEngine;
using System.Collections;

public class ChampListScript : MonoBehaviour {
    [SerializeField]
    private Transform[] _availableChampions;
    public Transform[] AvailableChampions
    {
        get { return _availableChampions; }
        set { _availableChampions = value; }
    }
}
