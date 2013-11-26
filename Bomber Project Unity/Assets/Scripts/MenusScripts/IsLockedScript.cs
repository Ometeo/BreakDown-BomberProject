using UnityEngine;
using System.Collections;

public class IsLockedScript : GUIItemScript
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
        Case.GetComponent<PlayerCaseScript>().SetLocked();
        this.gameObject.SetActive(false);
    }
}
