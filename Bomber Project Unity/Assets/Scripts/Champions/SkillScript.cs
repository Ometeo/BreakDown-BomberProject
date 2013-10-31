using UnityEngine;
using System.Collections;

public abstract class SkillScript : MonoBehaviour {

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

    private float _lastUseTime = -999999f;
    public float LastUseTime
    {
        get { return _lastUseTime; }
        set { _lastUseTime = value; }
    }

    public float TimeBeforeUse()
    {
        float timeBeforeUse = (LastUseTime + Cooldown) - Time.time;
        if (timeBeforeUse < 0)
            return 0;
        return timeBeforeUse;
    }

    public bool IsSkillActivated()
    {
        if (TimeBeforeUse()  == 0)
        {
            LastUseTime = Time.time;
            return true;
        }
        return false;
    }

    abstract public bool useSkill(Transform playerTransform);
}
