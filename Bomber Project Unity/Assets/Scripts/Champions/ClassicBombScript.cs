using UnityEngine;
using System.Collections;

public class ClassicBombScript : MonoBehaviour {

    [SerializeField]
    LayerMask _everythingButPlayerMask;
    public LayerMask EverythingButPlayerMask
    {
        get { return _everythingButPlayerMask; }
        set { _everythingButPlayerMask = value; }
    }

    public enum ExplosionDirections
    {
        Vertical, Horizontal, DiagonaleGauche, DiagonaleDroite
    }

    [SerializeField]
    private ExplosionDirections[] _bombExplositionDirections; // Default Horizontal / Vertical
    public ExplosionDirections[] BombExplositionDirections
    {
        get { return _bombExplositionDirections; }
        set { _bombExplositionDirections = value; }
    }

    [SerializeField]
    private Transform _defaultBombPrefab;
    public Transform DefaultBombPrefab
    {
        get { return _defaultBombPrefab; }
        set { _defaultBombPrefab = value; }
    }

    public bool UseBomb(Transform playerTransform)
    {
        var onGridPos = new Vector3(Mathf.Round(playerTransform.position.x), playerTransform.position.y, Mathf.Round(playerTransform.position.z));
        if (Network.isServer)
        {
            if (BombScript.IsTileEmpty(onGridPos, EverythingButPlayerMask))
            {
                Instantiate(DefaultBombPrefab, onGridPos, playerTransform.rotation);                
                return true;
            }
            else
                return false;
        }
        Instantiate(DefaultBombPrefab, onGridPos, playerTransform.rotation);
        return true;
    }
}
