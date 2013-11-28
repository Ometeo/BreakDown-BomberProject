using UnityEngine;
using System.Collections;
using System;

public class ChampionsStatsScript : MonoBehaviour {

    /// <summary>
    /// The parent player object
    /// </summary>
    private Transform _player;
    public Transform Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private int _lifePoint;
    public int LifePoint
    {
        get { return _lifePoint; }
        set
        {
            _lifePoint = value;
            if (InterfaceScpt != null)
                InterfaceScpt.LifePoints.text = _lifePoint.ToString();
            CheckDeath();
        }
    }

    [SerializeField]
    private int _defaultLifePoint; // Default 1 LP
    public int DefaultLifePoint
    {
        get { return _defaultLifePoint; }
        set { _defaultLifePoint = value; }
    }

    private float _movementSpeed; 
    public float MovementSpeed
    {
        get { return _movementSpeed; }
        set
        {
            _movementSpeed = value;
            if (InterfaceScpt != null)
                InterfaceScpt.MovementSpeed.text = _movementSpeed.ToString();
        }
    }

    [SerializeField]
    private float _defaultMovementSpeed; // Default 100
    public float DefaultMovementSpeed
    {
        get { return _defaultMovementSpeed; }
        set { _defaultMovementSpeed = value; }
    }

    [SerializeField]
    private int _nbMaxBombs; // Default 4
    public int NbMaxBomb
    {
        get { return _nbMaxBombs; }
        set { _nbMaxBombs = value; }
    }

    private int _nbBombs;
    public int NbBombs
    {
        get { return _nbBombs; }
        set
        {
            _nbBombs = value;
            if (InterfaceScpt != null)
                InterfaceScpt.BombsAvailable.text = _nbBombs.ToString();
        }
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
    private Texture _iconPassive;
    public Texture IconPassive
    {
        get { return _iconPassive; }
        set { _iconPassive = value; }
    }

    [SerializeField]
    private float _passiveCooldown;
    public float PassiveCooldown
    {
        get { return _passiveCooldown; }
        set { _passiveCooldown = value; }
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

    [SerializeField]
    private InterfaceScript _interfaceScpt;
    public InterfaceScript InterfaceScpt
    {
        get { return _interfaceScpt; }
        set { _interfaceScpt = value; }
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
        Player.gameObject.SetActive(false);
    }

    void Start()
    {
        Player = this.transform.parent;

        if (Player.GetComponent<PlayerInputManagerScript>().enabled)
        {
            InitializeInterface();    
        }
        MovementSpeed = DefaultMovementSpeed;
        LifePoint = DefaultLifePoint;
        NbBombs = NbMaxBomb;

        networkView.RPC("AskSynchronizeStats", RPCMode.Server, Network.player);
    }

    /// <summary>
    /// Order synchronization with server
    /// </summary>
    /// <param name="player"></param>
    [RPC]
    void AskSynchronizeStats(NetworkPlayer player)
    {
        float tbfPassive = -999999f, tbfSkill1 = -999999f, tbfSkill2 = -999999f, tbfUltimate = -999999f;
        foreach (var skScript in this.GetComponents<SkillScript>())
        {
            if (skScript.SkillType == SkillScript.E_SkillType.Passive)
            {
                tbfPassive = skScript.TimeBeforeUse();
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Skill1)
            {
                tbfSkill1 = skScript.TimeBeforeUse();
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Skill2)
            {
                tbfSkill2 = skScript.TimeBeforeUse();
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Ultimate)
            {
                tbfUltimate = skScript.TimeBeforeUse();
            }
        }
        networkView.RPC("ResponseSynchronizeStats", player, tbfPassive, tbfSkill1, tbfSkill2, tbfUltimate, LifePoint, NbBombs, MovementSpeed, RespawnFactor);
    }

    [RPC]
    void ResponseSynchronizeStats(float tbfPassive, float tbfSkill1, float tbfSkill2, float tbfUltimate, int lifePoint, int nbBombs, float movementSpeed, float spawnFactor)
    {
        foreach (var skScript in this.GetComponents<SkillScript>())
        {
            if (skScript.SkillType == SkillScript.E_SkillType.Passive)
            {
                skScript.LastTimeUsed = Time.time + tbfPassive - PassiveCooldown;
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Skill1)
            {
                skScript.LastTimeUsed = Time.time + tbfSkill1 - Skill1Cooldown;
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Skill2)
            {
                skScript.LastTimeUsed = Time.time + tbfSkill2 - Skill2Cooldown;
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Ultimate)
            {
                skScript.LastTimeUsed = Time.time + tbfUltimate - SkillUltimateCooldown;
            }
        }
        LifePoint = lifePoint;
        NbBombs = nbBombs;
        MovementSpeed = movementSpeed;
        RespawnFactor = spawnFactor;
    }


    void InitializeInterface()
    {
        GameObject interfacePlayer = GameObject.FindGameObjectWithTag("Interface");
        InterfaceScpt = interfacePlayer.GetComponent<InterfaceScript>();
        bool passiveInitDone = false;
        bool skill1InitDone = false;
        bool skill2InitDone = false;
        bool skillUltimateInitDone = false;
        InterfaceScpt.BombsAvailable.text = NbMaxBomb.ToString();

        foreach (var skScript in this.GetComponents<SkillScript>())
        {
            if (skScript.SkillType == SkillScript.E_SkillType.Passive)
            {
                if (!passiveInitDone)
                {
                    InterfaceScpt.PassiveSkill.InitializeSkill(IconPassive, skScript);
                }
                passiveInitDone = true;
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Skill1)
            {
                if (!skill1InitDone)
                {
                    InterfaceScpt.Skill1.InitializeSkill(IconSkill1, skScript);
                }
                skill1InitDone = true;
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Skill2)
            {
                if (!skill2InitDone)
                {
                    InterfaceScpt.Skill2.InitializeSkill(IconSkill2, skScript);
                }
                skill2InitDone = true;
            }
            else if (skScript.SkillType == SkillScript.E_SkillType.Ultimate)
            {
                if (!skillUltimateInitDone)
                {
                    InterfaceScpt.SkillUltimate.InitializeSkill(IconSkillUltimate, skScript);
                }
                skillUltimateInitDone = true;
            }
        }
    }
}
