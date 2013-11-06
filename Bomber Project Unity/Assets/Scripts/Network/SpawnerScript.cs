using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;
    public Transform[] SpawnPoints
    {
        get { return _spawnPoints; }
        set { _spawnPoints = value; }
    }
}
