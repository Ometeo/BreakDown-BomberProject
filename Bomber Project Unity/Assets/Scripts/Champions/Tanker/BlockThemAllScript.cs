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

    protected override bool IsSkillUsable(Transform playerTransform)
    {
        Vector3 futurBlocPosition = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z)) + playerTransform.forward;
        Debug.Log("Check if usable");

        return true;
    }

    protected override void InstantiaterSkill(NetworkViewID viewID, Transform playerTransform)
    {
        Debug.Log("In BlockThemAll!");
        Vector3 futurBlocPosition = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z)) + playerTransform.forward;

        Transform createdItem = (Transform)Instantiate(BlocDestructiblePrefab, futurBlocPosition, playerTransform.rotation);
        createdItem.networkView.viewID = viewID;
    }
}
