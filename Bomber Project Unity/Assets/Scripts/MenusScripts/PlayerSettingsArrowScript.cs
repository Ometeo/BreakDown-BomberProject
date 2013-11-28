/* --------------------------Header-------------------------------------
 * File : PlayerSettingsArrowScript.cs
 * Description : 
 * Version : 1.0.0
 * Created Date : 27/11/2013 17:57:07
 * Created by : Jonathan Bihet
 * Modification Date : 27/11/2013 17:57:07
 * Modified by : Jonathan Bihet
 * ------------------------------------------------------------------------ */

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class PlayerSettingsArrowScript : GUIItemScript
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
    public override void OnMouseDown()
    {
        foreach (Transform anim in Animations)
        {
            anim.animation[anim.animation.clip.name].time = 0;
            anim.animation.Play(anim.animation.clip.name);
            anim.animation[anim.animation.clip.name].speed = 1;
        }
    }



}
