using UnityEngine;
using System.Collections;
using System;

public class ChampionsStatsScript : MonoBehaviour {
   
    [SerializeField]
    private int _lifePoint; // Default 1 LP
    public int LifePoint
    {
        get { return _lifePoint; }
        set
        {
            _lifePoint = value;
            CheckDeath();
        }
    }

    [SerializeField]
    private int _movementSpeed; // Default 100
    public int MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    [SerializeField]
    private int _nbMaxBomb; // Default 4
    public int NbMaxBomb
    {
        get { return _nbMaxBomb; }
        set { _nbMaxBomb = value; }
    }

    [SerializeField]
    private float _bombDelay; // Default 3 sec
    public float BombDelay
    {
        get { return _bombDelay; }
        set { _bombDelay = value; }
    }

    [SerializeField]
    private float _respawnFactor; // Default RespawnFactor 1
    public float RespawnFactor
    {
        get { return _respawnFactor; }
        set { _respawnFactor = value; }
    }

    [SerializeField]
    private Color _skinColor;
    public Color SkinColor
    {
        get { return _skinColor; }
        set { _skinColor = value; }
    }

    [SerializeField]
    private Texture _iconSkill1;
    public Texture IconSkill1
    {
        get { return _iconSkill1; }
        set { _iconSkill1 = value; }
    }

    [SerializeField]
    private float _skill1Cooldown;
    public float Skill1Cooldown
    {
        get { return _skill1Cooldown; }
        set { _skill1Cooldown = value; }
    }

    [SerializeField]
    private Texture _iconSkill2;
    public Texture IconSkill2
    {
        get { return _iconSkill2; }
        set { _iconSkill2 = value; }
    }

    [SerializeField]
    private float _skill2Cooldown;
    public float Skill2Cooldown
    {
        get { return _skill2Cooldown; }
        set { _skill2Cooldown = value; }
    }

    [SerializeField]
    private Texture _iconSkillUltimate;
    public Texture IconSkillUltimate
    {
        get { return _iconSkillUltimate; }
        set { _iconSkillUltimate = value; }
    }

    [SerializeField]
    private float _skillUltimateCooldown;
    public float SkillUltimateCooldown
    {
        get { return _skillUltimateCooldown; }
        set { _skillUltimateCooldown = value; }
    }

    public void CheckDeath()
    {
        if (LifePoint <= 0 && Network.isServer)
        {
            networkView.RPC("Die", RPCMode.All);
        }
    }

    [RPC]
    private void Die()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    void Start()
    {
        GameObject interfacePlayer = GameObject.FindGameObjectWithTag("Interface");
        InterfaceScript interfScript = interfacePlayer.GetComponent<InterfaceScript>();
        bool skill1InitDone = false;
        bool skill2InitDone = false;
        bool skillUltimateInitDone = false;

        foreach (var skScript in this.GetComponents<SkillScript>())
        {
            if (skScript.SkillType == SkillScript.E_SkillType.Skill1)
            {
                skScript.Cooldown = Skill1Cooldown;
                if (!skill1InitDone)
                {
                    interfScript.Skill1.InitializeSkill(IconSkill1, skScript);
                }
                skill1InitDone = true;
            } else if (skScript.SkillType == SkillScript.E_SkillType.Skill2)
            {
                skScript.Cooldown = Skill2Cooldown;
                if (!skill2InitDone)
                {
                    interfScript.Skill2.InitializeSkill(IconSkill2, skScript);
                }
                skill2InitDone = true;
            }
            else
            {
                skScript.Cooldown = SkillUltimateCooldown;
                if (!skillUltimateInitDone)
                {
                    interfScript.SkillUltimate.InitializeSkill(IconSkillUltimate, skScript);
                }
                skillUltimateInitDone = true;
            }
        }
    }
}
