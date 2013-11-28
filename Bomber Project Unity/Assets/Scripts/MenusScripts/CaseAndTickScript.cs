/* --------------------------Header-------------------------------------
 * File : CaseAndTickScript.cs
 * Description : 
 * Version : 1.0.1
 * Created Date : 26/11/2013 08:23:57
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 18:00:46
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class CaseAndTickScript : GUIItemScript
{
    /// <summary>
    /// 
    /// </summary>
    private enum _caseMaterialEnum { Case = 0, CaseGlow, CaseTick, CaseTickGlow };

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool _checked;
    public bool Checked
    {
        get { return _checked; }
        set { _checked = value; }
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseUp()
    {
        ClickOnCaseAction();
    }

    /// <summary>
    /// 
    /// </summary>
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
