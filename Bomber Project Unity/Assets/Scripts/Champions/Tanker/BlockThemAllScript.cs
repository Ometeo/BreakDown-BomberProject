using UnityEngine;
using System.Collections;
using System;

public class BlockThemAllScript : SkillScript {

    [SerializeField]
    private LayerMask _mask;
    public LayerMask Mask
    {
        get { return _mask; }
        set { _mask = value; }
    }

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
        Collider[] cols = Physics.OverlapSphere(futurBlocPosition, 0.45f, Mask);

        if (cols.Length > 0)
            return false;
        return true;
    }

    protected override void InstantiaterSkill(NetworkViewID viewID, Transform playerTransform)
    {
        Vector3 futurBlocPosition = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z)) + playerTransform.forward;

        Transform createdItem = (Transform)Instantiate(BlocDestructiblePrefab, futurBlocPosition, playerTransform.rotation);
        createdItem.networkView.viewID = viewID;
    }
}
