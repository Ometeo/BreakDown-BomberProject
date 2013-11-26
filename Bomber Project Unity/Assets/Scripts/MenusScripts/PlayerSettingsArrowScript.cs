using UnityEngine;
using System.Collections;

public class PlayerSettingsArrowScript : GUIItemScript
{
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
