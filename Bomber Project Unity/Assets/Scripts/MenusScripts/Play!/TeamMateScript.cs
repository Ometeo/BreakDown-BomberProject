using UnityEngine;
using System.Collections;

public class TeamMateScript : MonoBehaviour {

    [SerializeField]
    private TextMesh _champTextMesh;
    public TextMesh ChampTextMesh
    {
        get { return _champTextMesh; }
        set { _champTextMesh = value; }
    }

    void Start()
    {
        if (GameOptionSingleton.Instance.NbTeams == 0)
            this.gameObject.SetActive(false);
    }
}
