/* --------------------------Header-------------------------------------
 * File : CaseAndTickScript.cs
 * Description : 
 * Version : 1.0.1
 * Created Date : 26/11/2013 08:23:57
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:36:27
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

public class CaseAndTickScript : GUIItemScript
{
    private enum _caseMaterialEnum { Case = 0, CaseGlow, CaseTick, CaseTickGlow };

    [SerializeField]
    private bool _checked;
    public bool Checked
    {
        get { return _checked; }
        set { _checked = value; }
    }

    [SerializeField]
    private Material[] _caseMaterial;
    public Material[] CaseMaterial
    {
        get { return _caseMaterial; }
        set { _caseMaterial = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        InitializeGUI();

        if (!Checked)
        {

            _item[1].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.Case];
            _item[2].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.CaseGlow];
        }
        else
        {
            _item[1].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.CaseTick];
            _item[2].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.CaseTickGlow];
        }
    }

    public override void OnMouseDown()
    {
        ClickOnCaseAction();
    }

    void OnMouseEnter()
    {
        _item[1].gameObject.SetActive(false);
        _item[2].gameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseExit()
    {
        _item[1].gameObject.SetActive(true);
        _item[2].gameObject.SetActive(false);
    }

    public void ClickOnCaseAction()
    {
        if (Checked)
        {
            Checked = false;
            _item[1].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.Case];
            _item[2].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.CaseGlow];
        }
        else
        {
            Checked = true;
            _item[1].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.CaseTick];
            _item[2].gameObject.renderer.material = CaseMaterial[(int)_caseMaterialEnum.CaseTickGlow];
        }
    }
}
