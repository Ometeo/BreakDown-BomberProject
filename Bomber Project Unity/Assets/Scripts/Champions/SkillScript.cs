using UnityEngine;
using System.Collections;

public abstract class SkillScript : MonoBehaviour {

    [SerializeField]
    private ChampionsStatsScript _champStatsScript;
    public ChampionsStatsScript ChampStatsScript
    {
        get { return _champStatsScript; }
        set { _champStatsScript = value; }
    }

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
        Passive, Skill1, Skill2, Ultimate
    }

    [SerializeField]
    private E_SkillType _skillType;
    public E_SkillType SkillType
    {
        get { return _skillType; }
        set { _skillType = value; }
    }

    [SerializeField]
    private bool _isInstantiater;
    public bool IsInstantiater
    {
        get { return _isInstantiater; }
        set { _isInstantiater = value; }
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

    public class SkillResponse
    {
        private bool _isActivated = false;
        public bool IsActivated
        {
            get { return _isActivated; }
            set { _isActivated = value; }
        }

        private NetworkViewID _viewID;
        public NetworkViewID ViewID
        {
            get { return _viewID; }
            set { _viewID = value; }
        }
    }

    public void UseSkill(NetworkViewID viewID, Transform playerTransform)
    {
        LastUseTime = Time.time;
        Debug.Log(viewID);
        if (viewID == NetworkViewID.unassigned)
            NormalSkill(playerTransform);
        else
            InstantiaterSkill(viewID, playerTransform);
    }

    protected virtual void NormalSkill(Transform playerTransform)
    {}

    protected virtual void InstantiaterSkill(NetworkViewID viewID, Transform playerTransform)
    {}

    protected virtual bool IsSkillUsable(Transform playerTransform)
    {
        Debug.Log("Mère");
        return true;
    }

    public SkillResponse GetSkillResponse(Transform playerTransform)
    {
        SkillResponse sr = new SkillResponse();

        // Check if skill isn't under cooldown
        if (!(TimeBeforeUse() <= .001f))
            return sr;

        // Check if skill is usable
        var tmp = IsSkillUsable(playerTransform);
        Debug.Log(tmp);
        if (!tmp)
            return sr;

        if (IsInstantiater)
            sr.ViewID = Network.AllocateViewID();
        sr.IsActivated = true;
        return sr;
    }

    void Start()
    {
        ChampionsStatsScript champStatsScript = GetComponent<ChampionsStatsScript>();
        if (champStatsScript != null)
        {
            if (SkillType == E_SkillType.Passive)
                Cooldown = champStatsScript.PassiveCooldown;
            else if (SkillType == E_SkillType.Skill1)
                Cooldown = champStatsScript.Skill1Cooldown;
            else if (SkillType == E_SkillType.Skill2)
                Cooldown = champStatsScript.Skill2Cooldown;
            else if (SkillType == E_SkillType.Ultimate)
                Cooldown = champStatsScript.SkillUltimateCooldown;
        }
    }
}
