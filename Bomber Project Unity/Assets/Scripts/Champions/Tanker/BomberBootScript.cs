using UnityEngine;
using System.Collections;

public class BomberBootScript : SkillScript {
    
    [SerializeField]
    LayerMask _bombMask;
    public LayerMask BombMask
    {
        get { return _bombMask; }
        set { _bombMask = value; }
    }

    [SerializeField]
    private float _speedFactor;
    public float SpeedFactor
    {
        get { return _speedFactor; }
        set { _speedFactor = value; }
    }
    
    Vector3 tilePosition;

    protected override void NormalSkill(Transform playerTransform)
    {
        tilePosition = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z)) + playerTransform.forward;

        Collider[] cols = Physics.OverlapSphere(tilePosition, 0.45f, BombMask);
        foreach (var col in cols)
        {
            Rigidbody rb = col.rigidbody;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            col.rigidbody.velocity = playerTransform.forward * SpeedFactor;
        }
    }

    protected override bool IsSkillUsable(Transform playerTransform)
    {
        Debug.Log("Fille");
        tilePosition = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z)) + playerTransform.forward;
        return checkTileForBomb(tilePosition);
    }

    /// <summary>
    /// Check the tile ahead for bombs
    /// </summary>
    /// <param name="tilePosition">Position of the tile ahead</param>
    /// <returns>true if there is a bomb ahead, false otherwise</returns>
    private bool checkTileForBomb(Vector3 tilePosition)
    {
        Collider[] cols = Physics.OverlapSphere(tilePosition, 0.45f, BombMask);
        return (cols.Length > 0);
    }
}
