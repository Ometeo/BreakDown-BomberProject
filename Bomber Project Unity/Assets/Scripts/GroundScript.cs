using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

    [SerializeField]
    private bool isSpawn;
    public bool IsSpawn
    {
        get
        {
            return isSpawn;
        }
        set
        {
            isSpawn = value;
        }
    }
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
