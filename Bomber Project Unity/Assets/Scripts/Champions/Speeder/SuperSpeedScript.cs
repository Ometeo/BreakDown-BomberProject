using UnityEngine;
using System.Collections;

public class SuperSpeedScript : SkillScript {
    
    [SerializeField]
    private int _speedBoost;
    public int SpeedBoost
    {
        get { return _speedBoost; }
        set { _speedBoost = value; }
    }
    

    void Awake()
    {
        ChampStatsScript.DefaultMovementSpeed += SpeedBoost;
    }
}
