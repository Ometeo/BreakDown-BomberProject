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
        if (Network.isServer)
        {
            if (TimeBeforeUse() <= .001f)
            {
                LastUseTime = Time.time;
                return true;
            }
            return false;
        }
        else
        {
            LastUseTime = Time.time;
            return true;
        }
        
    }

    abstract public bool useSkill(Transform playerTransform);

    void Start()
    {
        ChampionsStatsScript champStatsScript = GetComponent<ChampionsStatsScript>();
        if (champStatsScript != null)
        {
            if (SkillType == E_SkillType.Skill1)
                Cooldown = champStatsScript.Skill1Cooldown;
            else if (SkillType == E_SkillType.Skill2)
                Cooldown = champStatsScript.Skill2Cooldown;
            else if (SkillType == E_SkillType.Ultimate)
                Cooldown = champStatsScript.SkillUltimateCooldown;
        }
    }
}
