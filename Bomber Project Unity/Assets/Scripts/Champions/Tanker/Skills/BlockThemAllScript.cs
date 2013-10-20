using UnityEngine;
using System.Collections;
using System;

public class BlockThemAllScript : SkillScript {

    [SerializeField]
    private Transform _blocDestructiblePrefab;
    public Transform BlocDestructiblePrefab
    {
        get { return _blocDestructiblePrefab; }
        set { _blocDestructiblePrefab = value; }
    }

    public override bool useSkill(Transform playerTransform)
    {
        if (IsSkillActivated())
        {
            Instantiate(BlocDestructiblePrefab, playerTransform.position, playerTransform.rotation);
        }
        return false;       
    }
}
