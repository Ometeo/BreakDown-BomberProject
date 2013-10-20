using UnityEngine;
using System.Collections;

public class PlayerInputManagerScript : MonoBehaviour {

    private NetworkPlayer _theOwner;
    public NetworkPlayer TheOwner
    {
        get { return _theOwner; }
        set { _theOwner = value; }
    }

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
    private ChampionsStatsScript _champStatsScript;

    private ArrayList _skills1;
    private ArrayList _skills2;
    private ArrayList _skillsUltimate;


    // This method enable the possibility to move the champion only by the player who ordered the creation
    [RPC]
    void SetPlayer(NetworkPlayer player)
    {
        TheOwner = player;
        if (player == Network.player)
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
        if (TheOwner != null && Network.player == TheOwner)
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
    }

    private void cacheChampionData()
    {
        _champStatsScript = Champion.GetComponent<ChampionsStatsScript>();
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

    [RPC]
    void SendUseSkill1()
    {
        if (Network.isServer)
        {
            foreach (var skill in _skills1)
                ((SkillScript)skill).useSkill(transform);
        }
    }

    [RPC]
    void SendUseSkillUltimate()
    {
        if (Network.isServer)
        {
            foreach (var skill in _skillsUltimate)
                ((SkillScript)skill).useSkill(transform);
        }
    }

    [RPC]
    void SendUseBomb()
    {
        if (Network.isServer)
        {
            _champStatsScript.UseBomb(transform);
        }
    }

    /* Save if we decide to use NetworkStream on movement
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.position;
            stream.Serialize(ref pos);
        }
        else
        {
            Vector3 posReceive = Vector3.zero;
            stream.Serialize(ref posReceive);
            transform.position = posReceive;
        }
    }
    //*/
}
