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

    public override void useSkill(Transform playerTransform)
    {
        if (!IsSkillActivated())
            return;

        string tempPlayerString = playerTransform.GetComponent<PlayerInputManagerScript>().TheOwner.ToString();
        int playerNumber = Convert.ToInt32(tempPlayerString);

        Network.Instantiate(BlocDestructiblePrefab, playerTransform.position, playerTransform.rotation, playerNumber);
    }
}
