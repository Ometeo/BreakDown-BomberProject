using UnityEngine;
using System.Collections;

public class ChampionsStatsScript : MonoBehaviour {

    [SerializeField]
    private int _lifePoint; // Default 1 LP
    public int LifePoint
    {
        get { return _lifePoint; }
        set { _lifePoint = value; }
    }

    [SerializeField]
    private int _movementSpeed; // Default 100
    public int MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    [SerializeField]
    private int _nbMaxBomb; // Default 4
    public int NbMaxBomb
    {
        get { return _nbMaxBomb; }
        set { _nbMaxBomb = value; }
    }

    [SerializeField]
    private float _bombDelay; // Default 3 sec
    public float BombDelay
    {
        get { return _bombDelay; }
        set { _bombDelay = value; }
    }

    [SerializeField]
    private float _respawnFactor; // Default RespawnFactor 1
    public float RespawnFactor
    {
        get { return _respawnFactor; }
        set { _respawnFactor = value; }
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
    private Color _skinColor;
    public Color SkinColor
    {
        get { return _skinColor; }
        set { _skinColor = value; }
    }
 
}
