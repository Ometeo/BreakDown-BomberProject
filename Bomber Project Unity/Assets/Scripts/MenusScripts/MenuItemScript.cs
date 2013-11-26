/* --------------------------Header-------------------------------------
 * File : MenuItemScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 26/11/2013 08:21:14
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:21:14
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class MenuItemScript : GUIItemScript
{
    private string _itemName;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Transform _titleEffectObject;
    public Transform TitleEffectObject
    {
        get { return _titleEffectObject; }
        set { _titleEffectObject = value; }
    }


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _itemName = this.transform.name;
        InitializeGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseDown()
    {
        if (_itemName == "Quit")
            Application.Quit();
        else if (_itemName == "Title")
        {
            StartCoroutine(TitleEffect());
        }
        else
        {
            Application.LoadLevel(_itemName);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator TitleEffect()
    {
        TitleEffectObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        TitleEffectObject.gameObject.SetActive(false);
    }
}
