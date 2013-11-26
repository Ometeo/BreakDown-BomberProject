using UnityEngine;
using System.Collections;

public class PlayerItemScript : MonoBehaviour {

	[SerializeField]
    private bool _isIA;
	public bool IsIA
	{
		get { return _isIA;}
		set { _isIA = value;}
	}

    [SerializeField]
    private bool _isLocked;
    public bool IsLocked
    {
        get { return _isLocked; }
        set { _isLocked = value; }
    }
    
    [SerializeField]
    private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }
    
    [SerializeField]
    private TextMesh _nameText;
    public TextMesh NameText
    {
        get { return _nameText; }
        set { _nameText = value; }
    }
    
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
