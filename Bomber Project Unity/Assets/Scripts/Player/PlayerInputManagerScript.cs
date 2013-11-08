using UnityEngine;
using System.Collections;

public class PlayerInputManagerScript : MonoBehaviour {

    private Vector3 _lastClientDirection = Vector3.zero;

    private Transform _champion;
    public Transform Champion
    {
        get { return _champion; }
        set
        {
            _champion = value;
            cacheChampionData();
        }
    }
    private ClassicBombScript _classicBombScript;

    private ArrayList _skills1;
    private ArrayList _skills2;
    private ArrayList _skillsUltimate;



    public void SetPlayer()
    {
        enabled = true;
    }

    void Awake()
    {
        _skills1 = new ArrayList();
        _skills2 = new ArrayList();
        _skillsUltimate = new ArrayList();
    }

    void Update()
    {
        float xAxis = 0;
        float zAxis = 0;
        if (Input.GetKey(KeyCode.UpArrow))
            zAxis += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            zAxis -= 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            xAxis -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            xAxis += 1;
        Vector3 newDirection = new Vector3(xAxis, 0, zAxis).normalized;

        // If the direction change, send the information to the server
        if (newDirection != _lastClientDirection)
        {
            _lastClientDirection = newDirection;
            if (Network.isClient)
            {
                networkView.RPC("SendMovementDirection", RPCMode.Server, _lastClientDirection);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            networkView.RPC("SendUseSkill1", RPCMode.Server);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            networkView.RPC("SendUseSkill2", RPCMode.Server);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            networkView.RPC("SendUseSkillUltime", RPCMode.Server);
        if (Input.GetMouseButtonDown(0))
            networkView.RPC("SendUseBomb", RPCMode.Server);
    }

    private void cacheChampionData()
    {
        _classicBombScript = Champion.GetComponent<ClassicBombScript>();
        foreach (var skill in Champion.GetComponents<SkillScript>())
        {
            if (skill.SkillType == SkillScript.E_SkillType.Skill1)
                _skills1.Add(skill);
            if (skill.SkillType == SkillScript.E_SkillType.Skill2)
                _skills2.Add(skill);
            if (skill.SkillType == SkillScript.E_SkillType.Ultimate)
                _skillsUltimate.Add(skill);
        }
    }


    /// <summary>
    /// This function is call by clients to share their intent
    /// If the server allow the action, the skill is used and this information is shared with clients
    /// </summary>
    [RPC]
    void SendUseSkill1()
    {
        if (Network.isServer)
        {
            foreach (SkillScript skill in _skills1)
            {
                if (skill.useSkill(transform))
                {
                    networkView.RPC("ResponseUseSkill1", RPCMode.Others);
                }
            }   
        }
    }

    /// <summary>
    /// The authorisation for clients to start their skill
    /// </summary>
    [RPC]
    void ResponseUseSkill1()
    {
        Debug.Log("ResponseUseSkill1");
        foreach (SkillScript skill in _skills1)
            skill.useSkill(transform);
    }


    /// <summary>
    /// Same for skill 2
    /// </summary>
    [RPC]
    void SendUseSkill2()
    {
        if (Network.isServer)
        {
            foreach (SkillScript skill in _skills2)
            {
                if (skill.useSkill(transform))
                {
                    networkView.RPC("ResponseUseSkill2", RPCMode.Others);
                }
            }
        }
    }

    [RPC]
    void ResponseUseSkill2()
    {
        foreach (SkillScript skill in _skills2)
            skill.useSkill(transform);
    }

    /// <summary>
    /// Same for skill Ultimate
    /// </summary>

    [RPC]
    void SendUseSkillUltimate()
    {
        if (Network.isServer)
        {
            foreach (SkillScript skill in _skillsUltimate)
            {
                if (skill.useSkill(transform))
                {
                    networkView.RPC("ResponseUseSkillUltimate", RPCMode.Others);
                }
            }
        }
    }

    [RPC]
    void ResponseUseSkillUltimate()
    {
        foreach (SkillScript skill in _skillsUltimate)
            skill.useSkill(transform);
    }

    /// <summary>
    /// Same for the bomb
    /// </summary>
    [RPC]
    void SendUseBomb()
    {
        if (Network.isServer)
        {
            if (_classicBombScript.UseBomb(transform))
                networkView.RPC("ResponseUseBomb", RPCMode.Others);
        }
    }

    [RPC]
    void ResponseUseBomb()
    {
        _classicBombScript.UseBomb(transform);
    }
}
