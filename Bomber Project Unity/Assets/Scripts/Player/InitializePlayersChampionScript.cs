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

    // ViewID
    private NetworkViewID _nwViewID;
    public NetworkViewID NwViewID
    {
        get { return _nwViewID; }
        set { _nwViewID = value; }
    }

    private Transform[] _championList;
    public Transform[] ChampionList
    {
        get { return _championList; }
        set { _championList = value; }
    }

    /// <summary>
    /// Cache Transform
    /// </summary>
    private Transform _transform;

    void Awake()
    {
        _transform = transform;
        DatabaseManagerScript databaseScript = (DatabaseManagerScript)Resources.Load("DatabaseManager", typeof(DatabaseManagerScript));
        ChampionList = databaseScript.Champions;
    }

    void Start()
    {

    }

    public void SetChampion()
    {
        this.gameObject.SetActive(true);
        Transform champion = (Transform)Instantiate(ChampionList[ChampID], _transform.position, _transform.rotation);
        champion.parent = transform;
        champion.networkView.viewID = NwViewID;
        _transform.GetComponent<PlayerInputManagerScript>().Champion = champion;

        InitializeChampion(champion.GetComponent<ChampionsStatsScript>().SkinColor);
    }

    public void InitializeChampion(Color skinColor)
    {
        BodyMeshRenderer.material.color = skinColor;
        HeadMeshRenderer.material.color = skinColor;
    }
}
