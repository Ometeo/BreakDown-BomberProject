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
        Vector3 futurBlocPosition = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z)) + playerTransform.forward;
        Debug.Log(futurBlocPosition);

        if (!IsSkillActivated())
            return false;
        
        Instantiate(BlocDestructiblePrefab, futurBlocPosition, playerTransform.rotation);
        return true;
    }
}
