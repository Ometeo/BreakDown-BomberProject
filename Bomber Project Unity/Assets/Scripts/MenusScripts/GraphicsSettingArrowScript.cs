using UnityEngine;
using System.Collections;

public class GraphicsSettingArrowScript : GUIItemScript
{
    [SerializeField]
    private Transform[] _animations;
    public Transform[] Animations
    {
        get { return _animations; }
        set { _animations = value; }
    }


    void Start()
    {
        InitializeGUI();
    }

    public override void OnMouseDown()
    {
        foreach (Transform anim in Animations)
        {
            anim.animation[anim.animation.clip.name].time = anim.animation[anim.animation.clip.name].length;
            anim.animation.Play(anim.animation.clip.name);
            anim.animation[anim.animation.clip.name].speed = -1;
        }
        
    }
}
