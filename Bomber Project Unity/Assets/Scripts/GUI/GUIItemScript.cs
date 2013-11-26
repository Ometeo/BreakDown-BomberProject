/* --------------------------Header-------------------------------------
 * File : GUIItemScript.cs
 * Description : Base class to GUI Item, it allows hover effect on GUI and Click on it
 * Version : 1.0.1
 * Created Date : 26/11/2013 08:18:54
 * Created by : Jonathan Bihet
 * Modification Date : 26/11/2013 08:31:37
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

public abstract class GUIItemScript : MonoBehaviour
{

    protected Transform[] _item;

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    protected void InitializeGUI()
    {
        _item = this.gameObject.GetComponentsInChildren<Transform>();
        _item[2].gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract void OnMouseDown();

}
