using UnityEngine;
using System.Collections;

public class InitializePlayersChampionScript : MonoBehaviour {

    [SerializeField]
    private MeshRenderer _bodyMeshRenderer;
    public MeshRenderer BodyMeshRenderer
    {
        get { return _bodyMeshRenderer; }
        set { _bodyMeshRenderer = value; }
    }

    [SerializeField]
    private MeshRenderer _headMeshRenderer;
    public MeshRenderer HeadMeshRenderer
    {
        get { return _headMeshRenderer; }
        set { _headMeshRenderer = value; }
    }

    // The ID of the future Champion
    private int _champID;
    public int ChampID
    {
        get { return _champID; }
        set { _champID = value; }
    }

    /// <summary>
    /// Cache Transform
    /// </summary>
    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        SetChampion();
    }

    public void SetChampion()
    {
        Transform champion = (Transform)Instantiate(ServerInitializePlayersManagerScript.ChampDbScript.ChampionList[ChampID], _transform.position, _transform.rotation);
        champion.parent = transform;
        _transform.GetComponent<PlayerInputManagerScript>().Champion = champion;

        InitializeChampion(champion.GetComponent<ChampionsStatsScript>().SkinColor);
    }

    public void InitializeChampion(Color skinColor)
    {
        BodyMeshRenderer.material.color = skinColor;
        HeadMeshRenderer.material.color = skinColor;
    }
}
