/* --------------------------Header-------------------------------------
 * File : GraphicsSettingArrowScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:59:32
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:59:32
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GraphicsSettingArrowScript : GUIItemScript
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Transform[] _animations;
    public Transform[] Animations
    {
        get { return _animations; }
        set { _animations = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        InitializeGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnMouseUp()
    {
        foreach (Transform anim in Animations)
        {
            anim.animation[anim.animation.clip.name].time = anim.animation[anim.animation.clip.name].length;
            anim.animation.Play(anim.animation.clip.name);
            anim.animation[anim.animation.clip.name].speed = -1;
        }
        PlayerPrefs.Save();
    }
}
