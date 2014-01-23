using UnityEngine;
using System.Collections;

public class InitTextValueScript : MonoBehaviour {

    [SerializeField]
    private TextMesh _txtMesh;
    public TextMesh TxtMesh
    {
        get { return _txtMesh; }
        set { _txtMesh = value; }
    }

    private enum TextType
    {
        GameMode, Arena
    }

    [SerializeField]
    private TextType _txtType;
    private TextType TxtType
    {
        get { return _txtType; }
        set { _txtType = value; }
    }

	void Start () {
        DatabaseManagerScript databaseScript = (DatabaseManagerScript)Resources.Load("DatabaseManager", typeof(DatabaseManagerScript));

        switch (TxtType)
        {
            case TextType.Arena:
                TxtMesh.text = databaseScript.Arenas[GameOptionSingleton.Instance.NumScene];
                break;
            case TextType.GameMode:
                TxtMesh.text = databaseScript.GameModes[GameOptionSingleton.Instance.NumMode];
                break;
        }
	}
}
