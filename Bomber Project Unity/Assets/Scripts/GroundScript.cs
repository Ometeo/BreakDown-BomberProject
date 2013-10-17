using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour
{

    private int _spawnMax = 10;
    private int _spawnMin = 0;

    public Texture[] SpawnTextures;

    [SerializeField]
    private bool _isSpawn;
    public bool IsSpawn
    {
        get
        {
            return _isSpawn;
        }
        set
        {
            _isSpawn = value;
        }
    }

    [SerializeField]
    private int _spawnValue;
    public int SpawnValue
    {
        get
        {
            return _spawnValue;
        }
        set
        {
            if (_spawnValue >= _spawnMin && _spawnValue <= _spawnMax)
                _spawnValue = value;
        }
    }





    void Start()
    {
        if(IsSpawn)
            this.gameObject.renderer.material.SetTexture("_MainTex", SpawnTextures[SpawnValue]);
    }


    void Update()
    {

    }
}
