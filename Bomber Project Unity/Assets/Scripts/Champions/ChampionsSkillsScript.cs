using UnityEngine;
using System.Collections;
using System;

public class ChampionsSkillsScript : MonoBehaviour {

    [SerializeField]
    private Transform _defaultBombPrefab;
    public Transform DefaultBombPrefab
    {
        get { return _defaultBombPrefab; }
        set { _defaultBombPrefab = value; }
    }

    [SerializeField]
    private SkillScript _skill1;
    public SkillScript Skill1
    {
        get { return _skill1; }
        set { _skill1 = value; }
    }

    [SerializeField]
    private SkillScript _skill2;
    public SkillScript Skill2
    {
        get { return _skill2; }
        set { _skill2 = value; }
    }

    [SerializeField]
    private SkillScript _ultimate;
    public SkillScript Ultimate
    {
        get { return _ultimate; }
        set { _ultimate = value; }
    }

    public void UseBomb(Transform playerTransform)
    {
        string tempPlayerString = playerTransform.GetComponent<PlayerInputManagerScript>().TheOwner.ToString();
        int playerNumber = Convert.ToInt32(tempPlayerString);

        Network.Instantiate(DefaultBombPrefab, playerTransform.position, playerTransform.rotation, playerNumber);
    }
}
