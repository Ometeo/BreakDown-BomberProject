using UnityEngine;
using System.Collections;

public class NotSoFastScript : SkillScript {

    [SerializeField]
    private Transform _slowBombPrefab;
    public Transform SlowBombPrefab
    {
        get { return _slowBombPrefab; }
        set { _slowBombPrefab = value; }
    }

    [SerializeField]
    LayerMask _everythingButPlayerMask;
    public LayerMask EverythingButPlayerMask
    {
        get { return _everythingButPlayerMask; }
        set { _everythingButPlayerMask = value; }
    }

    protected override bool IsSkillUsable(Transform playerTransform)
    {
        Vector3 onGridPos = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z));
        
        return BombScript.IsTileEmpty(onGridPos, EverythingButPlayerMask);
    }

    protected override void InstantiaterSkill(NetworkViewID viewID, Transform playerTransform)
    {
        Vector3 onGridPos = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z));

        Transform createdItem = (Transform)Instantiate(SlowBombPrefab, onGridPos, playerTransform.rotation);
        createdItem.networkView.viewID = viewID;
    }
}
