using UnityEngine;
using System.Collections;

public abstract class SkillScript : MonoBehaviour {

    [SerializeField]
    private Texture _icon;
    public Texture Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    [SerializeField]
    private float _cooldown;
    public float Cooldown
    {
        get { return _cooldown; }
        set { _cooldown = value; }
    }


    /// <summary>
    /// Skill type 1, 2 or ultimate
    /// </summary>
    public enum E_SkillType
    {
        Skill1, Skill2, Ultimate
    }

    [SerializeField]
    private E_SkillType _skillType;
    public E_SkillType SkillType
    {
        get { return _skillType; }
        set { _skillType = value; }
    }

    private float _lastUseTime = 0f;
    public float LastUseTime
    {
        get { return _lastUseTime; }
        set { _lastUseTime = value; }
    }

    public bool IsSkillReady()
    {
        if (Time.time - (LastUseTime + Cooldown) > 0)
            return true;
        return false;
    }

    public bool IsSkillActivated()
    {
        if (IsSkillReady())
        {
            LastUseTime = Time.time;
            return true;
        }
        return false;
    }

    abstract public void useSkill(Transform playerTransform);
}
