using UnityEngine;
using System.Collections;

public class TripleShotScript : SkillScript {

    [SerializeField]
    private Transform _bombPrefab;
    public Transform BombPrefab
    {
        get { return _bombPrefab; }
        set { _bombPrefab = value; }
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

        Transform createdItem = (Transform)Instantiate(BombPrefab, onGridPos, playerTransform.rotation);
        if (createdItem.networkView != null)
            createdItem.networkView.viewID = viewID;
    }
}
