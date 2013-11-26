using UnityEngine;
using System.Collections;

public class IsIAScript : GUIItemScript
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Transform _case;
    public Transform Case
    {
        get { return _case; }
        set { _case = value; }
    }


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        InitializeGUI();
    }

    public override void OnMouseDown()
    {
        Case.GetComponent<PlayerCaseScript>().SetIA();
        this.gameObject.SetActive(false);
    }
}
